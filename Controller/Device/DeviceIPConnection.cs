using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


public class DeviceIPConnection : IDisposable
{
    private TcpClient _client;
    private NetworkStream _stream;
    private readonly object _lock = new object();

    public string Host { get; }
    public int Port { get; }
    public bool IsConnected => _client != null && _client.Connected;

    // Tuneable defaults
    public int ConnectTimeoutMs { get; set; } = 5000;
    public int ReadTimeoutMs { get; set; } = 3000;
    public int WriteTimeoutMs { get; set; } = 3000;
    public int MaxRetries { get; set; } = 3;
    public int RetryDelayMs { get; set; } = 1000;

    public DeviceIPConnection(string host, int port)
    {
        Host = host;
        Port = port;
    }

    // ── Connect (with retry) ──────────────────────────────────
    public void Connect()
    {
        for (int attempt = 1; attempt <= MaxRetries; attempt++)
        {
            try
            {
                Cleanup(); // dispose any stale connection first
                _client = new TcpClient();

                if (!_client.ConnectAsync(Host, Port).Wait(ConnectTimeoutMs))
                    throw new TimeoutException($"Connect timeout ({ConnectTimeoutMs} ms)");

                _stream = _client.GetStream();
                _stream.ReadTimeout = ReadTimeoutMs;
                _stream.WriteTimeout = WriteTimeoutMs;

                Log($"Connected to {Host}:{Port}");
                return; // success
            }
            catch (Exception ex)
            {
                Log($"Connect attempt {attempt}/{MaxRetries} failed: {ex.Message}");
                if (attempt < MaxRetries)
                    Thread.Sleep(RetryDelayMs);
            }
        }
        throw new SocketException((int)SocketError.TimedOut);
    }

    // ── Reconnect if dropped ──────────────────────────────────
    private void EnsureConnected()
    {
        if (!IsConnected)
        {
            Log("Connection lost — reconnecting...");
            Connect();
        }
    }

    // ── Write ─────────────────────────────────────────────────
    public void Write(byte[] buffer, int offset, int count)
    {
        lock (_lock)
        {
            EnsureConnected();
            try
            {
                _stream.Write(buffer, offset, count);
                _stream.Flush();
            }
            catch (Exception ex)
            {
                Log($"Write error: {ex.Message}");
                Cleanup();
                throw;
            }
        }
    }

    // ── Read (blocks until data or timeout) ───────────────────
    public int Read(byte[] buffer, int offset, int count)
    {
        lock (_lock)
        {
            EnsureConnected();
            try
            {
                return _stream.Read(buffer, offset, count);
            }
            catch (Exception ex)
            {
                Log($"Read error: {ex.Message}");
                Cleanup();
                throw;
            }
        }
    }

    // ── Read exact N bytes ────────────────────────────────────
    public int ReadExact(byte[] buffer, int offset, int count)
    {
        int total = 0;
        while (total < count)
            total += Read(buffer, offset + total, count - total);
        return total;
    }

    // ── Cleanup internal state ────────────────────────────────
    private void Cleanup()
    {
        try { _stream?.Close(); } catch { }
        try { _client?.Close(); } catch { }
        _stream = null;
        _client = null;
    }

    public void Disconnect() => Cleanup();
    public void Dispose() => Cleanup();

    private void Log(string msg) =>
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] [DeviceIP] {msg}");
}


using mixer_control_globalver.Model;
using System;
using System.IO;
using System.Text.Json;
using System.Threading;

public static class SettingsManager
{
    private static readonly string LogPath = @"C:\settings_log.txt";
    private static AppSettings _currentSettings = new AppSettings();
    private static string _settingsFilePath = null;

    private static string SettingsFilePath
    {
        get
        {
            if (_settingsFilePath == null)
                _settingsFilePath = GetSettingsFilePath();
            return _settingsFilePath;
        }
    }

    private static readonly SemaphoreSlim _settingsLock = new SemaphoreSlim(1, 1);

    private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
    {
        WriteIndented = true,
        Converters = { new InvariantDoubleConverter() }
    };

    private static string BackupFilePath => SettingsFilePath + ".backup";
    private static string TempFilePath => SettingsFilePath + ".tmp";

    // -------------------------------------------------------------------------
    // Atomic write: tmp → rename, so a crash never leaves a corrupt main file
    // -------------------------------------------------------------------------
    private static void AtomicWrite(string path, string json)
    {
        File.WriteAllText(TempFilePath, json);
        File.Replace(TempFilePath, path, path + ".prev", ignoreMetadataErrors: true);
    }

    private static void SaveBackup(string json)
    {
        try { File.WriteAllText(BackupFilePath, json); }
        catch { /* non-critical */ }
    }

    private static bool TryLoadBackup()
    {
        Log($"Trying backup: {BackupFilePath}");
        Log($"Backup exists: {File.Exists(BackupFilePath)}");

        try
        {
            if (!File.Exists(BackupFilePath)) return false;

            var json = File.ReadAllText(BackupFilePath);
            var settings = JsonSerializer.Deserialize<AppSettings>(json, JsonOptions);
            if (settings == null) return false;

            _currentSettings = settings;
            AtomicWrite(SettingsFilePath, json);   // restore main file too
            Log($"Backup restored — Language:{settings.Language} PlcIp:{settings.PlcIp}");
            return true;
        }
        catch (Exception ex)
        {
            Log($"BACKUP EXCEPTION: {ex.Message}");
            return false;
        }
    }

    private static string GetSettingsFilePath()
    {
        try
        {
            string baseFolder = Environment.GetFolderPath(
                Environment.SpecialFolder.CommonApplicationData);

            string appFolder = Path.Combine(
                baseFolder, "Tech-link Silicones", "Mixer Controller");

            Directory.CreateDirectory(appFolder); // no-op if already exists
            Log($"Settings folder: {appFolder}");
            return Path.Combine(appFolder, "user_settings.json");
        }
        catch (Exception ex)
        {
            Log($"GetSettingsFilePath ERROR: {ex.Message}");
            return @"C:\ProgramData\Tech-link Silicones\Mixer Controller\user_settings.json";
        }
    }

    // -------------------------------------------------------------------------
    // Centralised log helper
    // -------------------------------------------------------------------------
    private static void Log(string message)
    {
        try
        {
            File.AppendAllText(LogPath,
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] PID:{System.Diagnostics.Process.GetCurrentProcess().Id} {message}\n");
        }
        catch { /* log failure must never crash the app */ }
    }

    // -------------------------------------------------------------------------
    // Initialize  — call once on startup
    // -------------------------------------------------------------------------
    public static void Initialize()
    {
        Log($"App started — settings path: {SettingsFilePath}");
        Log($"File exists: {File.Exists(SettingsFilePath)}");

        _settingsLock.Wait();
        try
        {
            if (File.Exists(SettingsFilePath))
            {
                try
                {
                    var json = File.ReadAllText(SettingsFilePath);
                    var loaded = JsonSerializer.Deserialize<AppSettings>(json, JsonOptions);

                    // Treat null deserialization as a corrupt file — try backup first
                    if (loaded == null)
                    {
                        Log("Deserialization returned null — file may be corrupt, trying backup");
                        if (!TryLoadBackup())
                        {
                            Log("Backup also null/missing — keeping defaults (NOT overwriting file yet)");
                            // Do NOT write defaults over a file that might be temporarily bad
                        }
                        return;
                    }

                    _currentSettings = loaded;
                    Log($"Loaded — Language:{loaded.Language} PlcIp:{loaded.PlcIp}");

                    // Keep backup in sync with a known-good load
                    SaveBackup(json);
                }
                catch (Exception ex)
                {
                    Log($"LOAD EXCEPTION: {ex.Message}");
                    if (TryLoadBackup())
                        Log("Backup loaded successfully");
                    else
                        Log("Backup also failed — using in-memory defaults (disk untouched)");
                }
            }
            else
            {
                Log("File not found — trying backup");
                if (!TryLoadBackup())
                {
                    Log("No backup — writing initial defaults to disk");
                    var json = JsonSerializer.Serialize(_currentSettings, JsonOptions);
                    AtomicWrite(SettingsFilePath, json);
                    SaveBackup(json);
                    Log("Defaults written");
                }
            }
        }
        catch (Exception ex)
        {
            Log($"CRITICAL EXCEPTION in Initialize: {ex.Message}\n{ex.StackTrace}");
        }
        finally
        {
            _settingsLock.Release();
        }
    }

    // -------------------------------------------------------------------------
    // SaveSettings  — explicit flush (e.g. on app exit)
    // -------------------------------------------------------------------------
    public static void SaveSettings()
    {
        _settingsLock.Wait();
        try
        {
            var json = JsonSerializer.Serialize(_currentSettings, JsonOptions);
            AtomicWrite(SettingsFilePath, json);
            SaveBackup(json);
            Log($"Settings SAVED — Language:{_currentSettings.Language} PlcIp:{_currentSettings.PlcIp}");
        }
        catch (Exception ex)
        {
            Log($"SAVE ERROR: {ex.Message}");
        }
        finally
        {
            _settingsLock.Release();
        }
    }

    // -------------------------------------------------------------------------
    // UpdateSettings  — mutate + persist atomically
    // -------------------------------------------------------------------------
    public static void UpdateSettings(Action<AppSettings> updateAction)
    {
        _settingsLock.Wait();
        try
        {
            updateAction(_currentSettings);
            var json = JsonSerializer.Serialize(_currentSettings, JsonOptions);
            AtomicWrite(SettingsFilePath, json);
            SaveBackup(json);
            Log($"Settings UPDATED — Language:{_currentSettings.Language} PlcIp:{_currentSettings.PlcIp}");
        }
        catch (Exception ex)
        {
            Log($"UPDATE SAVE ERROR: {ex.Message}");
        }
        finally
        {
            _settingsLock.Release();
        }
    }

    // -------------------------------------------------------------------------
    // GetSetting  — thread-safe read
    // -------------------------------------------------------------------------
    public static T GetSetting<T>(Func<AppSettings, T> selector)
    {
        _settingsLock.Wait();
        try
        {
            return selector(_currentSettings);
        }
        finally
        {
            _settingsLock.Release();
        }
    }
}
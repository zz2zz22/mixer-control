using mixer_control_globalver.Model;
using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks; // Task is still useful for general async operations

public static class SettingsManager
{
    private static string LogPath = @"C:\settings_log.txt";
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

    private static readonly JsonSerializerOptions JsonOptions =
    new JsonSerializerOptions
    {
        WriteIndented = true,
        Converters =
        {
            new InvariantDoubleConverter()
        }
    };

    private static string BackupFilePath => SettingsFilePath + ".backup";

    // Call this every time SaveSettings succeeds
    private static void SaveBackup(string json)
    {
        try
        {
            File.WriteAllText(BackupFilePath, json);
        }
        catch { /* backup failure is non-critical */ }
    }

    private static bool TryLoadBackup()
    {
        try
        {
            File.AppendAllText(LogPath,
                $"[{DateTime.Now}] Trying backup: {BackupFilePath}\n");
            File.AppendAllText(LogPath,
                $"[{DateTime.Now}] Backup exists: {File.Exists(BackupFilePath)}\n");

            if (File.Exists(BackupFilePath))
            {
                var json = File.ReadAllText(BackupFilePath);
                var settings = JsonSerializer.Deserialize<AppSettings>(json);
                if (settings != null)
                {
                    _currentSettings = settings;
                    File.WriteAllText(SettingsFilePath, json);
                    File.AppendAllText(LogPath,
                        $"[{DateTime.Now}] Backup restored successfully\n");
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            File.AppendAllText(LogPath,
                $"[{DateTime.Now}] BACKUP EXCEPTION: {ex.Message}\n");
        }
        return false;
    }



    // Helper method to determine the persistent file path
    private static string GetSettingsFilePath()
    {
        // Try saving next to the executable instead
        string exeFolder = AppDomain.CurrentDomain.BaseDirectory;
        string settingsFolder = Path.Combine(exeFolder, "Settings");

        if (!Directory.Exists(settingsFolder))
            Directory.CreateDirectory(settingsFolder);

        return Path.Combine(settingsFolder, "user_settings.json");
    }

    /// <summary>
    /// Loads settings from the JSON file into memory on application startup (synchronous).
    /// </summary>
    public static void Initialize()
    {
        try
        {
            File.AppendAllText(LogPath,
                $"\n[{DateTime.Now}] App started - PID: {System.Diagnostics.Process.GetCurrentProcess().Id}\n");
            File.AppendAllText(LogPath,
                $"[{DateTime.Now}] Looking for: {SettingsFilePath}\n");
            File.AppendAllText(LogPath,
                $"[{DateTime.Now}] File exists: {File.Exists(SettingsFilePath)}\n");
        }
        catch (Exception ex)
        {
            // Log to a FIXED path in case SettingsFilePath itself failed
            File.AppendAllText(@"C:\settings_log.txt",
                $"[{DateTime.Now}] INIT EXCEPTION: {ex.Message}\n");
            File.AppendAllText(@"C:\settings_log.txt",
                $"[{DateTime.Now}] Stack: {ex.StackTrace}\n");
            return; // Don't proceed with broken state
        }

        _settingsLock.Wait();
        try
        {
            if (File.Exists(SettingsFilePath))
            {
                try
                {
                    var json = File.ReadAllText(SettingsFilePath);
                    var settingsFromFile = JsonSerializer.Deserialize<AppSettings>(json);
                    _currentSettings = settingsFromFile ?? new AppSettings();

                    // Log what was actually loaded
                    File.AppendAllText(LogPath,
                        $"[{DateTime.Now}] Loaded - Language:{_currentSettings.Language}" +
                        $" PlcIp:{_currentSettings.PlcIp}\n");
                }
                catch (Exception ex)
                {
                    File.AppendAllText(LogPath,
                        $"[{DateTime.Now}] LOAD EXCEPTION: {ex.Message}\n");

                    // Try backup
                    if (TryLoadBackup())
                        File.AppendAllText(LogPath,
                            $"[{DateTime.Now}] Backup loaded successfully\n");
                    else
                        File.AppendAllText(LogPath,
                            $"[{DateTime.Now}] Backup also failed - using defaults!\n");
                }
            }
            else
            {
                File.AppendAllText(LogPath,
                    $"[{DateTime.Now}] File not found - trying backup\n");

                if (!TryLoadBackup())
                {
                    File.AppendAllText(LogPath,
                        $"[{DateTime.Now}] No backup - creating defaults\n");
                    var json = JsonSerializer.Serialize(_currentSettings, JsonOptions);
                    File.WriteAllText(SettingsFilePath, json);
                }
            }
        }
        catch (Exception ex)
        {
            File.AppendAllText(LogPath,
                $"[{DateTime.Now}] CRITICAL EXCEPTION: {ex.Message}\n");
        }
        finally
        {
            _settingsLock.Release();
        }
    }
    /// <summary>
    /// Saves the current in-memory settings snapshot to the JSON file (synchronous).
    /// </summary>
    // This method is now synchronous (returns void/Task, but internally sync I/O)
    // Update SaveSettings to also save backup
    public static void SaveSettings()
    {
        _settingsLock.Wait();
        try
        {
            var json = JsonSerializer.Serialize(_currentSettings, JsonOptions);
            File.WriteAllText(SettingsFilePath, json);
            SaveBackup(json);

            File.AppendAllText(LogPath,
                $"[{DateTime.Now}] Settings SAVED - PID: {System.Diagnostics.Process.GetCurrentProcess().Id}\n");
        }
        catch (Exception ex)
        {
            File.AppendAllText(LogPath,
                $"[{DateTime.Now}] SAVE ERROR: {ex.Message}\n");
        }
        finally
        {
            _settingsLock.Release();
        }
    }

    // ... GetSetting<T>(...) and UpdateSettings(...) remain the same as the previous response ...

    /// <summary>
    /// Safely reads a specific setting value using a selector function.
    /// </summary>
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

    /// <summary>
    /// Safely updates one or more settings using an action delegate.
    /// </summary>
    public static void UpdateSettings(Action<AppSettings> updateAction)
    {
        _settingsLock.Wait();
        try
        {
            updateAction(_currentSettings);
            var json = JsonSerializer.Serialize(_currentSettings, JsonOptions);
            File.WriteAllText(SettingsFilePath, json);
            SaveBackup(json); 
        }
        catch (Exception ex)  
        {
            File.AppendAllText(LogPath,
                $"[{DateTime.Now}] UPDATE SAVE ERROR: {ex.Message}\n");
        }
        finally
        {
            _settingsLock.Release();
        }
    }

    /// <usage>
    //SettingsManager.Initialize(); // Load existing settings on startup

    //    // --- Multi-threaded operations simulation (Tasks still work fine) ---
    //    var task1 = Task.Run(() =>
    //    {
    //        SettingsManager.UpdateSettings(s =>
    //        {
    //            s.UserName = "ThreadUser1";
    //            s.ThemeId = 101;
    //        });
    //        Console.WriteLine("Thread 1 updated settings in memory.");
    //    });

    //var task2 = Task.Run(() =>
    //{
    //    int currentTheme = SettingsManager.GetSetting(s => s.ThemeId);
    //    Console.WriteLine($"Thread 2 read Theme ID: {currentTheme}");
    //});

    //// Wait for all in-memory updates to complete synchronously
    //Task.WhenAll(task1, task2).Wait(); // Use .Wait() instead of await Task.WhenAll() in sync Main

    //// Save the final consolidated state to the disk *once* synchronously
    //SettingsManager.SaveSettings(); 

    //    string finalName = SettingsManager.GetSetting(s => s.UserName);
    //Console.WriteLine($"Final saved User Name: {finalName}");
    /// </usage>
}

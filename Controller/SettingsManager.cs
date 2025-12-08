using mixer_control_globalver.Model;
using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks; // Task is still useful for general async operations

public static class SettingsManager
{
    private static AppSettings _currentSettings = new AppSettings();
    // Use a property to determine the path dynamically
    private static string SettingsFilePath { get; } = GetSettingsFilePath();

    // A semaphore ensures only one thread performs an operation at a time.
    private static readonly SemaphoreSlim _settingsLock = new SemaphoreSlim(1, 1);


    // Helper method to determine the persistent file path
    private static string GetSettingsFilePath()
    {
        // Get the path to the user's local AppData folder (works on Windows, Linux, macOS)
        string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        // Define a specific folder for your application to avoid conflicts
        string companyName = "Tech-link Silicones"; // Change this
        string appName = "Mixer Controller";       // Change this
        string appSpecificFolder = Path.Combine(appDataFolder, companyName, appName);

        // Ensure the directory exists
        if (!Directory.Exists(appSpecificFolder))
        {
            Directory.CreateDirectory(appSpecificFolder);
        }

        // Return the full path to the JSON settings file
        return Path.Combine(appSpecificFolder, "user_settings.json");
    }

    /// <summary>
    /// Loads settings from the JSON file into memory on application startup (synchronous).
    /// </summary>
    public static void Initialize()
    {
        // Acquire lock immediately as we are about to touch the file system and memory state
        _settingsLock.Wait();
        try
        {
            if (File.Exists(SettingsFilePath))
            {
                // --- File exists: Load existing settings ---
                try
                {
                    var json = File.ReadAllText(SettingsFilePath);
                    var settingsFromFile = JsonSerializer.Deserialize<AppSettings>(json);
                    _currentSettings = settingsFromFile ?? new AppSettings();
                    Console.WriteLine("Settings loaded successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading settings: {ex.Message}. Using default settings.");
                    // Optionally delete the corrupt file here: File.Delete(SettingsFilePath);
                }
            }
            else
            {
                // --- File does not exist: Create it with default settings ---
                Console.WriteLine("Settings file not found. Creating default settings file.");
                // We use the default values already present in _currentSettings 
                // and save them to disk immediately.
                var json = JsonSerializer.Serialize(_currentSettings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(SettingsFilePath, json);
            }
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
    public static void SaveSettings()
    {
        _settingsLock.Wait(); // Use synchronous wait for synchronous operation
        try
        {
            // Serialize the *current* atomic snapshot of the settings object
            var json = JsonSerializer.Serialize(_currentSettings, new JsonSerializerOptions { WriteIndented = true });

            // Synchronous write
            File.WriteAllText(SettingsFilePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving settings: {ex.Message}");
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

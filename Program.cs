using System;
using System.Threading;
using System.Windows.Forms;

namespace mixer_control_globalver
{
    internal static class Program
    {
        public static MainWindow main;
        static Mutex mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //try
            //{
            //    Properties.Settings.Default.Reload();
            //    string settingValue1 = Properties.Settings.Default.plc_ip;
            //    var path = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
            //    if (String.IsNullOrEmpty(settingValue1))
            //    {
            //        if (File.Exists(path))
            //            File.Delete(path);
            //        SubMethods.RestoreUserSettings(path);
            //    }
            //}
            //catch (ConfigurationException ex)
            //{ //(requires System.Configuration)
            //    string filename = ((ConfigurationException)ex.InnerException).Filename;
            //    File.Delete(filename);
            //    SubMethods.RestoreUserSettings(filename);
            //}

            SettingsManager.Initialize(); // Load existing settings on startup
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                try
                {
                    switch (SettingsManager.GetSetting(s => s.Language))
                    {
                        case 0:
                            SubMethods.SetLanguage("vi-VN");
                            break;
                        case 1:
                            SubMethods.SetLanguage("zh-CN");
                            break;
                        case 2:
                            SubMethods.SetLanguage("en-US");
                            break;
                        default:
                            SubMethods.SetLanguage("");
                            break;
                    }

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    main = new MainWindow();
                    Application.Run(main);
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
            else
            {
                Application.Exit();
            }
        }
    }
}

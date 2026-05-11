using System;
using System.Threading;
using System.Windows.Forms;

namespace mixer_control_globalver
{
    internal static class Program
    {
        public static MainWindow main;
        static Mutex mutex = new Mutex(false, "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");
        /// <summary>
                                                                                                /// The main entry point for the application.
                                                                                                /// </summary>
        [STAThread]
        static void Main()
        {
            // ✅ Check mutex FIRST before anything else
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                try
                {
                    // ✅ Only ONE instance ever reaches here
                    SettingsManager.Initialize();

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
                // ✅ Show already running message instead of silent exit
                MessageBox.Show(
                    "Mixer Controller is already running!",
                    "Already Running",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Application.Exit();
            }
        }
    }
}

using mixer_control_globalver.Controller;
using mixer_control_globalver.View.CustomComponent;
using mixer_control_globalver.View.CustomControls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace mixer_control_globalver
{
    internal static class Program
    {
        static string message = String.Empty, caption = String.Empty;
        public static MainWindow main;
        static Mutex mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Properties.Settings.Default.Reload();
                string settingValue1 = Properties.Settings.Default.plc_ip;
                var path = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
                if(String.IsNullOrEmpty(settingValue1))
                {
                    File.Delete(path);
                    SubMethods.RestoreUserSettings(path);
                }
            }
            catch (ConfigurationException ex)
            { //(requires System.Configuration)
                string filename = ((ConfigurationException)ex.InnerException).Filename;
                File.Delete(filename);
                SubMethods.RestoreUserSettings(filename);
            }
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                try
                {
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

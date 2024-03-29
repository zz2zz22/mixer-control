﻿using mixer_control_globalver.Controller;
using mixer_control_globalver.View.CustomComponent;
using mixer_control_globalver.View.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
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
                if (Properties.Settings.Default.language == 0)
                {
                    message = "Phần mềm đang chạy.";
                    caption = "Thông tin";
                }
                else if (Properties.Settings.Default.language == 1)
                {
                    message = "软件正在运行";
                    caption = "信息";
                }
                else if (Properties.Settings.Default.language == 2)
                {
                    message = "Program is running.";
                    caption = "Information";
                }

                CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

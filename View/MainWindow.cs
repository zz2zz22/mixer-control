using mixer_control_globalver.View.CustomComponent;
using mixer_control_globalver.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mixer_control_globalver.Controller.IniFile;

namespace mixer_control_globalver
{
    public partial class MainWindow : Form
    {
        IniFile ini = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "\\setting.ini");
        public MainWindow()
        {
            InitializeComponent();
            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelHeader_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pbxCompanyLogo_Click(object sender, EventArgs e)
        {
            Process.Start(Settings.Default.website);
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not enough time to develop!" + Environment.NewLine + "开发时间不够 !", "Information / 消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            
        }
    }
}

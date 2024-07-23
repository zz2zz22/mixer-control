using mixer_control_globalver.Controller;
using mixer_control_globalver.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mixer_control_globalver.View.SideUI
{
    public partial class CheckFormulaProcess : Form
    {
        public CheckFormulaProcess()
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
            this.Close();
        }

        private void CheckFormulaProcess_Load(object sender, EventArgs e)
        {
            dtgvSpecProcessList.DataSource = TemporaryVariables.processDT;
            dtgvSpecProcessList.Columns["init_speed"].Visible = false;
            dtgvSpecProcessList.Columns["init_time"].Visible = false;
            dtgvSpecProcessList.Columns["change_speed"].Visible = false;
            dtgvSpecProcessList.Columns["change_time"].Visible = false;
            dtgvSpecProcessList.Columns["is_vaccum"].Visible = false;
            dtgvSpecProcessList.Columns["max_temperature"].Visible = false;
            dtgvSpecProcessList.Columns["is_skip_announce"].Visible = false;
            dtgvSpecProcessList.Columns["is_finished"].Visible = false;
            dtgvSpecProcessList.Columns["is_oilfeed"].Visible = false;
            dtgvSpecProcessList.Columns["oil_mass"].Visible = false;
            dtgvSpecProcessList.Columns["oil_type"].Visible = false;
            dtgvSpecProcessList.Columns["total_powder_bags"].Visible = false;
            dtgvSpecProcessList.Columns["remain_powder_bags"].Visible = false;
            if (Settings.Default.language == 0)
            {
                dtgvSpecProcessList.Columns["process_no"].HeaderText = "Số bước\r\nStep No.";
                dtgvSpecProcessList.Columns["description"].HeaderText = "Mô tả\r\nDescription";
            }
            else if (Settings.Default.language == 1)
            {
                dtgvSpecProcessList.Columns["process_no"].HeaderText = "Số bước\r\n序号";
                dtgvSpecProcessList.Columns["description"].HeaderText = "Mô tả\r\n描述";
            }
            else if (Settings.Default.language == 2)
            {
                dtgvSpecProcessList.Columns["process_no"].HeaderText = "Step No.";
                dtgvSpecProcessList.Columns["description"].HeaderText = "Description";
            }
            else if (Settings.Default.language == 3)
            {
                dtgvSpecProcessList.Columns["process_no"].HeaderText = "Số bước";
                dtgvSpecProcessList.Columns["description"].HeaderText = "Mô tả";
            }
            else if (Settings.Default.language == 4)
            {
                dtgvSpecProcessList.Columns["process_no"].HeaderText = "序号";
                dtgvSpecProcessList.Columns["description"].HeaderText = "描述";
            }
            dtgvSpecProcessList.Columns["process_no"].Width = 120;
        }
    }
}

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
using XanderUI;
using mixer_control_globalver.View.MainUI;
using mixer_control_globalver.Controller;

namespace mixer_control_globalver
{
    public partial class MainWindow : Form
    {
        ChooseSpec specWindow = new ChooseSpec();
        IniFile ini = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "\\setting.ini");
        public MainWindow()
        {
            InitializeComponent();

            TemporaryVariables.resetAllTempVariables();

            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private Form activeForm = null;
        public void openChildForm(Form childForm)
        {
            if (activeForm != null) activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelMainForm.Controls.Add(childForm);
            panelMainForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void panelHeader_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Thoát chương trình?" + Environment.NewLine + "Exit the application?", "Cảnh báo / Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.OK)
            {
                Environment.Exit(0);
            }
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

        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

            btnChooseSpecTab.ButtonText = "Chọn công thức" + Environment.NewLine + "Choose formula";
            btnWeightTab.ButtonText = "Cân nguyên vật liệu" + Environment.NewLine + "Material Scale";
            btnAutomationTab.ButtonText = "Tự động hóa" + Environment.NewLine + "Automation";

            openSpecTab();
            specWindow.Owner = this;
        }

        public void openSpecTab()
        {
            this.Invoke(new EventHandler(btnChooseSpecTab_Click));
        }
        public void openScaleTab()
        {
            this.Invoke(new EventHandler(btnWeightTab_Click));
        }
        public void openAutomationTab()
        {
            this.Invoke(new EventHandler(btnAutomationTab_Click));
        }

        public void btnChooseSpecTab_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(TemporaryVariables.tempFileName) && TemporaryVariables.materialDT != null && TemporaryVariables.processDT != null)
            {
                bool isScaled = false;
                for (int i = 0; i < TemporaryVariables.materialDT.Rows.Count; i++)
                {
                    if (!(bool)TemporaryVariables.materialDT.Rows[i]["is_packed"])
                    {
                        if ((bool)TemporaryVariables.materialDT.Rows[i]["is_scaled"])
                            isScaled = true;
                    }
                }
                if (isScaled)
                {
                    DialogResult dialogResult = MessageBox.Show("Các dữ liệu đã làm sẽ bị mất! Bạn có muốn tiếp tục chọn công thức khác?" + Environment.NewLine + "Current data will be lost! Do you want to continue to choose other formula?", "Cảnh báo / Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.OK)
                    {
                        openChildForm(new ChooseSpec());
                        btnChooseSpecTab.BackgroundColor = Color.FromArgb(255, 255, 192);
                        btnWeightTab.BackgroundColor = Color.FromArgb(255, 255, 128);
                        btnAutomationTab.BackgroundColor = Color.FromArgb(255, 255, 128);
                    }
                }
                else
                {
                    openChildForm(new ChooseSpec());
                    btnChooseSpecTab.BackgroundColor = Color.FromArgb(255, 255, 192);
                    btnWeightTab.BackgroundColor = Color.FromArgb(255, 255, 128);
                    btnAutomationTab.BackgroundColor = Color.FromArgb(255, 255, 128);
                }
            }
            else
            {
                openChildForm(new ChooseSpec());
                btnChooseSpecTab.BackgroundColor = Color.FromArgb(255, 255, 192);
                btnWeightTab.BackgroundColor = Color.FromArgb(255, 255, 128);
                btnAutomationTab.BackgroundColor = Color.FromArgb(255, 255, 128);
            }  
        }

        public void btnWeightTab_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TemporaryVariables.tempFileName) && TemporaryVariables.materialDT != null && TemporaryVariables.processDT != null)
            {
                openChildForm(new MaterialScale(TemporaryVariables.tempFileName));
                btnChooseSpecTab.BackgroundColor = Color.FromArgb(255, 255, 128);
                btnWeightTab.BackgroundColor = Color.FromArgb(255, 255, 192);
                btnAutomationTab.BackgroundColor = Color.FromArgb(255, 255, 128);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một công thức trước!" + Environment.NewLine + "Please choose a formula first!", "Cảnh báo / Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void btnAutomationTab_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TemporaryVariables.tempFileName) && TemporaryVariables.materialDT != null && TemporaryVariables.processDT != null)
            {
                bool isAllScaled = true;
                for (int i = 0; i < TemporaryVariables.materialDT.Rows.Count; i ++)
                {
                    if (!(bool)TemporaryVariables.materialDT.Rows[i]["is_scaled"])
                    {
                        if (!(bool)TemporaryVariables.materialDT.Rows[i]["is_packed"])
                            isAllScaled = false;
                    }
                }
                if (isAllScaled)
                {
                    openChildForm(new AutomationInfo());
                    btnChooseSpecTab.BackgroundColor = Color.FromArgb(255, 255, 128);
                    btnWeightTab.BackgroundColor = Color.FromArgb(255, 255, 128);
                    btnAutomationTab.BackgroundColor = Color.FromArgb(255, 255, 192);
                }
                else
                {
                    MessageBox.Show("Vui lòng cân khối lượng các nguyên liệu trước!" + Environment.NewLine + "Please scale all materials weight first!", "Cảnh báo / Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một công thức trước!" + Environment.NewLine + "Please choose a formula first!", "Cảnh báo / Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

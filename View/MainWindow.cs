using mixer_control_globalver.Controller;
using mixer_control_globalver.Controller.LogFile;
using mixer_control_globalver.Controller.PLC;
using mixer_control_globalver.Properties;
using mixer_control_globalver.View.CustomComponent;
using mixer_control_globalver.View.CustomControls;
using mixer_control_globalver.View.MainUI;
using mixer_control_globalver.View.SideUI;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace mixer_control_globalver
{
    public partial class MainWindow : Form
    {
        ///
        /// FIELDS
        ///
        private object lockObject = new object();
        private Form activeForm = null;

        private BackgroundWorker statusCheckBackgroundWorker;
        private System.Windows.Forms.Timer tmrCallBWStatusCheck;
        private System.Threading.Timer tmrEnsureBWStatusCheckGetsCalled;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        ///
        /// CONSTRUCTOR
        ///
        public MainWindow()
        {
            switch (SettingsManager.GetSetting(s => s.Language))
            {
                case 0:
                    SubMethods.SetLanguage("vi-VN");
                    SystemLog.Output(SystemLog.MSG_TYPE.Nor, "Language set to Vietnamese", "Application language set to Vietnamese.");
                    break;
                case 1:
                    SubMethods.SetLanguage("zh-CN");
                    SystemLog.Output(SystemLog.MSG_TYPE.Nor, "Language set to Chinese", "Application language set to Chinese.");
                    break;
                case 2:
                    SubMethods.SetLanguage("en-US");
                    SystemLog.Output(SystemLog.MSG_TYPE.Nor, "Language set to English", "Application language set to English.");
                    break;
                default:
                    SubMethods.SetLanguage("");
                    break;
            }
            InitializeComponent();
            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\data"))
                {
                    //Check và tạo directory "data" trong thư mục cài đặt của phần mềm
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\data");
                }
                lbVersion.Text = "Version: " + GetExecutableInstallerVersion(Application.ExecutablePath);
                TemporaryVariables.resetAllTempVariables();

                this.Text = string.Empty;
                this.ControlBox = false;
                this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            }
            catch (Exception ex)
            {
                CTMessageBox.Show("Application initial process error : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Application initial process error", ex.Message);
                Environment.Exit(0);
            }
        }
        public static string GetExecutableInstallerVersion(string filePath)
        {
            if (File.Exists(filePath))
            {
                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(filePath);
                return versionInfo.ProductVersion;
            }
            return null; // Or throw an exception if the file doesn't exist
        }

        ///
        /// METHODS
        ///
        public void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelMainForm.Controls.Add(childForm);
            panelMainForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
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

        ///
        /// EVENTS HANDLER
        ///
        private void panelHeader_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = CTMessageBox.Show(GlobalStrings.Message_ExitApplication, GlobalStrings.MessageBoxTitle_Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                PLCMethods.ResetPLCVariables();
                SettingsManager.SaveSettings();
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

        private void MainWindow_Load(object sender, EventArgs e)
        {
            try
            {
                // this timer calls bgWorker again and again after regular intervals
                tmrCallBWStatusCheck = new System.Windows.Forms.Timer();//Timer for do task
                tmrCallBWStatusCheck.Tick += new EventHandler(timer_nextRun_Tick);
                tmrCallBWStatusCheck.Interval = 1000; //3600000;

                // this is our worker
                statusCheckBackgroundWorker = new BackgroundWorker();

                // work happens in this method
                statusCheckBackgroundWorker.DoWork += new DoWorkEventHandler(BW_DoWork);
                statusCheckBackgroundWorker.ProgressChanged += BW_ProgressChanged;
                statusCheckBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BW_RunWorkerCompleted);
                statusCheckBackgroundWorker.WorkerReportsProgress = true;

                tmrCallBWStatusCheck.Start();
            }
            catch (Exception ex)
            {
                CTMessageBox.Show("Main background worker initial error : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Main background worker initial error", ex.Message);
            }

            TemporaryVariables.InitSettingDT();
            cbxLanguageChoose.SelectedIndex = SettingsManager.GetSetting(s => s.Language);

            btnChooseSpecTab.ButtonText = GlobalStrings.btnChooseSpecTab_Text;
            btnChooseSpecTab.Font = new Font(GlobalStrings.Text_Font, 12, FontStyle.Bold);
            btnWeightTab.ButtonText = GlobalStrings.btnWeightTab_Text;
            btnWeightTab.Font = new Font(GlobalStrings.Text_Font, 12, FontStyle.Bold);
            btnAutomationTab.ButtonText = GlobalStrings.btnAutomationTab_Text;
            btnAutomationTab.Font = new Font(GlobalStrings.Text_Font, 12, FontStyle.Bold);
            openSpecTab();
        }


        private void timer_nextRun_Tick(object sender, EventArgs e)
        {
            if (Monitor.TryEnter(lockObject))
            {
                try
                {
                    // if bgworker is not busy the call the worker
                    if (!statusCheckBackgroundWorker.IsBusy)
                    {
                        statusCheckBackgroundWorker.RunWorkerAsync();
                    }
                }
                finally
                {
                    Monitor.Exit(lockObject);
                }
            }
            else
            {
                // as the bgworker is busy we will start a timer that will try to call the bgworker again after some time
                tmrEnsureBWStatusCheckGetsCalled = new System.Threading.Timer(new TimerCallback(tmrEnsureWorkerGetsCalled_Callback), null, 0, 10);
            }
        }

        private void BW_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //Logic check time ở đây
                bool isRequired2Reset = false;
                if (!string.IsNullOrEmpty(SettingsManager.GetSetting(s => s.OIlTestedTime)))
                {
                    string currentDate = DateTime.Now.ToString("dd/MM/yyyy");
                    DateTime curDateTime = DateTime.ParseExact(currentDate + " 08:00:00", "dd/MM/yyyy HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
                    DateTime checkDateTime = DateTime.ParseExact(SettingsManager.GetSetting(s => s.OIlTestedTime), "dd/MM/yyyy HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
                    if (checkDateTime < curDateTime)
                        isRequired2Reset = true;

                    if (isRequired2Reset)
                    {
                        SettingsManager.UpdateSettings(s => s.OIlTested = false);
                    }
                    else
                    {
                        SettingsManager.UpdateSettings(s => s.OIlTested = true);
                    }
                    statusCheckBackgroundWorker.ReportProgress(0);
                }
                else
                {
                    SettingsManager.UpdateSettings(s => s.OIlTested = false);
                    statusCheckBackgroundWorker.ReportProgress(0);
                }
            }
            catch (Exception ex)
            {
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Main background worker logic error", ex.Message);
            }
        }

        private void BW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string announceText = String.Empty;

            if (SettingsManager.GetSetting(s => s.OilSupplyEnabled))
            {
                if (SettingsManager.GetSetting(s => s.OIlTested))
                {
                    announceText = GlobalStrings.Message_OilTested + SettingsManager.GetSetting(s => s.OIlTestedTime);
                    lbOilTestStatus.BackColor = Color.Yellow;
                    lbOilTestStatus.ForeColor = Color.Black;
                }
                else
                {
                    announceText = String.Empty;
                    lbOilTestStatus.BackColor = Color.FromArgb(255, 255, 128);
                    lbOilTestStatus.ForeColor = Color.Black;
                }
            }
            else
            {
                announceText = String.Empty;
                lbOilTestStatus.BackColor = Color.FromArgb(255, 255, 128);
                lbOilTestStatus.ForeColor = Color.Black;
            }
            lbOilTestStatus.Text = announceText;
        }

        private void BW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) { }

        void tmrEnsureWorkerGetsCalled_Callback(object obj)
        {
            // this timer was started as the bgworker was busy before now it will try to call the bgworker again
            if (Monitor.TryEnter(lockObject))
            {
                try
                {
                    if (!statusCheckBackgroundWorker.IsBusy)
                    {
                        statusCheckBackgroundWorker.RunWorkerAsync();
                    }
                }
                finally
                {
                    Monitor.Exit(lockObject);
                }
                tmrEnsureBWStatusCheckGetsCalled = null;
            }
        }

        public void btnChooseSpecTab_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TemporaryVariables.tempFileName) && TemporaryVariables.materialDT != null && TemporaryVariables.processDT != null)
            {
                bool isFinished = true;
                for (int i = 0; i < TemporaryVariables.processDT.Rows.Count; i++)
                {
                    if (!(bool)TemporaryVariables.processDT.Rows[i]["is_finished"])
                    {
                        isFinished = false;
                    }
                }

                if (!isFinished)
                {
                    if (TemporaryVariables.materialDT.Rows.Count > 0)
                    {
                        DialogResult dialogResult = CTMessageBox.Show(GlobalStrings.Message_ResetSpecTab, GlobalStrings.MessageBoxTitle_Warning, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.OK)
                            OpenChildForm(new ChooseSpec());
                    }
                    else
                        OpenChildForm(new ChooseSpec());
                }
                else
                    OpenChildForm(new ChooseSpec());

            }
            else
                OpenChildForm(new ChooseSpec());
        }

        public void btnWeightTab_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TemporaryVariables.tempFileName) && TemporaryVariables.processDT != null)
                OpenChildForm(new MaterialScale());
            else
            {
                CTMessageBox.Show(GlobalStrings.Message_NotChooseFormulaAlert, GlobalStrings.MessageBoxTitle_Warning, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void btnAutomationTab_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TemporaryVariables.tempFileName) && TemporaryVariables.materialDT != null && TemporaryVariables.processDT != null)
            {
                if (!SettingsManager.GetSetting(s => s.DeveloperMode))
                {
                    if (TemporaryVariables.materialDT.Rows.Count > 0)
                    {
                        bool notSettingEnough = false;
                        for (int i = 0; i < TemporaryVariables.settingDT.Rows.Count; i++)
                        {
                            if (String.IsNullOrEmpty(PLCMethods.ini.Read(TemporaryVariables.settingDT.Rows[i]["value_member"].ToString(), "start")))
                            {
                                notSettingEnough = true;
                            }
                        }
                        if (String.IsNullOrEmpty(SettingsManager.GetSetting(s => s.PlcIp))
                    || SettingsManager.GetSetting(s => s.DatabaseNumber) == 0
                    || SettingsManager.GetSetting(s => s.MaxSpeed) == 0
                    || SettingsManager.GetSetting(s => s.SpindleDiameter) == 0
                    || SettingsManager.GetSetting(s => s.SensorDiameter) == 0
                    || SettingsManager.GetSetting(s => s.TransmissionRatio) == 0
                    || notSettingEnough)
                        {
                            CTMessageBox.Show(GlobalStrings.Message_NotInputAllBaseSetting, GlobalStrings.MessageBoxTitle_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            MainSetting mainSetting = new MainSetting();
                            mainSetting.ShowDialog();
                        }
                        else
                            OpenChildForm(new AutomationInfo());
                    }
                    else
                    {
                        CTMessageBox.Show(GlobalStrings.Message_NotScanAllMaterial, GlobalStrings.MessageBoxTitle_Warning, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                    OpenChildForm(new AutomationInfo());
            }
            else
            {
                CTMessageBox.Show(GlobalStrings.Message_NotChooseFormulaAlert, GlobalStrings.MessageBoxTitle_Warning, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void mainSettingFormClosed(object sender, EventArgs e)
        {
            ((Form)sender).FormClosed -= mainSettingFormClosed;
            if (ChooseSpec.isConfirmed)
            {
                ChooseSpec.isConfirmed = false;
                MainSetting mainSetting = new MainSetting();
                mainSetting.ShowDialog();
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            SettingsManager.SaveSettings();
            if (tmrCallBWStatusCheck != null)
            {
                tmrCallBWStatusCheck.Stop();
                tmrCallBWStatusCheck.Tick -= new EventHandler(timer_nextRun_Tick);
                statusCheckBackgroundWorker.DoWork -= new DoWorkEventHandler(BW_DoWork);
                statusCheckBackgroundWorker.ProgressChanged -= BW_ProgressChanged;
                statusCheckBackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(BW_RunWorkerCompleted);
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            PasswordConfirm passwordConfirm = new PasswordConfirm();
            passwordConfirm.FormClosed += mainSettingFormClosed;
            passwordConfirm.ShowDialog();

        }

        private void cbxLanguageChoose_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (SettingsManager.GetSetting(s => s.Language) != cbxLanguageChoose.SelectedIndex)
            {
                SettingsManager.UpdateSettings(s => s.Language = cbxLanguageChoose.SelectedIndex);
                DialogResult dialogResult = CTMessageBox.Show(GlobalStrings.Message_LanguageChange, GlobalStrings.MessageBoxTitle_Warning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}

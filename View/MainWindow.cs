using Microsoft.Win32;
using mixer_control_globalver.Controller;
using mixer_control_globalver.Controller.IniFile;
using mixer_control_globalver.Controller.LogFile;
using mixer_control_globalver.Model.PLC;
using mixer_control_globalver.Properties;
using mixer_control_globalver.View.CustomComponent;
using mixer_control_globalver.View.CustomControls;
using mixer_control_globalver.View.MainUI;
using mixer_control_globalver.View.SideUI;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace mixer_control_globalver
{
    public partial class MainWindow : Form
    {
        ///
        /// FIELDS
        ///
        private Form activeForm = null;

        public static int ConnectionPLC, ConnectionOilPLC;
        public static PLCConnector pLC;
        public static PLCConnector pLCOil;

        BackgroundWorker bgWorkerCheckOilTest;
        System.Windows.Forms.Timer tmrCallBgWorker;
        System.Threading.Timer tmrEnsureWorkerGetsCalled;
        object lockObject = new object();

        ChooseSpec specWindow = new ChooseSpec();

        int db = Settings.Default.database_no;
        int dbOil = Settings.Default.oil_feeder_db;
        string message = String.Empty, caption = String.Empty;

        IniFile ini = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "\\data\\setting.ini");

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        ///
        /// CONSTRUCTOR
        ///
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\data"))
                {
                    //Check và tạo directory "data" trong thư mục cài đặt của phần mềm
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\data");
                }
                TemporaryVariables.resetAllTempVariables();

                ////Lấy version hiện tại qua Assembly và thể hiện lên label
                //Version version = Assembly.GetExecutingAssembly().GetName().Version;
                //lbVersion.Text = $"{version}";

                this.Text = string.Empty;
                this.ControlBox = false;
                this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            }
            catch (Exception ex)
            {
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Application initial process error", ex.Message);
            }
        }

        ///
        /// METHODS
        ///
        public void openChildForm(Form childForm)
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
            //Change language message
            if (Settings.Default.language == 0)
            {
                message = "Thoát chương trình ?";
                caption = "Cảnh báo";
            }
            else if (Settings.Default.language == 1)
            {
                message = "退出应用 ?";
                caption = "提示";
            }
            else if (Settings.Default.language == 2)
            {
                message = "Exit the application ?";
                caption = "Warning";
            }
            DialogResult dialogResult = CTMessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                pLC = new PLCConnector(Settings.Default.plc_ip, 0, 0, out ConnectionPLC);
                //Oil comment
                if (ConnectionPLC == 0)
                {
                    pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("ER", "start")), Convert.ToInt32(ini.Read("ER", "bit")));
                    pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("SR", "start")), Convert.ToInt32(ini.Read("SR", "bit")));
                    pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")));
                    pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("CU", "start")), Convert.ToInt32(ini.Read("CU", "bit")));
                    pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("CD", "start")), Convert.ToInt32(ini.Read("CD", "bit")));
                    pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("OL", "start")), Convert.ToInt32(ini.Read("OL", "bit")));
                    pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("CL", "start")), Convert.ToInt32(ini.Read("CL", "bit")));
                    pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("TS", "start")), Convert.ToInt32(ini.Read("TS", "bit")));
                    pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")));
                    pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")));
                    pLC.WriteRealtoPLC(0, db, Convert.ToInt32(ini.Read("WS", "start")), 2);
                    pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("ONV", "start")), Convert.ToInt32(ini.Read("ONV", "bit")));
                    pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("OFFV", "start")), Convert.ToInt32(ini.Read("OFFV", "bit")));
                    pLC.WriteBoolToPLC(true, db, 24, 0); // Truyền reset variable
                    pLC.Diconnect();
                }
                if (Settings.Default.isOilFeed)
                {
                    pLCOil = new PLCConnector(Settings.Default.oil_feeder_ip, 0, 0, out ConnectionOilPLC);
                    //Reset 2 bit bắt đầu cấp dầu và dừng cấp dầu
                    if (ConnectionOilPLC == 0)
                    {
                        pLCOil.WriteBoolToPLC(false, dbOil, Convert.ToInt32(ini.Read("StopOil", "start")), Convert.ToInt32(ini.Read("StopOil", "bit")));
                        pLCOil.WriteBoolToPLC(false, dbOil, Convert.ToInt32(ini.Read("StartOil", "start")), Convert.ToInt32(ini.Read("StartOil", "bit")));
                        pLCOil.Diconnect();
                    }
                }

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

        private void pbxCompanyLogo_Click(object sender, EventArgs e)
        {
            Process.Start(Settings.Default.website);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            try
            {
                var settingValue1 = Properties.Settings.Default.database_no;
                var settingValue2 = Properties.Settings.Default.parityBits;
                var settingValue3 = Properties.Settings.Default.isTesting;
                var settingValue4 = Properties.Settings.Default.isSaveReport;
                var settingValue5 = Properties.Settings.Default.language;

                SubMethods.BackupUserSettings();
            }
            catch (ConfigurationErrorsException)
            {
                // Handle the error, e.g., delete the corrupted user.config file and inform the user
                SubMethods.RestoreUserSettings();
            }

            try
            {
                // this timer calls bgWorker again and again after regular intervals
                tmrCallBgWorker = new System.Windows.Forms.Timer();//Timer for do task
                tmrCallBgWorker.Tick += new EventHandler(timer_nextRun_Tick);
                tmrCallBgWorker.Interval = 1000; //3600000;

                // this is our worker
                bgWorkerCheckOilTest = new BackgroundWorker();

                // work happens in this method
                bgWorkerCheckOilTest.DoWork += new DoWorkEventHandler(BW_DoWork);
                bgWorkerCheckOilTest.ProgressChanged += BW_ProgressChanged;
                bgWorkerCheckOilTest.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BW_RunWorkerCompleted);
                bgWorkerCheckOilTest.WorkerReportsProgress = true;

                tmrCallBgWorker.Start();
            }
            catch (Exception ex)
            {
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Main background worker initial error", ex.Message);
            }

            TemporaryVariables.InitSettingDT();
            cbxLanguageChoose.SelectedIndex = Settings.Default.language;

            switch (Settings.Default.language)
            {
                case 0:
                    btnChooseSpecTab.ButtonText = "Chọn công thức";
                    btnWeightTab.ButtonText = "Xác nhận nguyên vật liệu";
                    btnAutomationTab.ButtonText = "Tự động hóa";
                    break;
                case 1:
                    btnChooseSpecTab.ButtonText = "选择产品型号";
                    btnWeightTab.ButtonText = "原料确认";
                    btnAutomationTab.ButtonText = "自动化";
                    break;
                case 2:
                    btnChooseSpecTab.ButtonText = "Choose formula";
                    btnWeightTab.ButtonText = "Material Confirmation";
                    btnAutomationTab.ButtonText = "Automation";
                    break;
                default:
                    btnChooseSpecTab.ButtonText = "Chọn công thức";
                    btnWeightTab.ButtonText = "Xác nhận nguyên vật liệu";
                    btnAutomationTab.ButtonText = "Tự động hóa";
                    break;
            }
            openSpecTab();
            specWindow.Owner = this;
        }


        private void timer_nextRun_Tick(object sender, EventArgs e)
        {
            if (Monitor.TryEnter(lockObject))
            {
                try
                {
                    // if bgworker is not busy the call the worker
                    if (!bgWorkerCheckOilTest.IsBusy)
                    {
                        bgWorkerCheckOilTest.RunWorkerAsync();
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
                tmrEnsureWorkerGetsCalled = new System.Threading.Timer(new TimerCallback(tmrEnsureWorkerGetsCalled_Callback), null, 0, 10);
            }
        }

        private void BW_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //Logic check time ở đây
                bool isRequired2Reset = false;
                if (!string.IsNullOrEmpty(Settings.Default.timeOilTested))
                {
                    string currentDate = DateTime.Now.ToString("dd/MM/yyyy");
                    DateTime curDateTime = DateTime.ParseExact(currentDate + " 08:00:00", "dd/MM/yyyy HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
                    DateTime checkDateTime = DateTime.ParseExact(Settings.Default.timeOilTested, "dd/MM/yyyy HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
                    if (checkDateTime < curDateTime)
                        isRequired2Reset = true;

                    if (isRequired2Reset)
                    {
                        Settings.Default.isOilTested = false;
                        bgWorkerCheckOilTest.ReportProgress(0);
                    }
                    else 
                    {
                        Settings.Default.isOilTested = true;
                        bgWorkerCheckOilTest.ReportProgress(0);
                    }
                    Settings.Default.Save();
                }
                else
                {
                    Settings.Default.isOilTested = false;
                    Settings.Default.Save();
                    bgWorkerCheckOilTest.ReportProgress(0);
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

            if (Settings.Default.isOilFeed)
            {
                if (Settings.Default.isOilTested)
                {
                    switch (Settings.Default.language)
                    {
                        case 0:
                            announceText = "Đã kiểm tra bộ nạp dầu!\r\nThời gian:\r\n" + Settings.Default.timeOilTested;
                            break;
                        case 1:
                            announceText = "供油器已检查！\r\n检查时间:\r\n" + Settings.Default.timeOilTested;
                            break;
                        case 2:
                            announceText = "Oil feeder checked!\r\nTime:\r\n" + Settings.Default.timeOilTested;
                            break;
                        default:
                            announceText = "Đã kiểm tra bộ nạp dầu!\r\nThời gian:\r\n" + Settings.Default.timeOilTested;
                            break;
                    }
                    lbOilTestStatus.BackColor = Color.Yellow;
                    lbOilTestStatus.ForeColor = Color.Black;
                }
                else
                {
                    switch (Settings.Default.language)
                    {
                        case 0:
                            announceText = "Chưa kiểm tra bộ nạp dầu.";
                            break;
                        case 1:
                            announceText = "没检查过供油器！";
                            break;
                        case 2:
                            announceText = "Haven't checked the oil feeder!";
                            break;
                        default:
                            announceText = "Chưa kiểm tra bộ nạp dầu.";
                            break;
                    }
                    lbOilTestStatus.BackColor = Color.Red;
                    lbOilTestStatus.ForeColor = Color.White;
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
                    if (!bgWorkerCheckOilTest.IsBusy)
                    {
                        bgWorkerCheckOilTest.RunWorkerAsync();
                    }
                }
                finally
                {
                    Monitor.Exit(lockObject);
                }
                tmrEnsureWorkerGetsCalled = null;
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
                        switch (Settings.Default.language)
                        {
                            case 0:
                                message = "Các dữ liệu đã làm sẽ bị mất! Bạn có muốn tiếp tục chọn công thức khác?";
                                caption = "Cảnh báo";
                                break;
                            case 1:
                                message = "您所做的更改可能无法保存。请选择其他产品型号？";
                                caption = "提示";
                                break;
                            case 2:
                                message = "Current data will be lost! Do you want to continue to choose other formula?";
                                caption = "Warning";
                                break;
                            default:
                                message = "Các dữ liệu đã làm sẽ bị mất! Bạn có muốn tiếp tục chọn công thức khác?";
                                caption = "Cảnh báo";
                                break;
                        }
                        DialogResult dialogResult = CTMessageBox.Show(message, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
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
            if (!String.IsNullOrEmpty(TemporaryVariables.tempFileName) && TemporaryVariables.processDT != null)
            {
                openChildForm(new MaterialScale());
                btnChooseSpecTab.BackgroundColor = Color.FromArgb(255, 255, 128);
                btnWeightTab.BackgroundColor = Color.FromArgb(255, 255, 192);
                btnAutomationTab.BackgroundColor = Color.FromArgb(255, 255, 128);
            }
            else
            {
                if (Settings.Default.language == 0)
                {
                    message = "Vui lòng chọn một công thức trước!";
                    caption = "Cảnh báo";
                }
                else if (Settings.Default.language == 1)
                {
                    message = "请先选择需要加工的产品型号！";
                    caption = "提示";
                }
                else if (Settings.Default.language == 2)
                {
                    message = "Please choose a formula first!";
                    caption = "Warning";
                }
                CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void btnAutomationTab_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TemporaryVariables.tempFileName) && TemporaryVariables.materialDT != null && TemporaryVariables.processDT != null)
            {
                if (!Settings.Default.isTesting)
                {
                    if (TemporaryVariables.materialDT.Rows.Count > 0)
                    {
                        bool notSettingEnough = false;
                        for (int i = 0; i < TemporaryVariables.settingDT.Rows.Count; i++)
                        {
                            if (String.IsNullOrEmpty(ini.Read(TemporaryVariables.settingDT.Rows[i]["value_member"].ToString(), "start")))
                            {
                                notSettingEnough = true;
                            }
                        }
                        if (String.IsNullOrEmpty(Settings.Default.plc_ip)
                    || Settings.Default.database_no == 0
                    || Settings.Default.max_speed == 0
                    || Settings.Default.spindle_diameter == 0
                    || Settings.Default.sensor_diameter == 0
                    || Settings.Default.transmission_ratio == 0
                    || notSettingEnough)
                        {
                            if (Settings.Default.language == 0)
                            {
                                message = "Vui lòng cài đặt đầy đủ các thông tin trong phần cài đặt!";
                                caption = "Cảnh báo";
                            }
                            else if (Settings.Default.language == 1)
                            {
                                message = "请在设置部分设置全部信息!";
                                caption = "提示";
                            }
                            else if (Settings.Default.language == 2)
                            {
                                message = "Please input all required setting first!";
                                caption = "Warning";
                            }
                            CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            MainSetting mainSetting = new MainSetting();
                            mainSetting.ShowDialog();
                        }
                        else
                        {
                            openChildForm(new AutomationInfo());
                            btnChooseSpecTab.BackgroundColor = Color.FromArgb(255, 255, 128);
                            btnWeightTab.BackgroundColor = Color.FromArgb(255, 255, 128);
                            btnAutomationTab.BackgroundColor = Color.FromArgb(255, 255, 192);
                        }
                    }
                    else
                    {
                        if (Settings.Default.language == 0)
                        {
                            message = "Vui lòng xác các nguyên liệu trước!";
                            caption = "Cảnh báo";
                        }
                        else if (Settings.Default.language == 1)
                        {
                            message = "请先确认好原料！";
                            caption = "提示";
                        }
                        else if (Settings.Default.language == 2)
                        {
                            message = "Please confirm all materials first!";
                            caption = "Warning";
                        }
                        CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    openChildForm(new AutomationInfo());
                    btnChooseSpecTab.BackgroundColor = Color.FromArgb(255, 255, 128);
                    btnWeightTab.BackgroundColor = Color.FromArgb(255, 255, 128);
                    btnAutomationTab.BackgroundColor = Color.FromArgb(255, 255, 192);
                }
            }
            else
            {
                if (Settings.Default.language == 0)
                {
                    message = "Vui lòng chọn một công thức trước!";
                    caption = "Cảnh báo";
                }
                else if (Settings.Default.language == 1)
                {
                    message = "请先选择需要加工的产品型号！";
                    caption = "提示";
                }
                else if (Settings.Default.language == 2)
                {
                    message = "Please choose a formula first!";
                    caption = "Warning";
                }
                CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (tmrCallBgWorker != null)
            {
                tmrCallBgWorker.Stop();
                tmrCallBgWorker.Tick -= new EventHandler(timer_nextRun_Tick);
                bgWorkerCheckOilTest.DoWork -= new DoWorkEventHandler(BW_DoWork);
                bgWorkerCheckOilTest.ProgressChanged -= BW_ProgressChanged;
                bgWorkerCheckOilTest.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(BW_RunWorkerCompleted);
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
            if (Settings.Default.language != cbxLanguageChoose.SelectedIndex)
            {
                Settings.Default.language = cbxLanguageChoose.SelectedIndex;
                Settings.Default.Save();

                //Change language message
                if (Settings.Default.language == 0)
                {
                    message = "Cần khởi động lại ứng dụng để áp dụng ngôn ngữ mới. Bạn có muốn thoát ?";
                    caption = "Cảnh báo";
                }
                else if (Settings.Default.language == 1)
                {
                    message = "切换语言需要重新启动才能生效，点击确认重新启动 ?";
                    caption = "提示";
                }
                else if (Settings.Default.language == 2)
                {
                    message = "A restart process is required to apply new language. Do you want to close the program ?";
                    caption = "Warning";
                }
                DialogResult dialogResult = CTMessageBox.Show(message, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}

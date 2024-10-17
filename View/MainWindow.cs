using mixer_control_globalver.Controller;
using mixer_control_globalver.Controller.IniFile;
using mixer_control_globalver.Controller.LogFile;
using mixer_control_globalver.Controller.PLC;
using mixer_control_globalver.Model.PLC;
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
        private string message = String.Empty, caption = String.Empty;
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
            InitializeComponent();
            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\data"))
                {
                    //Check và tạo directory "data" trong thư mục cài đặt của phần mềm
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\data");
                }
                TemporaryVariables.resetAllTempVariables();

                this.Text = string.Empty;
                this.ControlBox = false;
                this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            }
            catch (Exception ex)
            {
                CTMessageBox.Show("Application initial process error : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Application initial process error", ex.Message);
            }
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
            DialogResult dialogResult = CTMessageBox.Show("Exit the application ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                PLCMethods.ResetPLCVariables();
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
                        statusCheckBackgroundWorker.ReportProgress(0);
                    }
                    else
                    {
                        Settings.Default.isOilTested = true;
                        statusCheckBackgroundWorker.ReportProgress(0);
                    }
                    Settings.Default.Save();
                }
                else
                {
                    Settings.Default.isOilTested = false;
                    Settings.Default.Save();
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
                        switch (Settings.Default.language)
                        {
                            case 0:
                                message = "Chọn công thức mới sẽ khiến dữ liệu đang và đã làm trước đó sẽ bị mất và khởi tạo lại. Tiếp tục ?";
                                caption = "Cảnh báo";
                                break;
                            case 1:
                                message = "选择新公式将导致当前和以前的数据丢失并重置。继续 ？";
                                caption = "提示";
                                break;
                            case 2:
                                message = "Choosing a new formula will cause current and previous data to be lost and reset. Continue ?";
                                caption = "Warning";
                                break;
                            default:
                                message = "Chọn công thức mới sẽ khiến dữ liệu đang và đã làm trước đó sẽ bị mất và khởi tạo lại. Tiếp tục ?";
                                caption = "Cảnh báo";
                                break;
                        }
                        DialogResult dialogResult = CTMessageBox.Show(message, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
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
                            if (String.IsNullOrEmpty(PLCMethods.ini.Read(TemporaryVariables.settingDT.Rows[i]["value_member"].ToString(), "start")))
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
                            OpenChildForm(new AutomationInfo());
                    }
                    else
                    {
                        if (Settings.Default.language == 0)
                        {
                            message = "Vui lòng xác nhận các nguyên liệu trước!";
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
                    OpenChildForm(new AutomationInfo());
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
            if (Settings.Default.language != cbxLanguageChoose.SelectedIndex)
            {
                Settings.Default.language = cbxLanguageChoose.SelectedIndex;
                Settings.Default.Save();

                DialogResult dialogResult = CTMessageBox.Show("A restart process is required to apply new language. Do you want to close the program ?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}

using mixer_control_globalver.Controller;
using mixer_control_globalver.Controller.IniFile;
using mixer_control_globalver.Controller.LogFile;
using mixer_control_globalver.Model.PLC;
using mixer_control_globalver.Properties;
using mixer_control_globalver.View.CustomComponent;
using mixer_control_globalver.View.CustomControls;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace mixer_control_globalver.View.MainUI
{
    public partial class AutomationInfo : Form
    {
        /// <summary>
        /// FIELDS
        /// </summary>
        #region Fields
        public static int ConnectionPLC;
        public static int ConnectionOilPLC;

        CountDownTimer countDownTimer;
        bool AutoManual, ContainerUpSensor, CloseLidSensor, isFirstStart, isSpeedChanged, AutoTrigger, ManualTrigger;
        string message = String.Empty, caption = String.Empty, oilType = String.Empty;
        double oilMass, tempRT, maxTemp, speed, tempSpeed;
        int db, dbOil, currentRow, speed1, time1, speed2, time2, max_temp, rollMode = 1;
        bool isVaccum, isSkipAnnouce, isOilFeed, isOilFeeding;

        IniFile ini = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "\\data\\setting.ini");

        BackgroundWorker bgWorker;
        System.Windows.Forms.Timer tmrCallBgWorker;
        System.Threading.Timer tmrEnsureWorkerGetsCalled;

        object lockObject = new object();
        #endregion Fields

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        #region Contructor
        public AutomationInfo()
        {
            InitializeComponent();
        }
        #endregion Contructor

        /// <summary>
        /// 
        /// FORMS EVENT HANDLER
        /// </summary>
        #region Forms event handler
        private void AutomationInfo_Load(object sender, EventArgs e)
        {
            if (Settings.Default.language == 0)
            {
                lb1.Text = "Tốc độ hiện tại:";
                lb3.Text = "Nhiệt độ hiện tại:";
                lb2.Text = "(vòng/phút)";
                lb4.Text = "(Độ C)";
                lb5.Text = "Thời gian còn lại đến khi kết thúc bước:";
                lbShowF.Text = "Công thức:";

                btnStartProcess.ButtonText = "Bắt đầu thực hiện bước";
                btnNormalRoll.Text = "Quay Thuận";
                btnReverseRoll.Text = "Quay Ngược";
                btnResetRoll.Text = "Ngừng Quay";
            }
            else if (Settings.Default.language == 1)
            {
                lb1.Text = "实时转速:";
                lb3.Text = "实时温度:";
                lb2.Text = "(转/分)";
                lb4.Text = "(摄氏度)";
                lb5.Text = "时间倒计时致此步骤结束:";
                lbShowF.Text = "型号:";

                btnStartProcess.ButtonText = "开始执行此步骤";
                btnNormalRoll.Text = "顺转";
                btnReverseRoll.Text = "逆转";
                btnResetRoll.Text = "停止旋转";
            }
            else if (Settings.Default.language == 2)
            {
                lb1.Text = "Current speed:";
                lb3.Text = "Current temperature:";
                lb2.Text = "(rpm)";
                lb4.Text = "(Celsius)";
                lb5.Text = "Time left untill process ended:";
                lbShowF.Text = "Formula:";

                btnStartProcess.ButtonText = "Start Process";
                btnNormalRoll.Text = "Clockwise";
                btnReverseRoll.Text = "Reverse Clockwise";
                btnResetRoll.Text = "Stop Motor";
            }

            if (Settings.Default.isHideReverse)
                btnReverseRoll.Visible = false;

            lbFormulaName.Text = TemporaryVariables.tempFileName;

            isFirstStart = false;
            isSpeedChanged = false;

            TryConnectToPLC();
            GetNextProcess();
            LoadBackgroundWorker();
        }

        private void btnStartProcess_Click(object sender, EventArgs e)
        {
            try
            {
                if (Settings.Default.language == 0)
                {
                    message = "Hãy xác nhận đã cho nguyên liệu vào máy! Bấm \"OK\" để tiến hành bước đang thể hiện!";
                    caption = "Cảnh báo";
                }
                else if (Settings.Default.language == 1)
                {
                    message = "请确认料已经放好！继续执行此步骤，点击 \"OK\"。";
                    caption = "提示";
                }
                else if (Settings.Default.language == 2)
                {
                    message = "Please confirm the material have been put in the machine! Press \"OK\" to begin process!";
                    caption = "Warning";
                }
                DialogResult dialog = CTMessageBox.Show(message, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dialog == DialogResult.OK)
                {
                    isSpeedChanged = false;
                    bool isStartSuccess = false;
                    do
                    {
                        PLCConnector pLC = new PLCConnector(Settings.Default.plc_ip, 0, 0, out ConnectionPLC);
                        if (ConnectionPLC == 0)
                        {
                            isStartSuccess = true;
                            pLC.WriteBoolToPLC(true, db, Convert.ToInt32(ini.Read("ER", "start")), Convert.ToInt32(ini.Read("ER", "bit")));

                            pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("SR", "start")), Convert.ToInt32(ini.Read("SR", "bit")));
                        }
                    } while (isStartSuccess == false);

                    isFirstStart = true;

                    btnNormalRoll.Enabled = true;
                    btnReverseRoll.Enabled = true;
                    btnResetRoll.Enabled = true;
                    btnStartProcess.Enabled = false;
                }

            }
            catch (Exception ex) { SystemLog.Output(SystemLog.MSG_TYPE.Err, "Start Process", ex.Message); }
        }

        private void btnReverseRoll_Click(object sender, EventArgs e)
        {
            try
            {
                PLCConnector pLC = new PLCConnector(Settings.Default.plc_ip, 0, 0, out ConnectionPLC);
                if (ConnectionPLC == 0)
                {
                    rollMode = 2;
                    if (!countDownTimer.IsRunning)
                        countDownTimer.Continue();
                    pLC.WriteBoolToPLC(true, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")));
                    pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")));
                    pLC.WriteRealtoPLC(Convert.ToSingle(tempSpeed), db, Convert.ToInt32(ini.Read("WS", "start")), 2);
                    btnReverseRoll.BackColor = Color.Yellow;
                    btnNormalRoll.BackColor = Color.White;
                    btnResetRoll.BackColor = Color.White;

                }
            }
            catch (Exception ex) { SystemLog.Output(SystemLog.MSG_TYPE.Err, "Reverse Roll", ex.Message); }
        }

        private void btnResetRoll_Click(object sender, EventArgs e)
        {
            PLCConnector pLC = new PLCConnector(Settings.Default.plc_ip, 0, 0, out ConnectionPLC);
            if (ConnectionPLC == 0)
            {
                rollMode = 3;
                if (countDownTimer.IsRunning)
                    countDownTimer.Pause();
                btnResetRoll.BackColor = Color.Yellow;
                pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")));
                pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")));
                pLC.WriteRealtoPLC(0, db, Convert.ToInt32(ini.Read("WS", "start")), 2);
                btnNormalRoll.BackColor = Color.White;
                btnReverseRoll.BackColor = Color.White;
            }
        }

        private void btnNormalRoll_Click(object sender, EventArgs e)
        {
            PLCConnector pLC = new PLCConnector(Settings.Default.plc_ip, 0, 0, out ConnectionPLC);
            if (ConnectionPLC == 0)
            {
                rollMode = 1;
                if (!countDownTimer.IsRunning)
                    countDownTimer.Continue();
                pLC.WriteBoolToPLC(true, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")));
                pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")));
                pLC.WriteRealtoPLC(Convert.ToSingle(tempSpeed), db, Convert.ToInt32(ini.Read("WS", "start")), 2);
                btnNormalRoll.BackColor = Color.Yellow;
                btnReverseRoll.BackColor = Color.White;
                btnResetRoll.BackColor = Color.White;
            }
        }

        private void btnContinueStep_Click(object sender, EventArgs e)
        {
            if (Settings.Default.language == 0)
            {
                message = "Xác nhận bỏ qua bước đang thể hiện ?";
                caption = "Cảnh báo";
            }
            else if (Settings.Default.language == 1)
            {
                message = "确认跳过这个步骤 ?";
                caption = "提示";
            }
            else if (Settings.Default.language == 2)
            {
                message = "Skip current process ?";
                caption = "Warning";
            }

            DialogResult dialog = CTMessageBox.Show(message, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                PLCConnector pLC = new PLCConnector(Settings.Default.plc_ip, 0, 0, out ConnectionPLC);
                if (ConnectionPLC == 0)
                {
                    TemporaryVariables.processDT.Rows[currentRow]["is_finished"] = true;
                    if (countDownTimer.IsRunning)
                    {
                        countDownTimer.Delete();
                    }

                    LoadingDialog loading = new LoadingDialog();
                    Thread resetPLC = new Thread(
                            new ThreadStart(() =>
                            {
                                pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")));
                                pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")));
                                pLC.WriteRealtoPLC(0, db, Convert.ToInt32(ini.Read("WS", "start")), 2);
                                loading.BeginInvoke(new Action(() => loading.Close()));
                            }));
                    resetPLC.Start();
                    loading.ShowDialog();
                    btnResetRoll.BackColor = Color.Yellow;
                    btnNormalRoll.BackColor = Color.White;
                    btnReverseRoll.BackColor = Color.White;

                    lbCountDown.Text = "00:00:00";

                    GetNextProcess();
                    btnStartProcess.Enabled = true;
                }
            }
        }

        private void AutomationInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tmrCallBgWorker != null)
            {
                tmrCallBgWorker.Stop();
                tmrCallBgWorker.Tick -= new EventHandler(timer_nextRun_Tick);
                bgWorker.DoWork -= new DoWorkEventHandler(BW_DoWork);
                bgWorker.ProgressChanged -= BW_ProgressChanged;
                bgWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(BW_RunWorkerCompleted);
            }
            if (countDownTimer.IsRunning)
            {
                countDownTimer.Delete();
            }
            ResetVariablesPLC();

            btnStartProcess.Enabled = true;
            PLCConnector pLC = new PLCConnector(Settings.Default.plc_ip, 0, 0, out ConnectionPLC);
            if (ConnectionPLC == 0)
            {
                pLC.Diconnect();
            }
            if (Settings.Default.isOilFeed)
            {
                PLCConnector pLCOil = new PLCConnector(Settings.Default.oil_feeder_ip, 0, 0, out ConnectionOilPLC);
                if (ConnectionOilPLC == 0)
                    pLCOil.Diconnect();
            }
        }

        #endregion Forms event handler


        #region PLC logic
        /// <summary>
        /// 
        /// PLC METHODS
        /// 
        /// </summary>
        #region PLC Methods
        private void TryConnectToPLC()
        {
            try
            {
                //Khởi tạo kết nối PLC máy trộn
                db = Settings.Default.database_no;
                //Khởi tạo kết nối PLC máy cấp dầu
                if (Settings.Default.isOilFeed)
                {
                    dbOil = Settings.Default.oil_feeder_db;
                }

                PLCConnector pLC = new PLCConnector(Settings.Default.plc_ip, 0, 0, out ConnectionPLC);
                if (ConnectionPLC == 0)
                {
                    pLC.WriteRealtoPLC(Convert.ToSingle(Settings.Default.max_speed), db, Convert.ToInt32(ini.Read("MS", "start")), 2);
                    pLC.WriteRealtoPLC(Convert.ToSingle(Settings.Default.spindle_diameter), db, Convert.ToInt32(ini.Read("SD", "start")), 2);
                    pLC.WriteRealtoPLC(Convert.ToSingle(Settings.Default.sensor_diameter), db, Convert.ToInt32(ini.Read("SSD", "start")), 2);
                    pLC.WriteRealtoPLC(Convert.ToSingle(Settings.Default.transmission_ratio), db, Convert.ToInt32(ini.Read("TRMS", "start")), 2);
                }
            }
            catch (Exception ex)
            {
                message = "Failed to init connect to PLC\r\n" + ex.Message;
                caption = "Error";
                SystemLog.Output(SystemLog.MSG_TYPE.Err, caption, message);
                DialogResult dialogResult = CTMessageBox.Show(message, caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Retry)
                {
                    TryConnectToPLC();
                }
                else
                {
                    Program.main.openScaleTab();
                }
            }
        }

        private void ResetVariablesPLC()
        {
            PLCConnector pLC = new PLCConnector(Settings.Default.plc_ip, 0, 0, out ConnectionPLC);
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

                PLCConnector pLCOil = new PLCConnector(Settings.Default.oil_feeder_ip, 0, 0, out ConnectionOilPLC);
                if (Settings.Default.isOilFeed && ConnectionOilPLC == 0)
                {
                    //Reset oil stop and start bit
                    pLCOil.WriteBoolToPLC(false, dbOil, Convert.ToInt32(ini.Read("StopOil", "start")), Convert.ToInt32(ini.Read("StopOil", "bit")));
                    pLCOil.WriteBoolToPLC(false, dbOil, Convert.ToInt32(ini.Read("StartOil", "start")), Convert.ToInt32(ini.Read("StartOil", "bit")));
                }
            }
            else
            {
                if (Settings.Default.language == 0)
                {
                    message = "Reset thông số PLC thất bại";
                    caption = "Lỗi";
                }
                else if (Settings.Default.language == 1)
                {
                    message = "无法重置 PLC 变量";
                    caption = "错误";
                }
                else if (Settings.Default.language == 2)
                {
                    message = "Failed to reset PLC variables";
                    caption = "Error";
                }
                DialogResult dialogResult = CTMessageBox.Show(message, caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (dialogResult == DialogResult.Retry)
                {
                    ResetVariablesPLC();
                }
            }
        }

        private void TriggerAutomationON()
        {
            PLCConnector pLC = new PLCConnector(Settings.Default.plc_ip, 0, 0, out ConnectionPLC);
            AutoTrigger = true;
            ManualTrigger = false;
            btnActivateSpeedControl.BackColor = Color.Yellow;
            if (Settings.Default.language == 0)
            {
                btnActivateSpeedControl.Text = "Chế độ tự động đang bật";
            }
            else if (Settings.Default.language == 1)
            {
                btnActivateSpeedControl.Text = "自动化模式：开";
            }
            else if (Settings.Default.language == 2)
            {
                btnActivateSpeedControl.Text = "Automation mode ON";
            }
            btnNormalRoll.Visible = true;
            if (!Settings.Default.isHideReverse)
                btnReverseRoll.Visible = true;
            btnResetRoll.Visible = true;
            switch (rollMode)
            {
                case 1:
                    {
                        pLC.WriteBoolToPLC(true, db, Convert.ToInt32(ini.Read("TS", "start")), Convert.ToInt32(ini.Read("TS", "bit")));
                        pLC.WriteBoolToPLC(true, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")));
                        btnNormalRoll.BackColor = Color.Yellow;
                        pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")));
                        btnReverseRoll.BackColor = Color.White;
                        btnResetRoll.BackColor = Color.White;
                        break;
                    }
                case 2:
                    {
                        pLC.WriteBoolToPLC(true, db, Convert.ToInt32(ini.Read("TS", "start")), Convert.ToInt32(ini.Read("TS", "bit")));
                        pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")));
                        btnNormalRoll.BackColor = Color.White;
                        pLC.WriteBoolToPLC(true, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")));
                        btnReverseRoll.BackColor = Color.Yellow;
                        btnResetRoll.BackColor = Color.White;
                        break;
                    }
                case 3:
                    {
                        pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("TS", "start")), Convert.ToInt32(ini.Read("TS", "bit")));
                        pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")));
                        btnNormalRoll.BackColor = Color.White;
                        pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")));
                        btnReverseRoll.BackColor = Color.White;
                        btnResetRoll.BackColor = Color.Yellow;
                        break;
                    }
            }
        }

        private void TriggerAutomationOFF()
        {
            AutoTrigger = false;
            ManualTrigger = true;

            btnActivateSpeedControl.BackColor = Color.White;
            if (Settings.Default.language == 0)
            {
                btnActivateSpeedControl.Text = "Chế độ tự động đang tắt";
            }
            else if (Settings.Default.language == 1)
            {
                btnActivateSpeedControl.Text = "自动化模式：关";
            }
            else if (Settings.Default.language == 2)
            {
                btnActivateSpeedControl.Text = "Automation mode OFF";
            }
            btnNormalRoll.Visible = false;
            if (!Settings.Default.isHideReverse)
                btnReverseRoll.Visible = false;
            btnResetRoll.Visible = false;
        }

        private void TimerProcessTrigger()
        {
            try
            {
                lbCountDown.Text = countDownTimer.TimeLeftStr;
            }
            catch (Exception ex)
            {
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Change speed error", ex.Message);
            }
        }

        private void FinishProcess()
        {
            bool isFinishProcess = false;
            do
            {
                PLCConnector pLC = new PLCConnector(Settings.Default.plc_ip, 0, 0, out ConnectionPLC);
                if (ConnectionPLC == 0)
                {
                    isFinishProcess = true;
                    lbAnnounce.Text = String.Empty;

                    if (countDownTimer.IsRunning)
                        countDownTimer.Delete();

                    TemporaryVariables.processDT.Rows[currentRow]["is_finished"] = true;
                    pLC.WriteBoolToPLC(true, db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")));

                    if (isVaccum)
                    {
                        if (!pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("OFFV", "start")), Convert.ToInt32(ini.Read("OFFV", "bit")), 1))
                        {
                            pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("ONV", "start")), Convert.ToInt32(ini.Read("ONV", "bit")));
                            pLC.WriteBoolToPLC(true, db, Convert.ToInt32(ini.Read("OFFV", "start")), Convert.ToInt32(ini.Read("OFFV", "bit")));
                        }
                    }

                    btnStartProcess.Enabled = true;

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
                        if (Settings.Default.isStopBetweenStep)
                        {
                            pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("TS", "start")), Convert.ToInt32(ini.Read("TS", "bit")));
                            pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")));
                            pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")));
                            pLC.WriteRealtoPLC(0, db, Convert.ToInt32(ini.Read("WS", "start")), 2);
                        }

                        if (isSkipAnnouce)
                        {
                            pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")));
                            if (Settings.Default.isSkipOpenLid)
                            {
                                pLC.WriteBoolToPLC(true, db, Convert.ToInt32(ini.Read("OL", "start")), Convert.ToInt32(ini.Read("OL", "bit")));
                            }
                        }
                        else
                        {
                            if (Settings.Default.language == 0)
                            {
                                message = "Đã kết thúc bước, bấm \"OK\" để mở nắp, \"CANCEL\" để giữ nắp đóng!";
                                caption = "Thông tin";
                            }
                            else if (Settings.Default.language == 1)
                            {
                                message = "此步骤已经结束， 开盖 - 点击\"OK\", 继续运行 - 点击\"CANCEL\"";
                                caption = "信息";
                            }
                            else if (Settings.Default.language == 2)
                            {
                                message = "Process finished, press \"OK\" to open the lid, press \"Cancel\" to left the lid stay shut!";
                                caption = "Information";
                            }

                            DialogResult dialog = CTMessageBox.Show(message, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            if (dialog == DialogResult.OK)
                            {
                                pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")));
                                pLC.WriteBoolToPLC(true, db, Convert.ToInt32(ini.Read("OL", "start")), Convert.ToInt32(ini.Read("OL", "bit")));
                            }
                            else
                            {
                                pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")));
                            }
                        }
                        GetNextProcess();
                    }
                    else
                    {
                        if (Settings.Default.isStopBetweenStep)
                        {
                            pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("TS", "start")), Convert.ToInt32(ini.Read("TS", "bit")));
                            pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")));
                            pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")));
                            pLC.WriteRealtoPLC(0, db, Convert.ToInt32(ini.Read("WS", "start")), 2);
                        }
                        if (isSkipAnnouce)
                        {
                            pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")));
                            if (Settings.Default.isSkipOpenLid)
                            {
                                pLC.WriteBoolToPLC(true, db, Convert.ToInt32(ini.Read("OL", "start")), Convert.ToInt32(ini.Read("OL", "bit")));
                            }
                        }
                        pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("ER", "start")), Convert.ToInt32(ini.Read("ER", "bit")));
                        if (Settings.Default.language == 0)
                        {
                            message = "Đã hoàn thành thực hiện công thức! Vui lòng đổ liệu thủ công!";
                            caption = "Thông tin";
                        }
                        else if (Settings.Default.language == 1)
                        {
                            message = "生产完毕！请将产品从捏合机里取出！";
                            caption = "信息";
                        }
                        else if (Settings.Default.language == 2)
                        {
                            message = "Formula automation process is finished! Please take out the product manually!";
                            caption = "Information";
                        }
                        CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GC.Collect();
                        Program.main.openSpecTab();
                    }
                }
            } while (isFinishProcess == false);
        }

        private void startRunAutomationProcess(PLCConnector pLC)
        {
            lbAnnounce.Text = String.Empty;
            rollMode = 1;
            pLC.WriteBoolToPLC(true, db, Convert.ToInt32(ini.Read("TS", "start")), Convert.ToInt32(ini.Read("TS", "bit")));
            pLC.WriteBoolToPLC(true, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")));
            btnNormalRoll.BackColor = Color.Yellow;
            pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")));
            btnReverseRoll.BackColor = Color.White;
            btnResetRoll.BackColor = Color.White;
            pLC.WriteRealtoPLC(Convert.ToSingle(speed1), db, Convert.ToInt32(ini.Read("WS", "start")), 2);

            if (isVaccum)
            {
                if (!pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("ONV", "start")), Convert.ToInt32(ini.Read("ONV", "bit")), 1))
                {
                    pLC.WriteBoolToPLC(true, db, Convert.ToInt32(ini.Read("ONV", "start")), Convert.ToInt32(ini.Read("ONV", "bit")));
                    pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("OFFV", "start")), Convert.ToInt32(ini.Read("OFFV", "bit")));
                }
            }

            tempSpeed = speed1;

            int time = time1 + time2;
            countDownTimer.SetTime(time, 0);
            countDownTimer.Start();
            countDownTimer.TimeChanged += () => TimerProcessTrigger();
            countDownTimer.CountDownFinished += () => FinishProcess();
            countDownTimer.StepMs = 1000;
        }

        #endregion PLC Methods

        /// <summary>
        /// 
        /// PLC EVENT HANDLER
        /// 
        /// </summary>
        #region PLC event handler
        private void timer_nextRun_Tick(object sender, EventArgs e)
        {
            if (Monitor.TryEnter(lockObject))
            {
                try
                {
                    // if bgworker is not busy the call the worker
                    if (!bgWorker.IsBusy)
                    {
                        bgWorker.RunWorkerAsync();
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
                PLCConnector pLC = new PLCConnector(Settings.Default.plc_ip, 0, 0, out ConnectionPLC);
                PLCConnector pLCOil = new PLCConnector(Settings.Default.oil_feeder_ip, 0, 0, out ConnectionOilPLC);
                if (ConnectionPLC == 0 && ConnectionOilPLC == 0) //Check connection to PLC and connect
                {
                    //Read current temperature from the PLC
                    tempRT = Convert.ToDouble(pLC.ReadRealToString(db, Convert.ToInt32(ini.Read("RT", "start"))));
                    //Read current speed from the PLC
                    speed = Convert.ToDouble(pLC.ReadRealToString(db, Convert.ToInt32(ini.Read("RS", "start"))));

                    if (speed < 0)
                    {
                        speed *= (-1); // When run in reverse mode.
                    }

                    maxTemp = Convert.ToDouble(max_temp);

                    //Read auto/manual variable
                    AutoManual = pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("AM", "start")), Convert.ToInt32(ini.Read("AM", "bit")), 1);

                    //Read Container Sensor 
                    ContainerUpSensor = pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("SSCU", "start")), Convert.ToInt32(ini.Read("SSCU", "bit")), 1);

                    //Read Lid Sensor 
                    CloseLidSensor = pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("SSCL", "start")), Convert.ToInt32(ini.Read("SSCL", "bit")), 1);

                    bgWorker.ReportProgress(0);
                }
                else
                {
                    throw new Exception("Can NOT connect to PLC!");
                }
            }
            catch (Exception ex)
            {
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Read speed and temperature error:", ex.Message);
            }
        }

        private void BW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UpdateUIWithBGWorkerVariables();
        }

        private void BW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) { }

        void tmrEnsureWorkerGetsCalled_Callback(object obj)
        {
            // this timer was started as the bgworker was busy before now it will try to call the bgworker again
            if (Monitor.TryEnter(lockObject))
            {
                try
                {
                    if (!bgWorker.IsBusy)
                    {
                        bgWorker.RunWorkerAsync();
                    }
                }
                finally
                {
                    Monitor.Exit(lockObject);
                }
                tmrEnsureWorkerGetsCalled = null;
            }
        }

        #endregion PLC event handler
        #endregion PLC logic

        /// <summary>
        /// 
        /// METHODS
        /// 
        /// </summary>
        #region Methods
        private String SetTimeChange(int min, int sec = 0)
        {
            if (min > 0)
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(min * 60 + sec);
                return timeSpan.ToString(@"hh\:mm\:ss");
            }
            else
            {
                return null;
            }
        }

        private void GetNextProcess()
        {
            DataTable dt = TemporaryVariables.processDT;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!(bool)dt.Rows[i]["is_finished"])
                    {
                        currentRow = i;
                        rtbRemark.Text = dt.Rows[i]["description"].ToString();

                        if (Settings.Default.language == 0)
                        {
                            lbProcessNo.Text = "Thực hiện bước số : " + dt.Rows[i]["process_no"].ToString();
                        }
                        else if (Settings.Default.language == 1)
                        {
                            lbProcessNo.Text = "执行第" + dt.Rows[i]["process_no"].ToString() + "步骤";
                        }
                        else if (Settings.Default.language == 2)
                        {
                            lbProcessNo.Text = "Process no: " + dt.Rows[i]["process_no"].ToString();
                        }

                        speed1 = (int)dt.Rows[i]["init_speed"];
                        time1 = (int)dt.Rows[i]["init_time"];
                        time2 = (int)dt.Rows[i]["change_time"];
                        speed2 = (int)dt.Rows[i]["change_speed"];
                        isVaccum = (bool)dt.Rows[i]["is_vaccum"];
                        max_temp = (int)dt.Rows[i]["max_temperature"];
                        isSkipAnnouce = (bool)dt.Rows[i]["is_skip_announce"];
                        if (Settings.Default.isOilFeed)
                        {
                            isOilFeed = (bool)dt.Rows[i]["is_oilfeed"];
                            oilMass = (double)dt.Rows[i]["oil_mass"];
                            oilType = dt.Rows[i]["oil_type"].ToString();
                        }
                        else
                        {
                            isOilFeed = false;
                            oilMass = 0;
                            oilType = "";
                        }
                        isOilFeeding = false;
                        countDownTimer = new CountDownTimer();
                        isFirstStart = false;

                        string announce = String.Empty;
                        if (Settings.Default.language == 0)
                        {
                            announce = "Đang đợi bấm nút bắt đầu...";
                        }
                        else if (Settings.Default.language == 1)
                        {
                            announce = "在等待按下开始按钮...";
                        }
                        else if (Settings.Default.language == 2)
                        {
                            announce = "Waiting for start button...";
                        }
                        lbAnnounce.Text = announce;

                        break;
                    }
                }
            }
        }

        private void LoadBackgroundWorker()
        {   // this timer calls bgWorker again and again after regular intervals
            tmrCallBgWorker = new System.Windows.Forms.Timer();//Timer for do task
            tmrCallBgWorker.Tick += new EventHandler(timer_nextRun_Tick);
            tmrCallBgWorker.Interval = 1000; //3600000;

            // this is our worker
            bgWorker = new BackgroundWorker();

            // work happens in this method
            bgWorker.DoWork += new DoWorkEventHandler(BW_DoWork);
            bgWorker.ProgressChanged += BW_ProgressChanged;
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BW_RunWorkerCompleted);
            bgWorker.WorkerReportsProgress = true;

            tmrCallBgWorker.Start();
        }

        private void UpdateUIWithBGWorkerVariables()
        {
            try
            {
                PLCConnector pLC = new PLCConnector(Settings.Default.plc_ip, 0, 0, out ConnectionPLC);
                if (ConnectionPLC == 0)
                {
                    string announce = String.Empty;
                    // Update variables to UI
                    // Update current temperature
                    lbTemperature.Text = Math.Round(tempRT, 2).ToString();
                    // Update current speed
                    lbRollSpeed.Text = Math.Round(speed, 2).ToString();

                    //Compare the current temperature with the maximum temperature set in the excel file
                    if (tempRT > maxTemp)
                    {
                        panelShowTemperature.BackColor = Color.Red;
                        pLC.WriteBoolToPLC(true, db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")));
                    }
                    else
                    {
                        panelShowTemperature.BackColor = Color.Black;
                        pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")));
                    }

                    // Check auto/ manual variable
                    if (AutoManual)
                    {
                        if(ManualTrigger && !AutoTrigger)
                        {
                            if (!countDownTimer.IsRunning)
                            {
                                countDownTimer.Continue();
                            }
                            if(!isFirstStart)
                            {
                                pLC.WriteRealtoPLC(Convert.ToSingle(tempSpeed), db, Convert.ToInt32(ini.Read("WS", "start")), 2);
                            }
                        }
                        TriggerAutomationON();
                    }
                    else
                    {
                        if (!ManualTrigger && AutoTrigger)
                        {
                            if (countDownTimer.IsRunning)
                            {
                                countDownTimer.Pause();
                            }
                        } 
                        TriggerAutomationOFF();
                    }

                    if (isFirstStart)
                    {
                        if (ContainerUpSensor && CloseLidSensor)
                        {
                            if (isOilFeed && Settings.Default.isOilFeed) // Check to see if the current working step 
                            {
                                PLCConnector pLCOil = new PLCConnector(Settings.Default.oil_feeder_ip, 0, 0, out ConnectionOilPLC);
                                if (ConnectionOilPLC == 0)
                                {
                                    if (!pLCOil.ReadBitToBool(dbOil, Convert.ToInt32(ini.Read("StopOil", "start")), Convert.ToInt32(ini.Read("StopOil", "bit")), 1))
                                    {
                                        if (!isOilFeeding)
                                        {
                                            if (Settings.Default.language == 0)
                                            {
                                                announce = "Đang cấp dầu ...";
                                            }
                                            else if (Settings.Default.language == 1)
                                            {
                                                announce = "供油...";
                                            }
                                            else if (Settings.Default.language == 2)
                                            {
                                                announce = "Oil feeding ...";
                                            }

                                            pLCOil.WriteRealtoPLC(Convert.ToSingle(oilMass), dbOil, Convert.ToInt32(ini.Read("OilMass", "start")), 2);
                                            pLCOil.WriteDinttoPLC(dbOil, dbOil, Convert.ToInt32(ini.Read("MachineLocation", "start")), 2);
                                            pLCOil.WriteStringtoPLC(oilType, dbOil, Convert.ToInt32(ini.Read("OilType", "start")), 254);
                                            pLCOil.WriteBoolToPLC(true, dbOil, Convert.ToInt32(ini.Read("StartOil", "start")), Convert.ToInt32(ini.Read("StartOil", "bit")));
                                            
                                            lbAnnounce.Text = announce;

                                            isOilFeeding = true;
                                        }
                                    }
                                    else
                                    {
                                        lbCountDown.Text = "00:00:00";
                                        //Reset 2 bit bắt đầu cấp dầu và dừng cấp dầu
                                        pLCOil.WriteBoolToPLC(false, dbOil, Convert.ToInt32(ini.Read("StopOil", "start")), Convert.ToInt32(ini.Read("StopOil", "bit")));
                                        pLCOil.WriteBoolToPLC(false, dbOil, Convert.ToInt32(ini.Read("StartOil", "start")), Convert.ToInt32(ini.Read("StartOil", "bit")));

                                        isFirstStart = false;
                                        startRunAutomationProcess(pLC);
                                    }
                                }
                            }
                            else
                            {
                                isFirstStart = false;
                                startRunAutomationProcess(pLC);
                            }
                        }
                        else
                        {
                            if (Settings.Default.language == 0)
                            {
                                announce = "Đang chờ tín hiệu từ cảm biến ...";
                            }
                            else if (Settings.Default.language == 1)
                            {
                                announce = "在等传感器的信号...";
                            }
                            else if (Settings.Default.language == 2)
                            {
                                announce = "Waiting for sensor data ...";
                            }
                            lbAnnounce.Text = announce;
                        }
                    }

                    //Change speed
                    if (lbCountDown.Text != "00:00:00")
                    {
                        if (!isSpeedChanged)
                        {
                            string timeChange = SetTimeChange(time2, 0);
                            if (!String.IsNullOrEmpty(timeChange))
                            {
                                if (tempSpeed != speed2)
                                {
                                    TimeSpan time1 = TimeSpan.Parse(lbCountDown.Text);
                                    TimeSpan time2 = TimeSpan.Parse(timeChange);
                                    // Compare times
                                    int comparisonResult = TimeSpan.Compare(time1, time2);
                                    if (comparisonResult <= 0)
                                    {
                                        isSpeedChanged = true;
                                        pLC.WriteRealtoPLC(Convert.ToSingle(speed2), db, Convert.ToInt32(ini.Read("WS", "start")), 2);
                                        tempSpeed = speed2;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Can NOT connect to PLC!");
                }
            }
            catch (Exception ex)
            {
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Update UI/ check PLC variables error:", ex.Message);
            }
        }

        #endregion Methods
    }
}
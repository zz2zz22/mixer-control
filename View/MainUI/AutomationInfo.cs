using mixer_control_globalver.Controller;
using mixer_control_globalver.Controller.IniFile;
using mixer_control_globalver.Controller.LogFile;
using mixer_control_globalver.Model.PLC;
using mixer_control_globalver.Properties;
using mixer_control_globalver.View.CustomControls;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace mixer_control_globalver.View.MainUI
{
    public partial class AutomationInfo : Form
    {
        //Fields
        public static int ConnectionPLC;
        public static PLCConnector pLC;
        System.Windows.Forms.Timer checkTimer;
        CountDownTimer timer;
        double tempSpeed;
        bool startOne = false;
        bool AutoTrigger, ManualTrigger;
        bool isAlarmed = false;
        bool isSafeTemp = false;
        string message = String.Empty, caption = String.Empty;
        double tempTemp;
        int alarmTick = 1;
        double increasedTemp = 0;
        int db, currentRow, speed1, time1, speed2, time2, max_temp;
        bool isVaccum, isSkipAnnouce;
        IniFile ini = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "\\data\\setting.ini");

        EventBroker.EventObserver m_observerLog = null;
        EventBroker.EventParam m_timerEvent = null;

        BackgroundWorker bgWorker;
        System.Windows.Forms.Timer tmrCallBgWorker;
        System.Threading.Timer tmrEnsureWorkerGetsCalled;

        object lockObject = new object();

        //Contructor
        public AutomationInfo()
        {
            InitializeComponent();
        }

        //Methods
        private void TryConnectToPLC()
        {
            pLC = new PLCConnector(Settings.Default.plc_ip, 0, 0, out ConnectionPLC);
            db = Settings.Default.database_no;

            pLC.WriteRealtoPLC(Convert.ToSingle(Settings.Default.max_speed), db, Convert.ToInt32(ini.Read("MS", "start")), 2);
            pLC.WriteRealtoPLC(Convert.ToSingle(Settings.Default.spindle_diameter), db, Convert.ToInt32(ini.Read("SD", "start")), 2);
            pLC.WriteRealtoPLC(Convert.ToSingle(Settings.Default.sensor_diameter), db, Convert.ToInt32(ini.Read("SSD", "start")), 2);
            pLC.WriteRealtoPLC(Convert.ToSingle(Settings.Default.transmission_ratio), db, Convert.ToInt32(ini.Read("TRMS", "start")), 2);
        }

        private void ResetVariablesPLC()
        {
            pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("ER", "start")), Convert.ToInt32(ini.Read("ER", "bit")), 1);
            pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("SR", "start")), Convert.ToInt32(ini.Read("SR", "bit")), 1);
            pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1);
            pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("CU", "start")), Convert.ToInt32(ini.Read("CU", "bit")), 1);
            pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("CD", "start")), Convert.ToInt32(ini.Read("CD", "bit")), 1);
            pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("OL", "start")), Convert.ToInt32(ini.Read("OL", "bit")), 1);
            pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("CL", "start")), Convert.ToInt32(ini.Read("CL", "bit")), 1);
            pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("TS", "start")), Convert.ToInt32(ini.Read("TS", "bit")), 1);
            pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1);
            pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")), 1);
            pLC.WriteRealtoPLC(0, db, Convert.ToInt32(ini.Read("WS", "start")), 2);
            pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("ONV", "start")), Convert.ToInt32(ini.Read("ONV", "bit")), 1);
            pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("OFFV", "start")), Convert.ToInt32(ini.Read("OFFV", "bit")), 1);
        }

        private void TriggerAutomationON()
        {
            if (!timer.IsRunning)
                timer.Continue();

            AutoTrigger = true;
            ManualTrigger = false;
            btnActivateSpeedControl.BackColor = Color.Yellow;
            if (TemporaryVariables.language == 0)
            {
                btnActivateSpeedControl.Text = "Chế độ tự động đang bật\r\nAutomation mode ON";
            }
            else if (TemporaryVariables.language == 1)
            {
                btnActivateSpeedControl.Text = "Chế độ tự động đang bật\r\n自动化模式：开";
            }
            else if (Settings.Default.language == 2)
            {
                btnActivateSpeedControl.Text = "Automation mode ON";
            }
            else if (Settings.Default.language == 3)
            {
                btnActivateSpeedControl.Text = "Chế độ tự động đang bật";
            }
            else if (Settings.Default.language == 4)
            {
                btnActivateSpeedControl.Text = "自动化模式：开";
            }
            btnNormalRoll.Visible = true;
            btnReverseRoll.Visible = true;
            btnResetRoll.Visible = true;
            btnStartProcess.Enabled = true;

            if (!pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("TS", "start")), Convert.ToInt32(ini.Read("TS", "bit")), 1))
            {
                pLC.WritebittoPLC(true, db, Convert.ToInt32(ini.Read("TS", "start")), Convert.ToInt32(ini.Read("TS", "bit")), 1);
            }
            if (!pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1))
            {
                pLC.WritebittoPLC(true, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1);
                btnNormalRoll.BackColor = Color.Yellow;
            }

            if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")), 1))
            {
                pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")), 1);
                btnReverseRoll.BackColor = Color.White;
            }

            if (!pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1) && !pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")), 1))
            {
                btnResetRoll.BackColor = Color.Yellow;
            }
            else
            {
                btnResetRoll.BackColor = Color.White;
            }
        }

        private void TriggerAutomationOFF()
        {
            if (timer.IsRunning)
                timer.Pause();
            ManualTrigger = true;
            AutoTrigger = false;
            btnActivateSpeedControl.BackColor = Color.White;
            if (TemporaryVariables.language == 0)
            {
                btnActivateSpeedControl.Text = "Chế độ tự động đang tắt\r\nAutomation mode OFF";
            }
            else if (TemporaryVariables.language == 1)
            {
                btnActivateSpeedControl.Text = "Chế độ tự động đang tắt\r\n自动化模式：关";
            }
            else if (Settings.Default.language == 2)
            {
                btnActivateSpeedControl.Text = "Automation mode OFF";
            }
            else if (Settings.Default.language == 3)
            {
                btnActivateSpeedControl.Text = "Chế độ tự động đang tắt";
            }
            else if (Settings.Default.language == 4)
            {
                btnActivateSpeedControl.Text = "自动化模式：关";
            }
            btnNormalRoll.Visible = false;
            btnReverseRoll.Visible = false;
            btnResetRoll.Visible = false;
        }

        private string SetTimeChange(int min, int sec = 0)
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

        private void TimerProcessTrigger(string time)
        {
            try
            {
                lbCountDown.Text = timer.TimeLeftStr;

                if (lbCountDown.Text == time)
                {
                    pLC.WriteRealtoPLC(Convert.ToSingle(speed2), db, Convert.ToInt32(ini.Read("WS", "start")), 2);
                    tempSpeed = speed2;
                }
            }
            catch (Exception ex) { SystemLog.Output(SystemLog.MSG_TYPE.Err, "Change Speed Process", ex.Message); }
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

                        if (TemporaryVariables.language == 0)
                        {
                            lbProcessNo.Text = "Thực hiện bước số : " + dt.Rows[i]["process_no"].ToString() + Environment.NewLine + "Process no: " + dt.Rows[i]["process_no"].ToString();
                        }
                        else if (TemporaryVariables.language == 1)
                        {
                            lbProcessNo.Text = "Thực hiện bước số : " + dt.Rows[i]["process_no"].ToString() + Environment.NewLine + "执行第" + dt.Rows[i]["process_no"].ToString() + "步骤";
                        }
                        else if (Settings.Default.language == 2)
                        {
                            lbProcessNo.Text = "Process no: " + dt.Rows[i]["process_no"].ToString();
                        }
                        else if (Settings.Default.language == 3)
                        {
                            lbProcessNo.Text = "Thực hiện bước số : " + dt.Rows[i]["process_no"].ToString();
                        }
                        else if (Settings.Default.language == 4)
                        {
                            lbProcessNo.Text = "执行第" + dt.Rows[i]["process_no"].ToString() + "步骤";
                        }
                        speed1 = (int)dt.Rows[i]["init_speed"];

                        time1 = (int)dt.Rows[i]["init_time"];
                        time2 = (int)dt.Rows[i]["change_time"];
                        speed2 = (int)dt.Rows[i]["change_speed"];
                        isVaccum = (bool)dt.Rows[i]["is_vaccum"];
                        max_temp = (int)dt.Rows[i]["max_temperature"];
                        isSkipAnnouce = (bool)dt.Rows[i]["is_skip_announce"];
                        timer = new CountDownTimer();
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
        }

        private void FinishProcess()
        {
            TemporaryVariables.processDT.Rows[currentRow]["is_finished"] = true;
            if (!pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1))
                pLC.WritebittoPLC(true, db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1);

            increasedTemp = 0;
            alarmTick = 1;
            isAlarmed = false;
            isSafeTemp = false;

            if (isVaccum)
            {
                if (!pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("OFFV", "start")), Convert.ToInt32(ini.Read("OFFV", "bit")), 1))
                {
                    pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("ONV", "start")), Convert.ToInt32(ini.Read("ONV", "bit")), 1);
                    pLC.WritebittoPLC(true, db, Convert.ToInt32(ini.Read("OFFV", "start")), Convert.ToInt32(ini.Read("OFFV", "bit")), 1);
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
                timer.Delete();
                timer.Dispose();
                
                if(isSkipAnnouce)
                {
                    if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1))
                        pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1);
                    startOne = false;
                }
                else
                {
                    if (TemporaryVariables.language == 0)
                    {
                        message = "Đã kết thúc bước, bấm \"OK\" để mở nắp, \"CANCEL\" để giữ nắp đóng!\r\nProcess finished, press \"OK\" to open the lid, press \"Cancel\" to left the lid stay shut!";
                        caption = "Thông tin / Information";
                    }
                    else if (TemporaryVariables.language == 1)
                    {
                        message = "Đã kết thúc bước, bấm \"OK\" để mở nắp, \"CANCEL\" để giữ nắp đóng!\r\n此步骤已经结束， 开盖 - 点击\"OK\", 继续运行 - 点击\"CANCEL\"";
                        caption = "Thông tin / 信息";
                    }
                    else if (Settings.Default.language == 2)
                    {
                        message = "Process finished, press \"OK\" to open the lid, press \"Cancel\" to left the lid stay shut!";
                        caption = "Information";
                    }
                    else if (Settings.Default.language == 3)
                    {
                        message = "Đã kết thúc bước, bấm \"OK\" để mở nắp, \"CANCEL\" để giữ nắp đóng!";
                        caption = "Thông tin";
                    }
                    else if (Settings.Default.language == 4)
                    {
                        message = "此步骤已经结束， 开盖 - 点击\"OK\", 继续运行 - 点击\"CANCEL\"";
                        caption = "信息";
                    }
                    DialogResult dialog = CTMessageBox.Show(message, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (dialog == DialogResult.OK)
                    {
                        if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1))
                            pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1);

                        if (!pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("OL", "start")), Convert.ToInt32(ini.Read("OL", "bit")), 1))
                        {
                            pLC.WritebittoPLC(true, db, Convert.ToInt32(ini.Read("OL", "start")), Convert.ToInt32(ini.Read("OL", "bit")), 1);
                        }
                        startOne = false;
                    }
                    else
                    {
                        if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1))
                            pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1);
                        startOne = false;
                    }
                }
                GetNextProcess();
            }
            else
            {
                timer.Delete();
                timer.Dispose();
                btnResetRoll.BackColor = Color.Yellow;
                if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1))
                    pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1);

                pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("TS", "start")), Convert.ToInt32(ini.Read("TS", "bit")), 1);
                if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1))
                    pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1);
                if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")), 1))
                    pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")), 1);
                pLC.WriteRealtoPLC(0, db, Convert.ToInt32(ini.Read("WS", "start")), 2);

                btnNormalRoll.BackColor = Color.White;
                btnReverseRoll.BackColor = Color.White;

                if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("ER", "start")), Convert.ToInt32(ini.Read("ER", "bit")), 1))
                    pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("ER", "start")), Convert.ToInt32(ini.Read("ER", "bit")), 1);


                if (TemporaryVariables.language == 0)
                {
                    message = "Đã hoàn thành thực hiện công thức! Vui lòng đổ liệu thủ công!\r\nFormula automation process is finished! Please take out the product manually!";
                    caption = "Thông tin / Information";
                }
                else if (TemporaryVariables.language == 1)
                {
                    message = "Đã hoàn thành thực hiện công thức! Vui lòng đổ liệu thủ công!\r\n生产完毕！请将产品从捏合机里取出！";
                    caption = "Thông tin / 信息";
                }
                else if (Settings.Default.language == 2)
                {
                    message = "Formula automation process is finished! Please take out the product manually!";
                    caption = "Information";
                }
                else if (Settings.Default.language == 3)
                {
                    message = "Đã hoàn thành thực hiện công thức! Vui lòng đổ liệu thủ công!";
                    caption = "Thông tin";
                }
                else if (Settings.Default.language == 4)
                {
                    message = "生产完毕！请将产品从捏合机里取出！";
                    caption = "信息";
                }
                CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                Program.main.openSpecTab();
            }
        }

        //Event handler
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
        private void BW_DoWork(object sender, DoWorkEventArgs e)//Bg worker load nhiệt độ
        {
            var worker = sender as BackgroundWorker;
            try
            {
                double tempRT = Convert.ToDouble(pLC.ReadRealToString(db, Convert.ToInt32(ini.Read("RT", "start"))));
                if (tempRT > 0)
                {
                    lbTemperature.Text = Math.Round(tempRT, 2).ToString();
                    double speed = Convert.ToDouble(pLC.ReadRealToString(db, Convert.ToInt32(ini.Read("RS", "start"))));
                    if (speed < 0)
                    {
                        speed = speed * (-1); // Khi chạy ngược tốc độ bị âm
                    }
                    lbRollSpeed.Text = Math.Round(speed, 2).ToString();
                    double maxTemp = Convert.ToDouble(max_temp);
                    if (increasedTemp == 0)
                        increasedTemp = maxTemp;
                    if (tempRT > increasedTemp)
                    {
                        if (alarmTick <= 60)
                        {
                            isAlarmed = true;
                            alarmTick++;
                        }
                        else
                        {
                            isAlarmed = false;
                            increasedTemp += 10;
                            alarmTick = 1;
                        }
                    }
                    else
                    {
                        isAlarmed = false;
                        if (tempRT < maxTemp)
                        {
                            isSafeTemp = true;
                        }
                        else { isSafeTemp = false; }
                    }

                    if (isAlarmed)
                    {
                        if ((tempRT - tempTemp) > 10)
                            SystemLog.Output(SystemLog.MSG_TYPE.War, "High Temperature", tempRT.ToString());
                        panelShowTemperature.BackColor = Color.Red;
                        tempTemp = tempRT;
                        if (!pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1))
                        {
                            pLC.WritebittoPLC(true, db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1);
                        }
                    }
                    else
                    {
                        if (isSafeTemp)
                            panelShowTemperature.BackColor = Color.Black;
                        else
                            panelShowTemperature.BackColor = Color.Red;
                        if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1))
                        {
                            pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1);
                        }
                    }

                    //Check automation
                    if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("AM", "start")), Convert.ToInt32(ini.Read("AM", "bit")), 1))
                    {
                        if (!AutoTrigger && ManualTrigger)
                        {
                            TriggerAutomationON();
                            if (!timer.IsRunning)
                            {
                                pLC.WriteRealtoPLC(Convert.ToSingle(tempSpeed), db, Convert.ToInt32(ini.Read("WS", "start")), 2);
                                timer.Continue();
                            }
                        }
                    }
                    else
                    {
                        if (AutoTrigger && !ManualTrigger)
                        {
                            TriggerAutomationOFF();
                            if (timer.IsRunning)
                            {
                                pLC.WriteRealtoPLC(0, db, Convert.ToInt32(ini.Read("WS", "start")), 2);
                                timer.Pause();
                            }
                        }
                    }
                }
                else
                {
                    if (ConnectionPLC != 0)
                    {
                        TryConnectToPLC();
                    }
                    else
                    {
                        if (tempRT == 0)
                        {
                            if (ConnectionPLC == 0)
                                pLC.Diconnect();
                            TryConnectToPLC();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Update temp, speed, AM error:", ex.Message);
            }
        }
        private void BW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void BW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "update UI error", ex.Message);
            }
        }

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

        private void btnReverseRoll_Click(object sender, EventArgs e)
        {
            try
            {
                if (!timer.IsRunning)
                    timer.Continue();

                pLC.WritebittoPLC(true, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")), 1);
                if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1))
                    pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1);
                pLC.WriteRealtoPLC(Convert.ToSingle(tempSpeed), db, Convert.ToInt32(ini.Read("WS", "start")), 2);
                btnReverseRoll.BackColor = Color.Yellow;
                btnNormalRoll.BackColor = Color.White;
                btnResetRoll.BackColor = Color.White;
            }
            catch (Exception ex) { SystemLog.Output(SystemLog.MSG_TYPE.Err, "Reverse Roll", ex.Message); }
        }

        private void btnResetRoll_Click(object sender, EventArgs e)
        {
            try
            {
                if (timer.IsRunning)
                    timer.Pause();
                btnResetRoll.BackColor = Color.Yellow;
                if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1))
                    pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1);

                if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")), 1))
                    pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")), 1);
                pLC.WriteRealtoPLC(0, db, Convert.ToInt32(ini.Read("WS", "start")), 2);
                btnNormalRoll.BackColor = Color.White;
                btnReverseRoll.BackColor = Color.White;
            }
            catch (Exception ex) { SystemLog.Output(SystemLog.MSG_TYPE.Err, "Stop Roll", ex.Message); }
        }

        private void btnStartProcess_Click(object sender, EventArgs e)
        {
            try
            {
                if (TemporaryVariables.language == 0)
                {
                    message = "Hãy xác nhận đã cho nguyên liệu vào máy! Bấm \"OK\" để tiến hành bước đang thể hiện!\r\nPlease confirm the material have been put in the machine! Press \"OK\" to begin process!";
                    caption = "Cảnh báo / Warning";
                }
                else if (TemporaryVariables.language == 1)
                {
                    message = "Hãy xác nhận đã cho nguyên liệu vào máy! Bấm \"OK\" để tiến hành bước đang thể hiện!\r\n请确认料已经放好！继续执行此步骤，点击 \"OK\"。";
                    caption = "Cảnh báo / 提示";
                }
                else if (Settings.Default.language == 2)
                {
                    message = "Please confirm the material have been put in the machine! Press \"OK\" to begin process!";
                    caption = "Warning";
                }
                else if (Settings.Default.language == 3)
                {
                    message = "Hãy xác nhận đã cho nguyên liệu vào máy! Bấm \"OK\" để tiến hành bước đang thể hiện!";
                    caption = "Cảnh báo";
                }
                else if (Settings.Default.language == 4)
                {
                    message = "请确认料已经放好！继续执行此步骤，点击 \"OK\"。";
                    caption = "提示";
                }
                DialogResult dialog = CTMessageBox.Show(message, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dialog == DialogResult.OK)
                {
                    pLC.WritebittoPLC(true, db, Convert.ToInt32(ini.Read("ER", "start")), Convert.ToInt32(ini.Read("ER", "bit")), 1);
                    if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("SR", "start")), Convert.ToInt32(ini.Read("SR", "bit")), 1))
                        pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("SR", "start")), Convert.ToInt32(ini.Read("SR", "bit")), 1);

                    checkTimer = new System.Windows.Forms.Timer();
                    checkTimer.Tick += checkTimer_Tick;
                    checkTimer.Interval = 1000;
                    checkTimer.Enabled = true;

                    btnStartProcess.Enabled = false;
                }
            }
            catch (Exception ex) { SystemLog.Output(SystemLog.MSG_TYPE.Err, "Start Process", ex.Message); }
        }

        private void checkTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("SSCU", "start")), Convert.ToInt32(ini.Read("SSCU", "bit")), 1) && pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("SSCL", "start")), Convert.ToInt32(ini.Read("SSCL", "bit")), 1))
                {
                    if (!startOne)
                    {
                        startOne = true;
                        checkTimer.Enabled = false;
                        checkTimer.Dispose();

                        if (!pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("TS", "start")), Convert.ToInt32(ini.Read("TS", "bit")), 1))
                            pLC.WritebittoPLC(true, db, Convert.ToInt32(ini.Read("TS", "start")), Convert.ToInt32(ini.Read("TS", "bit")), 1);

                        if (!pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1))
                        {
                            pLC.WritebittoPLC(true, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1);
                            btnNormalRoll.BackColor = Color.Yellow;
                        }

                        if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")), 1))
                        {
                            pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")), 1);
                            btnReverseRoll.BackColor = Color.White;
                        }
                        btnResetRoll.BackColor = Color.White;

                        pLC.WriteRealtoPLC(Convert.ToSingle(speed1), db, Convert.ToInt32(ini.Read("WS", "start")), 2);
                        tempSpeed = speed1;
                        int time = time1 + time2;

                        timer.SetTime(time, 0);
                        string timeChange = SetTimeChange(time2, 0);

                        if (isVaccum)
                        {
                            if (!pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("ONV", "start")), Convert.ToInt32(ini.Read("ONV", "bit")), 1))
                            {
                                pLC.WritebittoPLC(true, db, Convert.ToInt32(ini.Read("ONV", "start")), Convert.ToInt32(ini.Read("ONV", "bit")), 1);
                                pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("OFFV", "start")), Convert.ToInt32(ini.Read("OFFV", "bit")), 1);
                            }
                        }

                        timer.Start();
                        timer.TimeChanged += () => TimerProcessTrigger(timeChange);
                        timer.CountDownFinished += () => FinishProcess();
                        timer.StepMs = 1000;
                    }
                }
            }
            catch (Exception ex) { SystemLog.Output(SystemLog.MSG_TYPE.Err, "Check Timer Process", ex.Message); }
        }

        private void btnContinueStep_Click(object sender, EventArgs e)
        {
            if (TemporaryVariables.language == 0)
            {
                message = "Xác nhận bỏ qua bước đang thể hiện ?\r\nSkip current process ?";
                caption = "Cảnh báo / Warning";
            }
            else if (TemporaryVariables.language == 1)
            {
                message = "Xác nhận bỏ qua bước đang thể hiện ?\r\n确认跳过这个步骤 ?";
                caption = "Cảnh báo / 提示";
            }
            else if (Settings.Default.language == 2)
            {
                message = "Skip current process ?";
                caption = "Warning";
            }
            else if (Settings.Default.language == 3)
            {
                message = "Xác nhận bỏ qua bước đang thể hiện ?";
                caption = "Cảnh báo";
            }
            else if (Settings.Default.language == 4)
            {
                message = "确认跳过这个步骤 ?";
                caption = "提示";
            }

            DialogResult dialog = CTMessageBox.Show(message, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                ResetVariablesPLC();
                TemporaryVariables.processDT.Rows[currentRow]["is_finished"] = true;
                timer.Delete();
                timer.Dispose();

                GetNextProcess();
                increasedTemp = 0;
                alarmTick = 1;
                isAlarmed = false;
                isSafeTemp = false;
                btnStartProcess.Enabled = true;
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

            timer.Delete();
            ResetVariablesPLC();

            increasedTemp = 0;
            alarmTick = 1;
            isAlarmed = false;
            isSafeTemp = false;
            btnStartProcess.Enabled = true;

            if (ConnectionPLC == 0)
                pLC.Diconnect();
        }

        private void btnNormalRoll_Click(object sender, EventArgs e)
        {
            if (!timer.IsRunning)
                timer.Continue();
            pLC.WritebittoPLC(true, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1);
            if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")), 1))
                pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")), 1);
            pLC.WriteRealtoPLC(Convert.ToSingle(tempSpeed), db, Convert.ToInt32(ini.Read("WS", "start")), 2);
            btnNormalRoll.BackColor = Color.Yellow;
            btnReverseRoll.BackColor = Color.White;
            btnResetRoll.BackColor = Color.White;
        }
        private void mainSettingFormClosing(object sender, FormClosingEventArgs e)
        {
            ((Form)sender).FormClosing -= mainSettingFormClosing;
        }

        private void AutomationInfo_Load(object sender, EventArgs e)
        {
            if (TemporaryVariables.language == 0)
            {
                lb1.Text = "Tốc độ hiện tại:\r\nCurrent speed:";
                lb3.Text = "Nhiệt độ hiện tại:\r\nCurrent temperature:";
                lb2.Text = "(vòng/phút)\r\n(rpm)";
                lb4.Text = "(Độ C)\r\n(Celsius)";
                lb5.Text = "Thời gian còn lại đến khi kết thúc bước:\r\nTime left untill process ended:";
                lbShowF.Text = "Công thức:\r\nFormula:";

                btnStartProcess.ButtonText = "Bắt đầu thực hiện bước\r\nStart Process";
                btnNormalRoll.Text = "Quay Thuận\r\nClockwise";
                btnReverseRoll.Text = "Quay Ngược\r\nReverse Clockwise";
                btnResetRoll.Text = "Ngừng Quay\r\nStop Motor";
            }
            else if (TemporaryVariables.language == 1)
            {
                lb1.Text = "Tốc độ hiện tại:\r\n实时转速:";
                lb3.Text = "Nhiệt độ hiện tại:\r\n实时温度:";
                lb2.Text = "(vòng/phút)\r\n(转/分)";
                lb4.Text = "(Độ C)\r\n(摄氏度)";
                lb5.Text = "Thời gian còn lại đến khi kết thúc bước:\r\n时间倒计时致此步骤结束:";
                lbShowF.Text = "Công thức:\r\n型号:";

                btnStartProcess.ButtonText = "Bắt đầu thực hiện bước\r\n开始执行此步骤";
                btnNormalRoll.Text = "Quay Thuận\r\n顺转";
                btnReverseRoll.Text = "Quay Ngược\r\n逆转";
                btnResetRoll.Text = "Ngừng Quay\r\n停止旋转";
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
            else if (Settings.Default.language == 3)
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
            else if (Settings.Default.language == 4)
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
            lbFormulaName.Text = TemporaryVariables.tempFileName;
            TryConnectToPLC();


            GetNextProcess();
            LoadBackgroundWorker();
            try
            {
                if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("AM", "start")), Convert.ToInt32(ini.Read("AM", "bit")), 1))
                {
                    TriggerAutomationON();
                }
                else
                {
                    TriggerAutomationOFF();
                }
                
                tmrCallBgWorker.Interval = 1000; //3600000;
                tmrCallBgWorker.Start();
            }
            catch (Exception ex)
            {
                if (TemporaryVariables.language == 0)
                {
                    message = "Kết nối với PLC không thành công!\r\nFail to connect to PLC! : " + ex.Message;
                    caption = "Thông tin / Information";
                }
                else if (TemporaryVariables.language == 1)
                {
                    message = "Kết nối với PLC không thành công!\r\n连接 PLC 失败！" + ex.Message;
                    caption = "Thông tin / 信息";
                }
                else if (Settings.Default.language == 2)
                {
                    message = "Fail to connect to PLC!" + ex.Message;
                    caption = "Information";
                }
                else if (Settings.Default.language == 3)
                {
                    message = "Kết nối với PLC không thành công!" + ex.Message;
                    caption = "Thông tin";
                }
                else if (Settings.Default.language == 4)
                {
                    message = "连接 PLC 失败！" + ex.Message;
                    caption = "信息";
                }
                CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
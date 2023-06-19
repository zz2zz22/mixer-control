using mixer_control_globalver.Controller;
using mixer_control_globalver.Controller.IniFile;
using mixer_control_globalver.Model.PLC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mixer_control_globalver.Properties;
using mixer_control_globalver.View.CustomComponent;
using System.Reflection.Emit;
using static System.Net.Mime.MediaTypeNames;
using mixer_control_globalver.View.SideUI;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using mixer_control_globalver.Controller.LogFile;

namespace mixer_control_globalver.View.MainUI
{
    public partial class AutomationInfo : Form
    {
        public static int ConnectionPLC;
        public static PLCConnector pLC;
        Timer runTimer, checkTimer;
        CountDownTimer timer;
        double tempSpeed;
        bool startOne = false;
        bool isStopping = false;
        string message = String.Empty, caption = String.Empty;
        double tempTemp;
        int db, currentRow, speed1, time1, speed2, time2, max_temp;
        bool isVaccum;
        IniFile ini = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "\\data\\setting.ini");
        public AutomationInfo()
        {
            InitializeComponent();
        }
        private void btnReverseRoll_Click(object sender, EventArgs e)
        {
            try
            {
                pLC.WritebittoPLC(true, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")), 1);
                if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1))
                    pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1);
                pLC.WriteRealtoPLC(Convert.ToSingle(tempSpeed), db, Convert.ToInt32(ini.Read("WS", "start")), 2);
                btnReverseRoll.BackColor = Color.Yellow;
                btnNormalRoll.BackColor = Color.White;
                btnResetRoll.BackColor = Color.White;
                if (timer != null)
                {
                    if (!timer.IsRunnign)
                        timer.Start();
                }
            }
            catch (Exception ex) { SystemLog.Output(SystemLog.MSG_TYPE.Err, "Reverse Roll", ex.Message); }
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

        private void btnResetRoll_Click(object sender, EventArgs e)
        {
            try
            {
                btnResetRoll.BackColor = Color.Yellow;
                if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1))
                    pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1);

                if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")), 1))
                    pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")), 1);
                pLC.WriteRealtoPLC(0, db, Convert.ToInt32(ini.Read("WS", "start")), 2);
                btnNormalRoll.BackColor = Color.White;
                btnReverseRoll.BackColor = Color.White;

                if (timer != null)
                {
                    if (timer.IsRunnign)
                        timer.Pause();
                }
            }
            catch(Exception ex) { SystemLog.Output(SystemLog.MSG_TYPE.Err, "Stop Roll", ex.Message); }
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
                DialogResult dialog = MessageBox.Show(message, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dialog == DialogResult.OK)
                {
                    pLC.WritebittoPLC(true, db, Convert.ToInt32(ini.Read("ER", "start")), Convert.ToInt32(ini.Read("ER", "bit")), 1);
                    if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("SR", "start")), Convert.ToInt32(ini.Read("SR", "bit")), 1))
                        pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("SR", "start")), Convert.ToInt32(ini.Read("SR", "bit")), 1);

                    checkTimer = new Timer();
                    checkTimer.Tick += checkTimer_Tick;
                    checkTimer.Interval = 1000;
                    checkTimer.Enabled = true;

                    btnStartProcess.Enabled = false;
                }
            }
            catch(Exception ex) { SystemLog.Output(SystemLog.MSG_TYPE.Err, "Start Process", ex.Message); }
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
                        timer = new CountDownTimer();
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
                        timer.StepMs = 77;
                    }
                }
            }
            catch(Exception ex) { SystemLog.Output(SystemLog.MSG_TYPE.Err, "Check Timer Process", ex.Message); }
        }

        private void TimerProcessTrigger(string time)
        {
            try
            {
                lbCountDown.Text = timer.TimeLeftStr;
                if (time != null)
                {
                    if (lbCountDown.Text == time)
                    {
                        pLC.WriteRealtoPLC(Convert.ToSingle(speed2), db, Convert.ToInt32(ini.Read("WS", "start")), 2);
                        tempSpeed = speed2;
                    }
                }
            }
            catch(Exception ex) { SystemLog.Output(SystemLog.MSG_TYPE.Err, "Change Speed Process", ex.Message); }
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
            DialogResult dialog = MessageBox.Show(message, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                TemporaryVariables.processDT.Rows[currentRow]["is_finished"] = true;
                GetNextProcess();
                if (timer != null)
                {
                    timer.Stop();
                    timer.Disable();
                    timer.Dispose();
                }
                if (runTimer != null)
                {
                    runTimer.Enabled = false;
                    runTimer.Dispose();
                }
                btnStartProcess.Enabled = true;
            }
        }

        private void FinishProcess()
        {
            TemporaryVariables.processDT.Rows[currentRow]["is_finished"] = true;
            if (!pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1))
                pLC.WritebittoPLC(true, db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1);
            startOne = false;
            if (timer.IsRunnign)
                timer.Disable();

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
                DialogResult dialog = MessageBox.Show(message, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dialog == DialogResult.OK)
                {
                    if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1))
                        pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1);

                    if (!pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("OL", "start")), Convert.ToInt32(ini.Read("OL", "bit")), 1))
                    {
                        pLC.WritebittoPLC(true, db, Convert.ToInt32(ini.Read("OL", "start")), Convert.ToInt32(ini.Read("OL", "bit")), 1);
                    }
                }
                else
                {
                    if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1))
                        pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1);
                }
                GetNextProcess();
            }
            else
            {
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
                DialogResult dialog = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (dialog == DialogResult.OK)
                {
                    if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1))
                        pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1);
                }
                else
                {
                    if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1))
                        pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1);
                }
                btnResetRoll.BackColor = Color.Yellow;


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
                if (timer != null)
                {
                    timer.Stop();
                    timer.Disable();
                    timer.Dispose();
                }
                if (runTimer != null)
                {
                    runTimer.Enabled = false;
                    runTimer.Dispose();
                }

                Program.main.openSpecTab();
            }

        }

        private void AutomationInfo_FormClosing(object sender, FormClosingEventArgs e)
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

            if (timer != null)
            {
                timer.Stop();
                timer.Disable();
                timer.Dispose();
            }
            if (runTimer != null)
            {
                runTimer.Enabled = false;
                runTimer.Dispose();
            }
            btnStartProcess.Enabled = true;
        }

        private void btnNormalRoll_Click(object sender, EventArgs e)
        {
            pLC.WritebittoPLC(true, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1);
            if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")), 1))
                pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")), 1);
            pLC.WriteRealtoPLC(Convert.ToSingle(tempSpeed), db, Convert.ToInt32(ini.Read("WS", "start")), 2);
            btnNormalRoll.BackColor = Color.Yellow;
            btnReverseRoll.BackColor = Color.White;
            btnResetRoll.BackColor = Color.White;
            if (timer != null)
            {
                if (!timer.IsRunnign)
                    timer.Start();
            }
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
            lbFormulaName.Text = TemporaryVariables.tempFileName;

            pLC = new PLCConnector(Settings.Default.plc_ip, 0, 0, out ConnectionPLC);
            db = Settings.Default.database_no;

            pLC.WriteRealtoPLC(Convert.ToSingle(Settings.Default.max_speed), db, Convert.ToInt32(ini.Read("MS", "start")), 2);
            pLC.WriteRealtoPLC(Convert.ToSingle(Settings.Default.spindle_diameter), db, Convert.ToInt32(ini.Read("SD", "start")), 2);
            pLC.WriteRealtoPLC(Convert.ToSingle(Settings.Default.sensor_diameter), db, Convert.ToInt32(ini.Read("SSD", "start")), 2);
            pLC.WriteRealtoPLC(Convert.ToSingle(Settings.Default.transmission_ratio), db, Convert.ToInt32(ini.Read("TRMS", "start")), 2);
            GetNextProcess();

            runTimer = new Timer();
            runTimer.Tick += runTimer_Tick;
            runTimer.Interval = 1000;
            runTimer.Enabled = true;

            if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("AM", "start")), Convert.ToInt32(ini.Read("AM", "bit")), 1))
            {
                isStopping = true;
                btnActivateSpeedControl.BackColor = Color.Yellow;
                if (TemporaryVariables.language == 0)
                {
                    btnActivateSpeedControl.Text = "Chế độ tự động đang bật\r\nAutomation mode ON";
                }
                else if (TemporaryVariables.language == 1)
                {
                    btnActivateSpeedControl.Text = "Chế độ tự động đang bật\r\n自动化模式：开";
                }
                btnNormalRoll.Visible = true;
                btnReverseRoll.Visible = true;
                btnResetRoll.Visible = true;
                btnStartProcess.Enabled = true;

                if (!pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("TS", "start")), Convert.ToInt32(ini.Read("TS", "bit")), 1))
                {
                    pLC.WritebittoPLC(true, db, Convert.ToInt32(ini.Read("TS", "start")), Convert.ToInt32(ini.Read("TS", "bit")), 1);
                }
                if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1))
                {
                    pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1);
                    btnNormalRoll.BackColor = Color.White;
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
            else
            {
                isStopping = false;
                btnActivateSpeedControl.BackColor = Color.White;
                if (TemporaryVariables.language == 0)
                {
                    btnActivateSpeedControl.Text = "Chế độ tự động đang tắt\r\nAutomation mode OFF";
                }
                else if (TemporaryVariables.language == 1)
                {
                    btnActivateSpeedControl.Text = "Chế độ tự động đang tắt\r\n自动化模式：关";
                }
                btnNormalRoll.Visible = false;
                btnReverseRoll.Visible = false;
                btnResetRoll.Visible = false;
            }
        }
        //Timer dùng để load nhiệt độ và tốc độ từ PLc theo giây
        private void runTimer_Tick(object sender, EventArgs e)
        {
            double tempRT = Convert.ToDouble(pLC.ReadRealToString(db, Convert.ToInt32(ini.Read("RT", "start"))));
            //if (tempRT > 0)
            //{
            //    tempRT = tempRT + 18.8;
            //}
            lbTemperature.Text = Math.Round(tempRT, 2).ToString();
            double speed = Convert.ToDouble(pLC.ReadRealToString(db, Convert.ToInt32(ini.Read("RS", "start"))));
            if (speed < 0)
            {
                speed = speed * (-1); // Khi chạy ngược tốc độ bị âm
            }
            lbRollSpeed.Text = Math.Round(speed, 2).ToString();
            double maxTemp = Convert.ToDouble(max_temp);
            if (tempRT > maxTemp)
            {
                if ((tempRT - tempTemp) > 5)
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
                panelShowTemperature.BackColor = Color.Black;
                if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1))
                {
                    pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1);
                }
            }
            if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("AM", "start")), Convert.ToInt32(ini.Read("AM", "bit")), 1))
            {
                if (!isStopping)
                {
                    isStopping = true;
                    btnActivateSpeedControl.BackColor = Color.Yellow;
                    if (TemporaryVariables.language == 0)
                    {
                        btnActivateSpeedControl.Text = "Chế độ tự động đang bật\r\nAutomation mode ON";
                    }
                    else if (TemporaryVariables.language == 1)
                    {
                        btnActivateSpeedControl.Text = "Chế độ tự động đang bật\r\n自动化模式：开";
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
                    if (timer != null)
                    {
                        if (!timer.IsRunnign)
                            timer.Start();
                    }
                }
            }
            else
            {
                isStopping = false;
                btnActivateSpeedControl.BackColor = Color.White;
                if (TemporaryVariables.language == 0)
                {
                    btnActivateSpeedControl.Text = "Chế độ tự động đang tắt\r\nAutomation mode OFF";
                }
                else if (TemporaryVariables.language == 1)
                {
                    btnActivateSpeedControl.Text = "Chế độ tự động đang tắt\r\n自动化模式：关";
                }
                btnNormalRoll.Visible = false;
                btnReverseRoll.Visible = false;
                btnResetRoll.Visible = false;
                if (timer != null)
                {
                    if (timer.IsRunnign)
                        timer.Pause();
                }
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

                        if (TemporaryVariables.language == 0)
                        {
                            lbProcessNo.Text = "Thực hiện bước số : " + dt.Rows[i]["process_no"].ToString() + Environment.NewLine + "Process no: " + dt.Rows[i]["process_no"].ToString();
                        }
                        else if (TemporaryVariables.language == 1)
                        {
                            lbProcessNo.Text = "Thực hiện bước số : " + dt.Rows[i]["process_no"].ToString() + Environment.NewLine + "执行第" + dt.Rows[i]["process_no"].ToString() + "步骤";
                        }
                        speed1 = (int)dt.Rows[i]["init_speed"];

                        time1 = (int)dt.Rows[i]["init_time"];
                        time2 = (int)dt.Rows[i]["change_time"];
                        speed2 = (int)dt.Rows[i]["change_speed"];
                        isVaccum = (bool)dt.Rows[i]["is_vaccum"];
                        max_temp = (int)dt.Rows[i]["max_temperature"];
                        break;
                    }
                }
            }
        }
    }
}
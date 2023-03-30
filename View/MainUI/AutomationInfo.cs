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

namespace mixer_control_globalver.View.MainUI
{
    public partial class AutomationInfo : Form
    {
        public static int ConnectionPLC;
        public static PLCConnector pLC;
        Timer runTimer;
        CountDownTimer timer;
        string tempSpeed;
        int db, currentRow, speed1, time1, speed2, time2, max_temp;
        bool isVaccum;
        IniFile ini = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "\\data\\setting.ini");
        public AutomationInfo()
        {
            InitializeComponent();
        }
        private void btnReverseRoll_Click(object sender, EventArgs e)
        {
            if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("TS", "start")), Convert.ToInt32(ini.Read("TS", "bit")), 1))
            {
                pLC.WritebittoPLC(true, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")), 1);
                if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1))
                    pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1);
            }
            btnReverseRoll.BackColor = Color.Yellow;
            btnNormalRoll.BackColor = Color.White;
            btnResetRoll.BackColor = Color.White;
            timer.Start();
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
            btnResetRoll.BackColor = Color.Yellow;
            if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("TS", "start")), Convert.ToInt32(ini.Read("TS", "bit")), 1))
            {
                if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1))
                    pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1);

                if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")), 1))
                    pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")), 1);
            }
            btnNormalRoll.BackColor = Color.White;
            btnReverseRoll.BackColor = Color.White;

            timer.Pause();
        }

        private void btnStartProcess_Click(object sender, EventArgs e)
        {

            DialogResult dialog = MessageBox.Show("Xác nhận đã cho ... vào máy ? Bấm \"OK\" để tiến hành bước đang thể hiện!", "Cảnh báo / Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialog == DialogResult.OK)
            {
                pLC.WritebittoPLC(true, db, Convert.ToInt32(ini.Read("ER", "start")), Convert.ToInt32(ini.Read("ER", "bit")), 1);
                if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("SR", "start")), Convert.ToInt32(ini.Read("SR", "bit")), 1))
                    pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("SR", "start")), Convert.ToInt32(ini.Read("SR", "bit")), 1);

                btnStartProcess.Enabled = false;

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

                int time = time1 + time2;
                timer = new CountDownTimer();
                timer.SetTime(time, 0);
                string timeChange = SetTimeChange(time2, 0);

                if (isVaccum)
                {
                    if (!pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("TV", "start")), Convert.ToInt32(ini.Read("TV", "bit")), 1))
                        pLC.WritebittoPLC(true, db, Convert.ToInt32(ini.Read("TV", "start")), Convert.ToInt32(ini.Read("TV", "bit")), 1);
                }

                timer.Start();
                timer.TimeChanged += () => TimerProcessTrigger(timeChange);
                timer.CountDownFinished += () => FinishProcess();
                timer.StepMs = 77;
            }
        }

        private void TimerProcessTrigger(string time)
        {
            lbCountDown.Text = timer.TimeLeftStr;
            if (time != null)
            {
                if (lbCountDown.Text == time)
                {
                    pLC.WriteRealtoPLC(Convert.ToSingle(speed2), db, Convert.ToInt32(ini.Read("WS", "start")), 2);
                }
            }
        }

        private void FinishProcess()
        {
            if (!pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1))
                pLC.WritebittoPLC(true, db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")), 1);

            if (timer.IsRunnign)
                timer.Disable();

            if (isVaccum)
            {
                if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("TV", "start")), Convert.ToInt32(ini.Read("TV", "bit")), 1))
                    pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("TV", "start")), Convert.ToInt32(ini.Read("TV", "bit")), 1);
            }

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
                TemporaryVariables.processDT.Rows[currentRow]["is_finished"] = true;
                DialogResult dialog = MessageBox.Show("Đã kết thúc bước, bấm \"OK\" để mở nắp, \"CANCEL\" để giữ nắp đóng!", "Thông tin / Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
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
                TemporaryVariables.processDT.Rows[currentRow]["is_finished"] = true;
                DialogResult dialog = MessageBox.Show("Đã hoàn thành thực hiện công thức! Vui lòng đổ liệu thủ công!" + Environment.NewLine + "Formula automation process is finished! Please take out the product manually!", "Thông tin / Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("TS", "start")), Convert.ToInt32(ini.Read("TS", "bit")), 1))
                {
                    if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1))
                        pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1);
                    if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")), 1))
                        pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")), 1);
                }

                btnNormalRoll.BackColor = Color.White;
                btnReverseRoll.BackColor = Color.White;

                if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("ER", "start")), Convert.ToInt32(ini.Read("ER", "bit")), 1))
                    pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("ER", "start")), Convert.ToInt32(ini.Read("ER", "bit")), 1);
                if (timer != null)
                {
                    timer.Disable();
                    timer.Dispose();
                }
                if (runTimer != null)
                {
                    runTimer.Enabled = false;
                    runTimer.Dispose();
                }

                AppIntro.main.openSpecTab();
            }
        }

        private void btnNormalRoll_Click(object sender, EventArgs e)
        {
            if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("TS", "start")), Convert.ToInt32(ini.Read("TS", "bit")), 1))
            {
                pLC.WritebittoPLC(true, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")), 1);
                if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")), 1))
                    pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")), 1);
            }
            btnNormalRoll.BackColor = Color.Yellow;
            btnReverseRoll.BackColor = Color.White;
            btnResetRoll.BackColor = Color.White;
            timer.Start();
        }

        private void btnActivateSpeedControl_Click(object sender, EventArgs e)
        {
            if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("TS", "start")), Convert.ToInt32(ini.Read("TS", "bit")), 1))
            {
                pLC.WritebittoPLC(false, db, Convert.ToInt32(ini.Read("TS", "start")), Convert.ToInt32(ini.Read("TS", "bit")), 1);

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
                btnResetRoll.BackColor = Color.Yellow;
                btnActivateSpeedControl.BackColor = Color.White;

                btnActivateSpeedControl.Text = "Chế độ tự động đang tắt" + Environment.NewLine + "Automation mode OFF";
                btnNormalRoll.Visible = false;
                btnReverseRoll.Visible = false;
            }
            else
            {
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

                btnActivateSpeedControl.BackColor = Color.Yellow;
                btnActivateSpeedControl.Text = "Chế độ tự động đang bật" + Environment.NewLine + "Automation mode ON";
                btnNormalRoll.Visible = true;
                btnNormalRoll.BackColor = Color.Yellow;
                btnReverseRoll.BackColor = Color.White;
                btnResetRoll.BackColor = Color.White;
                btnReverseRoll.Visible = true;
            }
        }

        private void mainSettingFormClosing(object sender, FormClosingEventArgs e)
        {
            ((Form)sender).FormClosing -= mainSettingFormClosing;
        }

        private void AutomationInfo_Load(object sender, EventArgs e)
        {
            btnStartProcess.ButtonText = "Bắt đầu thực hiện bước" + Environment.NewLine + "Start Process";

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


            if (pLC.ReadBitToBool(db, Convert.ToInt32(ini.Read("TS", "start")), Convert.ToInt32(ini.Read("TS", "bit")), 1))
            {
                btnActivateSpeedControl.BackColor = Color.Yellow;
                btnActivateSpeedControl.Text = "Chế độ tự động đang bật" + Environment.NewLine + "Automation mode ON";
                btnNormalRoll.Visible = true;
                btnReverseRoll.Visible = true;
                btnResetRoll.Visible = true;
            }
            else
            {
                btnActivateSpeedControl.BackColor = Color.White;
                btnActivateSpeedControl.Text = "Chế độ tự động đang tắt" + Environment.NewLine + "Automation mode OFF";
                btnNormalRoll.Visible = false;
                btnReverseRoll.Visible = false;
                btnResetRoll.Visible = false;
            }
        }

        private void runTimer_Tick(object sender, EventArgs e)
        {
            lbTemperature.Text = pLC.ReadRealToString(db, Convert.ToInt32(ini.Read("RT", "start")));
            double speed = Convert.ToDouble(pLC.ReadRealToString(db, Convert.ToInt32(ini.Read("RS", "start"))));
            if (speed < 0)
            {
                speed = speed * (-1);
            }
            lbRollSpeed.Text = Math.Round(speed, 2).ToString();
            double tempRT = Convert.ToDouble(pLC.ReadRealToString(db, Convert.ToInt32(ini.Read("RT", "start"))));
            double maxTemp = Convert.ToDouble(max_temp);
            if (tempRT > maxTemp)
            {
                panelShowTemperature.BackColor = Color.Red;
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

                        lbProcessNo.Text = "Thực hiện bước số : " + dt.Rows[i]["process_no"].ToString() + Environment.NewLine + "Process no: " + dt.Rows[i]["process_no"].ToString();
                        speed1 = (int)dt.Rows[i]["init_speed"];
                        time1 = (int)dt.Rows[i]["init_time"];
                        time2 = (int)dt.Rows[i]["change_speed"];
                        speed2 = (int)dt.Rows[i]["change_time"];
                        isVaccum = (bool)dt.Rows[i]["is_vaccum"];
                        max_temp = (int)dt.Rows[i]["max_temperature"];
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Công thức không có bước thực hiện nào!");
                this.Close();
            }
        }
    }
}
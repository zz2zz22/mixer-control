using ClosedXML.Excel;
using mixer_control_globalver.Controller;
using mixer_control_globalver.Controller.Device;
using mixer_control_globalver.Controller.IniFile;
using mixer_control_globalver.Controller.LogFile;
using mixer_control_globalver.Controller.PLC;
using mixer_control_globalver.Model.PLC;
using mixer_control_globalver.Properties;
using mixer_control_globalver.View.CustomComponent;
using mixer_control_globalver.View.CustomControls;
using mixer_control_globalver.View.SideUI;
using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Color = System.Drawing.Color;

namespace mixer_control_globalver.View.MainUI
{
    public partial class AutomationInfo : Form
    {
        /// <summary>
        /// FIELDS
        /// </summary>
        #region Fields
        public static int ConnectionPLC, countTimeOutOil, countTimeOutOil2;
        public static bool isAuthorSkip;

        private int bytesRead;
        private byte[] buffer;

        CountDownTimer countDownTimer;
        bool isExitApplication = false;
        bool AutoManual, ContainerUpSensor, CloseLidSensor, isFirstStart, isSpeedChanged, AutoTrigger, ManualTrigger;
        string message = String.Empty, caption = String.Empty, oilType = String.Empty, oilType2 = String.Empty, stepDesc, powderBefore, powderAfter;
        double oilMass, oilWeight, oilMass2, oilWeight2, tempRT, maxTemp, speed, tempSpeed;
        int db, currentRow, speed1, time1, speed2, time2, max_temp, rollMode = 1, processNumber, errorCode, totalPowder, remainPowder, tick, totalPowderInFormula;
        bool isVaccum, isSkipAnnouce, isOilFeed, isOilFeed2, isOilFeeding, isOilFeeding2, isSendOilMass, isSendOilMass2;
        bool isCompleteOIlSupply = false;
        bool isCompleteFirstOilSup = false;

        private int oilSupplyAttemps = 30;

        double realMass = 0, initMass = 0, finalMass = 0, allowMass = 0;

        bool isAutomationON;

        IniFileGenerator ini = new IniFileGenerator(AppDomain.CurrentDomain.BaseDirectory + "\\data\\setting.ini");

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
            if (SettingsManager.GetSetting(s => s.OilSupplyEnabled))
            {
                LoadConnection2SerialPort(1);
                LoadConnection2SerialPort(2);
            }

            if (SettingsManager.GetSetting(s => s.Language) == 0)
            {
                lb1.Text = "Tốc độ hiện tại:";
                lb3.Text = "Nhiệt độ hiện tại:";
                lb2.Text = "(vòng/phút)";
                lb4.Text = "(Độ C)";
                lbShowF.Text = "Công thức:";

                btnStartProcess.ButtonText = "Bắt đầu thực hiện bước";
                btnNormalRoll.Text = "Quay Thuận";
                btnReverseRoll.Text = "Quay Ngược";
                btnResetRoll.Text = "Ngừng Quay";
            }
            else if (SettingsManager.GetSetting(s => s.Language) == 1)
            {
                lb1.Text = "实时转速:";
                lb3.Text = "实时温度:";
                lb2.Text = "(转/分)";
                lb4.Text = "(摄氏度)";
                lbShowF.Text = "型号:";

                btnStartProcess.ButtonText = "开始执行此步骤";
                btnNormalRoll.Text = "顺转";
                btnReverseRoll.Text = "逆转";
                btnResetRoll.Text = "停止旋转";
            }
            else if (SettingsManager.GetSetting(s => s.Language) == 2)
            {
                lb1.Text = "Current speed:";
                lb3.Text = "Current temperature:";
                lb2.Text = "(rpm)";
                lb4.Text = "(Celsius)";
                lbShowF.Text = "Formula:";

                btnStartProcess.ButtonText = "Start Process";
                btnNormalRoll.Text = "Clockwise";
                btnReverseRoll.Text = "Reverse Clockwise";
                btnResetRoll.Text = "Stop Motor";
            }

            if (SettingsManager.GetSetting(s => s.SaveReportEnabled))
            {
                //Generate new report file name 
                if (SettingsManager.GetSetting(s => s.EndReportEnabled))
                {
                    string path = String.Empty;
                    if (!String.IsNullOrEmpty(SettingsManager.GetSetting(s => s.ReportDirectory)))
                    {
                        path = SettingsManager.GetSetting(s => s.ReportDirectory);
                    }
                    else
                    {
                        path = AppDomain.CurrentDomain.BaseDirectory + "Mixer_Reports";
                        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
                        if (dir.Exists == false)
                            dir.Create();
                    }
                    string reportFileName = SubMethods.ReturnCleanASCII(TemporaryVariables.tempFileName) + "_formula_report_" + DateTime.UtcNow.ToString("ddMMyyyy_HHmmss") + ".xlsx";
                    TemporaryVariables.tempReportPath = Path.Combine(path, reportFileName);
                    Reports mixerReport = new Reports();
                    mixerReport.ExportExcelMixerReport(TemporaryVariables.tempReportPath, TemporaryVariables.materialDT);
                    //Generate new Excel file with material sheet & proccess sheet
                    SettingsManager.UpdateSettings(s => s.EndReportEnabled = false);
                    SettingsManager.SaveSettings();
                }
            }
            lbFormulaName.Text = TemporaryVariables.tempFileName;

            isFirstStart = false;
            isSpeedChanged = false;

            isAuthorSkip = false;

            TryConnectToPLC();

            GetNextProcess();

            LoadBackgroundWorker();
        }

        private void lbProcessNo_Click(object sender, EventArgs e)
        {
            rtbRemark.Text = stepDesc;
        }

        private void lbFormulaName_Click(object sender, EventArgs e)
        {
            if (SettingsManager.GetSetting(s => s.ShowHiddenInfo))
            {
                StringBuilder infoText = new StringBuilder();
                infoText.Append("Tốc độ cài đặt: " + speed1 + " --> " + speed2 + "\r\n");
                infoText.Append("Thời gian cài đặt: " + time1 + " --> " + time2 + "\r\n");
                infoText.Append("Nhiệt độ tối đa: " + maxTemp + "\r\n");
                infoText.Append("Hút chân không: " + (isVaccum ? "Có" : "Không") + "\r\n");
                infoText.Append("Bỏ qua thông báo : " + (isSkipAnnouce ? "Có" : "Không") + "\r\n");
                infoText.Append("Cấp dầu: " + (isOilFeed ? "Có" : "Không") + "\r\n");
                infoText.Append("Khối lượng dầu(L): " + oilMass + "\r\n");
                infoText.Append("Trọng lượng dầu(KG): " + oilWeight + "\r\n");
                infoText.Append("Loại dầu: " + oilType + "\r\n");
                rtbRemark.Text = infoText.ToString();
            }
        }
        private byte[] BuildReadHoldingRegisterFrame(byte slaveAddr, byte function, ushort startAddr, ushort numRegs)
        {
            byte[] frame = new byte[8];
            frame[0] = slaveAddr;
            frame[1] = function;
            frame[2] = (byte)(startAddr >> 8);
            frame[3] = (byte)(startAddr & 0xFF);
            frame[4] = (byte)(numRegs >> 8);
            frame[5] = (byte)(numRegs & 0xFF);

            ushort crc = CalculateCRC(frame, 6);
            frame[6] = (byte)(crc & 0xFF);
            frame[7] = (byte)(crc >> 8);
            return frame;
        }

        private ushort CalculateCRC(byte[] data, int length)
        {
            ushort crc = 0xFFFF;

            for (int pos = 0; pos < length; pos++)
            {
                crc ^= data[pos];
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x0001) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0xA001;
                    }
                    else
                        crc >>= 1;
                }
            }

            return crc;
        }
        private void serialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int bytesToRead = serialPort2.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                serialPort2.Read(buffer, 0, bytesToRead);

                string hex = BitConverter.ToString(buffer);

                if (buffer.Length >= 17 && buffer[0] == 0x01 && buffer[1] == 0x03)
                {
                    this.Invoke(new Action(() =>
                    {
                        byte[] bytes1 = new byte[4];
                        byte[] bytes2 = new byte[4];
                        byte[] bytes3 = new byte[4];

                        for (int i = 0; i < 2; i++)
                        {
                            bytes1[i * 2] = buffer[i * 2 + 4];
                            bytes1[i * 2 + 1] = buffer[i * 2 + 3];

                            bytes2[i * 2] = buffer[i * 2 + 8];
                            bytes2[i * 2 + 1] = buffer[i * 2 + 7];

                            bytes3[i * 2] = buffer[i * 2 + 12];
                            bytes3[i * 2 + 1] = buffer[i * 2 + 11];
                        }

                        float flowrate = BitConverter.ToSingle(bytes1.ToArray(), 0);
                        //txtFlowrate.Text = flowrate.ToString();

                        float totalHundred = BitConverter.ToSingle(bytes2.ToArray(), 0);
                        float totalDecimal = BitConverter.ToSingle(bytes3.ToArray(), 0);
                        float result = totalHundred * 100 + totalDecimal;
                        realMass = result;

                        //txtDataRead.Clear();
                        //foreach (byte b in buffer)
                        //{
                        //    txtDataRead.Text += b.ToString() + " ";
                        //}
                    }));
                }
            }
            catch (Exception)
            {
                //this.Invoke(new Action(() =>
                //{
                //    txtDataRead.AppendText("Lỗi đọc: " + ex.Message + Environment.NewLine);
                //}));
            }
        }

        private void StartAutomationProcess()
        {
            try
            {
                if (isAutomationON)
                {
                    if (SettingsManager.GetSetting(s => s.Language) == 0)
                    {
                        message = "Hãy xác nhận đã cho nguyên liệu vào máy! Bấm \"OK\" để tiến hành bước đang thể hiện!";
                        caption = "Cảnh báo";
                    }
                    else if (SettingsManager.GetSetting(s => s.Language) == 1)
                    {
                        message = "请确认料已经放好！继续执行此步骤，点击 \"OK\"。";
                        caption = "提示";
                    }
                    else if (SettingsManager.GetSetting(s => s.Language) == 2)
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
                            PLCConnector pLC = new PLCConnector(SettingsManager.GetSetting(s => s.PlcIp), 0, 0, out ConnectionPLC);
                            if (ConnectionPLC == 0)
                            {
                                if (SettingsManager.GetSetting(s => s.AlwaysOpenMixerLid))
                                {
                                    pLC.WriteBoolToPLC(true, db, Convert.ToInt32(ini.Read("OLM", "start")), Convert.ToInt32(ini.Read("OLM", "bit")));
                                }
                                else
                                {
                                    pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("OLM", "start")), Convert.ToInt32(ini.Read("OLM", "bit")));
                                }

                                pLC.WriteBoolToPLC(true, db, Convert.ToInt32(ini.Read("ER", "start")), Convert.ToInt32(ini.Read("ER", "bit")));
                                pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("SR", "start")), Convert.ToInt32(ini.Read("SR", "bit")));

                                isStartSuccess = true;
                                if (SettingsManager.GetSetting(s => s.SaveReportEnabled))
                                {
                                    XLWorkbook workbook = new XLWorkbook(TemporaryVariables.tempReportPath);
                                    var reportSheet = workbook.Worksheet(1);

                                    string speedReport;
                                    int row = 8 + processNumber;
                                    reportSheet.Range("F" + row).Value = processNumber;
                                    reportSheet.Range("G" + row).Value = rtbRemark.Text.Trim();
                                    DateTime dateStartAuto = DateTime.UtcNow;
                                    reportSheet.Range("H" + row).Value = dateStartAuto;
                                    reportSheet.Range("I" + row).Value = dateStartAuto;
                                    reportSheet.Range("L" + row).Value = isVaccum ? "YES" : "NO";
                                    reportSheet.Range("M" + row).Value = isOilFeed ? "YES" : "NO";
                                    if (isOilFeed && SettingsManager.GetSetting(s => s.OilSupplyEnabled))
                                    {
                                        reportSheet.Range("N" + row).Value = oilType;
                                        reportSheet.Range("O" + row).Value = oilMass.ToString();
                                    }
                                    if (speed2 != 0)
                                        speedReport = speed1 + "-" + speed2;
                                    else
                                        speedReport = speed1.ToString();
                                    reportSheet.Range("T" + row).Value = speedReport;

                                    workbook.Save();
                                }
                            }
                        } while (isStartSuccess == false);

                        isFirstStart = true;
                        countTimeOutOil = 0;
                        countTimeOutOil2 = 0;
                        btnNormalRoll.Enabled = true;
                        btnReverseRoll.Enabled = true;
                        btnResetRoll.Enabled = true;
                        btnStartProcess.Enabled = false;
                    }
                }
                else
                {
                    if (SettingsManager.GetSetting(s => s.Language) == 0)
                    {
                        message = "Tự động hoá đang tắt không thể bắt đầu!";
                        caption = "Cảnh báo";
                    }
                    else if (SettingsManager.GetSetting(s => s.Language) == 1)
                    {
                        message = "自动化已关闭且无法启动！";
                        caption = "提示";
                    }
                    else if (SettingsManager.GetSetting(s => s.Language) == 2)
                    {
                        message = "Automation is off and cannot be started!";
                        caption = "Warning";
                    }
                    CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex) { SystemLog.Output(SystemLog.MSG_TYPE.Err, "Start Process", ex.Message); }

        }

        private void btnStartProcess_Click(object sender, EventArgs e)
        {
            if (SettingsManager.GetSetting(s => s.CheckPowderEnabled) && (!String.IsNullOrEmpty(powderBefore) || !String.IsNullOrEmpty(powderAfter)))
            {
                if(isOilFeed && SettingsManager.GetSetting(s => s.OilSupplyEnabled))
                {
                    PowderCheckUI checkPowder = new PowderCheckUI(powderBefore, powderAfter, processNumber, totalPowderInFormula, totalPowder - remainPowder, 1);
                    checkPowder.FormClosed += powderCheckUIBeforeOilSupplyFormClosed;
                    checkPowder.ShowDialog();
                }
                else
                {
                    PowderCheckUI checkPowder = new PowderCheckUI(powderBefore, powderAfter, processNumber, totalPowderInFormula, totalPowder, 0);
                    checkPowder.FormClosed += powderCheckUIBeforeOilSupplyFormClosed;
                    checkPowder.ShowDialog();
                }
            }
            else
            {
                StartAutomationProcess();
            }
        }

        private void powderCheckUIBeforeOilSupplyFormClosed(object sender, EventArgs e)
        {
            ((Form)sender).FormClosed -= powderCheckUIBeforeOilSupplyFormClosed;
            StartAutomationProcess();
            //TemporaryVariables.processDT.Rows[currentRow]["is_finished"] = true;
            //GetNextProcess();
        }

        private void btnReverseRoll_Click(object sender, EventArgs e)
        {
            try
            {
                PLCConnector pLC = new PLCConnector(SettingsManager.GetSetting(s => s.PlcIp), 0, 0, out ConnectionPLC);
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
            PLCConnector pLC = new PLCConnector(SettingsManager.GetSetting(s => s.PlcIp), 0, 0, out ConnectionPLC);
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
            PLCConnector pLC = new PLCConnector(SettingsManager.GetSetting(s => s.PlcIp), 0, 0, out ConnectionPLC);
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

        private void automationSkipFormClose(object sender, EventArgs e)
        {
            ((Form)sender).FormClosed -= automationSkipFormClose;
            if (isAuthorSkip)
            {
                SkipStepTrigger();
            }
            isAuthorSkip = false;
        }

        private void LoadConnection2SerialPort(int portNo)
        {
            try
            {
                switch (portNo)
                {
                    case 1:
                        if (!serialPort1.IsOpen)
                        {
                            if (!String.IsNullOrEmpty(SettingsManager.GetSetting(s => s.OilSupplyComPort)))
                            {
                                serialPort1.PortName = SettingsManager.GetSetting(s => s.OilSupplyComPort);
                                serialPort1.BaudRate = Convert.ToInt32(SettingsManager.GetSetting(s => s.OilSupplyBaudRate));
                                serialPort1.DataBits = Convert.ToInt32(SettingsManager.GetSetting(s => s.OilSupplyDataBits));
                                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), SettingsManager.GetSetting(s => s.OilSupplyStopBits));
                                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), SettingsManager.GetSetting(s => s.OilSupplyParity));
                                serialPort1.ReadTimeout = 150;
                                serialPort1.DtrEnable = true;
                                serialPort1.Handshake = Handshake.XOnXOff;
                                serialPort1.Open();

                                Thread.Sleep(serialPort1.ReadTimeout);
                                if (!serialPort1.IsOpen)
                                {
                                    throw new Exception("Connot open connection to serial port.");
                                }
                            }
                            else
                            {
                                CTMessageBox.Show("Please choose the port in the setting tab first!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                Program.main.openScaleTab();
                            }
                        }
                        break;
                    case 2:
                        if (!serialPort2.IsOpen)
                        {
                            if (!String.IsNullOrEmpty(SettingsManager.GetSetting(s => s.SecondaryOilSupplyComPort)))
                            {
                                serialPort2.PortName = SettingsManager.GetSetting(s => s.SecondaryOilSupplyComPort);
                                serialPort2.BaudRate = Convert.ToInt32(SettingsManager.GetSetting(s => s.OilSupplyBaudRate));
                                serialPort2.DataBits = Convert.ToInt32(SettingsManager.GetSetting(s => s.OilSupplyDataBits));
                                serialPort2.StopBits = (StopBits)Enum.Parse(typeof(StopBits), SettingsManager.GetSetting(s => s.OilSupplyStopBits));
                                serialPort2.Parity = (Parity)Enum.Parse(typeof(Parity), SettingsManager.GetSetting(s => s.OilSupplyParity));
                                serialPort2.ReadTimeout = 150;
                                serialPort2.DtrEnable = true;
                                serialPort2.Handshake = Handshake.XOnXOff;
                                serialPort2.Open();

                                Thread.Sleep(serialPort2.ReadTimeout);
                                if (!serialPort2.IsOpen)
                                {
                                    throw new Exception("Connot open connection to serial port.");
                                }
                            }
                            else
                            {
                                CTMessageBox.Show("Please choose the port in the setting tab first!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                Program.main.openScaleTab();
                            }
                        }
                        break;
                    case 3:
                        if (SettingsManager.GetSetting(s => s.FlowMeterEnabled))
                        {
                            if (!serialPort3.IsOpen)
                            {
                                if (!String.IsNullOrEmpty(SettingsManager.GetSetting(s => s.FlowMeterComPort)))
                                {
                                    serialPort3.PortName = SettingsManager.GetSetting(s => s.FlowMeterComPort);
                                    serialPort3.BaudRate = 9600;
                                    serialPort3.DataBits = 8;
                                    serialPort3.Parity = Parity.None;
                                    serialPort3.StopBits = StopBits.One;
                                    serialPort3.Handshake = Handshake.None;
                                    serialPort3.ReadTimeout = 1000;
                                    serialPort3.WriteTimeout = 1000;
                                    Thread.Sleep(serialPort3.ReadTimeout);
                                    if (!serialPort3.IsOpen)
                                    {
                                        throw new Exception("Connot open connection to diameter serial port.");
                                    }
                                }
                                else
                                {
                                    CTMessageBox.Show("Please choose the port in the setting tab first!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                    Program.main.openScaleTab();
                                }
                            }

                        }
                        break;
                    default: break;
                }
            }
            catch (Exception ex)
            {
                CTMessageBox.Show("Failed to init connection to serial port : " + ex.Message, "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Failed to init connection to serial port", ex.Message);
            }
        }

        private void SkipStepTrigger()
        {
            if (SettingsManager.GetSetting(s => s.Language) == 0)
            {
                message = "Xác nhận bỏ qua bước đang thể hiện ?";
                caption = "Cảnh báo";
            }
            else if (SettingsManager.GetSetting(s => s.Language) == 1)
            {
                message = "确认跳过这个步骤 ?";
                caption = "提示";
            }
            else if (SettingsManager.GetSetting(s => s.Language) == 2)
            {
                message = "Skip current process ?";
                caption = "Warning";
            }
            DialogResult dialog = CTMessageBox.Show(message, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                PLCConnector pLC = new PLCConnector(SettingsManager.GetSetting(s => s.PlcIp), 0, 0, out ConnectionPLC);
                if (ConnectionPLC == 0)
                {
                    TemporaryVariables.processDT.Rows[currentRow]["is_finished"] = true;
                    if (countDownTimer != null || countDownTimer.IsRunning)
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

                    if (SettingsManager.GetSetting(s => s.SaveReportEnabled))
                    {
                        XLWorkbook workbook = new XLWorkbook(TemporaryVariables.tempReportPath);
                        var reportSheet = workbook.Worksheet(1);
                        string speedReport;
                        int row = 8 + processNumber;
                        reportSheet.Range("F" + row).Value = processNumber;
                        reportSheet.Range("G" + row).Value = rtbRemark.Text.Trim();
                        reportSheet.Range("H" + row).Value = "Skipped";
                        reportSheet.Range("I" + row).Value = "Skipped";
                        reportSheet.Range("J" + row).Value = "Skipped";
                        reportSheet.Range("K" + row).Value = "Skipped";
                        if (isOilFeed && SettingsManager.GetSetting(s => s.OilSupplyEnabled))
                        {
                            reportSheet.Range("N" + row).Value = oilType;
                            reportSheet.Range("O" + row).Value = oilMass.ToString();
                        }
                        if (speed2 != 0)
                            speedReport = speed1 + "-" + speed2;
                        else
                            speedReport = speed1.ToString();
                        reportSheet.Range("T" + row).Value = speedReport;

                        workbook.Save();
                    }
                    GetNextProcess();
                    btnStartProcess.Enabled = true;
                }
                else
                {
                    if (SettingsManager.GetSetting(s => s.Language) == 0)
                    {
                        message = "Không thể kết nối PLC. Vui lòng thử lại.";
                        caption = "Cảnh báo";
                    }
                    else if (SettingsManager.GetSetting(s => s.Language) == 1)
                    {
                        message = "无法连接 PLC。请再试一次。";
                        caption = "提示";
                    }
                    else if (SettingsManager.GetSetting(s => s.Language) == 2)
                    {
                        message = "Unable to connect PLC. Please try again.";
                        caption = "Warning";
                    }
                }
            }
        }

        private void btnContinueStep_Click(object sender, EventArgs e)
        {
            if (SettingsManager.GetSetting(s => s.SkipPasswordEnabled))
            {
                PasswordConfirm passwordConfirm = new PasswordConfirm();
                passwordConfirm.FormClosed += automationSkipFormClose;
                passwordConfirm.ShowDialog();
            }
            else
            {
                SkipStepTrigger();
            }
        }

        private void AutomationInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1 != null)
            {
                if (serialPort1.IsOpen)
                    CloseSerialPort(serialPort1);
            }
            if (tmrCallBgWorker != null)
            {
                tmrCallBgWorker.Stop();
                tmrCallBgWorker.Tick -= new EventHandler(timer_nextRun_Tick);
                bgWorker.DoWork -= new DoWorkEventHandler(BW_DoWork);
                bgWorker.ProgressChanged -= BW_ProgressChanged;
                bgWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(BW_RunWorkerCompleted);
            }
            countDownTimer = null;
            PLCMethods.ResetPLCVariables();

            btnStartProcess.Enabled = true;
            PLCConnector pLC = new PLCConnector(SettingsManager.GetSetting(s => s.PlcIp), 0, 0, out ConnectionPLC);
            if (ConnectionPLC == 0)
            {
                pLC.Diconnect();
            }
            this.Dispose();
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
                db = SettingsManager.GetSetting(s => s.DatabaseNumber);
                //Khởi tạo kết nối PLC máy cấp dầu

                PLCConnector pLC = new PLCConnector(SettingsManager.GetSetting(s => s.PlcIp), 0, 0, out ConnectionPLC);
                if (ConnectionPLC == 0)
                {
                    pLC.WriteRealtoPLC(Convert.ToSingle(SettingsManager.GetSetting(s => s.MaxSpeed)), db, Convert.ToInt32(ini.Read("MS", "start")), 2);
                    pLC.WriteRealtoPLC(Convert.ToSingle(SettingsManager.GetSetting(s => s.SpindleDiameter)), db, Convert.ToInt32(ini.Read("SD", "start")), 2);
                    pLC.WriteRealtoPLC(Convert.ToSingle(SettingsManager.GetSetting(s => s.SensorDiameter)), db, Convert.ToInt32(ini.Read("SSD", "start")), 2);
                    pLC.WriteRealtoPLC(Convert.ToSingle(SettingsManager.GetSetting(s => s.TransmissionRatio)), db, Convert.ToInt32(ini.Read("TRMS", "start")), 2);
                }
                else
                {
                    throw new Exception("Connection to PLC return " + ConnectionPLC);
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

        private void TriggerAutomationON()
        {
            PLCConnector pLC = new PLCConnector(SettingsManager.GetSetting(s => s.PlcIp), 0, 0, out ConnectionPLC);
            AutoTrigger = true;
            ManualTrigger = false;
            isAutomationON = true;
            btnActivateSpeedControl.BackColor = Color.Yellow;
            if (SettingsManager.GetSetting(s => s.Language) == 0)
            {
                btnActivateSpeedControl.Text = "Chế độ tự động đang bật";
            }
            else if (SettingsManager.GetSetting(s => s.Language) == 1)
            {
                btnActivateSpeedControl.Text = "自动化模式：开";
            }
            else if (SettingsManager.GetSetting(s => s.Language) == 2)
            {
                btnActivateSpeedControl.Text = "Automation mode ON";
            }
            btnNormalRoll.Visible = true;
            btnResetRoll.Visible = true;
            btnReverseRoll.Visible = true;
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
            isAutomationON = false;
            btnActivateSpeedControl.BackColor = Color.White;
            if (SettingsManager.GetSetting(s => s.Language) == 0)
            {
                btnActivateSpeedControl.Text = "Chế độ tự động đang tắt";
            }
            else if (SettingsManager.GetSetting(s => s.Language) == 1)
            {
                btnActivateSpeedControl.Text = "自动化模式：关";
            }
            else if (SettingsManager.GetSetting(s => s.Language) == 2)
            {
                btnActivateSpeedControl.Text = "Automation mode OFF";
            }
            btnNormalRoll.Visible = false;
            btnResetRoll.Visible = false;
            btnReverseRoll.Visible = false;
        }

        private void TimerProcessTrigger()
        {
            try
            {
                if (countDownTimer != null)
                    lbCountDown.Text = countDownTimer.TimeLeftStr;
                else
                    Program.main.openScaleTab();
            }
            catch (Exception ex)
            {
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Countdown timer trigger", ex.Message);
            }
        }

        private void FinishProcess()
        {
            bool isFinishProcess = false;

            do
            {
                PLCConnector pLC = new PLCConnector(SettingsManager.GetSetting(s => s.PlcIp), 0, 0, out ConnectionPLC);
                if (ConnectionPLC == 0)
                {
                    isFinishProcess = true;
                    lbAnnounce.Text = String.Empty;

                    if (countDownTimer != null || countDownTimer.IsRunning)
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

                    if (SettingsManager.GetSetting(s => s.SaveReportEnabled))
                    {
                        XLWorkbook workbook = new XLWorkbook(TemporaryVariables.tempReportPath);
                        var reportSheet = workbook.Worksheet(1);
                        int row = 8 + processNumber;
                        DateTime dateEndAuto = DateTime.UtcNow;
                        reportSheet.Range("J" + row).Value = dateEndAuto;
                        reportSheet.Range("K" + row).Value = dateEndAuto;
                        reportSheet.Range("U" + row).Value = lbTemperature.Text;
                        workbook.Save();
                    }


                    if (!isFinished)
                    {
                        if (SettingsManager.GetSetting(s => s.StopMachineBetweenRuns))
                        {
                            pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("TS", "start")), Convert.ToInt32(ini.Read("TS", "bit")));
                            pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")));
                            pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")));
                            pLC.WriteRealtoPLC(0, db, Convert.ToInt32(ini.Read("WS", "start")), 2);
                        }

                        if (isSkipAnnouce)
                        {
                            pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")));
                            if (SettingsManager.GetSetting(s => s.OpenMixerLidAfterRun))
                            {
                                pLC.WriteBoolToPLC(true, db, Convert.ToInt32(ini.Read("OL", "start")), Convert.ToInt32(ini.Read("OL", "bit")));
                            }
                        }
                        else
                        {
                            if (!SettingsManager.GetSetting(s => s.AlwaysOpenMixerLid))
                            {
                                if (SettingsManager.GetSetting(s => s.Language) == 0)
                                {
                                    message = "Đã kết thúc bước, bấm \"OK\" để mở nắp, \"CANCEL\" để giữ nắp đóng!";
                                    caption = "Thông tin";
                                }
                                else if (SettingsManager.GetSetting(s => s.Language) == 1)
                                {
                                    message = "此步骤已经结束， 开盖 - 点击\"OK\", 继续运行 - 点击\"CANCEL\"";
                                    caption = "信息";
                                }
                                else if (SettingsManager.GetSetting(s => s.Language) == 2)
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
                        }

                        GetNextProcess();
                    }
                    else
                    {
                        if (SettingsManager.GetSetting(s => s.StopMachineBetweenRuns))
                        {
                            pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("TS", "start")), Convert.ToInt32(ini.Read("TS", "bit")));
                            pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")));
                            pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")));
                            pLC.WriteRealtoPLC(0, db, Convert.ToInt32(ini.Read("WS", "start")), 2);
                        }
                        if (isSkipAnnouce)
                        {
                            pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")));
                            if (SettingsManager.GetSetting(s => s.OpenMixerLidAfterRun))
                            {
                                pLC.WriteBoolToPLC(true, db, Convert.ToInt32(ini.Read("OL", "start")), Convert.ToInt32(ini.Read("OL", "bit")));
                            }
                        }
                        pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("ER", "start")), Convert.ToInt32(ini.Read("ER", "bit")));

                        if (SettingsManager.GetSetting(s => s.Language) == 0)
                        {
                            message = "Đã hoàn thành thực hiện công thức! Vui lòng đổ liệu thủ công!";
                            caption = "Thông tin";
                        }
                        else if (SettingsManager.GetSetting(s => s.Language) == 1)
                        {
                            message = "生产完毕！请将产品从捏合机里取出！";
                            caption = "信息";
                        }
                        else if (SettingsManager.GetSetting(s => s.Language) == 2)
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

            if (!isAutomationON)
            {
                countDownTimer.Pause();
            }
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
                PLCConnector pLC = new PLCConnector(SettingsManager.GetSetting(s => s.PlcIp), 0, 0, out ConnectionPLC);
                if (ConnectionPLC == 0) //Check connection to PLC and connect
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
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Read PLC error:", ex.Message);
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
        private void CloseSerialPort(SerialPort serialPort)
        {
            if (serialPort.IsOpen)
            {
                isExitApplication = true;
                Thread.Sleep(serialPort.ReadTimeout); //Wait for reading threads to finish
                serialPort.Close();
                isExitApplication = false;
            }
        }
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
                        stepDesc = dt.Rows[i]["description"].ToString();
                        rtbRemark.Text = stepDesc;
                        processNumber = Convert.ToInt32(dt.Rows[i]["process_no"].ToString());
                        if (SettingsManager.GetSetting(s => s.Language) == 0)
                        {
                            lbProcessNo.Text = "Thực hiện bước số : " + processNumber;
                        }
                        else if (SettingsManager.GetSetting(s => s.Language) == 1)
                        {
                            lbProcessNo.Text = "执行第" + processNumber + "步骤";
                        }
                        else if (SettingsManager.GetSetting(s => s.Language) == 2)
                        {
                            lbProcessNo.Text = "Process no: " + processNumber;
                        }

                        speed1 = (int)dt.Rows[i]["init_speed"];
                        time1 = (int)dt.Rows[i]["init_time"];
                        time2 = (int)dt.Rows[i]["change_time"];
                        speed2 = (int)dt.Rows[i]["change_speed"];
                        isVaccum = (bool)dt.Rows[i]["is_vaccum"];
                        max_temp = (int)dt.Rows[i]["max_temperature"];
                        isSkipAnnouce = (bool)dt.Rows[i]["is_skip_announce"];

                        isOilFeed = (bool)dt.Rows[i]["is_oilfeed"];
                        oilMass = double.Parse(dt.Rows[i]["oil_mass"].ToString(), CultureInfo.InvariantCulture);
                        oilWeight = double.Parse(dt.Rows[i]["oil_weight"].ToString(), CultureInfo.InvariantCulture);
                        oilType = dt.Rows[i]["oil_type"].ToString();

                        //Khai báo máy cấp dầu 2
                        isOilFeed2 = (bool)dt.Rows[i]["is_oilfeed_2"];
                        oilMass2 = double.Parse(dt.Rows[i]["oil_mass_2"].ToString(), CultureInfo.InvariantCulture);
                        oilWeight2 = double.Parse(dt.Rows[i]["oil_weight_2"].ToString(), CultureInfo.InvariantCulture);
                        oilType2 = dt.Rows[i]["oil_type_2"].ToString();

                        if (SettingsManager.GetSetting(s => s.CheckPowderEnabled))
                        {
                            powderBefore = dt.Rows[i]["powder_before"].ToString();
                            powderAfter = dt.Rows[i]["powder_after"].ToString();
                            totalPowderInFormula = dt.AsEnumerable().Sum(row => row.Field<int>("total_powder_bags"));
                        }
                        else
                        {
                            powderBefore = String.Empty;
                            powderAfter = String.Empty;
                            totalPowderInFormula = 0;
                        }

                        if (SettingsManager.GetSetting(s => s.FlowMeterEnabled))
                        {
                            allowMass = oilMass * 5 / 1000;
                        }
                        else
                        {
                            allowMass = 0;
                        }
                        realMass = 0;
                        initMass = 0;
                        finalMass = 0;

                        isCompleteOIlSupply = false;
                        isCompleteFirstOilSup = false;

                        totalPowder = (int)dt.Rows[i]["total_powder_bags"];
                        int nextStepTotalPowder = 0;
                        if ((i + 1) < dt.Rows.Count)
                            nextStepTotalPowder = (int)dt.Rows[i + 1]["total_powder_bags"];
                        remainPowder = (int)dt.Rows[i]["remain_powder_bags"];

                        isOilFeeding = false;
                        isOilFeeding2 = false;
                        isSendOilMass = false;
                        isSendOilMass2 = false;
                        countDownTimer = new CountDownTimer();
                        isFirstStart = false;

                        string announce = String.Empty;
                        string nextStepAnnounce = String.Empty;
                        switch (SettingsManager.GetSetting(s => s.Language))
                        {
                            case 0:
                                announce = "Đang đợi bấm nút bắt đầu...";
                                nextStepAnnounce = "Bước tiếp theo cần cấp " + nextStepTotalPowder + " bao bột.";
                                break;
                            case 1:
                                announce = "在等待按下开始按钮...";
                                nextStepAnnounce = "下一步需要" + nextStepTotalPowder + "袋面粉.";
                                break;
                            case 2:
                                announce = "Waiting for start button...";
                                nextStepAnnounce = "Next step requires " + nextStepTotalPowder + " bags of powder.";
                                break;
                        }
                        if (SettingsManager.GetSetting(s => s.ShowPowderAlert) && SettingsManager.GetSetting(s => s.OilSupplyEnabled) && isOilFeed)
                        {
                            int numberPowderBFOil = 0;
                            if (totalPowder > remainPowder)
                            {
                                numberPowderBFOil = totalPowder - remainPowder;
                                switch (SettingsManager.GetSetting(s => s.Language))
                                {
                                    case 0:
                                        announce = "Bước đang thể hiện cần cấp " + totalPowder + " bao bột, " + numberPowderBFOil + " cần cấp trước khi bắt đầu, " + remainPowder + " cần cấp sau khi cấp dầu. Đang đợi bấm nút bắt đầu...";
                                        break;
                                    case 1:
                                        announce = "此步骤需要加" + totalPowder + "包粉，" + numberPowderBFOil + "包在开始前要加，" + remainPowder + "包在加油后要加. 在等待按下开始按钮...";
                                        break;
                                    case 2:
                                        announce = "The step being shown requires " + totalPowder + " bags of powder, " + numberPowderBFOil + " needs to be supplied before starting, " + remainPowder + " needs to be supplied after supplying oil.Waiting for start button...";
                                        break;
                                }
                            }
                            lbAnnounce.Text = announce;
                            if ((i + 1) < dt.Rows.Count)
                                labelAnnounceNS.Text = nextStepAnnounce;
                        }
                        break;
                    }
                }
            }
        }

        private void LoadBackgroundWorker()
        {
            try
            {
                // this timer calls bgWorker again and again after regular intervals
                tmrCallBgWorker = new System.Windows.Forms.Timer();//Timer for do task
                tmrCallBgWorker.Tick += new EventHandler(timer_nextRun_Tick);
                tmrCallBgWorker.Interval = 500; //3600000;

                // this is our worker
                bgWorker = new BackgroundWorker();

                // work happens in this method
                bgWorker.DoWork += new DoWorkEventHandler(BW_DoWork);
                bgWorker.ProgressChanged += BW_ProgressChanged;
                bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BW_RunWorkerCompleted);
                bgWorker.WorkerReportsProgress = true;

                tmrCallBgWorker.Start();
            }
            catch (Exception ex)
            {
                CTMessageBox.Show(ex.Message);
            }
        }

        private void StartRequirementCheck(PLCConnector pLC)
        {
            if (SettingsManager.GetSetting(s => s.FlowMeterEnabled))
            {
                tmrCallBgWorker.Stop();
                Thread.Sleep(5000);
                finalMass = realMass;
                tmrCallBgWorker.Start();
            }
            if (SettingsManager.GetSetting(s => s.CheckPowderEnabled) && remainPowder > 0)
            {
                tmrCallBgWorker.Stop();
                PowderCheckUI checkPowder = new PowderCheckUI(powderBefore, powderAfter, processNumber, totalPowderInFormula, remainPowder, 2);
                checkPowder.FormClosed += powderCheckUIAfterOilSupplyFormClosed;
                checkPowder.ShowDialog();
            }
            else
            {
                isFirstStart = false;
                startRunAutomationProcess(pLC);
            }
        }

        private void powderCheckUIAfterOilSupplyFormClosed(object sender, EventArgs e)
        {
            tmrCallBgWorker.Start();
            ((Form)sender).FormClosed -= powderCheckUIAfterOilSupplyFormClosed;
            PLCConnector pLC = new PLCConnector(SettingsManager.GetSetting(s => s.PlcIp), 0, 0, out ConnectionPLC);
            if (ConnectionPLC == 0)
            {
                isFirstStart = false;
                startRunAutomationProcess(pLC);
            }
        }

        private void CheckStart()
        {
            isCompleteOIlSupply = true;
            PLCConnector pLC = new PLCConnector(SettingsManager.GetSetting(s => s.PlcIp), 0, 0, out ConnectionPLC);
            if (ConnectionPLC == 0)
            {
                lbAnnounce.Text = String.Empty;
                StartRequirementCheck(pLC);
                //if (Settings.Default.isAlertPowder)
                //{
                //    if (remainPowder != 0)
                //    {
                //        if (Settings.Default.language == 0)
                //        {
                //            message = "Công đoan cấp dầu đã hoàn tất vui lòng cấp " + remainPowder + " bao bột còn lại.";
                //        }
                //        else if (Settings.Default.language == 1)
                //        {
                //            message = "加油确认完成，请加剩余的" + remainPowder + "包粉";

                //        }
                //        else if (Settings.Default.language == 2)
                //        {
                //            message = "Oil feeding completed, still need to add " + remainPowder + " more bag of powder.";
                //        }
                //        lbAnnounce.Text = message;
                //        StartRequirementCheck(pLC);
                //    }
                //    else
                //    {
                //        lbAnnounce.Text = String.Empty;
                //        SystemLog.Output(SystemLog.MSG_TYPE.Nor, "Cài đặt trống", "Không có cài đặt số bao bột");
                //        StartRequirementCheck(pLC);
                //    }
                //}
                //else
                //{
                //    lbAnnounce.Text = String.Empty;
                //    StartRequirementCheck(pLC);
                //}
            }
        }
        private void UpdateUIWithBGWorkerVariables()
        {
            PLCConnector pLC = new PLCConnector(SettingsManager.GetSetting(s => s.PlcIp), 0, 0, out ConnectionPLC);
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
                    if (ManualTrigger && !AutoTrigger)
                    {
                        if (!countDownTimer.IsRunning)
                        {
                            countDownTimer.Continue();
                        }
                        if (!isFirstStart)
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

                if (isAutomationON)
                {
                    if (isFirstStart)
                    {
                        if ((ContainerUpSensor && CloseLidSensor) || (SettingsManager.GetSetting(s => s.AlwaysOpenMixerLid) && ContainerUpSensor && !CloseLidSensor))
                        {
                            if (isOilFeed && SettingsManager.GetSetting(s => s.OilSupplyEnabled)) // Check to see if the current working step 
                            {
                                if (!isOilFeeding)
                                {
                                    //bool checkConnect = SubMethods.CheckConnectStatus(serialPort1, new byte[] { 0x5A, 0x01, 0x03, 0x5E, 0xA5 });
                                    if (serialPort1.IsOpen)
                                    {
                                        if (!isSendOilMass)
                                        {
                                            try
                                            {
                                                if (countTimeOutOil <= oilSupplyAttemps)
                                                {
                                                    countTimeOutOil++;
                                                    if (SettingsManager.GetSetting(s => s.Language) == 0)
                                                    {
                                                        announce = "Đang truyền khối lượng dầu ...";
                                                    }
                                                    else if (SettingsManager.GetSetting(s => s.Language) == 1)
                                                    {
                                                        announce = "传输油量...";
                                                    }
                                                    else if (SettingsManager.GetSetting(s => s.Language) == 2)
                                                    {
                                                        announce = "Sending oil mass ...";
                                                    }
                                                    lbAnnounce.Text = announce;

                                                    SubMethods.FuelSetting(serialPort1, oilMass + 1); // Cho +1 lít để xem còn chênh lệch hay không

                                                    Thread.Sleep(200);
                                                    buffer = new byte[256]; // Tùy chỉnh kích thước buffer nếu cần
                                                    bytesRead = serialPort1.Read(buffer, 0, buffer.Length);
                                                    SystemLog.Output(SystemLog.MSG_TYPE.Err, "Mass set receive", buffer.ToString());
                                                    if (buffer[0] == 90 && buffer[1] == 1 && buffer[2] == 5 && buffer[3] == 96 && buffer[4] == 165)
                                                    {
                                                        Thread.Sleep(200);
                                                        byte[] command = new byte[] { 0x5A, 0x01, 0x03, 0x5E, 0xA5 };
                                                        serialPort1.Write(command, 0, command.Length);
                                                        buffer = new byte[256];
                                                        // Đọc dữ liệu phản hồi từ máy bơm xăng
                                                        Thread.Sleep(200);
                                                        bytesRead = serialPort1.Read(buffer, 0, buffer.Length);
                                                        SystemLog.Output(SystemLog.MSG_TYPE.Err, "Status receive", buffer.ToString());

                                                        if (buffer[0] == 90 && buffer[1] == 1 && buffer[2] == 3 && buffer[3] == 0 && buffer[4] == 94 && buffer[5] == 165)
                                                        {
                                                            Thread.Sleep(200);
                                                            SubMethods.SendCommand(serialPort1, new byte[] { 0x5A, 0x01, 0x01, 0x5C, 0xA5 });
                                                        }
                                                        else if (buffer[0] == 90 && buffer[1] == 1 && buffer[2] == 3 && buffer[3] == 1 && buffer[4] == 95 && buffer[5] == 165)
                                                        {
                                                            isSendOilMass = true; //Check if the signal is sent
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (tmrCallBgWorker != null)
                                                    {
                                                        tmrCallBgWorker.Stop();
                                                        tmrCallBgWorker.Tick -= new EventHandler(timer_nextRun_Tick);
                                                        bgWorker.DoWork -= new DoWorkEventHandler(BW_DoWork);
                                                        bgWorker.ProgressChanged -= BW_ProgressChanged;
                                                        bgWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(BW_RunWorkerCompleted);
                                                    }

                                                    if (SettingsManager.GetSetting(s => s.Language) == 0)
                                                    {
                                                        message = "Không thể kết nối máy cấp dầu!";
                                                        caption = "Thông tin";
                                                    }
                                                    else if (SettingsManager.GetSetting(s => s.Language) == 1)
                                                    {
                                                        message = "无法连接加油机！";
                                                        caption = "信息";
                                                    }
                                                    else if (SettingsManager.GetSetting(s => s.Language) == 2)
                                                    {
                                                        message = "Cannot connect to oil pump!";
                                                        caption = "Information";
                                                    }
                                                    CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    Program.main.openScaleTab();
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                isSendOilMass = false;
                                                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Send mass to serialport error", ex.Message);
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                if (SettingsManager.GetSetting(s => s.Language) == 0)
                                                {
                                                    announce = "Bắt đầu cấp dầu ...";
                                                }
                                                else if (SettingsManager.GetSetting(s => s.Language) == 1)
                                                {
                                                    announce = "开始注油...";
                                                }
                                                else if (SettingsManager.GetSetting(s => s.Language) == 2)
                                                {
                                                    announce = "Start oil feeding ...";
                                                }
                                                lbAnnounce.Text = announce;
                                                isOilFeeding = true;
                                                if (SettingsManager.GetSetting(s => s.SaveReportEnabled))
                                                {
                                                    XLWorkbook workbook = new XLWorkbook(TemporaryVariables.tempReportPath);
                                                    var reportSheet = workbook.Worksheet(1);
                                                    int row = 8 + processNumber;

                                                    DateTime timeOilStart = DateTime.UtcNow;
                                                    reportSheet.Range("P" + row).Value = timeOilStart;
                                                    reportSheet.Range("Q" + row).Value = timeOilStart;
                                                    workbook.Save();
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                isOilFeeding = false;
                                                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Save oil start report fail", ex.Message);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        LoadConnection2SerialPort(1);
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        if (!isCompleteOIlSupply)
                                        {
                                            if(!isCompleteFirstOilSup)
                                            {
                                                //byte[] command = new byte[] { 0x5A, 0x01, 0x04, 0x5F, 0xA5 };
                                                byte[] command = new byte[] { 0x5A, 0x01, 0x03, 0x5E, 0xA5 };
                                                serialPort1.Write(command, 0, command.Length);
                                                buffer = new byte[256];
                                                // Đọc dữ liệu phản hồi từ máy bơm xăng
                                                bytesRead = serialPort1.Read(buffer, 0, buffer.Length);

                                                if (buffer[0] == 90 && buffer[1] == 1 && buffer[2] == 3 && buffer[3] == 0 && buffer[4] == 94 && buffer[5] == 165)
                                                {
                                                    CloseSerialPort(serialPort1);

                                                    if (SettingsManager.GetSetting(s => s.SaveReportEnabled))
                                                    {
                                                        XLWorkbook workbookEnd = new XLWorkbook(TemporaryVariables.tempReportPath);
                                                        var reportSheetEnd = workbookEnd.Worksheet(1);
                                                        int rowEnd = 8 + processNumber;
                                                        lbAnnounce.Text = "";

                                                        if (SettingsManager.GetSetting(s => s.FlowMeterEnabled))
                                                        {
                                                            if (finalMass > initMass)
                                                            {
                                                                double diviantionMass = oilMass - (finalMass - initMass);
                                                                if (diviantionMass > allowMass)
                                                                {
                                                                    reportSheetEnd.Range("V" + rowEnd).Value = "Low - " + (diviantionMass - allowMass);
                                                                    lbAnnounce.Text = "Thiếu " + (diviantionMass - allowMass);
                                                                }
                                                                else if (diviantionMass < allowMass * -1)
                                                                {
                                                                    reportSheetEnd.Range("V" + rowEnd).Value = "High - " + (diviantionMass * -1 - allowMass);
                                                                    lbAnnounce.Text = "Dư" + (diviantionMass * -1 - allowMass);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                lbAnnounce.Text = "Giá trị cuối đang bé hơn " + finalMass + " < " + initMass;
                                                            }
                                                        }

                                                        DateTime timeOilEnd = DateTime.UtcNow;
                                                        reportSheetEnd.Range("R" + rowEnd).Value = timeOilEnd;
                                                        reportSheetEnd.Range("S" + rowEnd).Value = timeOilEnd;
                                                        workbookEnd.Save();
                                                    }
                                                    isCompleteFirstOilSup = true;
                                                }
                                            }
                                            else
                                            {
                                                if (isOilFeed2 && SettingsManager.GetSetting(s => s.OilSupplyEnabled)) // Check to see if the current working step 
                                                {
                                                    if (!isOilFeeding2)
                                                    {
                                                        //bool checkConnect = SubMethods.CheckConnectStatus(serialPort1, new byte[] { 0x5A, 0x01, 0x03, 0x5E, 0xA5 });
                                                        if (serialPort2.IsOpen)
                                                        {
                                                            if (!isSendOilMass2)
                                                            {
                                                                try
                                                                {

                                                                    if (countTimeOutOil2 <= oilSupplyAttemps)
                                                                    {
                                                                        countTimeOutOil2++;
                                                                        if (SettingsManager.GetSetting(s => s.Language) == 0)
                                                                        {
                                                                            announce = "Đang truyền khối lượng dầu 2 ...";
                                                                        }
                                                                        else if (SettingsManager.GetSetting(s => s.Language) == 1)
                                                                        {
                                                                            announce = "传输油量2...";
                                                                        }
                                                                        else if (SettingsManager.GetSetting(s => s.Language) == 2)
                                                                        {
                                                                            announce = "Sending oil mass 2...";
                                                                        }
                                                                        lbAnnounce.Text = announce;

                                                                        SubMethods.FuelSetting(serialPort2, oilMass2); // Cho +1 lít để xem còn chênh lệch hay không

                                                                        Thread.Sleep(200);
                                                                        buffer = new byte[256]; // Tùy chỉnh kích thước buffer nếu cần
                                                                        bytesRead = serialPort2.Read(buffer, 0, buffer.Length);
                                                                        SystemLog.Output(SystemLog.MSG_TYPE.Err, "Mass 2 set receive", buffer.ToString());
                                                                        if (buffer[0] == 90 && buffer[1] == 1 && buffer[2] == 5 && buffer[3] == 96 && buffer[4] == 165)
                                                                        {
                                                                            Thread.Sleep(200);
                                                                            byte[] command2 = new byte[] { 0x5A, 0x01, 0x03, 0x5E, 0xA5 };
                                                                            serialPort2.Write(command2, 0, command2.Length);
                                                                            buffer = new byte[256];
                                                                            // Đọc dữ liệu phản hồi từ máy bơm xăng
                                                                            Thread.Sleep(200);
                                                                            bytesRead = serialPort2.Read(buffer, 0, buffer.Length);
                                                                            SystemLog.Output(SystemLog.MSG_TYPE.Err, "Status 2 receive", buffer.ToString());
                                                                            if (buffer[0] == 90 && buffer[1] == 1 && buffer[2] == 3 && buffer[3] == 0 && buffer[4] == 94 && buffer[5] == 165)
                                                                            {
                                                                                Thread.Sleep(200);
                                                                                SubMethods.SendCommand(serialPort2, new byte[] { 0x5A, 0x01, 0x01, 0x5C, 0xA5 });
                                                                            }
                                                                            else if (buffer[0] == 90 && buffer[1] == 1 && buffer[2] == 3 && buffer[3] == 1 && buffer[4] == 95 && buffer[5] == 165)
                                                                            {
                                                                                isSendOilMass2 = true; //Check if the signal is sent
                                                                            }
                                                                        }

                                                                       
                                                                    }
                                                                    else
                                                                    {
                                                                        if (tmrCallBgWorker != null)
                                                                        {
                                                                            tmrCallBgWorker.Stop();
                                                                            tmrCallBgWorker.Tick -= new EventHandler(timer_nextRun_Tick);
                                                                            bgWorker.DoWork -= new DoWorkEventHandler(BW_DoWork);
                                                                            bgWorker.ProgressChanged -= BW_ProgressChanged;
                                                                            bgWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(BW_RunWorkerCompleted);
                                                                        }

                                                                        if (SettingsManager.GetSetting(s => s.Language) == 0)
                                                                        {
                                                                            message = "Không thể kết nối máy cấp dầu!";
                                                                            caption = "Thông tin";
                                                                        }
                                                                        else if (SettingsManager.GetSetting(s => s.Language) == 1)
                                                                        {
                                                                            message = "无法连接加油机！";
                                                                            caption = "信息";
                                                                        }
                                                                        else if (SettingsManager.GetSetting(s => s.Language) == 2)
                                                                        {
                                                                            message = "Cannot connect to oil pump!";
                                                                            caption = "Information";
                                                                        }
                                                                        CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                        Program.main.openScaleTab();
                                                                    }
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    isSendOilMass2 = false;
                                                                    SystemLog.Output(SystemLog.MSG_TYPE.Err, "Send mass to serialport error", ex.Message);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                try
                                                                {
                                                                    if (SettingsManager.GetSetting(s => s.Language) == 0)
                                                                    {
                                                                        announce = "Bắt đầu cấp dầu lần 2 ...";
                                                                    }
                                                                    else if (SettingsManager.GetSetting(s => s.Language) == 1)
                                                                    {
                                                                        announce = "开始注油 2...";
                                                                    }
                                                                    else if (SettingsManager.GetSetting(s => s.Language) == 2)
                                                                    {
                                                                        announce = "Start oil feeding 2...";
                                                                    }
                                                                    lbAnnounce.Text = announce;
                                                                    isOilFeeding2 = true;
                                                                    //if (Settings.Default.isSaveReport)
                                                                    //{
                                                                    //    XLWorkbook workbook = new XLWorkbook(TemporaryVariables.tempReportPath);
                                                                    //    var reportSheet = workbook.Worksheet(1);
                                                                    //    int row = 8 + processNumber;

                                                                    //    DateTime timeOilStart = DateTime.UtcNow;
                                                                    //    reportSheet.Range("P" + row).Value = timeOilStart;
                                                                    //    reportSheet.Range("Q" + row).Value = timeOilStart;
                                                                    //    workbook.Save();
                                                                    //}
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    isOilFeeding2 = false;
                                                                    SystemLog.Output(SystemLog.MSG_TYPE.Err, "Save oil start report fail", ex.Message);
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            LoadConnection2SerialPort(2);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        try
                                                        {
                                                            if (!isCompleteOIlSupply)
                                                            {
                                                                //byte[] command = new byte[] { 0x5A, 0x01, 0x04, 0x5F, 0xA5 };
                                                                byte[] command2 = new byte[] { 0x5A, 0x01, 0x03, 0x5E, 0xA5 };
                                                                serialPort2.Write(command2, 0, command2.Length);
                                                                buffer = new byte[256];
                                                                // Đọc dữ liệu phản hồi từ máy bơm xăng
                                                                bytesRead = serialPort2.Read(buffer, 0, buffer.Length);

                                                                if (buffer[0] == 90 && buffer[1] == 1 && buffer[2] == 3 && buffer[3] == 0 && buffer[4] == 94 && buffer[5] == 165)
                                                                {
                                                                    if (SettingsManager.GetSetting(s => s.Language) == 0)
                                                                    {
                                                                        announce = "Đã hoàn tất cấp dầu...";
                                                                    }
                                                                    else if (SettingsManager.GetSetting(s => s.Language) == 1)
                                                                    {
                                                                        announce = "供油完毕...";
                                                                    }
                                                                    else if (SettingsManager.GetSetting(s => s.Language) == 2)
                                                                    {
                                                                        announce = "Oil feed finish ...";
                                                                    }
                                                                    lbAnnounce.Text = announce;

                                                                    CheckStart();

                                                                    CloseSerialPort(serialPort2);

                                                                    //if (Settings.Default.isSaveReport)
                                                                    //{
                                                                    //    XLWorkbook workbookEnd = new XLWorkbook(TemporaryVariables.tempReportPath);
                                                                    //    var reportSheetEnd = workbookEnd.Worksheet(1);
                                                                    //    int rowEnd = 8 + processNumber;
                                                                    //    lbAnnounce.Text = "";

                                                                    //    if (Settings.Default.isOilMeasurement)
                                                                    //    {
                                                                    //        if (finalMass > initMass)
                                                                    //        {
                                                                    //            double diviantionMass = oilMass - (finalMass - initMass);
                                                                    //            if (diviantionMass > allowMass)
                                                                    //            {
                                                                    //                reportSheetEnd.Range("V" + rowEnd).Value = "Low - " + (diviantionMass - allowMass);
                                                                    //                lbAnnounce.Text = "Thiếu " + (diviantionMass - allowMass);
                                                                    //            }
                                                                    //            else if (diviantionMass < allowMass * -1)
                                                                    //            {
                                                                    //                reportSheetEnd.Range("V" + rowEnd).Value = "High - " + (diviantionMass * -1 - allowMass);
                                                                    //                lbAnnounce.Text = "Dư" + (diviantionMass * -1 - allowMass);
                                                                    //            }
                                                                    //        }
                                                                    //        else
                                                                    //        {
                                                                    //            lbAnnounce.Text = "Giá trị cuối đang bé hơn " + finalMass + " < " + initMass;
                                                                    //        }
                                                                    //    }

                                                                    //    DateTime timeOilEnd = DateTime.UtcNow;
                                                                    //    reportSheetEnd.Range("R" + rowEnd).Value = timeOilEnd;
                                                                    //    reportSheetEnd.Range("S" + rowEnd).Value = timeOilEnd;
                                                                    //    workbookEnd.Save();
                                                                    //}
                                                                }
                                                            }
                                                            else
                                                            {
                                                                CheckStart();
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            SystemLog.Output(SystemLog.MSG_TYPE.Err, "Serialport read data timeout", ex.Message);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //if (Settings.Default.language == 0)
                                                    //{
                                                    //    announce = "Đã hoàn tất cấp dầu...";
                                                    //}
                                                    //else if (Settings.Default.language == 1)
                                                    //{
                                                    //    announce = "供油完毕...";
                                                    //}
                                                    //else if (Settings.Default.language == 2)
                                                    //{
                                                    //    announce = "Oil feed finish ...";
                                                    //}
                                                    //lbAnnounce.Text = announce;

                                                    CheckStart();

                                                    //CloseSerialPort(serialPort1);

                                                    //if (Settings.Default.isSaveReport)
                                                    //{
                                                    //    XLWorkbook workbookEnd = new XLWorkbook(TemporaryVariables.tempReportPath);
                                                    //    var reportSheetEnd = workbookEnd.Worksheet(1);
                                                    //    int rowEnd = 8 + processNumber;
                                                    //    lbAnnounce.Text = "";

                                                    //    if (Settings.Default.isOilMeasurement)
                                                    //    {
                                                    //        if (finalMass > initMass)
                                                    //        {
                                                    //            double diviantionMass = oilMass - (finalMass - initMass);
                                                    //            if (diviantionMass > allowMass)
                                                    //            {
                                                    //                reportSheetEnd.Range("V" + rowEnd).Value = "Low - " + (diviantionMass - allowMass);
                                                    //                lbAnnounce.Text = "Thiếu " + (diviantionMass - allowMass);
                                                    //            }
                                                    //            else if (diviantionMass < allowMass * -1)
                                                    //            {
                                                    //                reportSheetEnd.Range("V" + rowEnd).Value = "High - " + (diviantionMass * -1 - allowMass);
                                                    //                lbAnnounce.Text = "Dư" + (diviantionMass * -1 - allowMass);
                                                    //            }
                                                    //        }
                                                    //        else
                                                    //        {
                                                    //            lbAnnounce.Text = "Giá trị cuối đang bé hơn " + finalMass + " < " + initMass;
                                                    //        }
                                                    //    }

                                                    //    DateTime timeOilEnd = DateTime.UtcNow;
                                                    //    reportSheetEnd.Range("R" + rowEnd).Value = timeOilEnd;
                                                    //    reportSheetEnd.Range("S" + rowEnd).Value = timeOilEnd;
                                                    //    workbookEnd.Save();
                                                    //}
                                                }

                                            }
                                            
                                        }
                                        else
                                        {
                                            CheckStart();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        SystemLog.Output(SystemLog.MSG_TYPE.Err, "Serialport read data timeout", ex.Message);
                                    }
                                }
                            }
                            else
                            {
                                lbAnnounce.Text = String.Empty;
                                StartRequirementCheck(pLC);
                            }
                        }
                        else
                        {
                            if (SettingsManager.GetSetting(s => s.Language) == 0)
                            {
                                announce = "Đang chờ tín hiệu từ cảm biến ...";
                            }
                            else if (SettingsManager.GetSetting(s => s.Language) == 1)
                            {
                                announce = "在等传感器的信号...";
                            }
                            else if (SettingsManager.GetSetting(s => s.Language) == 2)
                            {
                                announce = "Waiting for sensor data ...";
                            }
                            lbAnnounce.Text = announce;
                        }
                    }
                    //Change speed
                    try
                    {
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
                    catch (Exception ex)
                    {
                        SystemLog.Output(SystemLog.MSG_TYPE.Err, "Change speed error", ex.Message);
                    }
                }
            }
        }
        #endregion Methods
    }
}
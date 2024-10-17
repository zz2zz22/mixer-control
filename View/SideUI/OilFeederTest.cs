using mixer_control_globalver.Controller.IniFile;
using mixer_control_globalver.Controller.LogFile;
using mixer_control_globalver.Model.PLC;
using mixer_control_globalver.Properties;
using mixer_control_globalver.View.CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace mixer_control_globalver.View.SideUI
{
    public partial class OilFeederTest : Form
    {
        public static int ConnectionPLC;

        int db = Settings.Default.database_no;
        private int bytesRead;
        private byte[] buffer;
        double testMass;
        string message = String.Empty, caption = String.Empty, actualMass;

        IniFile ini = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "\\data\\setting.ini");

        BackgroundWorker bgWorkerTestOil;
        System.Windows.Forms.Timer tmrCallBgWorker;
        System.Threading.Timer tmrEnsureWorkerGetsCalled;

        object lockObject = new object();

        public OilFeederTest()
        {
            InitializeComponent();

            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        private void CloseSerialPort()
        {
            Thread.Sleep(serialPort1.ReadTimeout); //Wait for reading threads to finish
            serialPort1.Close();
        }

        private void LoadBackgroundWorker()
        {
            try
            {
                // this timer calls bgWorker again and again after regular intervals
                tmrCallBgWorker = new System.Windows.Forms.Timer();//Timer for do task
                tmrCallBgWorker.Tick += new EventHandler(timer_nextRun_Tick);
                tmrCallBgWorker.Interval = 200 /**Settings.Default.gasolinePumpTime*/; //3600000;

                // this is our worker
                bgWorkerTestOil = new BackgroundWorker();

                // work happens in this method
                bgWorkerTestOil.DoWork += new DoWorkEventHandler(BW_DoWork);
                bgWorkerTestOil.ProgressChanged += BW_ProgressChanged;
                bgWorkerTestOil.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BW_RunWorkerCompleted);
                bgWorkerTestOil.WorkerReportsProgress = true;

                tmrCallBgWorker.Start();
            }
            catch (Exception ex)
            {
                CTMessageBox.Show(ex.Message);
            }
        }

        private void btnStartTesting_Click(object sender, EventArgs e)
        {
            if (Settings.Default.language == 0)
            {
                message = "Bắt đầu test cấp dầu ?";
                caption = "Cảnh báo";
            }
            else if (Settings.Default.language == 1)
            {
                message = "开始油泵测试 ?";
                caption = "提示";
            }
            else if (Settings.Default.language == 2)
            {
                message = "Start oil pump test ?";
                caption = "Warning";
            }
            DialogResult dialogResult = CTMessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                rtb_log.Text = String.Empty;
                btnStartTesting.Enabled = false;
                try
                {
                    if (double.TryParse(txbTestMass.Text.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out testMass))
                    {
                        //do
                        //{
                        //    PLCConnector pLC = new PLCConnector(Settings.Default.plc_ip, 0, 0, out ConnectionPLC);
                        //    pLC.WriteBoolToPLC(true, db, Convert.ToInt32(ini.Read("OTV", "start")), Convert.ToInt32(ini.Read("OTV", "bit")));
                        //} while (ConnectionPLC != 0);

                        Thread.Sleep(200);

                        lbStatus.Text = "Transfer oil mass to pump machine...";
                        SubMethods.FuelSetting(serialPort1, testMass);

                        Thread.Sleep(200);
                        do
                        {
                            if (serialPort1.IsOpen)
                            {
                                try
                                {
                                    //byte[] command = new byte[] { 0x5A, 0x01, 0x05, 0x60, 0xA5 }; //Lệnh đọc phản hồi truyền
                                    //serialPort1.Write(command, 0, command.Length);
                                    // Tạo buffer để đọc dữ liệu phản hồi
                                    buffer = new byte[256]; // Tùy chỉnh kích thước buffer nếu cần
                                                            // Đọc dữ liệu phản hồi từ máy bơm xăng
                                    bytesRead = serialPort1.Read(buffer, 0, buffer.Length);
                                    string test = String.Empty;
                                    for (int i = 0; i < bytesRead; i++)
                                    {
                                        test += buffer[i].ToString() + " ";
                                    }
                                    rtb_log.Text = "\r\n\r\nRespond: " + test;
                                    Thread.Sleep(200);
                                }
                                catch (TimeoutException ex)
                                {
                                    lbStatus.Text = "Không có dữ liệu phản hồi hoặc dữ liệu sai!\r\n" + ex.Message;
                                    break;
                                }
                            }
                        } while (buffer[0] != 90 && buffer[1] != 1 && buffer[2] != 5 && buffer[3] != 96 && buffer[4] != 165);

                        if(buffer[0] == 90 && buffer[1] == 1 && buffer[2] == 5 && buffer[3] == 96 && buffer[4] == 165)
                        {
                            lbStatus.Text = "Đang truyền lệnh bắt đầu...";
                            // Chuyển đổi các byte mong muốn thành số nguyên
                            int settingMass = buffer[3] << 16 | buffer[4] << 8 | buffer[5];
                            double setMass = Convert.ToDouble(settingMass) / 100;

                            SubMethods.SendCommand(serialPort1, new byte[] { 0x5A, 0x01, 0x01, 0x5C, 0xA5 });
                            LoadBackgroundWorker();
                        }
                    }
                    else
                    {
                        throw new Exception("Can not regconise input mass!");
                    }
                }
                catch (Exception ex)
                {
                    CTMessageBox.Show(ex.Message);
                }
            }
        }

        private void OilFeederTest_Load(object sender, EventArgs e)
        {
            if (Settings.Default.language == 0)
            {
                message = "Nhập khối lượng dầu muốn test:";
            }
            else if (Settings.Default.language == 1)
            {
                message = "输入测试油量：";
            }
            else if (Settings.Default.language == 2)
            {
                message = "Input the test oil mass:";
            }
            label1.Text = message;
            try
            {
                if (!String.IsNullOrEmpty(Properties.Settings.Default.comPort))
                {
                    serialPort1.PortName = Properties.Settings.Default.comPort;
                    serialPort1.BaudRate = Convert.ToInt32(Properties.Settings.Default.baudRate);
                    serialPort1.DataBits = Convert.ToInt32(Properties.Settings.Default.dataBits);
                    serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), Properties.Settings.Default.stopBits);
                    serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), Properties.Settings.Default.parityBits);
                    serialPort1.ReadTimeout = 1000;
                    serialPort1.Open();
                    //bool isConnected = SubMethods.CheckConnectStatus(serialPort1, new byte[] { 0x5A, 0x01, 0x03, 0x5E, 0xA5 }); // Đọc trạng thái máy
                    if (!serialPort1.IsOpen)
                    {
                        if (Settings.Default.language == 0)
                        {
                            message = "Kết nối với máy bơm không thành công, vui lòng kiểm tra!";
                            caption = "Cảnh báo";
                        }
                        else if (Settings.Default.language == 1)
                        {
                            message = "泵连接失败，请检查！";
                            caption = "提示";
                        }
                        else if (Settings.Default.language == 2)
                        {
                            message = "Connection to the pump failed, please check!";
                            caption = "Warning";
                        }
                        CTMessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        CloseSerialPort();
                        this.Close();
                    }
                }
                else
                {
                    if (Settings.Default.language == 0)
                    {
                        message = "Chưa cài đặt cổng kết nối với máy bơm!";
                        caption = "Cảnh báo";
                    }
                    else if (Settings.Default.language == 1)
                    {
                        message = "泵连接口未安装！";
                        caption = "提示";
                    }
                    else if (Settings.Default.language == 2)
                    {
                        message = "The pump connection port is not installed!";
                        caption = "Warning";
                    }
                    CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                CTMessageBox.Show(ex.Message);
                this.Close();
            }
        }

        private void timer_nextRun_Tick(object sender, EventArgs e)
        {
            if (Monitor.TryEnter(lockObject))
            {
                try
                {
                    // if bgworker is not busy the call the worker
                    if (!bgWorkerTestOil.IsBusy)
                    {
                        bgWorkerTestOil.RunWorkerAsync();
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
                byte[] command = new byte[] { 0x5A, 0x01, 0x04, 0x5F, 0xA5 };
                serialPort1.Write(command, 0, command.Length);

                buffer = new byte[504];

                // Đọc dữ liệu phản hồi từ máy bơm xăng
                bytesRead = serialPort1.Read(buffer, 0, buffer.Length);
                bgWorkerTestOil.ReportProgress(0);
            }
            catch (Exception ex)
            {
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Oil Testing error", ex.Message);
            }
        }

        private void BW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string a = String.Empty;
            bool isFinish = false;
            rtb_log.Text += "\r\n\n";
            for (int i = 0; i < bytesRead; i++)
            {
                a += buffer[i].ToString() + " ";
            }
            rtb_log.Text += a;

            if (bytesRead == 8)
            {
                if (!String.IsNullOrEmpty(a) && a.Count(x => x == '0') == bytesRead)
                {
                    isFinish = true;
                }
                int intMass = buffer[3] << 16 | buffer[4] << 8 | buffer[5];
                actualMass = (Convert.ToDouble(intMass) / 100).ToString();
            }
            //lbStatus.Text = "Running...";
            lbStatus.Text = actualMass;

            if (isFinish || (testMass - Convert.ToDouble(actualMass)) < Settings.Default.toleranceMass)
            {
                lbStatus.Text = testMass.ToString();
                if (tmrCallBgWorker != null)
                {
                    tmrCallBgWorker.Stop();
                    tmrCallBgWorker.Tick -= new EventHandler(timer_nextRun_Tick);
                    bgWorkerTestOil.DoWork -= new DoWorkEventHandler(BW_DoWork);
                    bgWorkerTestOil.ProgressChanged -= BW_ProgressChanged;
                    bgWorkerTestOil.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(BW_RunWorkerCompleted);
                }
                if (serialPort1.IsOpen)
                    CloseSerialPort();
                Settings.Default.timeOilTested = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                Settings.Default.isOilTested = true;
                Settings.Default.Save();
                //Thread.Sleep(500);
                //do
                //{
                //    PLCConnector pLC = new PLCConnector(Settings.Default.plc_ip, 0, 0, out ConnectionPLC);
                //    pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("OTV", "start")), Convert.ToInt32(ini.Read("OTV", "bit")));
                //} while (ConnectionPLC != 0);

                CTMessageBox.Show("TEST SUCCESSFULLY", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }

        }

        private void BW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) { }

        private void txbTestMass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void rtb_log_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            rtb_log.SelectionStart = rtb_log.Text.Length;
            // scroll it automatically
            rtb_log.ScrollToCaret();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            do
            {
                PLCConnector pLC = new PLCConnector(Settings.Default.plc_ip, 0, 0, out ConnectionPLC);
                pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("OTV", "start")), Convert.ToInt32(ini.Read("OTV", "bit")));
            } while (ConnectionPLC != 0);
            if (serialPort1.IsOpen)
                CloseSerialPort();
            this.Close();
        }
        void tmrEnsureWorkerGetsCalled_Callback(object obj)
        {
            // this timer was started as the bgworker was busy before now it will try to call the bgworker again
            if (Monitor.TryEnter(lockObject))
            {
                try
                {
                    if (!bgWorkerTestOil.IsBusy)
                    {
                        bgWorkerTestOil.RunWorkerAsync();
                    }
                }
                finally
                {
                    Monitor.Exit(lockObject);
                }
                tmrEnsureWorkerGetsCalled = null;
            }
        }
    }
}

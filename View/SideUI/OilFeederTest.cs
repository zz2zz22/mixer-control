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

namespace mixer_control_globalver.View.SideUI
{
    public partial class OilFeederTest : Form
    {
        public static int ConnectionPLC;

        int db = Settings.Default.database_no;
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
            btnStartTesting.Enabled = false;
            try
            {
                lbStatus.Text = "Executing...";
                if (double.TryParse(txbTestMass.Text.Trim(), out testMass))
                {

                    lbStatus.Text = "Sending signal to PLC...";
                    do
                    {
                        PLCConnector pLC = new PLCConnector(Settings.Default.plc_ip, 0, 0, out ConnectionPLC);
                        pLC.WriteBoolToPLC(true, db, Convert.ToInt32(ini.Read("OTV", "start")), Convert.ToInt32(ini.Read("OTV", "bit")));
                    } while (ConnectionPLC != 0);

                    Thread.Sleep(200);

                    lbStatus.Text = "Transfer oil mass to pump machine...";
                    SubMethods.FuelSetting(serialPort1, testMass);

                    Thread.Sleep(200);

                    SubMethods.SendCommand(serialPort1, new byte[] { 0x5A, 0x01, 0x01, 0x5C, 0xA5 });

                    LoadBackgroundWorker();
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

        private void OilFeederTest_Load(object sender, EventArgs e)
        {
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
                actualMass = SubMethods.ReadCommand(serialPort1, new byte[] { 0x5A, 0x01, 0x04, 0x5F, 0xA5 }).ToString();
            }
            catch (Exception ex)
            {
                CloseSerialPort();
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Oil Testing error", ex.Message);
            }
            bgWorkerTestOil.ReportProgress(0);
        }

        private void BW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lbStatus.Text = "Running...";
            //lbStatus.Text = actualMass;

            if (double.TryParse(actualMass, out double acMass))
            {
                if ((testMass - acMass) < Settings.Default.toleranceMass)
                {
                    if (tmrCallBgWorker != null)
                    {
                        tmrCallBgWorker.Stop();
                        tmrCallBgWorker.Tick -= new EventHandler(timer_nextRun_Tick);
                        bgWorkerTestOil.DoWork -= new DoWorkEventHandler(BW_DoWork);
                        bgWorkerTestOil.ProgressChanged -= BW_ProgressChanged;
                        bgWorkerTestOil.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(BW_RunWorkerCompleted);
                    }
                    Settings.Default.timeOilTested = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    Settings.Default.isOilTested = true;
                    Settings.Default.Save();
                    Thread.Sleep(500);
                    do
                    {
                        PLCConnector pLC = new PLCConnector(Settings.Default.plc_ip, 0, 0, out ConnectionPLC);
                        pLC.WriteBoolToPLC(false, db, Convert.ToInt32(ini.Read("OTV", "start")), Convert.ToInt32(ini.Read("OTV", "bit")));
                    } while (ConnectionPLC != 0);

                    if (serialPort1.IsOpen)
                        CloseSerialPort();

                    CTMessageBox.Show("TEST SUCCESSFULLY", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
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

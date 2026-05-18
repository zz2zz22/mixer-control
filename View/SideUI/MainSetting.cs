using Microsoft.WindowsAPICodePack.Dialogs;
using mixer_control_globalver.Controller;
using mixer_control_globalver.Controller.IniFile;
using mixer_control_globalver.Properties;
using mixer_control_globalver.View.CustomControls;
using System;
using System.Configuration;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Globalization;
using mixer_control_globalver.Controller.LogFile;
using System.Threading.Tasks;

namespace mixer_control_globalver.View.SideUI
{
    public partial class MainSetting : Form
    {
        bool isExitApplication = false;
        IniFileGenerator ini = new IniFileGenerator(AppDomain.CurrentDomain.BaseDirectory + "\\data\\setting.ini");
        public MainSetting()
        {
            InitializeComponent();

            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void CloseSerialPort(SerialPort serialPort)
        {
            isExitApplication = true;
            Thread.Sleep(serialPort.ReadTimeout); //Wait for reading threads to finish
            serialPort.Close();
            isExitApplication = false;
        }
        private void LoadNotSettingValue()
        {
            ComboBox notSettingList = new ComboBox();
            for (int i = 0; i < TemporaryVariables.settingDT.Rows.Count; i++)
            {
                if (String.IsNullOrEmpty(ini.Read(TemporaryVariables.settingDT.Rows[i]["value_member"].ToString(), "start")))
                {
                    notSettingList.Items.Add(TemporaryVariables.settingDT.Rows[i]["display_member"].ToString());
                }
            }
            if (notSettingList.Items.Count > 0)
            {
                if (notSettingList.Items.Count > 1)
                {
                    lbSettingAnnounce.Text = "Có giá trị \"" + notSettingList.Items[0].ToString() + "\" và " + (notSettingList.Items.Count - 1) + " giá trị khác chưa được cài đặt!";
                }
                else
                {
                    lbSettingAnnounce.Text = "Có giá trị \"" + notSettingList.Items[0].ToString() + "\" chưa được cài đặt!";
                }
            }
            else
                lbSettingAnnounce.Text = String.Empty;
        }
        private void ControlDataLoad()
        {
            string[] ports = SerialPort.GetPortNames();
            cbComPort.Items.AddRange(ports);
            cbComPort.Text = SettingsManager.GetSetting(s => s.OilSupplyComPort);

            txbOilPumpIP.Text = SettingsManager.GetSetting(s => s.OilPumpIp);
            txbOilPumpPort.Text = SettingsManager.GetSetting(s => s.OilPumpPort.ToString());

            cbPump2ComPort.Items.AddRange(ports);
            cbPump2ComPort.Text = SettingsManager.GetSetting(s => s.SecondaryOilSupplyComPort);

            cbDiameterComPort.Items.AddRange(ports);
            cbDiameterComPort.Text = SettingsManager.GetSetting(s => s.FlowMeterComPort);

            cbBaudRate.Text = SettingsManager.GetSetting(s => s.OilSupplyBaudRate);
            cbDataBits.Text = SettingsManager.GetSetting(s => s.OilSupplyDataBits);
            cbStopBits.Text = SettingsManager.GetSetting(s => s.OilSupplyStopBits);
            cbParityBits.Text = SettingsManager.GetSetting(s => s.OilSupplyParity);

            btnSaveOffset.ButtonText = "Save Offset Setting";

            lbSettingAnnounce.Text = String.Empty;
            txbPLCIpSetting.Text = SettingsManager.GetSetting(s => s.PlcIp);
            txbDatabaseNo.Text = SettingsManager.GetSetting(s => s.DatabaseNumber).ToString(CultureInfo.InvariantCulture);
            txbMotorMaxSpeed.Text = SettingsManager.GetSetting(s => s.MaxSpeed).ToString(CultureInfo.InvariantCulture);
            txbMotorDiameter.Text = SettingsManager.GetSetting(s => s.SpindleDiameter).ToString(CultureInfo.InvariantCulture);
            txbSensorDiameter.Text = SettingsManager.GetSetting(s => s.SensorDiameter).ToString(CultureInfo.InvariantCulture);
            txbTransmissionRatio.Text = SettingsManager.GetSetting(s => s.TransmissionRatio).ToString(CultureInfo.InvariantCulture);
            txbAuthorSkipPass.Text = SettingsManager.GetSetting(s => s.SkipStepPassword);
            txbTolerance.Text = SettingsManager.GetSetting(s => s.OilToleranceMass).ToString(CultureInfo.InvariantCulture);

            txbOilSupplyAttempts.Text = SettingsManager.GetSetting(s => s.OilSupplyAttempts).ToString(CultureInfo.InvariantCulture);
            txbVolumnCompensation.Text = SettingsManager.GetSetting(s => s.VolumnCompensation).ToString(CultureInfo.InvariantCulture);
            txbVolumnCompareValue.Text = SettingsManager.GetSetting(s => s.VolumnCompareValue).ToString(CultureInfo.InvariantCulture);

            switchOilMode.SwitchState = SettingsManager.GetSetting(s => s.OilSupplyEnabled) ?
                XanderUI.XUISwitch.State.On :
                XanderUI.XUISwitch.State.Off;

            switchStopMode.SwitchState = SettingsManager.GetSetting(s => s.StopMachineBetweenRuns) ?
                XanderUI.XUISwitch.State.On :
                XanderUI.XUISwitch.State.Off;

            switchOpenLit.SwitchState = SettingsManager.GetSetting(s => s.OpenMixerLidAfterRun) ?
                XanderUI.XUISwitch.State.On :
                XanderUI.XUISwitch.State.Off;

            switchTest.SwitchState = SettingsManager.GetSetting(s => s.DeveloperMode) ?
                XanderUI.XUISwitch.State.On :
                XanderUI.XUISwitch.State.Off;

            switchAlertPowder.SwitchState = SettingsManager.GetSetting(s => s.ShowPowderAlert) ?
                XanderUI.XUISwitch.State.On :
                XanderUI.XUISwitch.State.Off;

            switchShowHiddenInfo.SwitchState = SettingsManager.GetSetting(s => s.ShowHiddenInfo) ?
                XanderUI.XUISwitch.State.On :
                XanderUI.XUISwitch.State.Off;

            switchSkipPassword.SwitchState = SettingsManager.GetSetting(s => s.SkipPasswordEnabled) ?
                XanderUI.XUISwitch.State.On :
                XanderUI.XUISwitch.State.Off;

            switchOpenLidMode.SwitchState = SettingsManager.GetSetting(s => s.AlwaysOpenMixerLid) ?
                XanderUI.XUISwitch.State.On :
                XanderUI.XUISwitch.State.Off;

            switchSaveReport.SwitchState = SettingsManager.GetSetting(s => s.SaveReportEnabled) ?
                XanderUI.XUISwitch.State.On :
                XanderUI.XUISwitch.State.Off;

            switchTestOilMultiple.SwitchState = SettingsManager.GetSetting(s => s.MultipleOilTestEnabled) ?
                XanderUI.XUISwitch.State.On :
                XanderUI.XUISwitch.State.Off;

            switchOilDiaMeasurement.SwitchState = SettingsManager.GetSetting(s => s.FlowMeterEnabled) ?
                XanderUI.XUISwitch.State.On :
                XanderUI.XUISwitch.State.Off;

            switchPowderBagCheck.SwitchState = SettingsManager.GetSetting(s => s.CheckPowderEnabled) ?
                XanderUI.XUISwitch.State.On :
                XanderUI.XUISwitch.State.Off;

            switchEnableSecondaryOilSup.SwitchState = SettingsManager.GetSetting(s => s.EnableSecondaryOilSupply) ?
                XanderUI.XUISwitch.State.On :
                XanderUI.XUISwitch.State.Off;

            cbxPLCValueSetting.DataSource = TemporaryVariables.settingDT;
            cbxPLCValueSetting.ValueMember = "value_member";
            cbxPLCValueSetting.DisplayMember = "display_member";

            cbxPLCValueSetting.SelectedIndex = -1;
            cbxLEDColor.SelectedIndex = SettingsManager.GetSetting(s => s.LedScreenColor) - 1;
            cbxLEDStyle.SelectedIndex = SettingsManager.GetSetting(s => s.LedScreenStyle);
            txbLEDIP.Text = SettingsManager.GetSetting(s => s.LedScreenIp);

            switchEnableExactMatch.SwitchState = SettingsManager.GetSetting(s => s.ExactMatchEnabled) ?
                XanderUI.XUISwitch.State.On :
                XanderUI.XUISwitch.State.Off;

            switchOilPumpIpConnectorEnabled.SwitchState = SettingsManager.GetSetting(s => s.OilPumpNewIpConnectorEnabled) ?
                XanderUI.XUISwitch.State.On :
                XanderUI.XUISwitch.State.Off;

            LoadNotSettingValue();
        }
        private void MainSetting_Load(object sender, EventArgs e)
        {
            ControlDataLoad();
        }

        private void panelHeader_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void MainSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            SettingsManager.UpdateSettings(s =>
            {
                s.PlcIp = txbPLCIpSetting.Text.Trim();
                s.DatabaseNumber = Convert.ToInt32(txbDatabaseNo.Text.Trim());
                s.MaxSpeed = Convert.ToInt32(txbMotorMaxSpeed.Text.Trim());
                s.SensorDiameter = double.Parse(txbSensorDiameter.Text.Trim(), CultureInfo.InvariantCulture);
                s.SpindleDiameter = double.Parse(txbMotorDiameter.Text.Trim(), CultureInfo.InvariantCulture);
                s.TransmissionRatio = double.Parse(txbTransmissionRatio.Text.Trim(), CultureInfo.InvariantCulture);
                s.OilToleranceMass = double.Parse(txbTolerance.Text, CultureInfo.InvariantCulture);
                s.OilSupplyEnabled = switchOilMode.SwitchState == XanderUI.XUISwitch.State.On ? true : false;
                s.StopMachineBetweenRuns = switchStopMode.SwitchState == XanderUI.XUISwitch.State.On ? true : false;
                s.OpenMixerLidAfterRun = switchOpenLit.SwitchState == XanderUI.XUISwitch.State.On ? true : false;
                s.DeveloperMode = switchTest.SwitchState == XanderUI.XUISwitch.State.On ? true : false;
                s.ShowPowderAlert = switchAlertPowder.SwitchState == XanderUI.XUISwitch.State.On ? true : false;
                s.ShowHiddenInfo = switchShowHiddenInfo.SwitchState == XanderUI.XUISwitch.State.On ? true : false;
                s.SkipPasswordEnabled = switchSkipPassword.SwitchState == XanderUI.XUISwitch.State.On ? true : false;
                s.AlwaysOpenMixerLid = switchOpenLidMode.SwitchState == XanderUI.XUISwitch.State.On ? true : false;
                s.SaveReportEnabled = switchSaveReport.SwitchState == XanderUI.XUISwitch.State.On ? true : false;
                s.MultipleOilTestEnabled = switchTestOilMultiple.SwitchState == XanderUI.XUISwitch.State.On ? true : false;
                s.FlowMeterEnabled = switchOilDiaMeasurement.SwitchState == XanderUI.XUISwitch.State.On ? true : false;
                s.CheckPowderEnabled = switchPowderBagCheck.SwitchState == XanderUI.XUISwitch.State.On ? true : false;
                s.EnableSecondaryOilSupply = switchEnableSecondaryOilSup.SwitchState == XanderUI.XUISwitch.State.On ? true : false;
                s.LedScreenIp = txbLEDIP.Text.Trim();
                s.LedScreenColor = cbxLEDColor.SelectedIndex + 1;
                s.LedScreenStyle = cbxLEDStyle.SelectedIndex;
                s.OilSupplyComPort = cbComPort.Text;
                s.SecondaryOilSupplyComPort = cbPump2ComPort.Text;
                s.FlowMeterComPort = cbDiameterComPort.Text;
                s.OilSupplyBaudRate = cbBaudRate.Text;
                s.OilSupplyDataBits = cbDataBits.Text;
                s.OilSupplyStopBits = cbStopBits.Text;
                s.OilSupplyParity = cbParityBits.Text;
                s.OilSupplyAttempts = int.Parse(txbOilSupplyAttempts.Text.Trim(), CultureInfo.InvariantCulture);
                s.VolumnCompensation = double.Parse(txbVolumnCompensation.Text.Trim(), CultureInfo.InvariantCulture);
                s.VolumnCompareValue = double.Parse(txbVolumnCompareValue.Text.Trim(), CultureInfo.InvariantCulture);

                s.ExactMatchEnabled = switchEnableExactMatch.SwitchState == XanderUI.XUISwitch.State.On ? true : false;
                s.OilPumpNewIpConnectorEnabled = switchOilPumpIpConnectorEnabled.SwitchState == XanderUI.XUISwitch.State.On ? true : false;

                s.OilPumpIp = txbOilPumpIP.Text.Trim();
                s.OilPumpPort = int.Parse(txbOilPumpPort.Text.Trim(), CultureInfo.InvariantCulture);
            });
           
            TemporaryVariables.InitSettingDT();
        }

        private void cbxPLCValueSetting_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                txbStartNo.Text = ini.Read(cbxPLCValueSetting.SelectedValue.ToString(), "start");
                txbBitNo.Text = ini.Read(cbxPLCValueSetting.SelectedValue.ToString(), "bit");
                txbStartNo.Focus();
            }
            catch (Exception) { throw; }
        }

        private void txbPLCIpSetting_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txbDatabaseNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.'))
            {
                e.Handled = true;
            }
        }

        private void btnSaveOffset_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(cbxPLCValueSetting.Text))
                {
                    ini.Write(cbxPLCValueSetting.SelectedValue.ToString(), "start", txbStartNo.Text);
                    ini.Write(cbxPLCValueSetting.SelectedValue.ToString(), "bit", txbBitNo.Text);

                    btnSaveOffset.ButtonText = "Lưu offset";

                    txbStartNo.Text = String.Empty;
                    txbBitNo.Text = String.Empty;
                    cbxPLCValueSetting.SelectedIndex = -1;
                    LoadNotSettingValue();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void txbStartNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.'))
            {
                e.Handled = true;
                txbBitNo.Focus();
            }
        }

        private void txbBitNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.'))
            {
                e.Handled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txbBitNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Invoke(new EventHandler(btnSaveOffset_Click));
            }
        }

        private void txbMotorMaxSpeed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.'))
            {
                e.Handled = true;
            }
        }

        private void txbMotorDiameter_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txbSensorDiameter_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnReportFolder_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            if (!String.IsNullOrEmpty(SettingsManager.GetSetting(s => s.ReportDirectory)))
            {
                dialog.InitialDirectory = SettingsManager.GetSetting(s => s.ReportDirectory);
            }
            else
            {
                dialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "Mixer_Reports";
            }
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                SettingsManager.UpdateSettings(s => s.ReportDirectory = dialog.FileName);
            }
        }

        private void btnTestConnect_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                CloseSerialPort(serialPort1);
            }
            else
            {
                try
                {
                    if (!String.IsNullOrEmpty(cbComPort.Text))
                    {
                        serialPort1.PortName = cbComPort.Text;
                        serialPort1.BaudRate = int.Parse(cbBaudRate.Text, CultureInfo.InvariantCulture);
                        serialPort1.DataBits = int.Parse(cbDataBits.Text, CultureInfo.InvariantCulture);
                        serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbStopBits.Text);
                        serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cbParityBits.Text);
                        serialPort1.ReadTimeout = 1000;
                        serialPort1.Open();
                        bool isConnected = SubMethods.CheckConnectStatus(serialPort1); // Đọc trạng thái máy
                        CloseSerialPort(serialPort1);
                        if (!isConnected)
                        {
                            CTMessageBox.Show("Connection to serialport fail : Port can not open.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        CTMessageBox.Show("Please choose a COM port first.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception err)
                {
                    CTMessageBox.Show(err.Message, "Serialport connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SystemLog.Output(SystemLog.MSG_TYPE.Err, "Serialport connection error", err.Message);
                }
            }
        }

        private void txbTolerance_KeyPress(object sender, KeyPressEventArgs e)
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

        private void MainSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            SubMethods.BackupUserSettings();
        }

        private void btnTestPort2_Click(object sender, EventArgs e)
        {
            if (serialPort2.IsOpen)
            {
                CloseSerialPort(serialPort2);
            }
            else
            {
                try
                {
                    if (!String.IsNullOrEmpty(cbPump2ComPort.Text))
                    {
                        serialPort2.PortName = cbPump2ComPort.Text;
                        serialPort2.BaudRate = Convert.ToInt32(cbBaudRate.Text);
                        serialPort2.DataBits = Convert.ToInt32(cbDataBits.Text);
                        serialPort2.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbStopBits.Text);
                        serialPort2.Parity = (Parity)Enum.Parse(typeof(Parity), cbParityBits.Text);
                        serialPort2.ReadTimeout = 1000;
                        serialPort2.Open();
                        bool isConnected = SubMethods.CheckConnectStatus(serialPort2); // Đọc trạng thái máy
                        CloseSerialPort(serialPort2);
                        if (!isConnected)
                        {
                            CTMessageBox.Show("Connection to serialport fail : Port can not open.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        CTMessageBox.Show("Please choose a COM port first.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception err)
                {
                    CTMessageBox.Show(err.Message, "Serialport connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SystemLog.Output(SystemLog.MSG_TYPE.Err, "Serialport connection error", err.Message);
                }
            }
        }

        private void btnTestFRConnect_Click(object sender, EventArgs e)
        {
            if (serialPort3.IsOpen)
            {
                CloseSerialPort(serialPort3);
            }
            else
            {
                try
                {
                    if (!String.IsNullOrEmpty(cbDiameterComPort.Text))
                    {
                        serialPort3.PortName = cbDiameterComPort.Text;
                        serialPort3.BaudRate = Convert.ToInt32(cbBaudRate.Text);
                        serialPort3.DataBits = Convert.ToInt32(cbDataBits.Text);
                        serialPort3.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbStopBits.Text);
                        serialPort3.Parity = (Parity)Enum.Parse(typeof(Parity), cbParityBits.Text);
                        serialPort3.ReadTimeout = 1000;
                        serialPort3.Open();
                        bool isConnected = SubMethods.CheckConnectStatus(serialPort3); // Đọc trạng thái máy
                        CloseSerialPort(serialPort3);
                        if (!isConnected)
                        {
                            CTMessageBox.Show("Connection to serialport fail : Port can not open.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        CTMessageBox.Show("Please choose a COM port first.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception err)
                {
                    CTMessageBox.Show(err.Message, "Serialport connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SystemLog.Output(SystemLog.MSG_TYPE.Err, "Serialport connection error", err.Message);
                }
            }
        }

        private void btnTransferUserSettings_Click(object sender, EventArgs e)
        {
            var task1 = Task.Run(() =>
            {
                SettingsManager.UpdateSettings(s =>
                {
                    s.Language = Settings.Default.language;
                    s.FormulaDirectory = Settings.Default.folder_directory;
                    s.SaveReportEnabled = Settings.Default.isSaveReport;
                    s.ReportDirectory = Settings.Default.report_directory;
                    s.EndReportEnabled = Settings.Default.isEndReport;
                    s.OilToleranceMass = Settings.Default.toleranceMass;
                    s.OilSupplyEnabled = Settings.Default.isOilFeed;
                    s.ShowHiddenInfo = Settings.Default.isShowHiddenInfo;
                    s.DeveloperMode = Settings.Default.isTesting;
                    s.MultipleOilTestEnabled = Settings.Default.isTestOilMultiple;
                    s.StopMachineBetweenRuns = Settings.Default.isStopBetweenStep;
                    s.OpenMixerLidAfterRun = Settings.Default.isSkipOpenLid;
                    s.AlwaysOpenMixerLid = Settings.Default.isOpenLidMode;
                    s.ShowPowderAlert = Settings.Default.isAlertPowder;
                    s.SkipPasswordEnabled = Settings.Default.isHaveSkipPassword;
                    s.SkipStepPassword = Settings.Default.authorSkipPassword;
                    s.OIlTested = Settings.Default.isOilTested;
                    s.OIlTestedTime = Settings.Default.timeOilTested;
                    s.CheckPowderEnabled = Settings.Default.isCheckPowderSupply;
                    s.FlowMeterEnabled = Settings.Default.isOilMeasurement;

                    s.PlcIp = Settings.Default.plc_ip;
                    s.DatabaseNumber = Settings.Default.database_no;
                    s.MaxSpeed = Settings.Default.max_speed;
                    s.SpindleDiameter = Settings.Default.spindle_diameter;
                    s.SensorDiameter = Settings.Default.sensor_diameter;
                    s.TransmissionRatio = Settings.Default.transmission_ratio;

                    s.OilSupplyComPort = Settings.Default.comPort;
                    s.SecondaryOilSupplyComPort = Settings.Default.comPortP2;
                    s.OilSupplyBaudRate = Settings.Default.baudRate;
                    s.OilSupplyDataBits = Settings.Default.dataBits;
                    s.OilSupplyStopBits = Settings.Default.stopBits;
                    s.OilSupplyParity = Settings.Default.parityBits;

                    s.LedScreenIp = Settings.Default.led_ip;
                    s.LedScreenColor = Settings.Default.led_color;
                    s.LedScreenStyle = Settings.Default.led_style;

                    s.FlowMeterComPort = Settings.Default.diameterComPort;
                });
            });
            task1.Wait();

            SettingsManager.SaveSettings();

            ControlDataLoad();

            CTMessageBox.Show("Update complete!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txbTransmissionRatio_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}

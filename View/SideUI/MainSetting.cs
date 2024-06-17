using Microsoft.WindowsAPICodePack.Dialogs;
using mixer_control_globalver.Controller;
using mixer_control_globalver.Controller.IniFile;
using mixer_control_globalver.Properties;
using mixer_control_globalver.View.CustomControls;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace mixer_control_globalver.View.SideUI
{
    public partial class MainSetting : Form
    {
        string message = String.Empty, caption = String.Empty;
        IniFile ini = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "\\data\\setting.ini");
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

        private void MainSetting_Load(object sender, EventArgs e)
        {
            btnSaveOffset.ButtonText = "Lưu cài đặt offset";

            lbSettingAnnounce.Text = String.Empty;
            txbPLCIpSetting.Text = Settings.Default.plc_ip;
            txbDatabaseNo.Text = Settings.Default.database_no.ToString();
            txbMotorMaxSpeed.Text = Settings.Default.max_speed.ToString();
            txbMotorDiameter.Text = Settings.Default.spindle_diameter.ToString();
            txbSensorDiameter.Text = Settings.Default.sensor_diameter.ToString();
            txbTransmissionRatio.Text = Settings.Default.transmission_ratio.ToString();
            txbOilFeederIP.Text = Settings.Default.oil_feeder_ip;
            txbOilFeederDatabase.Text = Settings.Default.oil_feeder_db.ToString();
            txbAuthorSkipPass.Text = Settings.Default.authorSkipPassword;

            if(Settings.Default.isOilFeed)
            {
                switchOilMode.SwitchState = XanderUI.XUISwitch.State.On;
            }
            else
            {
                switchOilMode.SwitchState = XanderUI.XUISwitch.State.Off;
            }

            if (Settings.Default.isStopBetweenStep)
            {
                switchStopMode.SwitchState = XanderUI.XUISwitch.State.On;
            }
            else
            {
                switchStopMode.SwitchState = XanderUI.XUISwitch.State.Off;
            }

            if (Settings.Default.isSkipOpenLid)
            {
                switchOpenLit.SwitchState = XanderUI.XUISwitch.State.On;
            }
            else
            {
                switchOpenLit.SwitchState = XanderUI.XUISwitch.State.Off;
            }

            if (Settings.Default.isHideReverse)
            {
                switchHideReverse.SwitchState = XanderUI.XUISwitch.State.On;
            }
            else
            {
                switchHideReverse.SwitchState = XanderUI.XUISwitch.State.Off;
            }

            if (Settings.Default.isTesting)
            {
                switchTest.SwitchState = XanderUI.XUISwitch.State.On;
            }
            else
            {
                switchTest.SwitchState = XanderUI.XUISwitch.State.Off;
            }

            if (Settings.Default.isAlertPowder)
            {
                switchAlertPowder.SwitchState = XanderUI.XUISwitch.State.On;
            }
            else
            {
                switchAlertPowder.SwitchState = XanderUI.XUISwitch.State.Off;
            }

            if (Settings.Default.isShowHiddenInfo)
            {
                switchShowHiddenInfo.SwitchState = XanderUI.XUISwitch.State.On;
            }
            else
            {
                switchShowHiddenInfo.SwitchState = XanderUI.XUISwitch.State.Off;
            }

            if (Settings.Default.isHaveSkipPassword)
            {
                switchSkipPassword.SwitchState = XanderUI.XUISwitch.State.On;
            }
            else
            {
                switchSkipPassword.SwitchState = XanderUI.XUISwitch.State.Off;
            }

            cbxPLCValueSetting.DataSource = TemporaryVariables.settingDT;
            cbxPLCValueSetting.ValueMember = "value_member";
            cbxPLCValueSetting.DisplayMember = "display_member";

            cbxPLCValueSetting.SelectedIndex = -1;

            LoadNotSettingValue();
        }

        private void panelHeader_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void MainSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.plc_ip = txbPLCIpSetting.Text.Trim();
            Settings.Default.database_no = Convert.ToInt32(txbDatabaseNo.Text.Trim());
            Settings.Default.max_speed = Convert.ToInt32(txbMotorMaxSpeed.Text.Trim());
            Settings.Default.spindle_diameter = Convert.ToDouble(txbMotorDiameter.Text.Trim());
            Settings.Default.sensor_diameter = Convert.ToDouble(txbSensorDiameter.Text.Trim());
            Settings.Default.transmission_ratio = Convert.ToDouble(txbTransmissionRatio.Text.Trim());
            Settings.Default.oil_feeder_ip = txbOilFeederIP.Text.Trim();
            Settings.Default.oil_feeder_db = Convert.ToInt32(txbOilFeederDatabase.Text.Trim());
            Settings.Default.authorSkipPassword = txbAuthorSkipPass.Text.Trim();

            if (switchOilMode.SwitchState == XanderUI.XUISwitch.State.On)
                Settings.Default.isOilFeed = true;
            else
                Settings.Default.isOilFeed = false;

            if (switchStopMode.SwitchState == XanderUI.XUISwitch.State.On)
                Settings.Default.isStopBetweenStep = true;
            else
                Settings.Default.isStopBetweenStep = false;

            if (switchOpenLit.SwitchState == XanderUI.XUISwitch.State.On)
                Settings.Default.isSkipOpenLid = true;
            else
                Settings.Default.isSkipOpenLid = false;

            if (switchHideReverse.SwitchState == XanderUI.XUISwitch.State.On)
                Settings.Default.isHideReverse = true;
            else
                Settings.Default.isHideReverse = false;

            if (switchTest.SwitchState == XanderUI.XUISwitch.State.On)
                Settings.Default.isTesting = true;
            else
                Settings.Default.isTesting = false;

            if (switchAlertPowder.SwitchState == XanderUI.XUISwitch.State.On)
                Settings.Default.isAlertPowder = true;
            else
                Settings.Default.isAlertPowder = false;

            if (switchShowHiddenInfo.SwitchState == XanderUI.XUISwitch.State.On)
                Settings.Default.isShowHiddenInfo = true;
            else
                Settings.Default.isShowHiddenInfo = false;

            if (switchSkipPassword.SwitchState == XanderUI.XUISwitch.State.On)
                Settings.Default.isHaveSkipPassword = true;
            else
                Settings.Default.isHaveSkipPassword = false;

            Settings.Default.Save();
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

                    message = "Lưu địa chỉ offset thành công!\r\n保存Offset地址成功!";
                    caption = "Thông tin / 信息";
                    CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            if (!String.IsNullOrEmpty(Properties.Settings.Default.report_directory))
            {
                dialog.InitialDirectory = Properties.Settings.Default.report_directory;
            }
            else
            {
                dialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "Mixer_Reports";
            }
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Properties.Settings.Default.report_directory = dialog.FileName;
                Properties.Settings.Default.Save();
            }
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

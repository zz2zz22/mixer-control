using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using mixer_control_globalver.Controller;
using mixer_control_globalver.Controller.IniFile;
using mixer_control_globalver.Properties;

namespace mixer_control_globalver.View.SideUI
{
    public partial class MainSetting : Form
    {
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
                    lbSettingAnnounce.Text = "Có giá trị \"" + notSettingList.Items[0].ToString() + "\" và " + (notSettingList.Items.Count - 1) + " giá trị khác chưa được cài đặt!"
                        + Environment.NewLine + "There are variable \"" + notSettingList.Items[0].ToString() + "\" and " + (notSettingList.Items.Count - 1) + " other not yet setting!";
                else
                    lbSettingAnnounce.Text = "Có giá trị \"" + notSettingList.Items[0].ToString() + "\" chưa được cài đặt!"
                        + Environment.NewLine + "There is variable \"" + notSettingList.Items[0].ToString() + "\" not yet setting!";
            }
            else
                lbSettingAnnounce.Text = String.Empty;
        }

        private void MainSetting_Load(object sender, EventArgs e)
        {
            btnSaveBaseSetting.ButtonText = "Lưu cài đặt cơ bản" + Environment.NewLine + "Save base setting";
            btnSaveOffset.ButtonText = "Lưu cài đặt offset" + Environment.NewLine + "Save offset setting";

            lbSettingAnnounce.Text = String.Empty;
            txbPLCIpSetting.Text = Settings.Default.plc_ip;
            txbDatabaseNo.Text = Settings.Default.database_no.ToString();
            txbMotorMaxSpeed.Text = Settings.Default.max_speed.ToString();
            txbMotorDiameter.Text = Settings.Default.spindle_diameter.ToString();
            txbSensorDiameter.Text = Settings.Default.sensor_diameter.ToString();
            txbTransmissionRatio.Text = Settings.Default.transmission_ratio.ToString();

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
                string message = String.Empty, caption = String.Empty;
                if (TemporaryVariables.language == 0)
                {
                    message = "Lưu địa chỉ offset thành công!" + Environment.NewLine + "Successfully saved offset address !";
                    caption = "Thông tin / Information";
                }
                ini.Write(cbxPLCValueSetting.SelectedValue.ToString(), "start", txbStartNo.Text);
                ini.Write(cbxPLCValueSetting.SelectedValue.ToString(), "bit", txbBitNo.Text);
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnSaveBaseSetting.ButtonText = "Lưu cài đặt" + Environment.NewLine + "Save Settings";
                btnSaveOffset.ButtonText = "Lưu offset" + Environment.NewLine + "Save Offsets";

                txbStartNo.Text = String.Empty;
                txbBitNo.Text = String.Empty;
                cbxPLCValueSetting.SelectedIndex = -1;
                LoadNotSettingValue();
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

        private void btnSaveBaseSetting_Click(object sender, EventArgs e)
        {
            string message = String.Empty, caption = String.Empty;
            if (TemporaryVariables.language == 0)
            {
                message = "Lưu cài đặt thành công!" + Environment.NewLine + "Successfully saved base settings !";
                caption = "Thông tin / Information";
            }

            Settings.Default.plc_ip = txbPLCIpSetting.Text;
            Settings.Default.database_no = Convert.ToInt32(txbDatabaseNo.Text.Trim());
            Settings.Default.max_speed = Convert.ToInt32(txbMotorMaxSpeed.Text.Trim());
            Settings.Default.spindle_diameter = Convert.ToDouble(txbMotorDiameter.Text.Trim());
            Settings.Default.sensor_diameter = Convert.ToDouble(txbSensorDiameter.Text.Trim());
            Settings.Default.transmission_ratio = Convert.ToDouble(txbTransmissionRatio.Text.Trim());
            Settings.Default.Save();

            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

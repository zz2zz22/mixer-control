using mixer_control_globalver.Controller;
using mixer_control_globalver.Properties;
using mixer_control_globalver.View.CustomControls;
using mixer_control_globalver.View.MainUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace mixer_control_globalver.View.CustomComponent
{
    public partial class PasswordConfirm : Form
    {
        string message = String.Empty, caption = String.Empty;
        public PasswordConfirm()
        {
            InitializeComponent();
        }
        private void CheckPassword(string password)
        {
            if (password == "techlink@123")
            {
                ChooseSpec.isConfirmed = true;
                this.Close();
            }
            else
            {
                if (Settings.Default.language == 0)
                {
                    message = "Sai mật khẩu!\r\nWrong password";
                    caption = "Thông tin / Information";
                }
                else if (Settings.Default.language == 1)
                {
                    message = "Sai mật khẩu!\r\n密码错误！";
                    caption = "Lỗi / 错误";
                }
                else if (Settings.Default.language == 2)
                {
                    message = "Wrong password";
                    caption = "Information";
                }
                else if (Settings.Default.language == 3)
                {
                    message = "Sai mật khẩu!";
                    caption = "Thông tin";
                }
                else if (Settings.Default.language == 4)
                {
                    message = "密码错误！";
                    caption = "错误";
                }
                CTMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                CheckPassword(txbPassword.Text.Trim());
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PasswordConfirm_Load(object sender, EventArgs e)
        {
            if (Settings.Default.language == 0)
            {
                lbAnnounce.Text = "Nhập mật khẩu\r\nEnter password";
                btnConfirm.ButtonText = "Xác nhận\r\nConfirm";
            }
            else if (Settings.Default.language == 1)
            {
                lbAnnounce.Text = "Nhập mật khẩu\r\n输入密码";
                btnConfirm.ButtonText = "Xác nhận\r\n确认";
            }
            else if (Settings.Default.language == 2)
            {
                lbAnnounce.Text = "Enter password";
                btnConfirm.ButtonText = "Confirm";
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            CheckPassword(txbPassword.Text.Trim());
        }
    }
}

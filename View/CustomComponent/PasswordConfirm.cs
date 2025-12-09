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
        public PasswordConfirm()
        {
            switch (SettingsManager.GetSetting(s => s.Language))
            {
                case 0:
                    SubMethods.SetLanguage("vi-VN");
                    break;
                case 1:
                    SubMethods.SetLanguage("zh-CN");
                    break;
                case 2:
                    SubMethods.SetLanguage("en-US");
                    break;
                default:
                    SubMethods.SetLanguage("");
                    break;
            }
            InitializeComponent();
        }
        private void CheckPassword(string password)
        {
            password = password.Trim().ToLower();
            if (password == "techlink@123")
            {
                ChooseSpec.isConfirmed = true;
                this.Close();
            }
            else if (password == SettingsManager.GetSetting(s => s.SkipStepPassword).Trim().ToLower())
            {
                AutomationInfo.isAuthorSkip = true;
                this.Close();
            }
            else
            {
                this.Close();
            }
        }

        private void txbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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
            lbAnnounce.Text = GlobalStrings.Label_PasswordConfirm;
            btnConfirm.ButtonText = GlobalStrings.btnConfirm;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            CheckPassword(txbPassword.Text.Trim());
        }
    }
}

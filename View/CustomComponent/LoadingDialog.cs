using mixer_control_globalver.Controller;
using mixer_control_globalver.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace mixer_control_globalver.View.CustomComponent
{
    public partial class LoadingDialog : Form
    {
        public LoadingDialog()
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
            this.lb1.Text = GlobalStrings.Message_Processing;
            this.lb1.Font = new Font(GlobalStrings.Text_Font, 12, FontStyle.Bold);
        }
    }
}

using mixer_control_globalver.Controller;
using mixer_control_globalver.Properties;
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
    public partial class LoadingDialog : Form
    {
        public LoadingDialog()
        {
            InitializeComponent();
            switch (SettingsManager.GetSetting(s => s.Language))
            {
                case 0:
                    lb1.Text = "Đang xử lý dữ liệu ...";
                    break;
                case 1:
                    lb1.Text = "处理数据...";
                    break;
                case 2:
                    lb1.Text = "Loading data ...";
                    break;
                default:
                    lb1.Text = "Đang xử lý dữ liệu ...";
                    break;
            }
        }
    }
}

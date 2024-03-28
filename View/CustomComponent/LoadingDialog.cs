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
            if (Settings.Default.language == 0)
            {
                lb1.Text = "Đang xử lý dữ liệu ...";
            }
            else if (Settings.Default.language == 1)
            {
                lb1.Text = "处理数据...";
            }
            else if (Settings.Default.language == 2)
            {
                lb1.Text = "Loading data ...";
            }
        }
    }
}

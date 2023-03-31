using mixer_control_globalver.Controller;
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
            if (TemporaryVariables.language == 0)
            {
                lb1.Text = "Đang thực hiện, vui lòng chờ.\r\nProcessing, please wait.";
            }
            else if (TemporaryVariables.language == 1)
            {
                lb1.Text = "Đang thực hiện, vui lòng chờ.\r\n加载中，请稍等。";
            }
        }
        public void UpdateProgress(int progress, string announce)
        {
            lbAnnounce.BeginInvoke(
                new Action(() =>
                {
                    lbAnnounce.Text = announce;
                }));
            pgbProcess.BeginInvoke(
                new Action(() =>
                {
                    pgbProcess.Value = progress;
                }
                ));
            lbPercentage.BeginInvoke(
                new Action(() =>
                {
                    lbPercentage.Text = progress + "%";
                }));
        }
    }
}

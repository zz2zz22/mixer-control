using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mixer_control_globalver.View.CustomComponent
{
    public partial class LoadingDialog : Form
    {
        public LoadingDialog()
        {
            InitializeComponent();
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

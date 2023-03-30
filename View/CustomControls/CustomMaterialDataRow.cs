using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Media.Media3D;

namespace mixer_control_globalver.View.CustomControls
{
    public partial class CustomMaterialDataRow : UserControl
    {
        public CustomMaterialDataRow(string matCode, string weight, bool status)
        {
            InitializeComponent();
            lbMatCode.Text = matCode;
            lbWeight.Text = weight;

            if (status)
            {
                panelStatus.BackColor = Color.Yellow;
                lbStatus.ForeColor = Color.Black;
                lbStatus.Text = "ĐÃ XÁC NHẬN" + Environment.NewLine + "CONFIRMED";
            }
            else
            {
                panelStatus.BackColor = Color.Red;
                lbStatus.ForeColor = Color.White;
                lbStatus.Text = "CHƯA XÁC NHẬN" + Environment.NewLine + "NOT CONFIRMED";
            }

        }
    }
}

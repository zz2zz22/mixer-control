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
using mixer_control_globalver.Controller;

namespace mixer_control_globalver.View.CustomControls
{
    public partial class CustomMaterialDataRow : UserControl
    {
        public CustomMaterialDataRow(string matCode, string weight, bool status)
        {
            InitializeComponent();
            lbMatCode.Text = matCode;
            lbWeight.Text = weight + " kg";

            if (status)
            {
                panelStatus.BackColor = Color.Yellow;
                lbStatus.ForeColor = Color.Black;
                if (TemporaryVariables.language == 0)
                {
                    lbStatus.Text = "ĐÃ XÁC NHẬN\r\nCONFIRMED";
                }
                else if (TemporaryVariables.language == 1)
                {
                    lbStatus.Text = "ĐÃ XÁC NHẬN\r\n已确认";
                }
            }
            else
            {
                panelStatus.BackColor = Color.Red;
                lbStatus.ForeColor = Color.White;
                if (TemporaryVariables.language == 0)
                {
                    lbStatus.Text = "CHƯA XÁC NHẬN\r\nNOT CONFIRMED";
                }
                else if (TemporaryVariables.language == 1)
                {
                    lbStatus.Text = "CHƯA XÁC NHẬN\r\n未确认";
                } 
            }
        }
    }
}

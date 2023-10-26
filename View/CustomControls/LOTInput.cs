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

namespace mixer_control_globalver.View.CustomControls
{
    public partial class LOTInput : Form
    {
        public LOTInput()
        {
            InitializeComponent();
        }
        private void inputLOT()
        {
            if (string.IsNullOrEmpty(txbLOTNo.Text.Trim()))
            {
                DialogResult dialogResult = CTMessageBox.Show("Gernerate Qr with empty LOT?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                {
                    TemporaryVariables.lotNo = string.Empty;
                    this.Close();
                }
            }
            else
            {
                DialogResult dialogResult = CTMessageBox.Show("Save this LOT \""+ txbLOTNo.Text.Trim() + "\" ?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                {
                    TemporaryVariables.lotNo = txbLOTNo.Text.Trim();
                    this.Close();
                }
            }
        }

        private void btnFinishAll_Click(object sender, EventArgs e)
        {
            inputLOT();
        }

        private void txbLOTNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                inputLOT();
            }
        }
    }
}

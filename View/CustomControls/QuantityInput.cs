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

namespace mixer_control_globalver.View.CustomControls
{
    public partial class QuantityInput : Form
    {
        string message = String.Empty, caption = String.Empty;
        public QuantityInput()
        {
            InitializeComponent();
        }

        private void inputWeight()
        {
            if (TemporaryVariables.language == 0)
            {
                message = "Xác nhận liệu \"" + TemporaryVariables.materialCode + "\" đã sẵn sàng ?\r\nConfirm \"" + TemporaryVariables.materialCode + "\" material is ready ?";
                caption = "Xác nhận / Confirmation";
            }
            else if (TemporaryVariables.language == 1)
            {
                message = "Xác nhận liệu \"" + TemporaryVariables.materialCode + "\" đã sẵn sàng ?\r\n确认原料 \"" + TemporaryVariables.materialCode + "\" 已经准备好？";
                caption = "Xác nhận / 确认";
            }
            else if (Settings.Default.language == 2)
            {
                message = "Confirm \"" + TemporaryVariables.materialCode + "\" material is ready ?";
                caption = "Confirmation";
            }
            else if (Settings.Default.language == 3)
            {
                message = "Xác nhận liệu \"" + TemporaryVariables.materialCode + "\" đã sẵn sàng ?";
                caption = "Xác nhận";
            }
            else if (Settings.Default.language == 4)
            {
                message = "确认原料 \"" + TemporaryVariables.materialCode + "\" 已经准备好？";
                caption = "确认";
            }
            DialogResult dialog = CTMessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(txbQuantity.Text.Trim()))
                {
                    DialogResult dialogResult = CTMessageBox.Show("Save data with '0' weight?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.OK)
                    {
                        TemporaryVariables.isInputQuantity = true;
                        TemporaryVariables.inputQuantity = 0;
                        this.Close();
                    }
                }
                else
                {
                    DialogResult dialogResult = CTMessageBox.Show("Save weight ?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.OK)
                    {
                        TemporaryVariables.isInputQuantity = true;
                        TemporaryVariables.inputQuantity = Convert.ToDouble(txbQuantity.Text.Trim());
                        this.Close();
                    }
                }
            }
            else
            {
                TemporaryVariables.isInputQuantity = false;
                TemporaryVariables.inputQuantity = 0;
                this.Close();
            }
        }

        private void txbQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                inputWeight();
            }
        }

        private void txbQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnFinishAll_Click(object sender, EventArgs e)
        {
            inputWeight();
        }
    }
}

using mixer_control_globalver.Controller;
using mixer_control_globalver.View.CustomComponent;
using mixer_control_globalver.View.CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace mixer_control_globalver.View.MainUI
{
    public partial class MaterialScale : Form
    {
        int flag;
        bool isFinished;
        string scaleMatName, scaleMatNo;
        string message = String.Empty, caption = String.Empty;
        public MaterialScale()
        {
            InitializeComponent();
        }
        public void LoadFlowLayoutMaterial(DataTable dt)
        {
            flag = 0;
            flpMaterialList.Controls.Clear();
            CustomMaterialDataRow customMaterial;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                if (!(bool)TemporaryVariables.materialDT.Rows[j]["is_confirmed"])
                {
                    flag = j;
                    break;
                }
                else
                {
                    flag++;
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!(bool)TemporaryVariables.materialDT.Rows[i]["is_confirmed"])
                    customMaterial = new CustomMaterialDataRow(dt.Rows[i]["mat_name"].ToString(), dt.Rows[i]["weight"].ToString(), false);
                else
                    customMaterial = new CustomMaterialDataRow(dt.Rows[i]["mat_name"].ToString(), dt.Rows[i]["weight"].ToString(), true);
                flpMaterialList.Controls.Add(customMaterial);
            }
        }

        private void MaterialScale_Load(object sender, EventArgs e)
        {
            isFinished = false;
            LoadFlowLayoutMaterial(TemporaryVariables.materialDT);
            NextMatScale(TemporaryVariables.materialDT, flag);

            if (TemporaryVariables.language == 0)
            {
                lb1.Text = "Danh sách nguyên vật liệu cần xác nhận:\r\nConfirmation required materials list:";
                lb2.Text = "Tên nguyên liệu:\r\nMaterial name:";

                btnConfirm.ButtonText = "Xác nhận nguyên liệu\r\nConfirm material";
                btnProceedAutomation.ButtonText = "Tiến hành chạy tự động\r\nBegin automation process";
            }
            else if (TemporaryVariables.language == 1)
            {
                lb1.Text = "Danh sách nguyên vật liệu cần xác nhận:\r\n需要确认的原料列表:";
                lb2.Text = "Tên nguyên liệu:\r\n原料名称:";

                btnConfirm.ButtonText = "Xác nhận nguyên liệu\r\n点击确认原料";
                btnProceedAutomation.ButtonText = "Tiến hành chạy tự động\r\n开始运行";
            }
            
        }



        private void btnProceedAutomation_Click(object sender, EventArgs e)
        {
            AppIntro.main.openAutomationTab();
        }

        private void NextMatScale(DataTable dt, int i)
        {
            if (i < dt.Rows.Count)
            {
                scaleMatName = dt.Rows[i]["mat_name"].ToString();
                lbMaterialName.Text = scaleMatName;
                scaleMatNo = dt.Rows[i]["mat_no"].ToString();
            }
            else
            {
                scaleMatName = null;
                scaleMatNo = null;
                isFinished = true;
                if (TemporaryVariables.language == 0)
                {
                    lbMaterialName.Text = "Đã hoàn thành xác nhận!\r\nConfirmation has completed!";
                }
                else if (TemporaryVariables.language == 1)
                {
                    lbMaterialName.Text = "Đã hoàn thành xác nhận!\r\n已确认！";
                }
            }
        }

        private void btnSaveWeight_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(scaleMatName))
            {
                if (TemporaryVariables.language == 0)
                {
                    message = "Xác nhận liệu \"" + scaleMatName + "\" đã sẵn sàng ?\r\nConfirm \"" + scaleMatName + "\" material is ready ?";
                    caption = "Xác nhận / Confirmation";
                }
                else if (TemporaryVariables.language == 1)
                {
                    message = "Xác nhận liệu \"" + scaleMatName + "\" đã sẵn sàng ?\r\n确认原料 \"" + scaleMatName + "\" 已经准备好？";
                    caption = "Xác nhận / 确认";
                }
                DialogResult dialog = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    foreach (DataRow dr in TemporaryVariables.materialDT.Rows)
                    {
                        if (dr["mat_name"].ToString() == scaleMatName && dr["mat_no"].ToString() == scaleMatNo)
                        {
                            dr["is_confirmed"] = true;
                            break;
                        }
                    }
                    LoadFlowLayoutMaterial(TemporaryVariables.materialDT);
                    NextMatScale(TemporaryVariables.materialDT, flag);
                }
            }
            else
            {
                if (TemporaryVariables.language == 0)
                {
                    message = "Đã hoàn thành xác nhận!\r\nConfirmation has completed!";
                    caption = "Thông tin / Information";
                }
                else if (TemporaryVariables.language == 1)
                {
                    message = "Đã hoàn thành xác nhận!\r\n已确认！";
                    caption = "Thông tin / Information";
                }
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

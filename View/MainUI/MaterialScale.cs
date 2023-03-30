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

            btnConfirm.ButtonText = "Xác nhận nguyên liệu" + Environment.NewLine + "Confirm material";
            btnProceedAutomation.ButtonText = "Tiến hành chạy tự động" + Environment.NewLine + "Begin automation process";
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
                lbMaterialName.Text = "Hoàn thành xác nhận" + Environment.NewLine + "Confirmation Completed";
            }
        }

        private void btnSaveWeight_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(scaleMatName))
            {
                DialogResult dialog = MessageBox.Show("Xác nhận liệu \"" + scaleMatName + "\" đã sẵn sàng ?" + Environment.NewLine + "Confirm \"" + scaleMatName + "\" material is ready ?", "Xác nhận / Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                MessageBox.Show("Đã hoàn thành xác nhận!" + Environment.NewLine + "Confirmation has completed!", "Thông tin / Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

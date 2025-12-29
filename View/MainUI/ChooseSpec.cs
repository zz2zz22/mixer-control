using Microsoft.WindowsAPICodePack.Dialogs;
using mixer_control_globalver.Controller;
using mixer_control_globalver.Controller.LogFile;
using mixer_control_globalver.Properties;
using mixer_control_globalver.View.CustomComponent;
using mixer_control_globalver.View.CustomControls;
using mixer_control_globalver.View.SideUI;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Globalization;

namespace mixer_control_globalver.View.MainUI
{
    public partial class ChooseSpec : Form
    {
        //Fields
        public static bool isConfirmed;

        public ChooseSpec()
        {
            switch (SettingsManager.GetSetting(s => s.Language))
            {
                case 0:
                    SubMethods.SetLanguage("vi-VN");
                    break;
                case 1:
                    SubMethods.SetLanguage("zh-CN");
                    break;
                case 2:
                    SubMethods.SetLanguage("en-US");
                    break;
                default:
                    SubMethods.SetLanguage("");
                    break;
            }
            InitializeComponent();
        }
        #region Methods
        //Load list cac file cong thuc
        private void LoadItemFilePath(string path)
        {
            try
            {
                dtgvListSpecification.DataSource = null;
                DataTable dt = new DataTable();
                dt.Columns.Add("file_name", typeof(string));
                dt.Columns.Add("file_path", typeof(string));
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
                if (dir.Exists == false)
                    dir.Create();
                else
                {
                    var files = Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".xls", StringComparison.OrdinalIgnoreCase));
                    foreach (string fileName in files)
                    {
                        dt.Rows.Add(Path.GetFileNameWithoutExtension(fileName), fileName);
                    }
                }
                dtgvListSpecification.DataSource = dt;
                dtgvListSpecification.Columns["file_name"].HeaderText = GlobalStrings.FormulaListHeaderText;
                dtgvListSpecification.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(GlobalStrings.Text_Font, dtgvListSpecification.ColumnHeadersDefaultCellStyle.Font.Size, dtgvListSpecification.ColumnHeadersDefaultCellStyle.Font.Style);
                dtgvListSpecification.DefaultCellStyle.Font = new System.Drawing.Font(GlobalStrings.Text_Font, dtgvListSpecification.DefaultCellStyle.Font.Size, dtgvListSpecification.DefaultCellStyle.Font.Style);

                dtgvListSpecification.Columns["file_path"].Visible = false;
            }
            catch (Exception)
            {
                CTMessageBox.Show(GlobalStrings.Error_CannotLoadFormulaList, GlobalStrings.MessageBoxTitle_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
        private void ChooseSpec_Load(object sender, EventArgs e)
        {
            lb1.Text = GlobalStrings.Label_SelectedFormula;
            lb1.Font = new System.Drawing.Font(GlobalStrings.Text_Font, lb1.Font.Size, lb1.Font.Style);
            lb2.Text = GlobalStrings.Label_FormulaDirectorySetup;
            lb2.Font = new System.Drawing.Font(GlobalStrings.Text_Font, lb2.Font.Size, lb2.Font.Style);

            btnTestOilFeed.ButtonText = GlobalStrings.btnTestOilFeed;
            btnTestOilFeed.Font = new System.Drawing.Font(GlobalStrings.Text_Font, btnTestOilFeed.Font.Size, btnTestOilFeed.Font.Style);
            btnConfirmChoose.ButtonText = GlobalStrings.btnConfirm;
            btnConfirmChoose.Font = new System.Drawing.Font(GlobalStrings.Text_Font, btnConfirmChoose.Font.Size, btnConfirmChoose.Font.Style);
            btnCheckProcess.ButtonText = GlobalStrings.btnCheckProcess;
            btnCheckProcess.Font = new System.Drawing.Font(GlobalStrings.Text_Font, btnCheckProcess.Font.Size, btnCheckProcess.Font.Style);

            SettingsManager.UpdateSettings(s => s.EndReportEnabled = true);
            try
            {
                if (String.IsNullOrEmpty(SettingsManager.GetSetting(s => s.FormulaDirectory)))
                {
                    string dirPath = AppDomain.CurrentDomain.BaseDirectory + "\\InputData";
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(dirPath);
                    if (dir.Exists == false)
                        dir.Create();

                    SettingsManager.UpdateSettings(s => s.FormulaDirectory = dir.FullName);
                }
                LoadItemFilePath(SettingsManager.GetSetting(s => s.FormulaDirectory));

                TemporaryVariables.resetAllTempVariables();
            }
            catch (Exception ex)
            {
                SystemLog.Output(SystemLog.MSG_TYPE.Nor, "Error load file", ex.Message);
            }
            SettingsManager.SaveSettings();
        }

        private void saveFileLocationPassFormClosed(object sender, EventArgs e)
        {
            ((Form)sender).FormClosed -= saveFileLocationPassFormClosed;
            if (isConfirmed)
            {
                isConfirmed = false;
                CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                dialog.InitialDirectory = SettingsManager.GetSetting(s => s.FormulaDirectory);
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    SettingsManager.UpdateSettings(s => s.FormulaDirectory = dialog.FileName);
                    SettingsManager.SaveSettings();
                }
                LoadItemFilePath(SettingsManager.GetSetting(s => s.FormulaDirectory));
            }
        }

        private void picbtnChooseDirectory_Click(object sender, EventArgs e)
        {
            PasswordConfirm passwordConfirm = new PasswordConfirm();
            passwordConfirm.FormClosed += saveFileLocationPassFormClosed;
            passwordConfirm.ShowDialog();
        }

        private void dtgvListSpecification_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            bool isSuccess = false;
            LoadingDialog loading = new LoadingDialog();
            Thread backgroundThreadSaveData = new Thread(
                    new ThreadStart(() =>
                    {
                        if (dtgvListSpecification.SelectedCells.Count > 0)
                        {
                            try
                            {
                                int selectedrowindex = dtgvListSpecification.SelectedCells[0].RowIndex;
                                DataGridViewRow selectedRow = dtgvListSpecification.Rows[selectedrowindex];
                                TemporaryVariables.tempFileName = Convert.ToString(selectedRow.Cells["file_name"].Value);
                                TemporaryVariables.tempFilePath = Convert.ToString(selectedRow.Cells["file_path"].Value);

                                //Save variable to Datatable
                                TemporaryVariables.InitMaterialDT();
                                TemporaryVariables.InitProcessDT();
                                DataTable processDT = SubMethods.ImportExceltoDatatable(TemporaryVariables.tempFilePath);
                                if (processDT != null)
                                {
                                    if (processDT.Rows.Count > 0)
                                    {
                                        for (int j = 0; j < processDT.Rows.Count; j++)
                                        {
                                            if (!String.IsNullOrEmpty(processDT.Rows[j][0].ToString())
                                                && !String.IsNullOrEmpty(processDT.Rows[j][1].ToString())
                                                && !String.IsNullOrEmpty(processDT.Rows[j][2].ToString())
                                                && !String.IsNullOrEmpty(processDT.Rows[j][5].ToString())
                                                && !String.IsNullOrEmpty(processDT.Rows[j][6].ToString())
                                                && !String.IsNullOrEmpty(processDT.Rows[j][7].ToString())
                                                && !String.IsNullOrEmpty(processDT.Rows[j][8].ToString()))
                                            {
                                                int changeSpeed = 0, changeTime = 0, totalPowder = 0, remainPowder = 0;
                                                double oilMass = 0, oilWeight = 0, oilMass2 = 0, oilWeight2 = 0;
                                                bool isVaccum = false, isSkipAnnounce = false, isOilFeed = false, isOilFeed2 = false;
                                                string powderBefore, powderAfter;

                                                if (!String.IsNullOrEmpty(processDT.Rows[j][3].ToString()))
                                                    changeSpeed = Convert.ToInt32(processDT.Rows[j][3].ToString());

                                                if (!String.IsNullOrEmpty(processDT.Rows[j][4].ToString()))
                                                    changeTime = Convert.ToInt32(processDT.Rows[j][4].ToString());

                                                if (processDT.Rows[j][5].ToString().ToLower() == "yes")
                                                    isVaccum = true;

                                                if (processDT.Rows[j][7].ToString().ToLower() == "yes")
                                                    isSkipAnnounce = true;

                                                if (processDT.Rows[j][8].ToString().ToLower() == "yes")
                                                    isOilFeed = true;

                                                if (!string.IsNullOrEmpty(processDT.Rows[j][10].ToString()) && !string.IsNullOrEmpty(processDT.Rows[j][9].ToString()))
                                                {
                                                    oilMass = Convert.ToDouble(processDT.Rows[j][10].ToString());
                                                    oilWeight = Convert.ToDouble(processDT.Rows[j][9].ToString());
                                                }
                                                else
                                                {
                                                    oilMass = 0;
                                                    oilWeight = 0;
                                                }

                                                if (processDT.Rows[j][17].ToString().ToLower() == "yes")
                                                    isOilFeed2 = true;

                                                if (!string.IsNullOrEmpty(processDT.Rows[j][18].ToString()) && !string.IsNullOrEmpty(processDT.Rows[j][19].ToString()))
                                                {
                                                    oilMass2 = Convert.ToDouble(processDT.Rows[j][19].ToString());
                                                    oilWeight2 = Convert.ToDouble(processDT.Rows[j][18].ToString());
                                                }
                                                else
                                                {
                                                    oilMass2 = 0;
                                                    oilWeight2 = 0;
                                                }

                                                if (!String.IsNullOrEmpty(processDT.Rows[j][13].ToString()) && !String.IsNullOrEmpty(processDT.Rows[j][14].ToString()))
                                                {
                                                    //Edit to read total powder bags
                                                    totalPowder = Convert.ToInt32(processDT.Rows[j][13].ToString());
                                                    remainPowder = Convert.ToInt32(processDT.Rows[j][14].ToString());
                                                }
                                                else
                                                {
                                                    totalPowder = 0;
                                                    remainPowder = 0;
                                                }

                                                string stepDesc = processDT.Rows[j][12].ToString();
                                                string oilType = processDT.Rows[j][11].ToString();
                                                string oilType2 = processDT.Rows[j][20].ToString();

                                                if (SettingsManager.GetSetting(s => s.CheckPowderEnabled))
                                                {
                                                    powderBefore = processDT.Rows[j][15].ToString();
                                                    powderAfter = processDT.Rows[j][16].ToString();
                                                }
                                                else
                                                {
                                                    powderBefore = String.Empty;
                                                    powderAfter = String.Empty;
                                                }

                                                TemporaryVariables.processDT.Rows.Add(processDT.Rows[j][0].ToString(),
                                                processDT.Rows[j][1].ToString(),
                                                processDT.Rows[j][2].ToString(),
                                                changeSpeed,
                                                changeTime,
                                                isVaccum,
                                                processDT.Rows[j][6].ToString(),
                                                isSkipAnnounce,
                                                stepDesc,
                                                false,
                                                isOilFeed,
                                                oilMass,
                                                oilWeight,
                                                oilType,
                                                totalPowder,
                                                remainPowder,
                                                powderBefore,
                                                powderAfter,
                                                isOilFeed2,
                                                oilMass2,
                                                oilWeight2,
                                                oilType2);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    throw new Exception(GlobalStrings.Error_ReadFile);
                                }
                                isSuccess = true;
                                loading.BeginInvoke(new Action(() => loading.Close()));
                            }
                            catch (Exception ex)
                            {
                                isSuccess = false;
                                loading.BeginInvoke(new Action(() => loading.Close()));
                                
                                TemporaryVariables.resetAllTempVariables();

                                SystemLog.Output(SystemLog.MSG_TYPE.Err, GlobalStrings.MessageBoxTitle_Error, GlobalStrings.Error_CannotLoadExcelFile);
                                CTMessageBox.Show(GlobalStrings.Error_CannotLoadExcelFile, GlobalStrings.MessageBoxTitle_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }));
            if (backgroundThreadSaveData.IsAlive)
                backgroundThreadSaveData.Join();
            backgroundThreadSaveData.Start();
            loading.ShowDialog();
            if (isSuccess)
                lbFormulaName.Text = TemporaryVariables.tempFileName;
            else
            {
                lbFormulaName.Text = "";
            }
        }

        private void btnRefreshFileList_Click(object sender, EventArgs e)
        {
            LoadItemFilePath(SettingsManager.GetSetting(s => s.FormulaDirectory));
        }

        private void btnConfirmChoose_Click(object sender, EventArgs e)
        {
            Program.main.openScaleTab();
        }

        
        private void txbSearchFormula_TextChanged(object sender, EventArgs e)
        {
            if (dtgvListSpecification.Rows.Count > 0)
            {
                string searchForText = txbSearchFormula.Text.Trim();
                CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[dtgvListSpecification.DataSource];
                currencyManager1.SuspendBinding();
                for (int i = 0; i < dtgvListSpecification.Rows.Count; i++)
                {
                    if (dtgvListSpecification.Rows[i].Cells[0].Value.ToString().ToLower().Contains(searchForText))
                    {
                        dtgvListSpecification.Rows[i].Selected = true;
                        dtgvListSpecification.Rows[i].Visible = true;
                    }
                    else
                    {
                        dtgvListSpecification.Rows[i].Visible = false;
                        dtgvListSpecification.Rows[i].Selected = false;
                    }
                }
                currencyManager1.ResumeBinding();
            }
        }

        private void btnTestOilFeed_Click(object sender, EventArgs e)
        {
            if (!SettingsManager.GetSetting(s => s.OIlTested) || SettingsManager.GetSetting(s => s.MultipleOilTestEnabled))
            {
                if (SettingsManager.GetSetting(s => s.OilSupplyEnabled))
                {
                    OilFeederTest oilFeederTest = new OilFeederTest();
                    oilFeederTest.ShowDialog();
                }
                else
                {
                    CTMessageBox.Show(GlobalStrings.Message_NotEnableOilSupplyFunctionYet, GlobalStrings.MessageBoxTitle_Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnCheckProcess_Click(object sender, EventArgs e)
        {
            if (TemporaryVariables.processDT != null && TemporaryVariables.processDT.Rows.Count > 0)
            {
                CheckFormulaProcess checkFormula = new CheckFormulaProcess();
                checkFormula.ShowDialog();
            }
        }
    }
}

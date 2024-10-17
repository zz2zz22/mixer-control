using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using mixer_control_globalver.Controller.LogFile;
using System.Data;

namespace mixer_control_globalver.Model.Logics
{
    public class ExcelDataConnector
    {
        private void LoadDirectoryFileListToDTGV(DataGridView dtgv, string directoryPath)
        {
            try 
            {
                ComboBox cbxCheck = new ComboBox();
                cbxCheck.DataSource = Directory.GetFiles(directoryPath, "*.xlsx").Select(Path.GetFileNameWithoutExtension).OrderBy(n => n.ToLower()).ToList();
                if (cbxCheck.Items.Count > 0)
                {
                    dtgv.DataSource = Directory.GetFiles(directoryPath, "*.xlsx").Select(Path.GetFileNameWithoutExtension).OrderBy(n => n.ToLower()).ToList();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Read files list from '" + directoryPath + "' failed", ex.Message);
            }
        }
    }
}

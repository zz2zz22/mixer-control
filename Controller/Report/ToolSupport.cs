using Spire.Xls.AI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace mixer_control_globalver.Controller.Report
{
    public class ToolSupport
    {
        private void reOject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Export to excel fail: " + ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                GC.Collect();
            }
        }
        public void ExportMixerReport(List<EndProcessReport> endProcesses, string pathSave, string pathForm)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            xlApp = new Excel.Application();
            var list_process = Win32Processes.GetProcessesLockingFile(pathForm);
            foreach (var item in list_process)
            {
                item.Kill();
            }
            xlWorkBook = xlApp.Workbooks.Open(pathForm, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            object misValue = System.Reflection.Missing.Value;
            try
            {
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                xlWorkSheet.Name = "MainReport";
                xlWorkSheet.Cells[6, "C"] = TemporaryVariables.tempFileName;
                xlWorkSheet.Cells[6, "E"] = TemporaryVariables.lotNo;
                xlWorkSheet.Cells[4, "F"] = DateTime.Now.ToUniversalTime().ToString("dd/MM/yyyy HH:mm:ss") + " UTC";
                
                for (int i = 0; i < endProcesses.Count; i++)
                {
                    xlWorkSheet.Cells[8 + i, "A"] = (i + 1).ToString();
                    xlWorkSheet.Cells[8 + i, "B"] = endProcesses[i].Code;
                    xlWorkSheet.Cells[8 + i, "C"] = endProcesses[i].LOT;
                    xlWorkSheet.Cells[8 + i, "D"] = endProcesses[i].Weight;
                    xlWorkSheet.Cells[8 + i, "E"] = endProcesses[i].Tolerance;
                }
                if (File.Exists(pathSave))
                    File.Delete(pathSave);
                xlWorkBook.SaveAs(pathSave, Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue,
                    misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close();

                xlApp.Quit();
                reOject(xlWorkSheet);
                reOject(xlWorkBook);
                reOject(xlApp);
            }
            catch (Exception)
            {
                xlWorkBook.Close(0);
                xlApp.Quit();
                throw;
            }

        }
    }
}

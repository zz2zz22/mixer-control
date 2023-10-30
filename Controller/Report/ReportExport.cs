using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mixer_control_globalver.Controller.Report
{
    public class ReportExport
    {
        public string EndProcessReport = Environment.CurrentDirectory + @"\Resources\EndProcessReport.xlsx";

        public void ExportExcelEndProcess(string PathSave, List<EndProcessReport> details)
        {
            ToolSupport toolSupport = new ToolSupport();
            toolSupport.ExportMixerReport(details, PathSave, EndProcessReport);
        }
    }
}

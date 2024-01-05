using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class SubMethods
{
    public static DataTable ImportExceltoDatatable(string filePath, string sheetName)
    {
        DataTable dataTable = new DataTable();

        Workbook workbook = new Workbook();
        workbook.LoadFromFile(filePath);
        Worksheet worksheet = workbook.Worksheets[sheetName];

        dataTable = worksheet.ExportDataTable(worksheet.AllocatedRange, true, true);

        return dataTable;
    }

}
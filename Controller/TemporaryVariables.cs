using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mixer_control_globalver.Controller
{
    public class TemporaryVariables
    {
        #region Basic Variables
        public static String tempFileName { get; set; }
        public static String tempFilePath { get; set; }

        public static String tempMatName { get; set; }
        public static String tempMatNo { get; set; }

        #endregion

        #region Temporary Datatables
        public static DataTable materialDT { get; set; }
        public static DataTable processDT { get; set; }
        public static void InitMaterialDT()
        {
            if (materialDT != null)
            {
                materialDT = null;
            }
            materialDT = new DataTable();
            DataColumn matCol;

            matCol = new DataColumn();
            matCol.DataType = Type.GetType("System.Int32");
            matCol.ColumnName = "mat_no";
            materialDT.Columns.Add(matCol);

            matCol = new DataColumn();
            matCol.DataType = Type.GetType("System.String");
            matCol.ColumnName = "mat_name";
            materialDT.Columns.Add(matCol);

            matCol = new DataColumn();
            matCol.DataType = Type.GetType("System.String");
            matCol.ColumnName = "lot_no";
            materialDT.Columns.Add(matCol);

            matCol = new DataColumn();
            matCol.DataType = Type.GetType("System.Double");
            matCol.ColumnName = "weight";
            materialDT.Columns.Add(matCol);

            matCol = new DataColumn();
            matCol.DataType = Type.GetType("System.Double");
            matCol.ColumnName = "tolerance";
            materialDT.Columns.Add(matCol);

            matCol = new DataColumn();
            matCol.DataType = Type.GetType("System.Boolean");
            matCol.ColumnName = "is_packed";
            materialDT.Columns.Add(matCol);
            
            matCol = new DataColumn();
            matCol.DataType = Type.GetType("System.Boolean");
            matCol.ColumnName = "is_scaled";
            materialDT.Columns.Add(matCol);
        }

        public static void InitProcessDT()
        {
            if (processDT != null)
            {
                processDT = null;
            }
            processDT = new DataTable();
            DataColumn processCol;

            processCol = new DataColumn();
            processCol.DataType = Type.GetType("System.Int32");
            processCol.ColumnName = "process_no";
            processDT.Columns.Add(processCol);

            processCol = new DataColumn();
            processCol.DataType = Type.GetType("System.Int32");
            processCol.ColumnName = "init_speed";
            processDT.Columns.Add(processCol);

            processCol = new DataColumn();
            processCol.DataType = Type.GetType("System.Int32");
            processCol.ColumnName = "init_time";
            processDT.Columns.Add(processCol);

            processCol = new DataColumn();
            processCol.DataType = Type.GetType("System.Int32");
            processCol.ColumnName = "change_speed";
            processDT.Columns.Add(processCol);

            processCol = new DataColumn();
            processCol.DataType = Type.GetType("System.Int32");
            processCol.ColumnName = "change_time";
            processDT.Columns.Add(processCol);

            processCol = new DataColumn();
            processCol.DataType = Type.GetType("System.Int32");
            processCol.ColumnName = "vaccum_time";
            processDT.Columns.Add(processCol);

            processCol = new DataColumn();
            processCol.DataType = Type.GetType("System.Int32");
            processCol.ColumnName = "max_temperature";
            processDT.Columns.Add(processCol);

            processCol = new DataColumn();
            processCol.DataType = Type.GetType("System.String");
            processCol.ColumnName = "description";
            processDT.Columns.Add(processCol);

            processCol = new DataColumn();
            processCol.DataType = Type.GetType("System.Boolean");
            processCol.ColumnName = "is_finished";
            processDT.Columns.Add(processCol);
        }
        #endregion

        public static void resetAllTempVariables()
        {
            //reset basic variables
            tempFileName = null;
            tempFilePath = null;

            //reset datatables
            materialDT = null;
            processDT = null;
        }
    }
}

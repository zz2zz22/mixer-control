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
        public static int language { get; set; }

        #endregion

        #region Temporary Datatables
        public static DataTable materialDT { get; set; }
        public static DataTable processDT { get; set; }
        public static DataTable settingDT { get; set; }


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
            matCol.ColumnName = "is_confirmed";
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
            processCol.DataType = Type.GetType("System.Boolean");
            processCol.ColumnName = "is_vaccum";
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

        public static void InitSettingDT()
        {
            if (settingDT != null)
            {
                settingDT = null;
            }
            settingDT = new DataTable();
            DataColumn settingCol;

            settingCol = new DataColumn();
            settingCol.DataType = Type.GetType("System.String");
            settingCol.ColumnName = "value_member";
            settingDT.Columns.Add(settingCol);

            settingCol = new DataColumn();
            settingCol.DataType = Type.GetType("System.String");
            settingCol.ColumnName = "display_member";
            settingDT.Columns.Add(settingCol);

            settingDT.Rows.Add("ER", "Kích hoạt chạy - Enable Run");
            settingDT.Rows.Add("SR", "Dừng chạy - Stop Run");
            settingDT.Rows.Add("AM", "Tự động/Thủ công - Auto/Manual");
            settingDT.Rows.Add("LA", "Đèn báo - Light Alarm");
            settingDT.Rows.Add("CU", "Lên thùng - Container Up");
            settingDT.Rows.Add("CD", "Lật thùng - Container Down");
            settingDT.Rows.Add("OL", "Mở nắp - Open Lid");
            settingDT.Rows.Add("CL", "Đóng nắp - Close Lid");
            settingDT.Rows.Add("TS", "Kích hoạt tốc độ - Toggle Speed");
            settingDT.Rows.Add("CW", "Quay chiều thuận - Clockwise Roll");
            settingDT.Rows.Add("RCW", "Quay chiều ngược - Reverse Clockwise Roll");
            settingDT.Rows.Add("WS", "Ghi tốc độ - Write Speed");
            settingDT.Rows.Add("RS", "Đọc tốc độ - Read Speed");
            settingDT.Rows.Add("RT", "Đọc nhiệt độ - Read Temperature");
            settingDT.Rows.Add("TV", "Kích hút chân không - Toggle Vaccum");
            settingDT.Rows.Add("MS", "Tốc độ tối đa (động cơ) - Maximum Speed (Motor)");
            settingDT.Rows.Add("SD", "Đường kính trục xuay động cơ - Spindle Diameter");
            settingDT.Rows.Add("SSD", "Đường kính trục xuay cảm biến - Sensor Spindle Diameter");
            settingDT.Rows.Add("TRMS", "Tỉ lệ truyền - Transmission Ratio");
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

using mixer_control_globalver.Controller.Ini;
using mixer_control_globalver.Controller.LogFile;
using mixer_control_globalver.Properties;
using mixer_control_globalver.View.CustomControls;
using System;
using System.Windows.Forms;

namespace mixer_control_globalver.Controller.PLC
{
    public class PLCMethods
    {
        private static PLCConnector pLC;
        private static int databaseNo;

        public static IniFile ini = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "\\data\\setting.ini");

        private static bool PLCConnect()
        {
            databaseNo = Settings.Default.database_no;
            int connectionPLC;
            try
            {
                pLC = new PLCConnector(Settings.Default.plc_ip, 0, 0, out connectionPLC);
                if (connectionPLC != 0)
                    throw new Exception("Connection to PLC failed, error code : " + connectionPLC);
                else
                    return true;
            }
            catch (Exception ex)
            {
                CTMessageBox.Show(ex.Message, "PLC connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "PLC connection failed : ", ex.Message);
                return false;
            }
        }

        public static void ResetPLCVariables() //Reset các biến PLC khi thoát chương trình hoặc phần tự động
        {
            if (PLCConnect())
            {
                try
                {
                    pLC.WriteBoolToPLC(false, databaseNo, Convert.ToInt32(ini.Read("ER", "start")), Convert.ToInt32(ini.Read("ER", "bit")));
                    pLC.WriteBoolToPLC(false, databaseNo, Convert.ToInt32(ini.Read("SR", "start")), Convert.ToInt32(ini.Read("SR", "bit")));
                    pLC.WriteBoolToPLC(false, databaseNo, Convert.ToInt32(ini.Read("LA", "start")), Convert.ToInt32(ini.Read("LA", "bit")));
                    pLC.WriteBoolToPLC(false, databaseNo, Convert.ToInt32(ini.Read("CU", "start")), Convert.ToInt32(ini.Read("CU", "bit")));
                    pLC.WriteBoolToPLC(false, databaseNo, Convert.ToInt32(ini.Read("CD", "start")), Convert.ToInt32(ini.Read("CD", "bit")));
                    pLC.WriteBoolToPLC(false, databaseNo, Convert.ToInt32(ini.Read("OL", "start")), Convert.ToInt32(ini.Read("OL", "bit")));
                    pLC.WriteBoolToPLC(false, databaseNo, Convert.ToInt32(ini.Read("CL", "start")), Convert.ToInt32(ini.Read("CL", "bit")));
                    pLC.WriteBoolToPLC(false, databaseNo, Convert.ToInt32(ini.Read("TS", "start")), Convert.ToInt32(ini.Read("TS", "bit")));
                    pLC.WriteBoolToPLC(false, databaseNo, Convert.ToInt32(ini.Read("CW", "start")), Convert.ToInt32(ini.Read("CW", "bit")));
                    pLC.WriteBoolToPLC(false, databaseNo, Convert.ToInt32(ini.Read("RCW", "start")), Convert.ToInt32(ini.Read("RCW", "bit")));
                    pLC.WriteRealtoPLC(0, databaseNo, Convert.ToInt32(ini.Read("WS", "start")), 2);
                    pLC.WriteBoolToPLC(false, databaseNo, Convert.ToInt32(ini.Read("ONV", "start")), Convert.ToInt32(ini.Read("ONV", "bit")));
                    pLC.WriteBoolToPLC(false, databaseNo, Convert.ToInt32(ini.Read("OFFV", "start")), Convert.ToInt32(ini.Read("OFFV", "bit")));
                    pLC.WriteBoolToPLC(true, databaseNo, 24, 0); // Truyền reset variable
                    pLC.Diconnect();
                }
                catch (Exception ex)
                {
                    CTMessageBox.Show(ex.Message, "Reset PLC variables error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SystemLog.Output(SystemLog.MSG_TYPE.Err, "Reset PLC variables error : ", ex.Message);
                }
            }
        }
    }
}

using mixer_control_globalver.Controller.LogFile;
using Sharp7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mixer_control_globalver.Model.PLC
{
    public class PLCConnector
    {
        private S7Client Client;
        private string _IP;
        private int _Rack;
        private int _Slot;
        private byte[] Buffer = new byte[65536];
        private byte[] DB_A = new byte[1024];
        private byte[] DB_B = new byte[1024];
        private byte[] DB_C = new byte[1024];
        private byte[] DB_D = new byte[1024];
        public string ConnectionMessage;
        public PLCConnector(string IP, int Rack, int Slot, out int Result)
        {
            Client = new S7Client();
            _IP = IP;
            _Rack = Rack;
            _Rack = Slot;
            Client.ConnTimeout = 500;
            Client.RecvTimeout = 500;
            Client.SendTimeout = 500;
            Result = Client.ConnectTo(_IP, _Rack, _Slot);
            if (Result == 0)
            {
                ConnectionMessage = Client.ErrorText(Result);
            }
            var StrMessage = Client.ErrorText(0);
        }
        public int Diconnect()
        {
            int Result = -1;
            try
            {
                Result = Client.Disconnect();
            }
            catch (Exception ex)
            {
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Diconnect PLC fail", ex.Message);
            }
            return Result;
        }
        public int isConnectionPLC()
        {
            int Result = -1;
            try
            {
                Result = Client.ConnectTo(_IP, _Rack, _Slot);
                return Result;
            }
            catch (Exception ex)
            {
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Connection PLC Error", ex.Message);
            }
            return Result;
        }
        public string ReadAreaByteToString(int DbNumber, int Start, int Amount)
        {
            string Result = "";
            try
            {
                Client.ReadArea(S7Area.DB, DbNumber, Start, Amount, S7WordLength.Byte, Buffer);
                for (int i = 0; i <= Amount - 1; i++)
                {
                    Result = Result + (Convert.ToString((char)Buffer[i]));
                }
            }
            catch (Exception ex)
            {
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "ReadArea Byte to String fail", ex.Message);
            }
            return Result;

        }
        public void WriteQRMESto(int DbNumber, int Start, int Amount, string Text)
        {

            try
            {
                byte[] Buffer = new byte[65536];
                Sharp7.S7.SetCharsAt(Buffer, 0, Text);
                Client.WriteArea(S7Area.DB, DbNumber, Start, Amount, S7WordLength.Byte, Buffer);

            }
            catch (Exception ex)
            {
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Write QR MES fail", ex.Message);
            }


        }
        public List<int> ReadAreaIntToListInt(int DbNumber, int Start, int Amount)
        {
            List<int> Result = new List<int>();
            try
            {
                Client.ReadArea(S7Area.DB, DbNumber, Start, Amount, S7WordLength.DInt, Buffer);
                for (int i = 0; i <= Amount - 1; i = i + 2)
                {
                    Result.Add(Buffer[i] * 256 + Buffer[i + 1]);
                }
            }
            catch (Exception ex)
            {
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Read Area Int to List Int fail", ex.Message);
            }
            return Result;

        }

        public bool ReadBitToBool(int db, int start, int bit, int size)
        {
            bool Result = false;
            try
            {
                byte[] buffer = new byte[1];

                Client.DBRead(db, start, size, buffer);
                Result = Sharp7.S7.GetBitAt(buffer, 0, bit);
            }
            catch (Exception ex)
            {
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Read bit to Bool fail", ex.Message);
            }
            return Result;
        }
        public void WritebittoPLC(bool value, int db, int start, int bit, int size)
        {
            try
            {
                byte[] buffer = new byte[1];
                Sharp7.S7.SetBitAt(buffer, 0, bit, value);
                Client.DBWrite(db, start, size, buffer);
            }
            catch (Exception ex)
            {
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Write bit to PLC fail", ex.Message);
            }
        }
        public string ReadRealToString(int DbNumber, int Start)
        {
            string Result = "";
            try
            {
                byte[] buffer = new byte[4];

                Client.ReadArea(S7Area.DB, DbNumber, Start, 4, S7WordLength.Byte, buffer);
                Result = Convert.ToString(Sharp7.S7.GetRealAt(buffer, 0));
            }
            catch (Exception ex)
            {
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Read real to String fail", ex.Message);
            }
            return Result;
        }
        public void WriteRealtoPLC(float value, int db, int start, int size)
        {
            try
            {
                byte[] buffer = new byte[4];
                Sharp7.S7.SetRealAt(buffer, 0, value);
                Client.WriteArea(S7Area.DB, db, start, 4, S7WordLength.Byte, buffer);
            }
            catch (Exception ex)
            {
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Write real to PLC fail", ex.Message);
            }
        }
        public void WriteDinttoPLC(int value, int db, int start, int size)
        {
            try
            {
                byte[] buffer = new byte[2];
                Sharp7.S7.SetWordAt(buffer, 0, ushort.Parse(value.ToString()));
                Client.WriteArea(S7Area.DB, db, start, 2, S7WordLength.Byte, buffer);
            }
            catch (Exception ex)
            {
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Write Dint to PLC fail", ex.Message);
            }
        }

        public void WriteStringtoPLC(string value, int db, int start, int size)
        {
            try
            {
                byte[] buffer = new byte[254];
                Sharp7.S7.SetStringAt(buffer, 0, 254, value);
                //Sharp7.S7.SetWordAt(buffer, 0, ushort.Parse(value.ToString()));
                Client.WriteArea(S7Area.DB, db, start, 254, S7WordLength.Byte, buffer);
            }
            catch (Exception ex)
            {
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "Write String to PLC fail", ex.Message);
            }
        }
    }
}

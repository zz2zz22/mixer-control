using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace mixer_control_globalver.Controller.Device
{
    class QYLED_DLL
    {
        private const string Ddll = "QYLED.dll";

        [DllImport(Ddll, CallingConvention = CallingConvention.StdCall)]
        //[DllImport(Ddll)]
        public static extern byte SendInternalText_Net(string TshowContent, string TcardIP, int TnetProtocol, int TareaWidth, int TareaHigth, int Tuid, int TscreenColor, int TshowStyle, int TshowSpeed, int TstopTime, int TfontColor, int TfontBody, int TfontSize, int TupdateStyle, bool TpowerOffSave, int nRotateMode);

        [DllImport(Ddll, CallingConvention = CallingConvention.StdCall)]
        public static extern int ClearLED(int LedType);
    }
}

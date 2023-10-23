using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.QrCode;

namespace mixer_control_globalver.Controller.Device
{
    public class QRGenerate
    {
        public Image GeneratingQRCode(string QR)
        {
            QrCodeEncodingOptions options = new QrCodeEncodingOptions();
            options.Height = 500;
            options.Width = 500;
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            barcodeWriter.Options = options;
            Image QRImage = barcodeWriter.Write(QR);
            return QRImage;
        }
    }
}

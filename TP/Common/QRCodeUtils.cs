using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using Xceed.Wpf.DataGrid;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace TP.Common
{
    public class QRCodeUtils
    {

        public static BitmapImage GetQrCode(string originString, int height, int width, int margin)
        {
            BitmapImage bi = new BitmapImage();

            var barcodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions
                {
                    Height = height,
                    Width = width,
                    Margin = margin
                }
            };

            using (var bitmap = barcodeWriter.Write(originString))
                using (var stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Png);
                    bi.BeginInit();
                    stream.Seek(0, SeekOrigin.Begin);
                    bi.StreamSource = stream;
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.EndInit();
                }
            return bi;
        }
    }
}

using EntitiesDABL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Xml;
using TP.BLL;
using TP.Common;
using TP.Common.StringLib;
using TP.View;
using System.Windows.Controls;
namespace TP.printing
{
    public class Printer
    {

        public static void Priview(MainView mainView)
        {
            var doc1 = new System.Windows.Documents.FlowDocument();
            doc1.PageWidth = 600;
            //Add res image
            var resPath = ConfigurationManager.AppSettings["PrintResImagePath"];
            if (!string.IsNullOrEmpty(resPath))
            {
                var resImg = new Image();
                var path = Path.Combine(Environment.CurrentDirectory, resPath);
                resImg.Source = new BitmapImage(new Uri(path));
                InlineUIContainer resContainer = new InlineUIContainer(resImg);
                Paragraph resPar = new Paragraph(resContainer);
                resPar.TextAlignment = TextAlignment.Center;
                Section s = new Section();
                s.Blocks.Add(resPar);
                doc1.Blocks.Add(s);
            }

            //Add print body
            var reader = new StreamReader(ConfigurationManager.AppSettings["PrintFile"]);
            var xmalString = reader.ReadToEnd();


            //Query Bill Infos
            var billInfo = new JzzpBillBLL().GetBillByBillId(mainView.DeliveryTabView.TPBillRefMV.BillId_FK);
            //Parse the value

            xmalString = xmalString.HaackFormat(
                new
                {

                    TPBillRef = mainView.DeliveryTabView.TPBillRefMV,
                    TPUser = mainView.DeliveryTabView.TPBillRefMV.TPUser,
                    TPUserAddress = mainView.DeliveryTabView.TPBillRefMV.TPUserAddress,
                    Bill = billInfo.Bill
                });

            StringReader stringReader = new StringReader(xmalString);
            var xmlReader = XmlReader.Create(stringReader);
            var sec = XamlReader.Load(xmlReader) as Section;

            //Looking for billItem 
            var table = sec.Blocks.Where(i => i is Table).OfType<Table>().FirstOrDefault();
            if (table != null)
            {
                buildBillItemTable(table, billInfo.BillItems);
            }
            doc1.Blocks.Add(sec);

            //Add qr image
            var qrImg = new Image();
            var bitmap = QRCodeUtils.GetQrCode(string.Format(ConfigurationManager.AppSettings["DirectionUrl"], mainView.DeliveryTabView.TPBillRefMV.TPUserAddress.Postcode),
                int.Parse(ConfigurationManager.AppSettings["QRHeight"]),
                int.Parse(ConfigurationManager.AppSettings["QRWidth"]),
                0);
            qrImg.Source = bitmap;
            InlineUIContainer qrContainer = new InlineUIContainer(qrImg);
            Paragraph qrPar = new Paragraph(qrContainer);
            qrPar.TextAlignment = TextAlignment.Center;
            Section sqr = new Section();
            sqr.Blocks.Add(qrPar);
            doc1.Blocks.Add(sqr);

            PrintUtils.DoPreview("test", doc1);
        }
        
        private static void buildBillItemTable(Table table, ICollection<BillItem> billItems)
        {

            var reader = new StreamReader(ConfigurationManager.AppSettings["PrintItemSampleFile"]);
            var xmalString = reader.ReadToEnd();

            foreach (var billItem in billItems)
            {
                var billItemXmal = xmalString.HaackFormat(new
                {
                    BillItem = billItem,
                    Amount = billItem.AmountOrder - billItem.AmountCancel,
                    Price = billItem.SumOfConsume - billItem.SumForDiscount
                });

                StringReader stringReader = new StringReader(billItemXmal);
                var xmlReader = XmlReader.Create(stringReader);
                var row = XamlReader.Load(xmlReader) as TableRow;
                table.RowGroups[0].Rows.Add(row);
            }
        }
    }
}

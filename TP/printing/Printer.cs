using EntitiesDABL;
using System;
using System.Collections.Generic;


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
using TP.AppStatic;
using System.Printing;

namespace TP.printing
{
    public class Printer
    {
        protected static FlowDocument GetPrintDocument(MainView mainView)
        {
            var doc1 = new System.Windows.Documents.FlowDocument();
            doc1.PageWidth = TPConfig.PrintPageWidth;
            //Add res image
            var resPath = TPConfig.PrintResImagePath;
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
            var reader = new StreamReader(TPConfig.PrintFile);
            var xmalString = reader.ReadToEnd();


            //Query Bill Infos
            var billInfo = new JzzpBillBLL().GetTempBillByBillId(mainView.DeliveryTabView.TPBillRefMV.BillId_FK);

            //Parse the value
            xmalString = xmalString.HaackFormat(
                new
                {

                    TPCallIn = mainView.DeliveryTabView.TPBillRefMV.TPCallIn,
                    TPBillRef = mainView.DeliveryTabView.TPBillRefMV,
                    TPUser = mainView.DeliveryTabView.TPBillRefMV.TPUser,
                    TPUserAddress = mainView.DeliveryTabView.TPBillRefMV.TPUserAddress,
                    Bill = billInfo.TempBill
                });

            StringReader stringReader = new StringReader(xmalString);
            var xmlReader = XmlReader.Create(stringReader);
            var sec = XamlReader.Load(xmlReader) as FlowDocument;

            //Looking for billItem 
            var table = sec.Blocks.Where(i => i is Table).OfType<Table>().FirstOrDefault();
            if (table != null)
            {
                buildBillItemTable(table, billInfo.TempBillItems);
            }
            for (int i = 0; i < sec.Blocks.Count; i++)
            {
                doc1.Blocks.Add(sec.Blocks.ElementAt(i));
            }

            //Add qr image
            var qrImg = new Image();
            var bitmap = QRCodeUtils.GetQrCode(string.Format(TPConfig.DirectionUrl, mainView.DeliveryTabView.TPBillRefMV.TPUserAddress.Postcode),
                TPConfig.QRHeight,
                TPConfig.QRWidth,
                0);
            qrImg.Source = bitmap;
            InlineUIContainer qrContainer = new InlineUIContainer(qrImg);
            Paragraph qrPar = new Paragraph(qrContainer);
            qrPar.TextAlignment = TextAlignment.Center;
            Section sqr = new Section();
            sqr.Blocks.Add(qrPar);
            doc1.Blocks.Add(sqr);
            return doc1;
        }

        public static void Priview(MainView mainView)
        {

            var document = GetPrintDocument(mainView);
            PrintUtils.DoPreview("test", document);
        }

        public static void Print(MainView mainView)
        {
            FlowDocument copy = GetPrintDocument(mainView);

            // Create a XpsDocumentWriter object, implicitly opening a Windows common print dialog,
            // and allowing the user to select a printer.

            // get information about the dimensions of the seleted printer+media.
            var pQueue = LocalPrintServer.GetDefaultPrintQueue();
            var pCapabilities = pQueue.GetPrintCapabilities();
            var printableArea = new Size(pCapabilities.PageImageableArea.ExtentWidth, pCapabilities.PageImageableArea.ExtentHeight);
            var docWriter = PrintQueue.CreateXpsDocumentWriter(pQueue);

            if (docWriter != null && printableArea != null)
            {
                DocumentPaginator paginator = ((IDocumentPaginatorSource)copy).DocumentPaginator;

                // Change the PageSize and PagePadding for the document to match the CanvasSize for the printer device.
                paginator.PageSize = new Size(printableArea.Width, printableArea.Height);
                Thickness t = new Thickness(72); // copy.PagePadding;
                copy.PagePadding = new Thickness(0, 0, 0, 0);
                copy.ColumnWidth = double.PositiveInfinity;
                //copy.PageWidth = 528; // allow the page to be the natural with of the output device

                // Send content to the printer.
                docWriter.Write(paginator);
            }
        }

        protected static void buildBillItemTable(Table table, ICollection<TempBillItem> billItems)
        {

            var reader = new StreamReader(TPConfig.PrintItemSampleFile);
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

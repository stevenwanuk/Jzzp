﻿using EntitiesDABL;
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
                var resImg = new Image() {
                    Height = TPConfig.QRHeight,
                    Width = TPConfig.QRWidth
                };
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
                    TPDriver = mainView.DeliveryTabView.TPDeliverMV.TPDriver,
                    TPUserAddress = mainView.DeliveryTabView.TPBillRefMV.TPUserAddress,
                    Bill = billInfo.TempBill,
                    Total = billInfo.TempBill.SumToPay + mainView.DeliveryTabView.TPBillRefMV.DeliverFee,
                    SavedDeliverFee = mainView.DeliveryTabView.TPBillRefMV.DeliverFeeOrigin - mainView.DeliveryTabView.TPBillRefMV.DeliverFee
                });

            StringReader stringReader = new StringReader(xmalString);
            var xmlReader = XmlReader.Create(stringReader);
            var document = XamlReader.Load(xmlReader) as FlowDocument;

            //Looking for billItem 
            var table = GetTable(document);
            if (table != null && billInfo.TempBillItems != null)
            {
                buildBillItemTable(table, billInfo.TempBillItems);
            }
            for (int i = 0; i < document.Blocks.Count; i++)
            {
                doc1.Blocks.Add(document.Blocks.ElementAt(i));
            }

            //Add qr image
            var qrImg = new Image();
            var bitmap = QRCodeUtils.GetQrCode(string.Format(TPConfig.GMap, mainView.DeliveryTabView.TPBillRefMV.TPUserAddress.Postcode),
                50,
                50,
                TPConfig.QRMargin);
            qrImg.Source = bitmap;
            InlineUIContainer qrContainer = new InlineUIContainer(qrImg);
            Paragraph qrPar = new Paragraph(qrContainer);
            qrPar.TextAlignment = TextAlignment.Center;
            Section sqr = new Section();
            sqr.Blocks.Add(qrPar);
            doc1.Blocks.Add(sqr);
            return doc1;
        }
        private static Table GetTable(FlowDocument document)
        {
            Table table = null;
            var sec = document.Blocks.Where(i => i is Section).OfType<Section>().FirstOrDefault();
            if(sec != null)
            {
                table = sec.Blocks.Where(i => i is Table).OfType<Table>().FirstOrDefault();
            }
            return table;
        }

        public static string DataContainerCheck(MainView mainView)
        {
            /* make sure not null here.
            TPCallIn = mainView.DeliveryTabView.TPBillRefMV.TPCallIn,
                    TPBillRef = mainView.DeliveryTabView.TPBillRefMV,
                    TPUser = mainView.DeliveryTabView.TPBillRefMV.TPUser,
                    TPDriver = mainView.DeliveryTabView.TPDeliverMV.TPDriver,
                    TPUserAddress = mainView.DeliveryTabView.TPBillRefMV.TPUserAddress,
                    Bill = billInfo.TempBill,
                    Total = billInfo.TempBill.SumToPay + mainView.DeliveryTabView.TPBillRefMV.DeliverFee,
                    SavedDeliverFee = mainView.DeliveryTabView.TPBillRefMV.DeliverFeeOrigin - mainView.DeliveryTabView.TPBillRefMV.DeliverFee
            */
            var errorMsg = string.Empty;
            if (mainView?.DeliveryTabView == null)
            {
                errorMsg = "No PrintData";

            }
            else
            {
                var TPBillRefMV = mainView.DeliveryTabView.TPBillRefMV;
                if (TPBillRefMV == null)
                {
                    errorMsg = "No Selected Bill";
                }
                else
                {
                    if (TPBillRefMV.TPCallIn == null)
                    {
                        errorMsg = "No Calling Infos";
                    }
                    if (TPBillRefMV.TPUser == null)
                    {
                        errorMsg = "No User Infos";
                    }
                    if (TPBillRefMV.TPUserAddress == null)
                    {
                        errorMsg = "No User Address Infos";
                    }
                    if (mainView.DeliveryTabView.TPDeliverMV?.TPDriver == null)
                    {
                        errorMsg = "No Drivers Infos";
                    }
                    if (string.IsNullOrEmpty(TPBillRefMV.BillId_FK))
                    {
                        errorMsg = "Unbinding Jzzp Bill";
                    }
                }
                
            }
            return errorMsg;
        }


        public static void Priview(MainView mainView)
        {

            var document = GetPrintDocument(mainView);
            PrintUtils.DoPreview("test", document);
        }

        protected static void SinglePrint(FlowDocument copy, PrintQueue pQueue)
        {
            // Create a XpsDocumentWriter object, implicitly opening a Windows common print dialog,
            // and allowing the user to select a printer.

            // get information about the dimensions of the seleted printer+media.
            var pCapabilities = pQueue.GetPrintCapabilities();
            var printableArea = new Size(pCapabilities.PageImageableArea.ExtentWidth, pCapabilities.PageImageableArea.ExtentHeight);
            var docWriter = PrintQueue.CreateXpsDocumentWriter(pQueue);

            if (docWriter != null && printableArea != null)
            {
                DocumentPaginator paginator = ((IDocumentPaginatorSource)copy).DocumentPaginator;

                // Change the PageSize and PagePadding for the document to match the CanvasSize for the printer device.
                paginator.PageSize = new Size(printableArea.Width, 60000);
                Log4netUtil.For(typeof(Printer)).Debug("printer.printableArea" + printableArea.Width);
                Log4netUtil.For(typeof(Printer)).Debug("printer.printableArea" + printableArea.Height * 2);
                Thickness t = new Thickness(72); // copy.PagePadding;
                //copy.PageWidth = printableArea.Width;
                copy.PagePadding = new Thickness(0, 0, 0, 0);
                copy.ColumnWidth = double.PositiveInfinity;
                copy.PageWidth = printableArea.Width; // allow the page to be the natural with of the output device

                // Send content to the printer.
                docWriter.Write(paginator);
            }
        }

        public static void Print(MainView mainView)
        {
            FlowDocument copy = GetPrintDocument(mainView);

            if (TPConfig.PrinterNames != null)
            {

                //Mutil-Print
                var printServer = new PrintServer();
                var printQueueCollection =
                    printServer.GetPrintQueues(new[]
                    {EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections});
                
                foreach (var item in TPConfig.PrinterNames)
                {
                    if (!String.IsNullOrEmpty(item))
                    {
                        var selectedPrinterName = item.Trim();
                        Log4netUtil.For(typeof(Printer)).Debug("try to print on " + selectedPrinterName);
                        var printQueue = printQueueCollection.FirstOrDefault(i => i.Name.Equals(selectedPrinterName));

                        if (printQueue != null)
                        {
                            try
                            {
                                SinglePrint(copy, printQueue);
                            }
                            catch (Exception e)
                            {
                                Log4netUtil.For(typeof(Printer)).Error(e);
                            }
                            
                        }
                    }
                }
            }
            else
            {
                Log4netUtil.For(typeof (Printer)).Debug("using default printer");
                var pQueue = LocalPrintServer.GetDefaultPrintQueue();
                SinglePrint(copy, pQueue);
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

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestFlowdocument
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //var doc = Application.LoadComponent(new Uri("/FlowDocument1.xaml", UriKind.RelativeOrAbsolute)) as FlowDocument;
            //DocumentScrollViewer.Document = doc;
            DocumentScrollViewer.Document = LoadFlowDocumentScrollViewerWithXAMLFile("FlowDocument1.xaml");
        }
        FlowDocument LoadFlowDocumentScrollViewerWithXAMLFile(string fileName)
        {
            // Open the file that contains the FlowDocument...
            FileStream xamlFile = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            // and parse the file with the XamlReader.Load method.
            FlowDocument content = XamlReader.Load(xamlFile) as FlowDocument;
            // Finally, set the Document property to the FlowDocument object that was
            // parsed from the input file.
            

            xamlFile.Close();

            FlowDocument doc = new FlowDocument();
            var resImg = new Image();
            var path = System.IO.Path.Combine(Environment.CurrentDirectory, "images.png");
            resImg.Source = new BitmapImage(new Uri(path));
            resImg.Width = 100;
            resImg.Height = 100;
            InlineUIContainer resContainer = new InlineUIContainer(resImg);
            Paragraph resPar = new Paragraph(resContainer);
            resPar.TextAlignment = TextAlignment.Center;
            Section s = new Section();
            s.Blocks.Add(resPar);
            doc.Blocks.Add(s);

            for (int i = 0; i < content.Blocks.Count; i++)
            {
                doc.Blocks.Add(content.Blocks.ElementAt(i));
            }

            var qrImg = new Image();
            var bitmap = QRCodeUtils.GetQrCode(string.Format("www.google.com", "test"),
                100,
                100,
                0);
            qrImg.Source = bitmap;
            InlineUIContainer qrContainer = new InlineUIContainer(qrImg);
            Paragraph qrPar = new Paragraph(qrContainer);
            qrPar.TextAlignment = TextAlignment.Center;
            Section sqr = new Section();
            sqr.Blocks.Add(qrPar);
            doc.Blocks.Add(sqr);

            //content.Blocks.Add(s);
            return doc;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var copy = LoadFlowDocumentScrollViewerWithXAMLFile("FlowDocument1.xaml");

            // Create a XpsDocumentWriter object, implicitly opening a Windows common print dialog,
            // and allowing the user to select a printer.

            // get information about the dimensions of the seleted printer+media.
            System.Printing.PrintDocumentImageableArea ia = null;
            System.Windows.Xps.XpsDocumentWriter docWriter = System.Printing.PrintQueue.CreateXpsDocumentWriter(ref ia);

            if (docWriter != null && ia != null)
            {
                DocumentPaginator paginator = ((IDocumentPaginatorSource) copy).DocumentPaginator;

                // Change the PageSize and PagePadding for the document to match the CanvasSize for the printer device.
                paginator.PageSize = new Size(ia.MediaSizeWidth, ia.MediaSizeHeight);
                Thickness t = new Thickness(72); // copy.PagePadding;
                copy.PagePadding = new Thickness(
                    Math.Max(ia.OriginWidth, t.Left),
                    Math.Max(ia.OriginHeight, t.Top),
                    Math.Max(ia.MediaSizeWidth - (ia.OriginWidth + ia.ExtentWidth), t.Right),
                    Math.Max(ia.MediaSizeHeight - (ia.OriginHeight + ia.ExtentHeight), t.Bottom));

                copy.ColumnWidth = double.PositiveInfinity;
                //copy.PageWidth = 528; // allow the page to be the natural with of the output device

                // Send content to the printer.
                docWriter.Write(paginator);
            }
        }
        private void ButtonBase_OnClick1(object sender, RoutedEventArgs e)
        {
            var copy = DocumentScrollViewer.Document;

            // Create a XpsDocumentWriter object, implicitly opening a Windows common print dialog,
            // and allowing the user to select a printer.

            // get information about the dimensions of the seleted printer+media.
            System.Printing.PrintDocumentImageableArea ia = null;
            System.Windows.Xps.XpsDocumentWriter docWriter = System.Printing.PrintQueue.CreateXpsDocumentWriter(ref ia);

            if (docWriter != null && ia != null)
            {
                DocumentPaginator paginator = ((IDocumentPaginatorSource)copy).DocumentPaginator;

                // Change the PageSize and PagePadding for the document to match the CanvasSize for the printer device.
                paginator.PageSize = new Size(ia.MediaSizeWidth, ia.MediaSizeHeight);
                Thickness t = new Thickness(72); // copy.PagePadding;
                copy.PagePadding = new Thickness(
                    Math.Max(ia.OriginWidth, t.Left),
                    Math.Max(ia.OriginHeight, t.Top),
                    Math.Max(ia.MediaSizeWidth - (ia.OriginWidth + ia.ExtentWidth), t.Right),
                    Math.Max(ia.MediaSizeHeight - (ia.OriginHeight + ia.ExtentHeight), t.Bottom));

                copy.ColumnWidth = double.PositiveInfinity;
                //copy.PageWidth = 528; // allow the page to be the natural with of the output device

                // Send content to the printer.
                docWriter.Write(paginator);
            }
        }
    }
}

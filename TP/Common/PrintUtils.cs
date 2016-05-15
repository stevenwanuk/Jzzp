using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Xps.Packaging;
using System.Xml;

namespace TP.Common
{
    public class PrintUtils
    {
        private static string _previewWindowXaml =
            @"<Window
        xmlns                 ='http://schemas.microsoft.com/netfx/2007/xaml/presentation'
        xmlns:x               ='http://schemas.microsoft.com/winfx/2006/xaml'
        Title                 ='Print Preview - @@TITLE'
        Height                ='800'
        Width                 ='600'
        WindowStartupLocation ='CenterOwner'>
        <DocumentViewer Name='dv1'/>
     </Window>";

        public static void DoPreview(string title, FlowDocument fdoc)
        {

            var paginator = ((IDocumentPaginatorSource)fdoc).DocumentPaginator;
            var package = Package.Open(new MemoryStream(), FileMode.Create, FileAccess.ReadWrite);
            var packUri = new Uri("pack://temp.xps");
            PackageStore.RemovePackage(packUri);
            PackageStore.AddPackage(packUri, package);
            var xps = new XpsDocument(package, CompressionOption.NotCompressed, packUri.ToString());
            XpsDocument.CreateXpsDocumentWriter(xps).Write(paginator);
            FixedDocument doc = xps.GetFixedDocumentSequence().References[0].GetDocument(true);


            string s = _previewWindowXaml;
            s = s.Replace("@@TITLE", title.Replace("'", "&apos;"));

            using (var reader = new System.Xml.XmlTextReader(new StringReader(s)))
            {
                Window preview = System.Windows.Markup.XamlReader.Load(reader) as Window;

                DocumentViewer dv1 = LogicalTreeHelper.FindLogicalNode(preview, "dv1") as DocumentViewer;
                dv1.Document = doc;

                preview.ShowDialog();
            }
        }

        public static void DoThePrint(System.Windows.Documents.FlowDocument document)
        {
            // Clone the source document's content into a new FlowDocument.
            // This is because the pagination for the printer needs to be
            // done differently than the pagination for the displayed page.
            // We print the copy, rather that the original FlowDocument.
            System.IO.MemoryStream s = new System.IO.MemoryStream();
            TextRange source = new TextRange(document.ContentStart, document.ContentEnd);
            source.Save(s, DataFormats.Xaml);
            var copy = new System.Windows.Documents.FlowDocument();
            TextRange dest = new TextRange(copy.ContentStart, copy.ContentEnd);
            dest.Load(s, DataFormats.Xaml);

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

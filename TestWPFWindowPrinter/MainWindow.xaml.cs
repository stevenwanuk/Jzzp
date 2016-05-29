using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Management;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestWPFWindowPrinter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetPrintersByQuery();
        }

        public void TestPrinter()
        {

            var m_PrintServer = new PrintServer();
            var m_PrintQueueCollection = m_PrintServer.GetPrintQueues(new[] { EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections });
            foreach (var printQueue in m_PrintQueueCollection)
            {
                
                PrinterLb.Items.Add(printQueue.FullName);
            }

        }



        protected void GetPrintersByQuery()
        {
            var m_PrintServer = new PrintServer();
            var m_PrintQueueCollection = m_PrintServer.GetPrintQueues(new[] { EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections });
            foreach (var printQueue in m_PrintQueueCollection)
            {
                PrinterLb.Items.Add(printQueue.Name);
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

            var m_PrintServer = new PrintServer();
            var m_PrintQueueCollection = m_PrintServer.GetPrintQueues(new[] { EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections });

            foreach (var item in PrinterLb.SelectedItems)
            {
                var selectedPrinterName = item.ToString();
                var list = m_PrintQueueCollection.Where(i => i.Name.Equals(selectedPrinterName)).ToList();

                var flowDocument = GetFlowDocument();
                foreach (var printQueue in list)
                {
                    MessageBox.Show(printQueue.FullName + " is printing");
                    var docWriter = PrintQueue.CreateXpsDocumentWriter(printQueue);
                    DocumentPaginator paginator = ((IDocumentPaginatorSource)flowDocument).DocumentPaginator;
                    var pCapabilities = printQueue.GetPrintCapabilities();
                    var printableArea = new Size(pCapabilities.PageImageableArea.ExtentWidth, pCapabilities.PageImageableArea.ExtentHeight);
                    paginator.PageSize = new Size(printableArea.Width, printableArea.Height);
                    flowDocument.PagePadding = new Thickness(0, 0, 0, 0);
                    flowDocument.ColumnWidth = double.PositiveInfinity;
                    flowDocument.PageWidth = printableArea.Width;
                    docWriter.Write(paginator);
                }
                
            }
        }

        private FlowDocument GetFlowDocument()
        {
            var flow = new FlowDocument();
            
            var resPar = new Paragraph();
            resPar.Inlines.Add(new Run("A test for specific printer"));
            resPar.TextAlignment = TextAlignment.Center;
            Section s = new Section();
            s.Blocks.Add(resPar);
            flow.Blocks.Add(s);
            return flow;
        }
    }

}

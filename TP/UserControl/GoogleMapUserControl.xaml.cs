using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TP.UserControl;

namespace TP.UserControl
{
    /// <summary>
    /// Interaction logic for GoogleMapUserControl.xaml
    /// </summary>
    public partial class GoogleMapUserControl : System.Windows.Controls.UserControl
    {

        string mapURL = AppDomain.CurrentDomain.BaseDirectory + "map/map.html";
        private string routeUrl = "map/map_route.html";
        private void DockPanel_Loaded(object sender, RoutedEventArgs e)
        {
            Load("25.520581,-103.40607", "25.520581, -103.50607");
        }


        public GoogleMapUserControl()
        {
            InitializeComponent();
        }

        protected void Load(string origin, string destination)
        {
            if (File.Exists(routeUrl))
            {
                using (var streamReader = new StreamReader(routeUrl))
                {
                    string line = streamReader.ReadToEnd();
                    line = line.Replace("[origin]", origin);
                    line = line.Replace("[destination]", destination);
                    MapWb.NavigateToString(line);
                }
            }
        }

        private void MapWb_OnLoaded(object sender, RoutedEventArgs e)
        {
            //((WebBrowser)sender).ObjectForScripting = new HtmlInteropInternalTestClass();
        }
        [System.Runtime.InteropServices.ComVisibleAttribute(true)]
        public class HtmlInteropInternalTestClass
        {
            public void endDragMarkerCS(Decimal Lat, Decimal Lng)
            {
                //((GoogleMapUserControl)Application.Current.MainWindow.Content).tbLocation.Text = Math.Round(Lat, 5) + "," + Math.Round(Lng, 5);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
    public partial class GoogleMapUserControl : System.Windows.Controls.UserControl, INotifyPropertyChanged
    {

        public string OPostCode
        {
            get { return (string)GetValue(OPostCodeProperty); }
            set { SetValue(OPostCodeProperty, value); }
        }
        public static readonly DependencyProperty OPostCodeProperty = DependencyProperty.Register("OPostCode", typeof(string), typeof(GoogleMapUserControl), new PropertyMetadata(OnCustomerChangedCallBack));

        private static void OnCustomerChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var thiscontrol = sender as GoogleMapUserControl;

            if (thiscontrol != null && (!string.IsNullOrEmpty(thiscontrol.OPostCode) || !string.IsNullOrEmpty(thiscontrol.DPostCode)))
            {
                thiscontrol.Load(thiscontrol.OPostCode, thiscontrol.DPostCode);
            }
        }

        public string DPostCode
        {
            get { return (string)GetValue(DPostCodeProperty); }
            set { SetValue(DPostCodeProperty, value); }
        }
        public static readonly DependencyProperty DPostCodeProperty = DependencyProperty.Register("DPostCode", typeof(string), typeof(GoogleMapUserControl), new PropertyMetadata(OnCustomerChangedCallBack));


        string mapURL = AppDomain.CurrentDomain.BaseDirectory + "map/map.html";
        private string routeUrl = "map/map_route.html";

        private string GMapUrl = ConfigurationManager.AppSettings["GMap"];

        public event PropertyChangedEventHandler PropertyChanged;

        private void DockPanel_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(OPostCode) && !string.IsNullOrEmpty(DPostCode))
            {
                Load(OPostCode, DPostCode);
            }
        }


        public GoogleMapUserControl()
        {
            InitializeComponent();
        }

        protected void Load(string origin, string destination)
        {

            if (!string.IsNullOrEmpty(GMapUrl))
            {
                //var url = string.Format(GMapUrl, this.OPostCode, this.DPostCode);
                var url = string.Format(GMapUrl, this.DPostCode);
                MapWb.Navigate(url);
            }
            /**
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
            **/
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataMaintenance.View;
using TP.BLL;
using TP.ModelView;

namespace DataMaintenance.WindowForm
{
    /// <summary>
    /// Interaction logic for DriverWindow.xaml
    /// </summary>
    public partial class DriverWindow : Window
    {

        public DriverWindowView DriverWv { get; set; }

        public DriverWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void DriverWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            DriverWv = new DriverWindowView();
            var drivers = new TPDriverBLL().GetDriversIfAvailable();

            drivers.ForEach(i => DriverWv.TpDriverMvs.Add(TPDriverMV.Mapper(i)));
        }

        private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

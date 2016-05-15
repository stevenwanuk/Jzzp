using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        private List<long> RemovingList = new List<long>();

        public DriverWindowView DriverWv { get; set; }

        public DriverWindow()
        {
            InitializeComponent();
            DriverWv = new DriverWindowView();
            this.DataContext = this.DriverWv;
        }

        private void DriverWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Reload();
        }

        private void Reload()
        {
            DriverWv.TpDriverMvs.Clear();
            var drivers = new TPDriverBLL().GetDriversIfAvailable();
            drivers.ForEach(i => DriverWv.TpDriverMvs.Add(TPDriverMV.Mapper(i)));


            foreach (var tpDriverMv in DriverWv.TpDriverMvs)
            {
                tpDriverMv.DeliverCount = new TPDriverBLL().GetDeliverCount(tpDriverMv.DriverId);
            }

            RemovingList.Clear();
        }

        private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
        {
            DriverGrid.CanUserAddRows = true;
            DriverGrid.IsReadOnly = false;
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {

            DriverGrid.CanUserAddRows = false;
            DriverGrid.IsReadOnly = true;

            var driverMvs = DriverWv.TpDriverMvs;
            foreach (var driverMv in driverMvs)
            {
                if (!string.IsNullOrEmpty(driverMv.FirstName) && !string.IsNullOrEmpty(driverMv.LastName))
                {
                    new TPDriverBLL().SaveOrUpdate(driverMv.MapperTo());
                }
            }

            Reload();
        }


        private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            RemovingList.ForEach(i => new TPDriverBLL().Remove(i));
            RemovingList.Clear();

            Reload();
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {

            var select = (sender as CheckBox);
            var driverId = (long) select.Tag;
            if (select.IsChecked.Value)
            {
                if (!RemovingList.Contains(driverId))
                {
                    RemovingList.Add(driverId);
                }
            }
            else
            {
                RemovingList.Remove(driverId);
            }
        }
    }
}

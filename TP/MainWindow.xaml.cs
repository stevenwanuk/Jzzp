using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Jzzp.DAL;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using TP.ModelView;
using TP.View;

namespace TP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        public MainView mainView { get; set; }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                mainView = new MainView();
                mainView.callInMvs = new ObservableCollection<TPCallInMV>();
                var callins = new TPCallInDAL().GetCallInsByBillStatus();
                callins.ForEach(i => mainView.callInMvs.Add(TPCallInMV.Mapper(i)));
                this.DataContext = mainView;
                mainView.callInMvs.First().CallInId = 2;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }
    }
}

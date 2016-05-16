using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
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
using System.Linq.Expressions;
using System.Security.Policy;
using DataMaintenance.View;
using Microsoft.Practices.Prism.Commands;
using TP.Common;
using TP.Common.CustomConfig;

namespace DataMaintenance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindowView WindowView { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            WindowView = new MainWindowView
            {
                StartupList = GetAppStartupSettings(),
                OpenWindowCommand = new DelegateCommand<string>(this.OnSubmit, this.CanSubmit)
            };
            
            this.DataContext = WindowView;
        }

        private void OnSubmit(string uri)
        {

            if (!string.IsNullOrEmpty(uri))
            {
                if (Application.Current.Windows.OfType<Window>().Any(i => uri.Contains(i.GetType().Name)))
                {
                    Application.Current.Windows.OfType<Window>().FirstOrDefault(i => uri.Contains(i.GetType().Name))?.Show();
                }
                else
                {
                    try
                    {
                        var window = (Window) Application.LoadComponent(new Uri(uri, UriKind.Relative));
                        window.Show();
                    }
                    catch (IOException e)
                    {
                        MessageBox.Show("No found " + uri);
                    }
                    
                }
            }
        }
        private bool CanSubmit(string arg) { return true; }

        protected ObservableCollection<StartupConfigurationElement> GetAppStartupSettings()
        {

            ObservableCollection<StartupConfigurationElement> result = new ObservableCollection<StartupConfigurationElement>();
            var startConfigs = ConfigurationManager.GetSection("dispatcherConfigurationSection") as DispatcherConfigurationSection;
            if (startConfigs != null)
            {

                //1,Looking for command
                startConfigs.Startups.OfType<StartupConfigurationElement>()
                    .Where(i => !i.IsNavigation).ToList().ForEach(i =>result.Add(i));
            }

            return result;
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            ;
        }
    }
}

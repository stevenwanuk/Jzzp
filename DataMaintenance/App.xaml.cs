using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Xml.XPath;
using TP.BLL;
using TP.Common;
using TP.Common.CustomConfig;

namespace DataMaintenance
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {

            //Binding error handler
            Application.Current.DispatcherUnhandledException +=
                new DispatcherUnhandledExceptionEventHandler(AppDispatcherUnhandledException);

            //Setup chulture to GB
            ProjectSetUpUtils.SetDefaultCulture(new CultureInfo("en-GB"));

            //Init Automapper
            Log4netUtil.For(this).Info("Init Automapper start");
            AutoMapperUtils.InitAutoMapper();
            Log4netUtil.For(this).Info("Init Automapper end");
            //DB Test
            Log4netUtil.For(this).Info("Test db start");
            new DBBLL().DBTestBillRef();
            Log4netUtil.For(this).Info("Test db end");
        }

        void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
#if DEBUG // In debug mode do not custom-handle the exception, let Visual Studio handle it

            //e.Handled = false;

#else

            ShowUnhandeledException(e);    

#endif
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            
            var startupCommand = e.Args.FirstOrDefault();

            var config = GetStartupOrDefaultByCommand(startupCommand);

            if (config == null)
            {
                MessageBox.Show(string.Format("No startup setting with {0} Command", startupCommand));
                base.Shutdown();
            }
            else
            {

                base.OnStartup(e);

                var mainWindow = (Window)Application.LoadComponent(new Uri(config.FileUri, UriKind.Relative));
                base.MainWindow = mainWindow;
                this.ShutdownMode = ShutdownMode.OnMainWindowClose;
                mainWindow.Show();
            }
        }

        protected StartupConfigurationElement GetStartupOrDefaultByCommand(string command)
        {
            StartupConfigurationElement result = null;
            var startConfigs = ConfigurationManager.GetSection("dispatcherConfigurationSection") as DispatcherConfigurationSection;
            if (startConfigs != null)
            {

                //1,Looking for command
                result = startConfigs.Startups
                .OfType<StartupConfigurationElement>()
                .FirstOrDefault(i => i.StartupCommmand.Equals(command));

                if (result == null)
                {

                    //2,looking for navigation
                    result = startConfigs.Startups
                        .OfType<StartupConfigurationElement>()
                        .FirstOrDefault(i => i.IsNavigation);
                }
            }
            
            return result;
        }

        void ShowUnhandeledException(DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            string errorMessage =
                string.Format(
                    "An application error occurred.\nPlease check whether your data is correct and repeat the action. If this error occurs again there seems to be a more serious malfunction in the application, and you better close it.\n\nError:{0}\n\nDo you want to continue?\n(if you click Yes you will continue with your work, if you click No the application will close)",

                    e.Exception.Message + (e.Exception.InnerException != null
                        ? "\n" +
                          e.Exception.InnerException.Message
                        : null));

            if (
                MessageBox.Show(errorMessage, "Application Error", MessageBoxButton.YesNoCancel, MessageBoxImage.Error) ==
                MessageBoxResult.No)
            {
                if (
                    MessageBox.Show(
                        "WARNING: The application will close. Any changes will not be saved!\nDo you really want to close it?",
                        "Close the application!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) ==
                    MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
            }
        }
    }
}

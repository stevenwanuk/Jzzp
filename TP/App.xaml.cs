using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Newtonsoft.Json;
using TP.BLL;
using TP.Common;
using TP.Gmap;
using TP.ModelView;

namespace TP
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

            //DeliveryCaculator test
            Log4netUtil.For(this).Info("DeliveryCaculator test start");
            var i = DeliveryFeeCaculator.GetDeliveryFee(5);
            var j = DeliveryFeeCaculator.GetDeliveryFee(2, 5);
            Log4netUtil.For(this).Info("DeliveryCaculator test end");

        }


        public void test()
        {
            var a = new TPUserAddressMV();
            var postCode = "WC1H9JP";
            if (!string.IsNullOrEmpty(postCode))
            {
                
                GmapUtils.GetGeoCode(postCode, (o, args) =>
                {

                    MessageBox.Show(args.Result);
                    var geoResponse = JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(args.Result);

                    if (geoResponse != null)
                    {

                        a.RenderFromGoogleGeoCodeResponse(geoResponse);
                    }
                });

                GmapUtils.GetDriection(postCode, (o, args) =>
                {

                    MessageBox.Show(args.Result);
                    var geoResponse = JsonConvert.DeserializeObject<GoogleDirectionsResponse>(args.Result);

                    if (geoResponse != null)
                    {

                        a.RenderFromGoogleDirectionsResponse(geoResponse);
                    }
                });
            }
        }



        void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {

            Log4netUtil.For(this).Error(e.ToString(), e.Exception);

#if DEBUG // In debug mode do not custom-handle the exception, let Visual Studio handle it

            //e.Handled = false;

#else

            ShowUnhandeledException(e);

#endif

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

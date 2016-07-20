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
using TP.AppStatic;
using System.Security;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace TP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct MyStruct
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string Message;
        }
        internal const int WM_COPYDATA = 0x004A;
        [StructLayout(LayoutKind.Sequential)]
        internal struct COPYDATASTRUCT
        {
            public IntPtr dwData;       // Specifies data to be passed
            public int cbData;          // Specifies the data size in bytes
            public IntPtr lpData;       // Pointer to data to be passed
        }
        [SuppressUnmanagedCodeSecurity]
        internal class NativeMethod
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr SendMessage(IntPtr hWnd, int Msg,
                IntPtr wParam, ref COPYDATASTRUCT lParam);


            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        }


        private void Singleton()
        {

            if (System.Diagnostics.Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Count() > 1)
            //if (System.Diagnostics.Process.GetProcessesByName("TestSingleton.vshost").Where(i=> i.Id != Process.GetCurrentProcess().Id).Count() >= 1)
            {
                IntPtr hTargetWnd = NativeMethod.FindWindow(null, "MainWindow");
                if (hTargetWnd == IntPtr.Zero)
                {
                    return;
                }
                MyStruct myStruct;
                myStruct.Message = "Show Up";
                int myStructSize = Marshal.SizeOf(myStruct);
                IntPtr pMyStruct = Marshal.AllocHGlobal(myStructSize);
                try
                {
                    Marshal.StructureToPtr(myStruct, pMyStruct, true);

                    COPYDATASTRUCT cds = new COPYDATASTRUCT();
                    cds.cbData = myStructSize;
                    cds.lpData = pMyStruct;
                    NativeMethod.SendMessage(hTargetWnd, WM_COPYDATA, new IntPtr(), ref cds);

                    int result = Marshal.GetLastWin32Error();
                    if (result != 0)
                    {
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(pMyStruct);
                }
                Application.Current.Shutdown(0);
            }
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {

            Singleton();
            //Binding error handler
            Application.Current.DispatcherUnhandledException +=
                new DispatcherUnhandledExceptionEventHandler(AppDispatcherUnhandledException);

            //config log4net
            log4net.Config.XmlConfigurator.Configure();

            //Setup chulture to GB
            ProjectSetUpUtils.SetDefaultCulture(new CultureInfo("en-GB"));

            //Load Appsetting
            Log4netUtil.For(this).Info("Init App setting start");
            var tpLoad = TPConfig.Load();
            Log4netUtil.For(this).Info("Init App setting end:" + tpLoad);

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

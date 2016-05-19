using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;
using System.Windows;

namespace TestSingleton
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMAXIMIZED = 3;
        private const int SW_RESTORE = 9;

        [DllImport("user32.dll")]
        private static extern Boolean ShowWindow(IntPtr hWnd, Int32 nCmdShow);

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

        private void App_OnStartup(object sender, StartupEventArgs e)
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
    }
}

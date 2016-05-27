using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using TP.Common;
using TP.ModelView;
using TP.View;
using TP.WindowForm;
using EntitiesDABL.DAL;
using EntitiesDABL;
using Newtonsoft.Json;
using TP.BLL;
using TP.Gmap;
using System.IO;
using System.Xml;
using System.Windows.Markup;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using TP.Common.StringLib;
using Xceed.Wpf.AvalonDock.Controls;
using TP.Pstn;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using TP.printing;
using System.Text;
using TP.AppStatic;
using System.Windows.Media;

namespace TP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HwndSource source;

        #region Init
        public MainWindow()
        {
            InitializeComponent();
            InitConfig();
            this.FontSize = TPConfig.FontSize;
            this.FontFamily = new FontFamily(TPConfig.FontFamily);
        }

        public int terminalId = 0;

        protected void InitConfig()
        {
            terminalId = TPConfig.TerminalId;
        }


        public MainView mainView { get; set; }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                mainView = new MainView();
                this.DataContext = mainView;

                SetupTimer();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }
        #endregion Init

        #region Timer
        protected void SetupTimer()
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);

            dispatcherTimer_Tick(this, null);

            //dispatcherTimer.Start();
        }



        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            var tPBillRefs = new TPBillRefBLL().GetDisplayedTPBillRef(terminalId);

            foreach (var existedMV in mainView.TPBillRefs)
            {

                if (!tPBillRefs.Any(i => i.BillRefId.Equals(existedMV.BillRefId)))
                {
                    //mainView.TPBillRefs.Remove(existedMV);
                }
            }

            foreach (var temp in tPBillRefs)
            {

                if (!mainView.TPBillRefs.Any(i => i.BillRefId.Equals(temp.BillRefId)))
                {
                    var newBillRef = TPBillRefMV.Mapper(temp);
                    newBillRef.TPCallIn = TPCallInMV.Mapper(temp.TPCallIn);

                    mainView.TPBillRefs.Add(newBillRef);
                }
            }

            if (mainView.SelectedTpBillRefMv == null ||
                !mainView.TPBillRefs.Any(i => i.BillRefId.Equals(mainView.SelectedTpBillRefMv.BillRefId)))
            {
                mainView.SelectedTpBillRefMv = mainView.TPBillRefs.FirstOrDefault();
            }
        }
        #endregion Timer

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var billRefId = (long)((ToggleButton)sender).Tag;

            mainView.SelectedTpBillRefMv = mainView.TPBillRefs.Where(i => i.BillRefId == billRefId).FirstOrDefault();

            LoadTabControlView(billRefId);

        }

        private void LoadTabControlView(long BillRefId)
        {
            switch (tPTabControl.SelectedIndex)
            {

                case 0:
                    mainView.UsersTabView = new UsersTabView(BillRefId);

                    break;
                case 1:
                    mainView.OrderHistoryTabView = new OrderHistoryTabView(BillRefId);
                    break;
                case 2:
                    mainView.DeliveryTabView = new DeliveryTabView(BillRefId);
                    break;
            }
        }

        private void QueryUser_OnClick(object sender, RoutedEventArgs e)
        {
            //TODO
            MessageBox.Show(this.mainView.UsersTabView.TPBillRefMV.TPUser.FirstName);

            var userWindow = new UsersWindow();
            userWindow.RaiseCustomEvent += QUeryUserCallBack;
            userWindow.Owner = this;
            userWindow.ShowDialog();
        }

        protected void QUeryUserCallBack(object sender, CustomEventArgs e)
        {

            Guid userId;
            if (!string.IsNullOrEmpty(e.Message) && Guid.TryParse(e.Message, out userId))
            {

                if (mainView.SelectedTpBillRefMv != null)
                {
                    //Update userId
                    var currTPBillRefId = this.mainView.SelectedTpBillRefMv.BillRefId;
                    new TPBillRefBLL().UpdateBillRefUser(currTPBillRefId, userId);

                    LoadTabControlView(currTPBillRefId);
                    //LoadView
                    /**
                    var userMV = TPUserMV.Mapper(new TPUserBLL().GeTPUserById(userId));
                    mainView.SelectedTpBillRefMv.UserId_FK = userId;
                    mainView.SelectedTpBillRefMv.TPUser = userMV;

                    //load userAddressList
                    var userAddress = new TPUserAddressBLL().GetTPUserAddressByUserId(userId);

                    var addressMv = mainView.SelectedTpBillRefMv.TPUser.TPUserAddress;
                    addressMv.Clear();
                    userAddress.ForEach(i => addressMv.Add(TPUserAddressMV.Mapper(i)));
                    **/
                }
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((TPUserAddressMV)((ListBox)sender).SelectedItem != null)
            {
                mainView.UsersTabView.TPUserAddressMV = (TPUserAddressMV)((ListBox)sender).SelectedItem;
            }
            else
            {
                mainView.UsersTabView.TPUserAddressMV = new TPUserAddressMV();
            }

        }

        #region User

        private void UserClear_OnClick(object sender, RoutedEventArgs e)
        {

            mainView.UsersTabView.TPUserMV = new TPUserMV()
            {
                UserId = Guid.NewGuid()
            };

            mainView.UsersTabView.TPUserAddressMVs.Clear();
            mainView.UsersTabView.TPUserAddressMV = new TPUserAddressMV();
        }

        private void UserSave_OnClick(object sender, RoutedEventArgs e)
        {

            if (mainView.SelectedTpBillRefMv == null)
            {
                mainView.ErrorMsg = "No selected call";
                return;
            }
            
            var userMV = mainView.UsersTabView.TPUserMV;
            mainView.ErrorMsg = "Saved";

            var user = userMV.MapperTo();
            if (user.UserId == Guid.Empty)
            {
                user.UserId = Guid.NewGuid();
            }
            var billRefId = mainView.UsersTabView.TPBillRefMV.BillRefId;
            new TPBillRefBLL().SaveUser(billRefId, user);

            LoadTabControlView(billRefId);
        }

        #endregion User

        #region UserAddress

        private void UserAddressClear_OnClick(object sender, RoutedEventArgs e)
        {
            mainView.UsersTabView.TPUserAddressMV = new TPUserAddressMV();
        }

        private void UserAddressRemove_OnClick(object sender, RoutedEventArgs e)
        {

            if (mainView.SelectedTpBillRefMv == null)
            {
                mainView.ErrorMsg = "No selected call";
                return;
            }

            var selectedItem = mainView.UsersTabView.TPUserAddressMV;
            if (selectedItem != null && mainView.UsersTabView.TPUserAddressMVs.Any(i => i.UserAddressId == selectedItem.UserAddressId))
            {

                new TPBillRefBLL().RemoveUserAddress(mainView.UsersTabView.TPBillRefMV.BillRefId, selectedItem.UserAddressId);
                mainView.UsersTabView = new UsersTabView(mainView.UsersTabView.TPBillRefMV.BillRefId);
            }

        }

        private void UserAddressSave_OnClick(object sender, RoutedEventArgs e)
        {

            if (mainView.SelectedTpBillRefMv == null)
            {
                mainView.ErrorMsg = "No selected call";
                return;
            }

            var userAddressMV = mainView.UsersTabView.TPUserAddressMV;
            var userAddress = userAddressMV.MapperTo();
            var billRefId = mainView.UsersTabView.TPBillRefMV.BillRefId;

            if (mainView.UsersTabView.TPBillRefMV.UserId_FK != null)
            {
                userAddress.UserId_FK = mainView.UsersTabView.TPBillRefMV.UserId_FK.Value;


                new TPBillRefBLL().SaveAddress(billRefId, userAddress);
                new TPBillRefBLL().SaveDliveryInfos(billRefId,
                    mainView.UsersTabView.TPUserAddressMV.DeliveryMiles, mainView.UsersTabView.TPBillRefMV.DeliverFeeOrigin);

                LoadTabControlView(billRefId);
            }

        }

        #endregion UserAddress

        private void TPTabControl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //Because all tab elemens' selectionchanged events are past to here, need to know where it comes from
            if (e.OriginalSource is TabControl)
            {
                if (mainView != null && mainView.SelectedTpBillRefMv != null)
                {

                    var billRefId = mainView.SelectedTpBillRefMv.BillRefId;
                    LoadTabControlView(billRefId);
                }
            }
        }


        private void DriverRadioButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var derverRB = (RadioButton)sender;
            var driverId = (long)derverRB.Tag;
            var billRefId = mainView.DeliveryTabView.TPBillRefMV?.BillRefId;
            if (billRefId != null && driverId != 0)
            {
                new TPDriverBLL().SaveOrUpdate(driverId, billRefId.Value);
                LoadTabControlView(billRefId.Value);
            }
        }

        private void DeliveryCaculator_OnClick(object sender, RoutedEventArgs e)
        {
            if (mainView.SelectedTpBillRefMv == null)
            {
                mainView.ErrorMsg = "No selected call";
                return;
            }

            var deliverMiles = mainView.DeliveryTabView.TPBillRefMV.DeliverMiles;
            if (deliverMiles == null)
            {
                mainView.ErrorMsg = "No deliverMiles";
                return;
            }


            
            var billId = mainView.DeliveryTabView.TPBillRefMV.BillId_FK;
            var deliveryFee = decimal.Zero;
            if (string.IsNullOrEmpty(billId))
            {
                deliveryFee = DeliveryFeeCaculator.GetDeliveryFee(deliverMiles.Value);
            }
            else
            {

                var bill = new JzzpBillBLL().GetTempBillByBillId(billId);
                if (bill != null && bill.TempBill != null)
                {
                    var amount = bill.TempBill.SumToPay;
                    if (amount != null)
                    {
                        deliveryFee = DeliveryFeeCaculator.GetDeliveryFee(deliverMiles.Value, amount.Value);
                    }
                }
            }

            if (deliverMiles != null)
            {


                mainView.DeliveryTabView.TPBillRefMV.DeliverFee = deliveryFee;
            }
        }
        private void DeliverySave_OnClick(object sender, RoutedEventArgs e)
        {
            if (mainView.SelectedTpBillRefMv == null)
            {
                mainView.ErrorMsg = "No selected call";
                return;
            }

            if (mainView.DeliveryTabView.TPBillRefMV != null)
            {

                var billRefId = mainView.DeliveryTabView.TPBillRefMV.BillRefId;
                var deliveryMiles = mainView.DeliveryTabView.TPBillRefMV.DeliverMiles;
                var deliveryFee = mainView.DeliveryTabView.TPBillRefMV.DeliverFee;

                new TPBillRefBLL().UpdateDeliveryInfos(billRefId, deliveryMiles, deliveryFee);

                //Refresh usercontrol
                mainView.DeliveryTabView.TPBillRefMV.DeliverFee = deliveryFee;
                mainView.DeliveryTabView.TPBillRefMV.DeliverMiles = deliveryMiles;

                BUControl.LoadBill();
            }

        }

        private void BillCbx_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = mainView.DeliveryTabView.UnBindingBillMvs.SelectedItem;

            if (mainView.SelectedTpBillRefMv != null)
            {
               //Bind billid
                new TPBillRefBLL().BindingBillId(mainView.SelectedTpBillRefMv.BillRefId, item?.BillID);
                mainView.SelectedTpBillRefMv.BillId_FK = item?.BillID;
                if (mainView.DeliveryTabView.TPBillRefMV != null)
                {
                    mainView.DeliveryTabView.TPBillRefMV.BillId_FK = item?.BillID;
                }

                BUControl.LoadBill();
            }

            
        }

        private void GmapBtn_Click(object sender, RoutedEventArgs e)
        {
            var postCode = mainView.UsersTabView.TPUserAddressMV.Postcode;
            if (!string.IsNullOrEmpty(postCode))
            {
                var gmap = new GMapWindow();
                gmap.Owner = this;
                gmap.GMapUC.DPostCode = postCode;
                gmap.Show();
            }
        }

        private void LCodeBtn_Click(object sender, RoutedEventArgs e)
        {
            var postCode = mainView.UsersTabView.TPUserAddressMV.Postcode;
            if (!string.IsNullOrEmpty(postCode))
            {
                var address = new TPAddressBLL().GetAddressByPostCode(postCode);
                if (address != null)
                {
                    mainView.UsersTabView.TPUserAddressMV.RenderFromTpAddress(address);
                }
            }
        }

        private void GCodeBtn_Click(object sender, RoutedEventArgs e)
        {
            var postCode = mainView.UsersTabView.TPUserAddressMV.Postcode;
            if (!string.IsNullOrEmpty(postCode))
            {
                //looking for address
                GmapUtils.GetGeoCode(postCode, (o, args) =>
                {

                    //MessageBox.Show(args.Result);
                    var geoResponse = JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(args.Result);

                    if (geoResponse != null)
                    {

                        mainView.UsersTabView.TPUserAddressMV.RenderFromGoogleGeoCodeResponse(geoResponse);
                    }
                });

                GmapUtils.GetDriection(postCode, (o, args) =>
                {

                    //MessageBox.Show(args.Result);
                    var geoResponse = JsonConvert.DeserializeObject<GoogleDirectionsResponse>(args.Result);

                    if (geoResponse != null)
                    {

                        mainView.UsersTabView.TPUserAddressMV.RenderFromGoogleDirectionsResponse(geoResponse);

                        //caculator intial delivery fee
                        var deliveryMiles = mainView.UsersTabView.TPBillRefMV.DeliverMiles;
                        if (deliveryMiles != null && deliveryMiles > 0)
                        {
                            mainView.UsersTabView.TPBillRefMV.DeliverFee = DeliveryFeeCaculator.GetDeliveryFee(deliveryMiles.Value);
                        }


                    }
                });
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {

            var currTextBox = sender as TextBox;
            decimal deliverMiles = 0;
            if (Decimal.TryParse(currTextBox.Text, out deliverMiles))
            {
                var deliveryFee = DeliveryFeeCaculator.GetDeliveryFee(deliverMiles);
                mainView.UsersTabView.TPBillRefMV.DeliverFeeOrigin = deliveryFee;
            }
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {

            var msg = string.Empty;
            try
            {

                msg = Printer.DataContainerCheck(mainView);
                if (string.IsNullOrEmpty(msg))
                {
                    Printer.Print(mainView);
                    new TPBillRefBLL().UpdateBillStatusAfterPrint(mainView.DeliveryTabView.TPBillRefMV.BillRefId);
                    msg = "Print Successful";
                }
            }
            catch (Exception ex)
            {
                Log4netUtil.For(this).Error(ex);
                msg = ex.ToString();
            }
            mainView.ErrorMsg = msg;
        }



        #region winmsg

        internal const int WM_COPYDATA = 0x004A;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct MyStruct
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string Message;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct COPYDATASTRUCT
        {
            public IntPtr dwData;       // Specifies data to be passed
            public int cbData;          // Specifies the data size in bytes
            public IntPtr lpData;       // Pointer to data to be passed
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {

                case WM_COPYDATA:
                    {
                        // Get the COPYDATASTRUCT struct from lParam.
                        var cds = (COPYDATASTRUCT)Marshal.PtrToStructure(lParam, typeof(COPYDATASTRUCT));

                        // If the size matches
                        if (cds.cbData == Marshal.SizeOf(typeof(MyStruct)))
                        {
                            // Marshal the data from the unmanaged memory block to a 
                            // MyStruct managed struct.
                            MyStruct myStruct = (MyStruct)Marshal.PtrToStructure(cds.lpData,
                                typeof(MyStruct));

                            // Display the MyStruct data members.
                            if (myStruct.Message == "Show Up")
                            {
                                this.Show();
                            }
                        }
                        break;
                    }

                case BriSDKLib.BRI_EVENT_MESSAGE:
                    {
                        BriSDKLib.TBriEvent_Data EventData = (BriSDKLib.TBriEvent_Data)Marshal.PtrToStructure(lParam, typeof(BriSDKLib.TBriEvent_Data));
                        string strValue = "";
                        switch (EventData.lEventType)
                        {
                            case BriSDKLib.BriEvent_PhoneHook:
                                {
                                    strValue = "通道" + (EventData.uChannelID + 1).ToString() + "：电话机摘机";
                                }
                                break;
                            case BriSDKLib.BriEvent_PhoneHang:
                                {
                                    strValue = "通道" + (EventData.uChannelID + 1).ToString() + "：电话机挂机";
                                }
                                break;
                            case BriSDKLib.BriEvent_CallIn:
                                {////两声响铃结束后开始呼叫转移到CC
                                    strValue = "通道" + (EventData.uChannelID + 1).ToString() + "：来电响铃";
                                }
                                break;
                            case BriSDKLib.BriEvent_GetCallID:
                                {

                                    var telno = StringUtils.FromASCIIByteArray(EventData.szData);
                                    strValue = "通道" + (EventData.uChannelID + 1).ToString() + "：接收到来电号码 " + telno;
                                    AppendStatus(strValue);
                                    AddNewBillRef(telno);

                                }
                                break;
                            case BriSDKLib.BriEvent_StopCallIn:
                                {
                                    strValue = "通道" + (EventData.uChannelID + 1).ToString() + "：停止呼入，产生一个未接电话 ";
                                }
                                break;
                            case BriSDKLib.BriEvent_GetDTMFChar: strValue = "通道" + (EventData.uChannelID + 1).ToString() + "：接收到按键 " + StringUtils.FromASCIIByteArray(EventData.szData); break;
                            case BriSDKLib.BriEvent_RemoteHang:
                                {
                                    strValue = "通道" + (EventData.uChannelID + 1).ToString() + "：远程挂机 ";
                                }
                                break;
                            case BriSDKLib.BriEvent_Busy:
                                {

                                    strValue = "通道" + (EventData.uChannelID + 1).ToString() + "：接收到忙音,线路已经断开 ";
                                }
                                break;
                            case BriSDKLib.BriEvent_DialTone: strValue = "通道" + (EventData.uChannelID + 1).ToString() + "：检测到拨号音 "; break;
                            case BriSDKLib.BriEvent_PhoneDial: strValue = "通道" + (EventData.uChannelID + 1).ToString() + "：电话机拨号 " + StringUtils.FromASCIIByteArray(EventData.szData); break;
                            case BriSDKLib.BriEvent_RingBack: strValue = "通道" + (EventData.uChannelID + 1).ToString() + "：拨号后接收到回铃音 "; break;
                            case BriSDKLib.BriEvent_DevErr:
                                {
                                    if (EventData.lResult == 3)
                                    {
                                        strValue = "通道" + (EventData.uChannelID + 1).ToString() + "：设备可能被移除 ";
                                    }
                                }
                                break;
                            default: break;
                        }
                        if (strValue.Length > 0)
                        {
                            AppendStatus(strValue);
                        }

                        handled = true;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        private void AddNewBillRef(string telno)
        {

            var billRef = new TPBillRefBLL().CreatNewBillRefWithDefaultDriver(telno, terminalId, TPConfig.DefaultDriverName);
            if (mainView.TPBillRefs != null)
            {

                var billRefMv = TPBillRefMV.Mapper(billRef);
                if (billRef.TPCallIn != null)
                {
                    billRefMv.TPCallIn = TPCallInMV.Mapper(billRef.TPCallIn);
                }

                mainView.TPBillRefs.Add(billRefMv);
            }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            source = PresentationSource.FromVisual(this) as HwndSource;
            AppendStatus("register handle with source:" + source.Handle);
            source.AddHook(WndProc);

            //open device

            if (BriSDKLib.QNV_OpenDevice(BriSDKLib.ODT_LBRIDGE, 0, "") <= 0 || BriSDKLib.QNV_DevInfo(0, BriSDKLib.QNV_DEVINFO_GETCHANNELS) <= 0)
            {

                MessageBox.Show("can't open fax modem");
                //throw new IOException("can't open fax modem");
            }

            for (Int16 i = 0; i < BriSDKLib.QNV_DevInfo(-1, BriSDKLib.QNV_DEVINFO_GETCHANNELS); i++)
            {//在windowproc处理接收到的消息
                BriSDKLib.QNV_Event(i, BriSDKLib.QNV_EVENT_REGWND, (Int32)source.Handle, "", new StringBuilder(0), 0);
            }

            AppendStatus("打开设备成功");
        }

        private void AppendStatus(string str)
        {
            Log4netUtil.For(this).Info(str);
        }

        private void MWindow_Closed(object sender, EventArgs e)
        {
            try
            {
                BriSDKLib.QNV_CloseDevice(BriSDKLib.ODT_ALL, 0);
            }
            catch
            {

            }

        }

        #endregion winmsg

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddNewBillRef(string.Empty);
            mainView.SelectedTpBillRefMv = mainView.TPBillRefs.LastOrDefault();
            if (mainView.SelectedTpBillRefMv != null)
            {
                LoadTabControlView(mainView.SelectedTpBillRefMv.BillRefId);
            }
        }


        private void CallInSave_Click(object sender, RoutedEventArgs e)
        {
            if (mainView.SelectedTpBillRefMv.TPCallIn != null)
            {
                var telno = mainView.SelectedTpBillRefMv.TPCallIn.CellNumber;
                if (!String.IsNullOrEmpty(telno))
                {
                    new TPCallInBLL().UpdateCellNumber(telno, mainView.SelectedTpBillRefMv.TPCallIn.CallInId, mainView.SelectedTpBillRefMv.BillRefId);
                    LoadTabControlView(mainView.SelectedTpBillRefMv.BillRefId);
                }
            }


        }

        private void CloseCurrTab_Click(object sender, RoutedEventArgs e)
        {
            if (mainView.SelectedTpBillRefMv != null)
            {
                new TPBillRefBLL().UpdateShowStatus(mainView.SelectedTpBillRefMv.BillRefId, false);
                mainView.TPBillRefs.Remove(mainView.SelectedTpBillRefMv);
                mainView.SelectedTpBillRefMv = mainView.TPBillRefs.LastOrDefault();
                if (mainView.SelectedTpBillRefMv != null)
                {
                    LoadTabControlView(mainView.SelectedTpBillRefMv.BillRefId);
                }

            }
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            (sender as ToggleButton).IsChecked = true;
        }

        private void HideBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            if (mainView.SelectedTpBillRefMv != null)
            {
                mainView.DeliveryTabView.UnBindingBillMvs.SelectedItem = null;
            }
        }

        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            BUControl.LoadBill();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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
using TP.BLL;

namespace TP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Init
        public MainWindow()
        {
            InitializeComponent();
            InitConfig();
        }

        public int terminalId = 0;

        protected void InitConfig()
        {
            terminalId = Convert.ToInt32(ConfigurationManager.AppSettings["TerminalId"]);
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

            dispatcherTimer.Start();
        }



        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            
            var tPBillRefs = new TPBillRefBLL().GetUnCompletedCallIns(terminalId);

            foreach (var existedMV in mainView.TPBillRefs)
            {

                if (!tPBillRefs.Any(i => i.BillRefId.Equals(existedMV.BillRefId)))
                {
                    mainView.TPBillRefs.Remove(existedMV);
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
            var billRefId = (long)((ToggleButton) sender).Tag;
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
            if ((TPUserAddressMV) ((ListBox) sender).SelectedItem != null)
            {
                mainView.UsersTabView.TPUserAddressMV = (TPUserAddressMV) ((ListBox) sender).SelectedItem;
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
            var userMV = mainView.UsersTabView.TPUserMV;
            mainView.ErrorMsg = "test";
            
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
            var selectedItem = mainView.UsersTabView.TPUserAddressMV;
            if (selectedItem != null && mainView.UsersTabView.TPUserAddressMVs.Any(i => i.UserAddressId == selectedItem.UserAddressId))
            {
                
                new TPBillRefBLL().RemoveUserAddress(mainView.UsersTabView.TPBillRefMV.BillRefId, selectedItem.UserAddressId);
                mainView.UsersTabView = new UsersTabView(mainView.UsersTabView.TPBillRefMV.BillRefId);
            }
            
        }

        private void UserAddressSave_OnClick(object sender, RoutedEventArgs e)
        {
            var userAddressMV = mainView.UsersTabView.TPUserAddressMV;
            var userAddress = userAddressMV.MapperTo();
            var billRefId = mainView.UsersTabView.TPBillRefMV.BillRefId;

            if (mainView.UsersTabView.TPBillRefMV.UserId_FK != null)
            {
                userAddress.UserId_FK = mainView.UsersTabView.TPBillRefMV.UserId_FK.Value;

                
                new TPBillRefBLL().SaveAddress(billRefId, userAddress);
                new TPBillRefBLL().SaveDliveryInfos(billRefId, 
                    mainView.UsersTabView.TPBillRefMV.DeliverMiles, mainView.UsersTabView.TPBillRefMV.DeliverFee);

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
            if (driverId != 0)
            {
                new TPDriverBLL().SaveOrUpdate(driverId, billRefId.Value);
            }
        }

        private void DeliverySave_OnClick(object sender, RoutedEventArgs e)
        {
            if (mainView.DeliveryTabView.TPBillRefMV != null)
            {

                var billRefId = mainView.DeliveryTabView.TPBillRefMV.BillRefId;
                var deliveryMiles = mainView.DeliveryTabView.TPBillRefMV.DeliverMiles;
                var deliveryFee = mainView.DeliveryTabView.TPBillRefMV.DeliverFee;

                new TPBillRefBLL().UpdateDeliveryInfos(billRefId, deliveryMiles, deliveryFee);

                //Refresh usercontrol
                BUControl.LoadBill();
            }
            
        }
    }
}

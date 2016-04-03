using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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
using EntitiesDABL.BLL;

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
        

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var billRefId = (long)((ToggleButton) sender).Tag;
            LoadTabControlView(billRefId);

        }

        private void LoadTabControlView(long? BillRefId)
        {
            switch (tPTabControl.SelectedIndex)
            {

                case 0:
                    mainView.UsersTabView = new UsersTabView(BillRefId.Value);

                    //TODO
                break;
                case 1:
                break;
                case 2:
                break;
            }
        }

        private void QueryUser_OnClick(object sender, RoutedEventArgs e)
        {

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


                    //LoadView
                    var userMV = TPUserMV.Mapper(new TPUserBLL().GeTPUserById(userId));
                    mainView.SelectedTpBillRefMv.UserId_FK = userId;
                    mainView.SelectedTpBillRefMv.TPUser = userMV;

                    //load userAddressList
                    var userAddress = new TPUserAddressBLL().GetTPUserAddressByUserId(userId);

                    var addressMv = mainView.SelectedTpBillRefMv.TPUser.TPUserAddress;
                    addressMv.Clear();
                    userAddress.ForEach(i => addressMv.Add(TPUserAddressMV.Mapper(i)));
                    
                }
            }
            
        }

        private void ListBox_Selected(object sender, RoutedEventArgs e)
        {

            
            
            //mainView.UsersTabView.TPBillRefMV.TPUserAddress = mainView.UsersTabView.TPBillRefMV.TPUser.TPUserAddress.Where(i => i.UserAddressId == )
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mainView.UsersTabView.TPBillRefMV.TPUserAddress = (TPUserAddressMV)((ListBox)sender).SelectedItem;
        }
    }
}

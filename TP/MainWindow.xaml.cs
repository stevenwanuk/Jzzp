using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Jzzp.DAL;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using TP.Common;
using TP.ModelView;
using TP.View;
using TP.WindowForm;

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
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            var tPBillRefs = TPBillRefDAL.GetUnCompletedCallIns(terminalId);

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
                    mainView.TPBillRefs.Add(TPBillRefMV.Mapper(temp));
                }
            }

            if (mainView.SelectedTpBillRefMv == null ||
                !mainView.TPBillRefs.Any(i => i.BillRefId.Equals(mainView.SelectedTpBillRefMv.BillRefId)))
            {
                mainView.SelectedTpBillRefMv = mainView.TPBillRefs.FirstOrDefault();
            }

            /**
            var callins = new TPCallInDAL().GetUnCompletedCallIns(terminalId);
            var tempList = new ObservableCollection<TPCallInMV>();

            callins.ForEach(i => tempList.Add(TPCallInMV.Mapper(i)));
            
            if (!tempList.CompareToUsingJson(mainView.CallInMvs))
            {
                mainView.CallInMvs = tempList;

                if (mainView.SelectedTPCallInMV == null)
                {
                    mainView.SelectedTPCallInMV = tempList.FirstOrDefault();
                }
            }
        **/
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


                break;
                case 1:
                break;
                case 2:
                break;
            }
            


            /*
            if (mainView.CallInMvs != null && mainView.CallInMvs.Count > 0)
            {
                if (CallInId == null)
                {
                    CallInId = mainView.CallInMvs.First().CallInId;
                    mainView.SelectedTPCallInMV = mainView.CallInMvs.First();
                }

                if (mainView.SelectedTPCallInMV?.CallInId != CallInId)
                {
                    var tempCallInMV = mainView.CallInMvs.FirstOrDefault(i => i.CallInId == CallInId);
                    if (tempCallInMV == null)
                    {
                        CallInId = mainView.CallInMvs.First().CallInId;
                        mainView.SelectedTPCallInMV = mainView.CallInMvs.First();
                    }
                    else
                    {
                        mainView.SelectedTPCallInMV = tempCallInMV;
                    }
                }
            }
            */
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
                    TPBillRefDAL.BindUser(currTPBillRefId, userId);

                    //LoadView
                    var userMV = TPUserMV.Mapper(TPUserDAL.GeTPUserById(userId));
                    mainView.SelectedTpBillRefMv.UserId_FK = userId;
                    mainView.SelectedTpBillRefMv.TPUser = userMV;
                }
            }
            
        }
    }
}

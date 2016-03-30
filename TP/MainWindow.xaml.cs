using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using Jzzp.DAL;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using TP.Common;
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
            
        }
        

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var callInId = (long)((ToggleButton) sender).Tag;
            OnSelectedTPCallInMvChanged(callInId);

        }

        private void OnSelectedTPCallInMvChanged(long? CallInId)
        {

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
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using DataMaintenance.View;
using TP.BLL;
using TP.Common;
using TP.ModelView;
using TP.WindowForm;

namespace DataMaintenance.WindowForm
{

    /// <summary>
    /// Interaction logic for BillWindow.xaml
    /// </summary>
    public partial class BillWindow : Window
    {

        public BillWindowView BillWv { get; set; }
        public BillWindow()
        {
            InitializeComponent();
            BillWv = new BillWindowView();
            this.DataContext = BillWv;
        }

        private void UserSearchBtn_OnClick(object sender, RoutedEventArgs e)
        {
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

                BillWv.QBillRefMv.UserId_FK = userId;

                var user = new TPUserBLL().GeTPUserById(userId);

                BillWv.QBillRefMv.TPUser = TPUserMV.Mapper(user);
            }
        }

        private void Clear_OnClick(object sender, RoutedEventArgs e)
        {
            this.BillWv = new BillWindowView();
        }

        private void Query_OnClick(object sender, RoutedEventArgs e)
        {
            var billRefs = new TPBillRefBLL().GetBillRefssWithParameters(
                
                BillWv.QBillRefMv.MapperTo(),
                BillWv.QStatus,
                BillWv.QDriverId,
                BillWv.QStartDate,
                BillWv.QEndDate
                );
            BillWv.BillRefMvs.Clear();
            foreach(var billRef in billRefs)
            {
                var tempMv = TPBillRefMV.Mapper(billRef);
                if(billRef.TPUser != null)
                {
                    tempMv.TPUser = TPUserMV.Mapper(billRef.TPUser);
                }
                if (billRef.TPDeliver != null)
                {
                    tempMv.TPDeliver = TPDeliverMV.Mapper(billRef.TPDeliver);
                    if (billRef.TPDeliver.TPDriver != null)
                    {
                        tempMv.TPDeliver.TPDriver = TPDriverMV.Mapper(billRef.TPDeliver.TPDriver);
                    }
                }
                BillWv.BillRefMvs.Add(tempMv);
            }
            
        }
    }
}

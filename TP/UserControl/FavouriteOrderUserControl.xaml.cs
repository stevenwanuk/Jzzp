using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using TP.Annotations;
using TP.BLL;
using TP.ModelView;

namespace TP.UserControl
{
    /// <summary>
    /// Interaction logic for FavouriteOrderUserControl.xaml
    /// </summary>
    public partial class FavouriteOrderUserControl : System.Windows.Controls.UserControl, INotifyPropertyChanged
    {


        public Guid? UserId
        {
            get { return (Guid?)GetValue(UserIdProperty); }
            set { SetValue(UserIdProperty, value); }
        }
        public static readonly DependencyProperty UserIdProperty = DependencyProperty.Register("UserId", typeof(Guid?), typeof(FavouriteOrderUserControl), new PropertyMetadata(OnCustomerChangedCallBack));

        private ObservableCollection<OrderHistoryMV> _orderHistoryMVs;
        public ObservableCollection<OrderHistoryMV> OrderHistoryMVs {
            get { return _orderHistoryMVs; }
            set
            {
                if (_orderHistoryMVs != value)
                {
                    _orderHistoryMVs = value;
                    OnPropertyChanged();
                }
            }
        }

        private static void OnCustomerChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var thiscontrol = sender as FavouriteOrderUserControl;

            if (thiscontrol != null)
            {
                thiscontrol.LoadHistory();
            }
        }

        public FavouriteOrderUserControl()
        {
            InitializeComponent();
            LoadHistory();
        }

        protected void LoadHistory()
        {

            if (UserId != null && UserId != Guid.Empty)
            {
                var orderHistory = new JzzpBillBLL().GetFavouriteByUserId(UserId.Value, 10);

                OrderHistoryMVs = new ObservableCollection<OrderHistoryMV>();

                if (orderHistory != null)
                {
                    orderHistory.ForEach(i => OrderHistoryMVs.Add(OrderHistoryMV.Mapper(i)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

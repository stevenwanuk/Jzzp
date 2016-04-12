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
using EntitiesDABL;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using TP.Annotations;
using TP.BLL;
using TP.ModelView;

namespace TP.UserControl
{
    /// <summary>
    /// Interaction logic for BillUserControl.xaml
    /// </summary>
    public partial class BillUserControl : System.Windows.Controls.UserControl, INotifyPropertyChanged
    {

        public long BillRefId
        {
            get { return (long)GetValue(BillRefIdProperty); }
            set { SetValue(BillRefIdProperty, value); }
        }
        public static readonly DependencyProperty BillRefIdProperty = DependencyProperty.Register("BillRefId", typeof(long), typeof(BillUserControl), new PropertyMetadata(OnCustomerChangedCallBack));

        private static void OnCustomerChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var thiscontrol = sender as BillUserControl;

            if (thiscontrol != null)
            {
                thiscontrol.LoadBill();
            }
        }

        private TPBillRefMV _billRefMV;

        public TPBillRefMV BIllRefMV
        {
            get { return _billRefMV; }
            set
            {
                if (_billRefMV != value)
                {
                    _billRefMV = value;
                    OnPropertyChanged();
                }
            }
        }

        private BillMV _billMV;
        public BillMV BillMV
        {
            get { return _billMV; }
            set
            {
                if (_billMV != value)
                {
                    _billMV = value;
                    OnPropertyChanged();
                }
            }
        }


        private ObservableCollection<BillItemMV> _billItemMVs;
        public ObservableCollection<BillItemMV> BillItemMVs
        {
            get { return _billItemMVs; }
            set
            {
                if (_billItemMVs != value)
                {
                    _billItemMVs = value;
                    OnPropertyChanged();
                }
            }
        }
        

        public BillUserControl()
        {
            InitializeComponent();

            LoadBill();
        }

        public void LoadBill()
        {

            var billRef = new TPBillRefBLL().GeBillRefWithUserByTpBillRefId(BillRefId);

            BIllRefMV = TPBillRefMV.Mapper(billRef);
            if (BIllRefMV == null || string.IsNullOrEmpty(BIllRefMV.BillId_FK))
            {
                BillMV = null;
                BillItemMVs = null;
            }
            else
            {
                var billDTO = new JzzpBillBLL().GetBillByBillId(BIllRefMV.BillId_FK);
                BillMV = BillMV.Mapper(billDTO.Bill);
                BillItemMVs = new ObservableCollection<BillItemMV>();
                billDTO.BillItems.ForEach(i => BillItemMVs.Add(BillItemMV.Mapper(i)));
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

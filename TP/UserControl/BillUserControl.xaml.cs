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
using System.Windows.Navigation;
using System.Windows.Shapes;
using EntitiesDABL;
using TP.ModelView;

namespace TP.UserControl
{
    /// <summary>
    /// Interaction logic for BillUserControl.xaml
    /// </summary>
    public partial class BillUserControl : System.Windows.Controls.UserControl
    {

        public BillMV BillMV {
            get { return (BillMV)GetValue(BillMVProperty); }
            set { SetValue(BillMVProperty, value); }
        }
        public static readonly DependencyProperty BillMVProperty = DependencyProperty.Register("BillMV", typeof(BillMV), typeof(BillUserControl));

        public ObservableCollection<BillItemMV> BillItemMVs
        {
            get { return (ObservableCollection<BillItemMV>)GetValue(BillItemMVsProperty); }
            set { SetValue(BillItemMVsProperty, value); }
        }
        public static readonly DependencyProperty BillItemMVsProperty = DependencyProperty.Register("BillItemMVs", typeof(ObservableCollection<BillItemMV>), typeof(BillUserControl));

        public BillUserControl()
        {
            InitializeComponent();
        }
    }
}

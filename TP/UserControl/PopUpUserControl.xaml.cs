using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;
using TP.Annotations;
using TP.AppStatic;

namespace TP.UserControl
{
    /// <summary>
    /// Interaction logic for PopUpUserControl.xaml
    /// </summary>
    public partial class PopUpUserControl : System.Windows.Controls.UserControl
    {
        public PopUpUserControl()
        {
            InitializeComponent();
        }   

        void PopUpUC_Loaded(object sender, RoutedEventArgs e)
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(this);

            if (parentObject is FrameworkElement)
            {
                var ui = (FrameworkElement)parentObject;

                PopupTest.PlacementTarget = ui;
                PopupTest.Width = ui.ActualWidth;
            }

        }

        public string ErrorMsg
        {
            get { return (string)GetValue(ErrorMsgProperty); }
            set { SetValue(ErrorMsgProperty, value); }
        }
        public static readonly DependencyProperty ErrorMsgProperty = DependencyProperty.Register("ErrorMsg", typeof(string), typeof(PopUpUserControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnCustomerChangedCallBack));

        private static void OnCustomerChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var thiscontrol = sender as PopUpUserControl;

            if (thiscontrol != null && !string.IsNullOrEmpty((string)e.NewValue))
            {
                thiscontrol.PopUp();
            }
        }

        public void PopUp()
        {
            var d = new DispatcherTimer();
            d.Interval = new TimeSpan(0, 0, TPConfig.MsgPopupDuration);
            d.Tick += new EventHandler((a, b) =>
            {

                PopupTest.IsOpen = false;
                ErrorMsg = string.Empty;
                (a as DispatcherTimer).Stop();
            });
            d.Start();

            PopupTest.IsOpen = true;
        }
    }
}

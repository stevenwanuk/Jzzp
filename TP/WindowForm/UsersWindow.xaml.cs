using System;
using System.Collections.Generic;
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
using TP.Common;

namespace TP.WindowForm
{
    /// <summary>
    /// Interaction logic for UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {

        public event EventHandler<CustomEventArgs> RaiseCustomEvent;


        public UsersWindow()
        {
            InitializeComponent();
        }

        private void Confirm_OnClick(object sender, RoutedEventArgs e)
        {
            RaiseCustomEvent(this, new CustomEventArgs("BA7D1506-5F5E-4177-8025-17E5B8D9A7DD"));
            this.Close();
        }
    }
}

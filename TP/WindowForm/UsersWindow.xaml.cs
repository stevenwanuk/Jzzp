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
using TP.BLL;
using TP.Common;
using TP.ModelView;
using TP.View;

namespace TP.WindowForm
{
    /// <summary>
    /// Interaction logic for UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {

        public UserQueryWindowView QUserWv { get; set; }
        public event EventHandler<CustomEventArgs> RaiseCustomEvent;



        public UsersWindow()
        {
            InitializeComponent();
            QUserWv = new UserQueryWindowView();
            this.DataContext = QUserWv;
        }

        private void Confirm_OnClick(object sender, RoutedEventArgs e)
        {
            var userId = (sender as RadioButton).Tag;

            RaiseCustomEvent(this, new CustomEventArgs(userId.ToString()));
            this.Close();
        }


        private void Clear_OnClick(object sender, RoutedEventArgs e)
        {
            QUserWv = new UserQueryWindowView();
        }

        private void Query_OnClick(object sender, RoutedEventArgs e)
        {
            var users = new TPUserBLL().GetUsersWithParameters(QUserWv.QUserMv.MapperTo(), 
                QUserWv.QAddressMv.MapperTo(),
                QUserWv.QUserCellMv.MapperTo());
            QUserWv.UserMvs.Clear();
            users.ForEach(i => QUserWv.UserMvs.Add(TPUserMV.Mapper(i)));
            
        }
    }
}

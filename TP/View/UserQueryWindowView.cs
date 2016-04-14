using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.Common;
using TP.ModelView;

namespace TP.View
{
    public class UserQueryWindowView : BindableBase
    {
        private TPUserMV _qUserMv;

        public TPUserMV QUserMv
        {
            get { return _qUserMv; }
            set { SetProperty(ref _qUserMv, value); }
        }

        private TPUserAddressMV _qAddressMv;

        public TPUserAddressMV QAddressMv
        {
            get { return _qAddressMv; }
            set { SetProperty(ref _qAddressMv, value); }
        }

        private TPUserCellMV _tpUserCellMv;

        public TPUserCellMV QUserCellMv
        {
            get { return _tpUserCellMv; }
            set { SetProperty(ref _tpUserCellMv, value); }
        }

        public ObservableCollection<TPUserMV> UserMvs { get; set; }
    

        public UserQueryWindowView()
        {
            UserMvs = new ObservableCollection<TPUserMV>();
            QUserMv = new TPUserMV();
            QAddressMv = new TPUserAddressMV();
            QUserCellMv = new TPUserCellMV();
        }

        private string _errorMsg;
        public string ErrorMsg
        {
            get { return _errorMsg; }
            set { SetProperty(ref _errorMsg, value); }
        }
    }
}

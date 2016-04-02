using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jzzp.DAL;
using TP.Common;
using TP.ModelView;

namespace TP.View
{
    public class UsersTabView : BindableBase
    {
        private TPBillRefMV _tPBillRefMV;

        public TPBillRefMV TPBillRefMV
        {
            get { return _tPBillRefMV; }
            set { SetProperty(ref _tPBillRefMV, value); }
        }
        private TPUserMV _tPUserMV;
        private TPUserAddressMV _tPUserAddressMv;

        private ObservableCollection<TPUserAddressMV> _tPUserAddressMvs;

        public UsersTabView(long tPBillRef)
        {

            _tPBillRefMV = TPBillRefMV.Mapper(TPBillRefDAL.GetUserDetails(tPBillRef));


        }

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.Common;
using TP.ModelView;
using EntitiesDABL.BLL;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;

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

            var billRef = new TPBillRefBLL().GetTPBillRefById(tPBillRef);
            _tPBillRefMV = TPBillRefMV.Mapper(billRef);
            _tPBillRefMV.TPUser = TPUserMV.Mapper(billRef.TPUser);

            //load userAddressList
            var userAddress = new TPUserAddressBLL().GetTPUserAddressByUserId(_tPBillRefMV.TPUser.UserId);

            var addressMv = _tPBillRefMV.TPUser.TPUserAddress;
            addressMv.Clear();
            userAddress.ForEach(i => addressMv.Add(TPUserAddressMV.Mapper(i)));

        }

    }
}

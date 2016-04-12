using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesDABL;
using TP.Common;
using TP.ModelView;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using TP.BLL;

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
        public TPUserMV TPUserMV
        {
            get { return _tPUserMV; }
            set { SetProperty(ref _tPUserMV, value); }
        }

        private SelectionCollection<TPUserAddressMV> _tPUserAddressMVs;
        public SelectionCollection<TPUserAddressMV> TPUserAddressMVs
        {
            get { return _tPUserAddressMVs; }
            set { SetProperty(ref _tPUserAddressMVs, value); }
        }

        private TPUserAddressMV _tPUserAddressMV;
        public TPUserAddressMV TPUserAddressMV
        {
            get { return _tPUserAddressMV; }
            set { SetProperty(ref _tPUserAddressMV, value); }
        }


        public UsersTabView(long tPBillRef)
        {

            var billRef = new TPBillRefBLL().GeBillRefWithUserAndUserAddressByTpBillRefId(tPBillRef);
            TPBillRefMV = TPBillRefMV.Mapper(billRef);

            

            if (billRef.TPUser != null)
            {
                _tPUserMV = TPUserMV.Mapper(billRef.TPUser);

                var userAddress = billRef.TPUser.TPUserAddress;
                _tPUserAddressMVs = new SelectionCollection<TPUserAddressMV>(); ;
                userAddress.ForEach(i => _tPUserAddressMVs.Add(TPUserAddressMV.Mapper(i)));
            }
            else
            {
                _tPUserMV = new TPUserMV();
            }

            if (billRef.AddressId_FK != null)
            {
                TPUserAddressMV = _tPUserAddressMVs.FirstOrDefault(i => i.UserAddressId == billRef.AddressId_FK);
                _tPUserAddressMVs.SelectedItem = TPUserAddressMV;
            }
            else
            {
                _tPUserAddressMV = new TPUserAddressMV();
            }


        }
    }
}

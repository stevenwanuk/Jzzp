using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
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

        public bool EnabledGmap { get; set; }

        private TPBillRefMV _tPBillRefMV;
        public TPBillRefMV TPBillRefMV
        {
            get { return _tPBillRefMV; }
            set { SetProperty(ref _tPBillRefMV, value, "TPBillRefMV"); }
        }

        private TPUserMV _tPUserMV;
        public TPUserMV TPUserMV
        {
            get { return _tPUserMV; }
            set { SetProperty(ref _tPUserMV, value, "TPUserMV"); }
        }

        private SelectionCollection<TPUserAddressMV> _tPUserAddressMVs;
        public SelectionCollection<TPUserAddressMV> TPUserAddressMVs
        {
            get { return _tPUserAddressMVs; }
            set { SetProperty(ref _tPUserAddressMVs, value, "TPUserAddressMVs"); }
        }

        private TPUserAddressMV _tPUserAddressMV;
        public TPUserAddressMV TPUserAddressMV
        {
            get { return _tPUserAddressMV; }
            set { SetProperty(ref _tPUserAddressMV, value, "TPUserAddressMV"); }
        }


        public UsersTabView(long tPBillRef)
        {
            EnabledGmap = true;
            bool appsetting;
            if (bool.TryParse(ConfigurationManager.AppSettings["EnabledGmap"], out appsetting))
            {
                EnabledGmap = appsetting;
            }
            

            _tPUserAddressMVs = new SelectionCollection<TPUserAddressMV>();

            var billRef = new TPBillRefBLL().GeBillRefWithUserAndUserAddressByTpBillRefId(tPBillRef);
            TPBillRefMV = TPBillRefMV.Mapper(billRef);

            

            if (billRef.TPUser != null)
            {
                _tPUserMV = TPUserMV.Mapper(billRef.TPUser);

                var userAddress = billRef.TPUser.TPUserAddress;
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

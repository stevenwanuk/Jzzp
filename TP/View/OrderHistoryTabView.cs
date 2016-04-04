using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using TP.BLL;
using TP.Common;
using TP.ModelView;

namespace TP.View
{
    public class OrderHistoryTabView : BindableBase
    {
        private TPBillRefMV _tPBillRefMV;

        public TPBillRefMV TPBillRefMV
        {
            get { return _tPBillRefMV; }
            set { SetProperty(ref _tPBillRefMV, value); }
        }

        public OrderHistoryTabView(long tPBillRef)
        {

            var billRef = new TPBillRefBLL().GetUsersTabViewByTpBillRefId(tPBillRef);
            _tPBillRefMV = TPBillRefMV.Mapper(billRef);
            _tPBillRefMV.TPUser = TPUserMV.Mapper(billRef.TPUser);

            if (billRef.TPUser != null)
            {
                var userAddress = billRef.TPUser.TPUserAddress;
                var addressMv = _tPBillRefMV.TPUser.TPUserAddress;
                addressMv.Clear();
                userAddress.ForEach(i => addressMv.Add(TPUserAddressMV.Mapper(i)));
            }
        }
    }
}

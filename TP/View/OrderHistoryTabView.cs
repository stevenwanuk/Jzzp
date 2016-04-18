using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using EntitiesDABL.DAL;
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
            set { SetProperty(ref _tPBillRefMV, value, "TPBillRefMV"); }
        }

        private long? _lastBillRefId;
        public long? LastBillRefId
        {
            get { return _lastBillRefId; }
            set { SetProperty(ref _lastBillRefId, value, "LastBillRefId"); }
        }

        public OrderHistoryTabView(long tPBillRef)
        {

            var billRef = new TPBillRefBLL().GeBillRefWithUserByTpBillRefId(tPBillRef);
            _tPBillRefMV = TPBillRefMV.Mapper(billRef);
            _tPBillRefMV.TPUser = TPUserMV.Mapper(billRef.TPUser);

            if (billRef.TPUser != null)
            {
                _lastBillRefId = new TPBillRefBLL().GetLastBillRefIdByUser(billRef.TPUser.UserId);
            }
            

        }
    }
}

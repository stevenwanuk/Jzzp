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
            set { SetProperty(ref _tPBillRefMV, value); }
        }

        private BillMV _billMV;

        public BillMV BillMV
        {
            get { return _billMV; }
            set { SetProperty(ref _billMV, value); }
        }

        private ObservableCollection<BillItemMV> _billItemMVs;

        public ObservableCollection<BillItemMV> BillItemMVs
        {
            get { return _billItemMVs; }
            set { SetProperty(ref _billItemMVs, value); }
        }

        public OrderHistoryTabView(long tPBillRef)
        {

            var billRef = new TPBillRefBLL().GeBillRefWithUserByTpBillRefId(tPBillRef);
            _tPBillRefMV = TPBillRefMV.Mapper(billRef);
            _tPBillRefMV.TPUser = TPUserMV.Mapper(billRef.TPUser);

            if (billRef.UserId_FK != null && billRef.TPUser != null)
            {
                //Looking for history
                var billDTO = new JzzpBillBLL().GetLastPaidBillByUserId(billRef.UserId_FK.Value);
                if (billDTO != null)
                {
                    _billMV = BillMV.Mapper(billDTO.Bill);
                    _billItemMVs = new ObservableCollection<BillItemMV>();
                    billDTO.BillItems.ForEach(i => _billItemMVs.Add(BillItemMV.Mapper(i)));
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class DeliveryTabView : BindableBase
    {

        private TPBillRefMV _tPBillRefMV;

        public TPBillRefMV TPBillRefMV
        {
            get { return _tPBillRefMV; }
            set { SetProperty(ref _tPBillRefMV, value, "TPBillRefMV"); }
        }


        private SelectionCollection<TempBillMV> _unBindingBillMvs;

        public SelectionCollection<TempBillMV> UnBindingBillMvs
        {
            get { return _unBindingBillMvs; }
            set { SetProperty(ref _unBindingBillMvs, value, "BillMvs"); }
        }

        private TPDeliverMV _tPDeliverMV;

        public TPDeliverMV TPDeliverMV
        {
            get { return _tPDeliverMV; }
            set { SetProperty(ref _tPDeliverMV, value, "TPDeliverMV"); }
        }

        private ObservableCollection<TPDriverMV> _tPDriverMVs;
        public ObservableCollection<TPDriverMV> TpDriverMVs
        {
            get { return _tPDriverMVs; }
            set { SetProperty(ref _tPDriverMVs, value, "TpDriverMVs"); }
        }


        public DeliveryTabView(long tPBillRef)
        {

            var billRef = new TPBillRefBLL().GeBillRefWithUserAndDriverByTpBillRefId(tPBillRef);
            TPBillRefMV = TPBillRefMV.Mapper(billRef);
            TPBillRefMV.TPUser = TPUserMV.Mapper(billRef.TPUser);
            TPBillRefMV.TPUserAddress = TPUserAddressMV.Mapper(billRef.TPUserAddress);
            TPBillRefMV.TPCallIn = TPCallInMV.Mapper(billRef.TPCallIn);

            TPDeliverMV = TPDeliverMV.Mapper(billRef.TPDeliver);
            if (billRef.TPDeliver != null)
            {
                TPDeliverMV.TPDriver = TPDriverMV.Mapper(billRef.TPDeliver.TPDriver);
            }

            var drivers = new TPDriverBLL().GetDriversIfAvailable();
            TpDriverMVs = new ObservableCollection<TPDriverMV>();
            drivers.ForEach(i => TpDriverMVs.Add(TPDriverMV.Mapper(i)));

            var unBindingList = new JzzpBillBLL().GetUnBindListWithCurrentBillId(billRef.BillId_FK);
            UnBindingBillMvs = new SelectionCollection<TempBillMV>();
            unBindingList.ForEach(i => UnBindingBillMvs.Add(TempBillMV.Mapper(i)));
            UnBindingBillMvs.SelectedItem = UnBindingBillMvs.Where(i => i.BillID == billRef.BillId_FK).FirstOrDefault();
        }
    }
}

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
            set { SetProperty(ref _tPBillRefMV, value); }
        }



        private TPDeliverMV _tPDeliverMV;

        public TPDeliverMV TPDeliverMV
        {
            get { return _tPDeliverMV; }
            set { SetProperty(ref _tPDeliverMV, value); }
        }

        private ObservableCollection<TPDriverMV> _tPDriverMVs;
        public ObservableCollection<TPDriverMV> TpDriverMVs
        {
            get { return _tPDriverMVs; }
            set { SetProperty(ref _tPDriverMVs, value); }
        }


        public DeliveryTabView(long tPBillRef)
        {

            var billRef = new TPBillRefBLL().GeBillRefWithUserAndDriverByTpBillRefId(tPBillRef);
            TPBillRefMV = TPBillRefMV.Mapper(billRef);
            TPBillRefMV.TPUser = TPUserMV.Mapper(billRef.TPUser);

            TPDeliverMV = TPDeliverMV.Mapper(billRef.TPDeliver);
            if (billRef.TPDeliver != null)
            {
                TPDeliverMV.TPDriver = TPDriverMV.Mapper(billRef.TPDeliver.TPDriver);
            }

            var drivers = new TPDriverBLL().GetDriversIfAvailable();
            TpDriverMVs = new ObservableCollection<TPDriverMV>();
            drivers.ForEach(i => TpDriverMVs.Add(TPDriverMV.Mapper(i)));

            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelGenerator;
using TP.Common;

namespace TP.ModelView
{
    public class TPDeliverMV : BindableBase
    {

        public TPDeliverMV()
        {
            _tPBillRef = new ObservableCollection<TPBillRefMV>();
        }
        public static TPDeliverMV Mapper(TPDeliver entity)
        {
            return AutoMapperUtils.GetMapper().Map<TPDeliver, TPDeliverMV>(entity);
        }

        private long _deliverId;

        public long DeliverId
        {
            get { return _deliverId; }
            set { SetProperty(ref _deliverId, value); }
        }

        private long _driverId_FK;
        public long DriverId_FK {
            get { return _driverId_FK; }
            set { SetProperty(ref _driverId_FK, value); }
        }

        private DateTime? _startDate;
        public Nullable<System.DateTime> StartDate {
            get { return _startDate; }
            set { SetProperty(ref _startDate, value); }
        }

        private DateTime? _endDate;
        public Nullable<System.DateTime> EndDate {
            get { return _endDate; }
            set { SetProperty(ref _endDate, value); }
        }

        private int _status;
        public int Status {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }

        private TPDriverMV _tPDriver;
        public virtual TPDriverMV TPDriver {
            get { return _tPDriver; }
            set { SetProperty(ref _tPDriver, value); }
        }

        private ObservableCollection<TPBillRefMV> _tPBillRef;
        public virtual ObservableCollection<TPBillRefMV> TPBillRef {
            get { return _tPBillRef; }
            set { SetProperty(ref _tPBillRef, value); }
        }
    }
}

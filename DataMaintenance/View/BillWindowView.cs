using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.Common;
using TP.ModelView;

namespace DataMaintenance.View
{
    public class BillWindowView : BindableBase
    {

        private TPBillRefMV _qBillRefMv;
        public TPBillRefMV QBillRefMv
        {
            get { return _qBillRefMv; }
            set { SetProperty(ref _qBillRefMv, value); }
        }

        private DateTime? _qStartDate;

        public DateTime? QStartDate
        {
            get { return _qStartDate; }
            set { SetProperty(ref _qStartDate, value); }
        }

        private DateTime? _qEndDate;
        public DateTime? QEndDate
        {
            get { return _qEndDate; }
            set { SetProperty(ref _qEndDate, value); }
        }

        private int? _qStatus;

        public int? QStatus
        {
            get { return _qStatus; }
            set { SetProperty(ref _qStatus, value); }
        }

        private long? _qDriverId;

        public long? QDriverId
        {
            get { return _qDriverId; }
            set { SetProperty(ref _qDriverId, value); }
        }

        public ObservableCollection<TPBillRefMV> BillRefMvs { get; set; }

        public BillWindowView()
        {
            _qBillRefMv = new TPBillRefMV();
            BillRefMvs = new ObservableCollection<TPBillRefMV>();
            //_qStartDate = DateTime.Now.Date;
            //_qEndDate = DateTime.Now.Date.AddDays(1);
        }

        private string _errorMsg;
        public string ErrorMsg
        {
            get { return _errorMsg; }
            set { SetProperty(ref _errorMsg, value); }
        }

    }
}

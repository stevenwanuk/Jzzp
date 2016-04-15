
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AutoMapper;
using TP.Annotations;
using TP.Common;

namespace TP.ModelView
{
    using EntitiesDABL;
    using System;
    using System.Collections.Generic;

    public class TPCallInMV : BindableBase
    {
        public TPCallInMV()
        {
            TPBillRef = new ObservableCollection<TPBillRefMV>();
        }

        public static TPCallInMV Mapper(TPCallIn entity)
        {
            return AutoMapperUtils.GetMapper().Map<TPCallIn, TPCallInMV>(entity);
        }

        private long _callinId;

        public long CallInId
        {
            get { return _callinId; }
            set { SetProperty(ref _callinId, value, "CallInId"); }
        }

        private string _cellNumber;
        public string CellNumber
        {
            get { return _cellNumber; }
            set { SetProperty(ref _cellNumber, value, "CellNumber"); }
        }


        private DateTime? _startDate;
        public DateTime? StartDate {
            get { return _startDate; }
            set { SetProperty(ref _startDate, value, "StartDate"); }
        }

        private DateTime? _endDate;
        public DateTime? EndDate {
            get { return _endDate; }
            set { SetProperty(ref _endDate, value, "EndDate"); }
        }
        
        private int _teminalId;
        public int TeminalId {
            get { return _teminalId; }
            set { SetProperty(ref _teminalId, value, "TeminalId"); }
        }

        private ObservableCollection<TPBillRefMV> _tPBillRef;
        public virtual ObservableCollection<TPBillRefMV> TPBillRef
        {
            get { return _tPBillRef; }
            set { SetProperty(ref _tPBillRef, value, "TPBillRef"); }
        }
    }
}

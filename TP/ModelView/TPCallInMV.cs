
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AutoMapper;
using ModelGenerator;
using TP.Annotations;
using TP.Common;

namespace TP.ModelView
{
    using System;
    using System.Collections.Generic;
    
    public class TPCallInMV : INotifyPropertyChanged
    {
        public TPCallInMV()
        {
            TPBillRef = new ObservableCollection<TPBillRef>();
        }

        public static TPCallInMV Mapper(TPCallIn entity)
        {
            return AutoMapperUtils.GetMapper().Map<TPCallIn, TPCallInMV>(entity);
        }

        private long _callinId;
        public long CallInId {
            get { return _callinId;}
            set
            {
                if (_callinId != value)
                {
                    _callinId = value;
                    OnPropertyChanged();
                }
            } }

        public string _cellNumber;
        public string CellNumber
        {
            get { return _cellNumber; }
            set
            {
                if (_cellNumber != value)
                {
                    _cellNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TeminalId { get; set; }
   
        public virtual ObservableCollection<TPBillRef> TPBillRef { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

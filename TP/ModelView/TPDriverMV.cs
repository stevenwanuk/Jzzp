using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelGenerator;
using TP.Common;
using EntitiesDABL;

namespace TP.ModelView
{
    public class TPDriverMV : BindableBase
    {

        public static TPDriverMV Mapper(TPDriver entity)
        {
            return AutoMapperUtils.GetMapper().Map<TPDriver, TPDriverMV>(entity);
        }

        public TPDriver MapperTo()
        {
            return AutoMapperUtils.GetMapper().Map<TPDriverMV, TPDriver>(this);
        }

        public TPDriverMV()
        {
            _tPDeliver = new ObservableCollection<TPDeliver>();
        }

        private long _driverId;
        public long DriverId
        {
            get { return _driverId; }
            set { SetProperty(ref _driverId, value); }
        }


        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        private string _cellName;
        public string CellName
        {
            get { return _cellName; }
            set { SetProperty(ref _cellName, value); }
        }

        private long _deliverCount;

        public long DeliverCount
        {
            get { return _deliverCount; }
            set { SetProperty(ref _deliverCount, value); }
        }

        private ObservableCollection<TPDeliver> _tPDeliver;
        public virtual ObservableCollection<TPDeliver> TPDeliver
        {
            get { return _tPDeliver; }
            set { SetProperty(ref _tPDeliver, value); }
        }
    }
}

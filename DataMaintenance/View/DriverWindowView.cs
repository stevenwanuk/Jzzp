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
    public class DriverWindowView : BindableBase
    {
        public DriverWindowView()
        {
            _tpDriverMvs = new ObservableCollection<TPDriverMV>();
        }

        private ObservableCollection<TPDriverMV> _tpDriverMvs;

        public ObservableCollection<TPDriverMV> TpDriverMvs
        {
            get { return _tpDriverMvs; }
            set { SetProperty(ref _tpDriverMvs, value); }
        }
    }
}

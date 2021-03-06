﻿using System;
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

        private string _errorMsg;
        public string ErrorMsg
        {
            get { return _errorMsg; }
            set { SetProperty(ref _errorMsg, value, "ErrorMsg"); }
        }

        private bool _isEdit;

        public bool IsEdit
        {
            get { return _isEdit; }
            set { SetProperty(ref _isEdit, value, "IsEdit"); }
        }

        private bool _isDelete;
        public bool IsDelete
        {
            get { return _isDelete; }
            set { SetProperty(ref _isDelete, value, "IsDelete"); }
        }

        private ObservableCollection<TPDriverMV> _tpDriverMvs;

        public ObservableCollection<TPDriverMV> TpDriverMvs
        {
            get { return _tpDriverMvs; }
            set { SetProperty(ref _tpDriverMvs, value, "TpDriverMvs"); }
        }
    }
}

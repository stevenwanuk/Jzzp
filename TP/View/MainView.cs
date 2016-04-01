using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Internal;
using Jzzp.DAL;
using ModelGenerator;
using TP.Annotations;
using TP.Common;
using TP.ModelView;

namespace TP.View
{
    public class MainView : BindableBase
    {

        public MainView()
        {
            _tPBillRefs = new ObservableCollection<TPBillRefMV>();
        }

        private UsersTabView _usersTabView;
        public UsersTabView UsersTabView
        {
            get { return _usersTabView; }
            set { SetProperty(ref _usersTabView, value); }
        }


        private TPBillRefMV _selectedTPBillRefMv;
        public TPBillRefMV SelectedTpBillRefMv
        {
            get { return _selectedTPBillRefMv; }
            set { SetProperty(ref _selectedTPBillRefMv, value); }
        }

        private ObservableCollection<TPBillRefMV> _tPBillRefs;
        public ObservableCollection<TPBillRefMV> TPBillRefs
        {
            get { return _tPBillRefs; }
            set { SetProperty(ref _tPBillRefs, value); }
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Internal;

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

        private string _errorMsg;
        public string ErrorMsg
        {
            get { return _errorMsg; }
            set
            {
                if (value != _errorMsg)
                {

                    _errorMsg = value;
                    OnPropertyChanged();
                }
            }
        }


        private UsersTabView _usersTabView;
        public UsersTabView UsersTabView
        {
            get { return _usersTabView; }
            set { SetProperty(ref _usersTabView, value); }
        }

        private DeliveryTabView _deliveryTabView;
        public DeliveryTabView DeliveryTabView
        {
            get { return _deliveryTabView; }
            set { SetProperty(ref _deliveryTabView, value); }
        }

        private OrderHistoryTabView _orderHistoryTabView;
        public OrderHistoryTabView OrderHistoryTabView
        {
            get { return _orderHistoryTabView; }
            set { SetProperty(ref _orderHistoryTabView, value); }
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

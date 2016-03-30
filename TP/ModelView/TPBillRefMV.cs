
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
    
    public class TPBillRefMV : BindableBase
    {

        public static TPBillRefMV Mapper(TPBillRef entity)
        {
            return AutoMapperUtils.GetMapper().Map<TPBillRef, TPBillRefMV>(entity);
        }

        private long _billRefId;
        public long BillRefId
        {
            get { return _billRefId; }
            set { SetProperty(ref _billRefId, value); }
        }

        private long _callInId_FK;
        public long CallInId_FK
        {
            get { return _callInId_FK; }
            set { SetProperty(ref _callInId_FK, value); }
        }

        private Guid? _userId_FK;
        public Guid? UserId_FK
        {
            get { return _userId_FK; }
            set { SetProperty(ref _userId_FK, value); }
        }

        private long _addressId_FK;
        public long AddressId_FK
        {
            get { return _addressId_FK; }
            set { SetProperty(ref _addressId_FK, value); }
        }

        private string _billId_FK;
        public string BillId_FK
        {
            get { return _billId_FK; }
            set { SetProperty(ref _billId_FK, value); }
        }


        private decimal? _deliverMiles;
        public decimal? DeliverMiles
        {
            get { return _deliverMiles; }
            set { SetProperty(ref _deliverMiles, value); }
        }

        private decimal? _deliverFee;
        public decimal? DeliverFee
        {
            get { return _deliverFee; }
            set { SetProperty(ref _deliverFee, value); }
        }

        
        private int _status;
        public int Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }


        private TPCallIn _tPCallIn;
        public virtual TPCallIn TPCallIn {
            get { return _tPCallIn; }
            set { SetProperty(ref _tPCallIn, value); }
        }

        private TPUser _tPUser;
        public virtual TPUser TPUser
        {
            get { return _tPUser; }
            set { SetProperty(ref _tPUser, value); }
        }

        private TPUserAddressMV _tPUserAddress;
        public virtual TPUserAddressMV TPUserAddress {
            get { return _tPUserAddress; }
            set { SetProperty(ref _tPUserAddress, value); }
        }


        private TPDeliverMV _tPDeliver;
        public virtual TPDeliverMV TPDeliver {
            get { return _tPDeliver; }
            set { SetProperty(ref _tPDeliver, value); }
        }
    }
}



using TP.Common;
using EntitiesDABL;
using System;

namespace TP.ModelView
{



    public class TPBillRefMV : BindableBase
    {

        public TPBillRefMV()
        {
        }

        public static TPBillRefMV Mapper(TPBillRef entity)
        {
            return AutoMapperUtils.GetMapper().Map<TPBillRef, TPBillRefMV>(entity);
        }

        public TPBillRef MapperTo()
        {
            return AutoMapperUtils.GetMapper().Map<TPBillRefMV, TPBillRef>(this);
        }

        private long _billRefId;
        public long BillRefId
        {
            get { return _billRefId; }
            set { SetProperty(ref _billRefId, value, "BillRefId"); }
        }

        private long? _callInId_FK;
        public long? CallInId_FK
        {
            get { return _callInId_FK; }
            set { SetProperty(ref _callInId_FK, value, "CallInId_FK"); }
        }

        private Guid? _userId_FK;
        public Guid? UserId_FK
        {
            get { return _userId_FK; }
            set { SetProperty(ref _userId_FK, value, "UserId_FK"); }
        }

        private long? _addressId_FK;
        public long? AddressId_FK
        {
            get { return _addressId_FK; }
            set { SetProperty(ref _addressId_FK, value, "AddressId_FK"); }
        }

        private string _billId_FK;
        public string BillId_FK
        {
            get { return _billId_FK; }
            set { SetProperty(ref _billId_FK, value, "BillId_FK"); }
        }


        private decimal? _deliverMiles;
        public decimal? DeliverMiles
        {
            get { return _deliverMiles; }
            set { SetProperty(ref _deliverMiles, value, "DeliverMiles"); }
        }

        private decimal? _deliverFee;
        public decimal? DeliverFee
        {
            get { return _deliverFee; }
            set { SetProperty(ref _deliverFee, value, "DeliverFee"); }
        }

        private decimal? _deliverFeeOrigin;
        public Nullable<decimal> DeliverFeeOrigin {
            get { return _deliverFeeOrigin; }
            set { SetProperty(ref _deliverFeeOrigin, value, "DeliverFeeOrigin"); }
        }


        private int _status;
        public int Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value, "Status"); }
        }

        private long? _deliverId_FK;
        public Nullable<long> DeliverId_FK {
            get { return _deliverId_FK; }
            set { SetProperty(ref _deliverId_FK, value, "DeliverId_FK"); }
        }

        private TPCallInMV _tPCallIn;
        public virtual TPCallInMV TPCallIn {
            get { return _tPCallIn; }
            set { SetProperty(ref _tPCallIn, value, "TPCallIn"); }
        }

        private TPUserMV _tPUser;
        public virtual TPUserMV TPUser
        {
            get { return _tPUser; }
            set { SetProperty(ref _tPUser, value, "TPUser"); }
        }

        private TPUserAddressMV _tPUserAddress;
        public virtual TPUserAddressMV TPUserAddress {
            get { return _tPUserAddress; }
            set { SetProperty(ref _tPUserAddress, value, "TPUserAddress"); }
        }


        private TPDeliverMV _tPDeliver;
        public virtual TPDeliverMV TPDeliver {
            get { return _tPDeliver; }
            set { SetProperty(ref _tPDeliver, value, "TPDeliver"); }
        }

        
    }
}

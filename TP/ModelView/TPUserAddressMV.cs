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
    public class TPUserAddressMV : BindableBase
    {

        public TPUserAddressMV()
        {
            _tPBillRef = new ObservableCollection<TPBillRefMV>();
        }

        public static TPUserAddressMV Mapper(TPUserAddress entity)
        {
            return AutoMapperUtils.GetMapper().Map<TPUserAddress, TPUserAddressMV>(entity);
        }
        public TPUserAddress MapperTo()
        {
            return AutoMapperUtils.GetMapper().Map<TPUserAddressMV, TPUserAddress>(this);
        }


        private long _userAddressId;
        public long UserAddressId
        {
            get { return _userAddressId; }
            set { SetProperty(ref _userAddressId, value, "UserAddressId"); }
        }

        private Guid _userId_FK;
        public System.Guid UserId_FK {
            get { return _userId_FK; }
            set { SetProperty(ref _userId_FK, value, "UserId_FK"); }
        }

        private string _houseNumber;
        public string HouseNumber
        {
            get { return _houseNumber; }
            set { SetProperty(ref _houseNumber, value, "HouseNumber"); }
        }

        private string _addressField1;
        public string AddressField1
        {
            get { return _addressField1; }
            set { SetProperty(ref _addressField1, value, "AddressField1"); }
        }

        private string _addressField2;
        public string AddressField2
        {
            get { return _addressField2; }
            set { SetProperty(ref _addressField2, value, "AddressField2"); }
        }

        private string _addressField3;
        public string AddressField3
        {
            get { return _addressField3; }
            set { SetProperty(ref _addressField3, value, "AddressField3"); }
        }

        private string _townCity;
        public string TownCity {
            get { return _townCity; }
            set { SetProperty(ref _townCity, value, "TownCity"); }
        }

        private string _postcode;
        public string Postcode {
            get { return _postcode; }
            set { SetProperty(ref _postcode, value, "Postcode"); }
        }

        private string _country;
        public string Country {
            get { return _country; }
            set { SetProperty(ref _country, value, "Country"); }
        }

        private decimal? _deliveryMiles;
        public Nullable<decimal> DeliveryMiles {
            get { return _deliveryMiles; }
            set { SetProperty(ref _deliveryMiles, value, "DeliveryMiles"); }
        }


        private ObservableCollection<TPBillRefMV> _tPBillRef;
        public virtual ObservableCollection<TPBillRefMV> TPBillRef {
            get { return _tPBillRef; }
            set { SetProperty(ref _tPBillRef, value, "TPBillRef"); }
        }

        private TPUserMV _tPUser;
        public virtual TPUserMV TPUser {
            get { return _tPUser; }
            set { SetProperty(ref _tPUser, value, "TPUser"); }
        }
    }
}

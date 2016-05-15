using EntitiesDABL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.Common;

namespace TP.ModelView
{
    public class TPUserMV : BindableBase, IDataErrorInfo
    {

        public static TPUserMV Mapper(TPUser entity)
        {
            return AutoMapper.Mapper.Map<TPUser, TPUserMV>(entity);
        }

        public TPUser MapperTo()
        {
            return AutoMapper.Mapper.Map<TPUserMV, TPUser>(this);
        }

        public TPUserMV()
        {
            _tPBillRef = new ObservableCollection<TPBillRefMV>();
            _tPUserCell = new ObservableCollection<TPUserCellMV>();
            _tPUserAddress = new ObservableCollection<TPUserAddressMV>();
        }

        private Guid _userId;
        public System.Guid UserId {
            get { return _userId; }
            set { SetProperty(ref _userId, value, "UserId"); }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value, "FirstName"); }
        }

        private string _lastName;
        public string LastName {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value, "LastName"); }
        }

        private int? _gender;
        public int? Gender
        {
            get { return _gender; }
            set { SetProperty(ref _gender, value, "Gender"); }
        }


        private ObservableCollection<TPBillRefMV> _tPBillRef;
        public virtual ObservableCollection<TPBillRefMV> TPBillRef
        {
            get { return _tPBillRef; }
            set { SetProperty(ref _tPBillRef, value, "TPBillRef"); }
        }

        private ObservableCollection<TPUserCellMV> _tPUserCell;
        public virtual ObservableCollection<TPUserCellMV> TPUserCell
        {
            get { return _tPUserCell; }
            set { SetProperty(ref _tPUserCell, value, "TPUserCell"); }
        }

        private ObservableCollection<TPUserAddressMV> _tPUserAddress;
        public virtual ObservableCollection<TPUserAddressMV> TPUserAddress
        {
            get { return _tPUserAddress; }
            set { SetProperty(ref _tPUserAddress, value, "TPUserAddress"); }
        }

        public string this[string columnName]
        {
            get
            {

                var i = string.Empty;

                switch (columnName)
                {
                    case "FirstName":
                        if (!("111").Equals(_firstName))
                        i = "testttt";
                        break;
                }

                return i;
            }
        }


        private string _error;

        public string Error {
            get { return _error; }
        }
    }
}

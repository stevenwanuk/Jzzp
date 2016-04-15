using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelGenerator;
using TP.Common;
using EntitiesDABL;

namespace TP.ModelView
{
    public class TPUserCellMV : BindableBase
    {

        public static TPUserCellMV Mapper(TPUserCell entity)
        {
            return AutoMapperUtils.GetMapper().Map<TPUserCell, TPUserCellMV>(entity);
        }

        public TPUserCell MapperTo()
        {
            return AutoMapperUtils.GetMapper().Map<TPUserCellMV, TPUserCell>(this);
        }


        private long _userCellId;
        public long UserCellId
        {
            get { return _userCellId; }
            set { SetProperty(ref _userCellId, value, "UserCellId"); }
        }

        private Guid _userId_FK;
        public System.Guid UserId_FK
        {
            get { return _userId_FK; }
            set { SetProperty(ref _userId_FK, value, "UserId_FK"); }
        }

        private string _cellNumber;
        public string CellNumber {
            get { return _cellNumber; }
            set { SetProperty(ref _cellNumber, value, "CellNumber"); }
        }

        private TPUserMV _tPUser;
        public virtual TPUserMV TPUser {
            get { return _tPUser; }
            set { SetProperty(ref _tPUser, value, "TPUser"); }
        }

    }
}

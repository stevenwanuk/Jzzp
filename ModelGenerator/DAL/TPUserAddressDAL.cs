using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelGenerator;

namespace EntitiesDABL.DAL
{
    public class TPUserAddressDAL : DALBase
    {
        public TPUserAddressDAL(JZZPEntities entities)
        {

            this.Entities = entities;
        }

        public TPUserAddress GeTPUserAddressById(long tPUserAddressId)
        {

            var query = Entities.TPUserAddresses.Where(i => i.UserAddressId == tPUserAddressId).ToList().FirstOrDefault();
            return query;
        }

        public ICollection<TPUserAddress> GetTPUserAddressByUserId(Guid userId)
        {

            var query = Entities.TPUserAddresses.Where(i => i.UserId_FK == userId).ToList();
            return query;
        }
    }
}

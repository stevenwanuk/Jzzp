using EntitiesDABL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesDABL.BLL
{
    public class TPUserAddressBLL
    {
        public ICollection<TPUserAddress> GetTPUserAddressByUserId(Guid tPUserId)
        {
            ICollection<TPUserAddress> result = new List<TPUserAddress>();
            using (var entities = new JZZPEntities())
            {

                result = new TPUserAddressDAL(entities).GetTPUserAddressByUserId(tPUserId);
            }

            return result;
        }

        public TPUserAddress GeTPUserAddressById(long tPUserAddressId)
        {
            TPUserAddress result = null;
            using (var entities = new JZZPEntities())
            {

                result = new TPUserAddressDAL(entities).GeTPUserAddressById(tPUserAddressId);
            }

            return result;
        }

    }
}

using EntitiesDABL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesDABL;

namespace TP.BLL
{
    public class TPUserAddressBLL
    {
        public ICollection<TPUserAddress> GetTPUserAddressByUserId(Guid tPUserId)
        {
            ICollection<TPUserAddress> result = new List<TPUserAddress>();
            using (var entities = new JZZPEntities())
            {

                result = new TPUserAddressDAL(entities).GetTPUserAddressByUserId(tPUserId).ToList();
            }

            return result;
        }

        public TPUserAddress GeTPUserAddressById(long tPUserAddressId)
        {
            TPUserAddress result = null;
            using (var entities = new JZZPEntities())
            {

                result = new TPUserAddressDAL(entities).GeTPUserAddressById(tPUserAddressId).ToList().FirstOrDefault();
            }

            return result;
        }

    }
}

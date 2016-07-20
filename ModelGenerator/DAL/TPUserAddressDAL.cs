using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
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

        public IQueryable<TPUserAddress> GeTPUserAddressById(long tPUserAddressId)
        {

            return Entities.TPUserAddresses.Where(i => i.UserAddressId == tPUserAddressId);
        }

        public IQueryable<TPUserAddress> GetTPUserAddressByUserId(Guid userId)
        {

            return Entities.TPUserAddresses.Where(i => i.UserId_FK == userId);
        }

        public void Save(TPUserAddress userAddress)
        {

            if (userAddress.UserAddressId <= 0)
            {
                Entities.TPUserAddresses.Add(userAddress);
            } else
            {
                Entities.TPUserAddresses.Attach(userAddress);
                Entities.Entry(userAddress).State = System.Data.Entity.EntityState.Modified;
            }
            Entities.SaveChanges();
        }

        public void Remove(long userAddressId)
        {

            var userAddress = GeTPUserAddressById(userAddressId).ToList().FirstOrDefault();
            if (userAddress != null)
            {
                Entities.TPUserAddresses.Remove(userAddress);
            }
        }
    }
}

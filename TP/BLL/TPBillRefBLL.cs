using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EntitiesDABL;
using EntitiesDABL.DAL;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;

namespace TP.BLL
{
    public class TPBillRefBLL
    {

        public ICollection<TPBillRef> GetUnCompletedCallIns(int terminalId)
        {
            ICollection<TPBillRef> result = null;
            using (var entities = new JZZPEntities())
            {

                var query = new TPBillRefDAL(entities).GetUnCompletedTpBillRefs(terminalId);
                result = query.Include(i => i.TPCallIn).ToList();
            }
            return result;
        }

        public void UpdateBillRefUser(long billRefId, Guid userId)
        {
            
            using (var entities = new JZZPEntities())
            {
                
                new TPBillRefDAL(entities).UpdateBillRefUser(billRefId, userId);
                entities.SaveChanges();
            }
        }

        public TPBillRef GeBillRefWithUserAndUserAddressByTpBillRefId(long billRefId)
        {
            TPBillRef result = null;
            using (var entities = new JZZPEntities())
            {

                var query = new TPBillRefDAL(entities).GetTPBillRefById(billRefId);
                result = query.Include(i => i.TPUser)
                    .Include(i => i.TPUser.TPUserAddress)
                    .ToList().FirstOrDefault();
            }
            return result;
        }

        public TPBillRef GeBillRefWithUserByTpBillRefId(long billRefId)
        {
            TPBillRef result = null;
            using (var entities = new JZZPEntities())
            {

                var query = new TPBillRefDAL(entities).GetTPBillRefById(billRefId);
                result = query.Include(i => i.TPUser).ToList().FirstOrDefault();
            }
            return result;
        }



        public TPBillRef GetDeliveryTabViewByTpBillRefId(long billRefId)
        {
            return null;
        }

        public void SaveUser(long billRefId, TPUser user)
        {
            if (user != null)
            {
                using (var entities = new JZZPEntities())
                {

                    new TPUserDAL(entities).Save(user);
                    new TPBillRefDAL(entities).SaveUser(billRefId, user.UserId);
                    entities.SaveChanges();
                }
            }
        }

        public void SaveAddress(long billRefId, TPUserAddress userAddress)
        {
            if (userAddress != null)
            {
                using (var entities = new JZZPEntities())
                {

                    new TPUserAddressDAL(entities).Save(userAddress);
                    new TPBillRefDAL(entities).SaveUserAddress(billRefId, userAddress.UserAddressId);
                    entities.SaveChanges();
                }
            }
        }

        public void RemoveUserAddress(long billRefId, long userAddressId)
        {

            using (var entities = new JZZPEntities())
            {

                new TPBillRefDAL(entities).SaveUserAddress(billRefId, null);
                new TPUserAddressDAL(entities).Remove(userAddressId);
                
                entities.SaveChanges();
            }


        }


    }
}

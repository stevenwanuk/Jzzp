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
                    //.Include(i => i.TPUserAddress)
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

        public TPBillRef GeBillRefWithUserAndDriverByTpBillRefId(long billRefId)
        {
            TPBillRef result = null;
            using (var entities = new JZZPEntities())
            {

                var query = new TPBillRefDAL(entities).GetTPBillRefById(billRefId);
                result = query.Include(i => i.TPUser).Include(i => i.TPDeliver).Include(i => i.TPDeliver.TPDriver).ToList().FirstOrDefault();
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

        public void SaveDliveryInfos(long billRefId, decimal? deliveryMiles, decimal? deliveryFee)
        {

            using (var entitites = new JZZPEntities())
            {
                var billRef = new TPBillRefDAL(entitites).GetTPBillRefById(billRefId).ToList().FirstOrDefault();
                if (billRef != null)
                {
                    billRef.DeliverMiles = deliveryMiles;
                    billRef.DeliverFee = deliveryFee;
                    entitites.SaveChanges();
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

        public void UpdateDeliveryInfos(long billRefId, decimal? deliveryMiles, decimal? deliveryFee)
        {
            using (var entities = new JZZPEntities())
            {
                var billRef = new TPBillRefDAL(entities).GetTPBillRefById(billRefId).ToList().FirstOrDefault();
                if (billRef != null)
                {

                    billRef.DeliverMiles = deliveryMiles;
                    billRef.DeliverFee = deliveryFee;

                    entities.SaveChanges();
                }
                
            }

        }


        public long? GetLastBillRefIdByUser(Guid userId)
        {

            long? result = null;
            using (var entities = new JZZPEntities())
            {
                var lastPaidBillId = new JzzpBillBLL().GetLastPaidBillIdByUserId(userId);
                if (!string.IsNullOrEmpty(lastPaidBillId))
                {
                    result = new TPBillRefDAL(entities).GetBillRefIdByBillId(lastPaidBillId);
                }
            }
            return result;
        }


    }
}

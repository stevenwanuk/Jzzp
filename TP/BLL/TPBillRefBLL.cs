using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EntitiesDABL;
using EntitiesDABL.DAL;
using Jzzp.Enum;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using TP.Common;

namespace TP.BLL
{
    public class TPBillRefBLL
    {

        public void BindingBillId(long billRefId, string billId)
        {

            using (var entities = new JZZPEntities())
            {
                var billRef = entities.TPBillRefs.Where(i => i.BillRefId == billRefId).FirstOrDefault();
                if (billRef != null)
                {
                    billRef.BillId_FK = billId;
                    entities.SaveChanges();
                }
            }

        }

        public List<TPBillRef> GetBillRefssWithParameters(TPBillRef qBillRef, int? queryStatus, long? driverId, DateTime? qStartDate, DateTime? qEndDate)
        {

            var result = new List<TPBillRef>();
            using (var entities = new JZZPEntities())
            {
                var query = entities.TPBillRefs.AsQueryable();

                if (qBillRef != null)
                {

                    if (qBillRef.UserId_FK != null)
                    {
                        query = query.Where(i => i.TPUser != null && qBillRef.UserId_FK == i.UserId_FK);
                    }
                    
                }

                if (driverId != null)
                {
                    query = query.Where(i => i.TPDeliver != null && driverId == i.TPDeliver.DriverId_FK);
                }

                if (queryStatus != null)
                {
                    query = query.Where(i => queryStatus == i.Status);
                }

                if (qStartDate != null)
                {
                    query = query.Where(i => i.TPCallIn.StartDate >= qStartDate);
                }
                if (qEndDate != null)
                {
                    query = query.Where(i => i.TPCallIn.EndDate <= qEndDate);
                }
                result = query.Include(i => i.TPUser).Include(i => i.TPDeliver.TPDriver).ToList();
            }

            return result;
        }

        public ICollection<TPBillRef> GetDisplayedTPBillRef(int terminalId)
        {
            var result = new List<TPBillRef>();

            using (var entities = new JZZPEntities())
            {

                var query = from br in entities.TPBillRefs
                            where br.ShowOnMain 
                            && br.TPCallIn != null 
                            && br.TPCallIn.TerminalId == terminalId
                            select br;
                result = query.Include(i => i.TPCallIn).ToList();
            }

            return result;
        }

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

                var billRef = entities.TPBillRefs.FirstOrDefault(i => i.BillRefId == billRefId);
                if (billRef != null)
                {
                    billRef.UserId_FK = userId;
                    BillRefStatusProcess(billRef, (int)BillRefStatus.Started);
                }

                entities.SaveChanges();
            }
        }
        public static void BillRefStatusProcess(TPBillRef billRef, int status)
        {
            if (billRef.Status<status)
            {
                billRef.Status = status;
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
                result = query.Include(i => i.TPUser).Include(i =>i.TPUserAddress).Include(i => i.TPDeliver).Include(i => i.TPDeliver.TPDriver).ToList().FirstOrDefault();
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

                    var billRef = entities.TPBillRefs.FirstOrDefault(i => i.BillRefId == billRefId);
                    if (billRef != null)
                    {
                        billRef.UserId_FK = user.UserId;
                        BillRefStatusProcess(billRef, (int)BillRefStatus.Started);
                    }
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

        public void SaveDliveryInfos(long billRefId, decimal? deliveryMiles, decimal? deliveryFeeOrigin)
        {

            using (var entitites = new JZZPEntities())
            {
                var billRef = new TPBillRefDAL(entitites).GetTPBillRefById(billRefId).FirstOrDefault();
                if (billRef != null)
                {
                    billRef.DeliverMiles = deliveryMiles;
                    billRef.DeliverFeeOrigin = deliveryFeeOrigin;
                    BillRefStatusProcess(billRef, (int)BillRefStatus.Started);
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
                    TPBillRefBLL.BillRefStatusProcess(billRef, (int)BillRefStatus.Distributed);

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

        public TPBillRef CreatNewBillRef(string telno, int terminalId)
        {
            
            TPBillRef billRef = null;
            using (var entities = new JZZPEntities())
            {
                billRef = new TPBillRef()
                {
                    Status = (int)BillRefStatus.Initial,
                    ShowOnMain = true
                };
                entities.TPBillRefs.Add(billRef);
                var callIn = new TPCallIn()
                {
                    TerminalId = terminalId,
                    StartDate = DateTime.Now
                };
                entities.TPCallIns.Add(callIn);
                billRef.TPCallIn = callIn;
                if (!string.IsNullOrEmpty(telno))
                {
                    Log4netUtil.For(this).Debug(telno);
                    telno = new string(telno.Where(char.IsDigit).ToArray());
                    Log4netUtil.For(this).Debug(telno);
                    callIn.CellNumber = telno;

                    //looking for useraddress
                    var query = entities.TPBillRefs.Where(i => i.TPCallIn != null
                        && i.TPCallIn.CellNumber == telno && i.BillRefId != billRef.BillRefId)
                        .OrderByDescending(i => i.BillRefId).FirstOrDefault();

                    if (query != null)
                    {
                        billRef.UserId_FK = query.UserId_FK;
                        billRef.AddressId_FK = query.AddressId_FK;
                        billRef.DeliverFeeOrigin = query.DeliverFeeOrigin;
                    }

                }

                Log4netUtil.For(this).Debug(string.Format("save billref: telno=>{0},terminalId=>{1},UserId_FK=>{2},AddressId_FK=>{3}", telno, terminalId, billRef.UserId_FK, billRef.AddressId_FK));
                entities.SaveChanges();
            }
            return billRef;
        }

        public void UpdateShowStatus(long tpBillRefId, bool isShow)
        {

            using (var entities = new JZZPEntities())
            {

                var billRef = entities.TPBillRefs.Where(i => i.BillRefId == tpBillRefId).FirstOrDefault();
                if (billRef != null)
                {
                    billRef.ShowOnMain = isShow;
                    entities.SaveChanges();
                }

            }
        }
    }
}

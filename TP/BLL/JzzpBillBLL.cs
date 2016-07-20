using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesDABL;
using EntitiesDABL.DAL;
using EntitiesDABL.DTO;
using System.Data.Entity;
using TP.AppStatic;

namespace TP.BLL
{
    public class JzzpBillBLL
    {

        public ICollection<OrderHistoryDTO> GetFavouriteByUserId(Guid userId, int count)
        {
            return new JzzpBillDAL(new JZZPEntities()).GetFavouriteByUserId(userId).Take(count).ToList();
        }


        public List<TempBill> GetUnBindListWithCurrentBillId(string billId)
        {
            var result = new List<TempBill>();
            using (var entities = new JZZPEntities())
            {

                var nowDay = DateTime.Now.Date;

                var query = from b in entities.TempBills
                            where (!entities.TPBillRefs.Any(i => i.BillId_FK == b.BillID)
                            || b.BillID == billId)
                            && b.BillDate != null
                            //&& b.CheckOutTime >= nowDay
                            orderby b.BillDate descending 
                            select b;
                result = query.Take(TPConfig.UnBindingBillIdDisplayCount).ToList();
            }
            return result;
        }

        public BillDTO GetLastPaidBillByUserId(Guid userId)
        {

            BillDTO result = null;
            using (var entities = new JZZPEntities())
            {
                var dal = new JzzpBillDAL(entities);
                var billId = dal.GetLastPaidBillByUserId(userId);
                if (!string.IsNullOrEmpty(billId))
                {
                    result = new BillDTO()
                    {
                        Bill = dal.GetBillByBillId(billId).ToList().FirstOrDefault(),
                        BillItems = dal.GetBillItemsByBIllId(billId).ToList()
                    };
                }
            }
            
            return result;
        }

        public string GetLastPaidBillIdByUserId(Guid userId)
        {

            string result = null;
            using (var entities = new JZZPEntities())
            {
                
                result = new JzzpBillDAL(entities).GetLastPaidBillByUserId(userId);
            }

            return result;
        }

        public TempBillDTO GetTempBillByBillId(string billId)
        {
            TempBillDTO result = null;
            using (var entities = new JZZPEntities())
            {
                var dal = new JzzpBillDAL(entities);
                result = new TempBillDTO()
                {
                    TempBill = dal.GetTempBillByBillId(billId).ToList().FirstOrDefault(),
                    TempBillItems = dal.GetTempBillItemsByBIllId(billId).ToList()
                };
            }
            return result;
        }

        public BillDTO GetBillByBillId(string billId)
        {
            BillDTO result = null;
            using (var entities = new JZZPEntities())
            {
                var dal = new JzzpBillDAL(entities);
                result = new BillDTO()
                {
                    Bill = dal.GetBillByBillId(billId).ToList().FirstOrDefault(),
                    BillItems = dal.GetBillItemsByBIllId(billId).ToList()
                };
            }
            return result;
        }

    }
}

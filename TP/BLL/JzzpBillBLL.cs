using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesDABL;
using EntitiesDABL.DAL;
using EntitiesDABL.DTO;

namespace TP.BLL
{
    public class JzzpBillBLL
    {

        public ICollection<OrderHistoryDTO> GetFavouriteByUserId(Guid userId, int count)
        {
            return new JzzpBillDAL(new JZZPEntities()).GetFavouriteByUserId(userId).ToList().Take(count).ToList();
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

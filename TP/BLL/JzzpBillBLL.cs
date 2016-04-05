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
            var dal = new JzzpBillDAL(new JZZPEntities());
            var billId = dal.GetLastPaidBillByUserId(userId);
            if (!string.IsNullOrEmpty(billId))
            {
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

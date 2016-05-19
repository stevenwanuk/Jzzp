using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using EntitiesDABL.DTO;

namespace EntitiesDABL.DAL
{
    public class JzzpBillDAL : DALBase
    {

        public JzzpBillDAL(JZZPEntities entities)
        {
            this.Entities = entities;
        }

        public IQueryable<OrderHistoryDTO> GetFavouriteByUserId(Guid userId)
        {
            var query = from bi in Entities.BillItems
                join br in Entities.TPBillRefs on bi.BillID equals br.BillId_FK
                where br.UserId_FK == userId
                group bi by new { bi.MenuID, bi.MenuName} into gbi
                orderby gbi.Count()
                select new OrderHistoryDTO
                {
                    MenuID = gbi.Key.MenuID,
                    MenuName = gbi.Key.MenuName,
                    TotalAmount = gbi.Sum(s => s.AmountOrder - s.AmountCancel),
                    TotalCount = gbi.Count()
                };

            return query;
        }

        public string GetLastPaidBillByUserId(Guid userId)
        {
            //because of sql 2000 issue, won't able to use take() to retrieve data
            //consider with performance, raw sql is only option.
            var sql =
                "select top 1 a.billid from bi_billpayment a " +
                " join TP_BillRef t on a.billid = t.billid_FK " + 
                " where t.userid_FK = '{0}' " +
                " order by a.billid desc ";

            return Entities.Database.SqlQuery<string>(string.Format(sql, userId)).ToList().FirstOrDefault();
        }

        public IQueryable<Bill> GetBillByBillId(string billId)
        {
            return Entities.Bills.Where(i => i.BillID.Equals(billId));
        }

        public IQueryable<BillItem> GetBillItemsByBIllId(string billId)
        {

            return Entities.BillItems.Where(i => i.BillID.Equals(billId));
        }

        public IQueryable<TempBill> GetTempBillByBillId(string billId)
        {
            return Entities.TempBills.Where(i => i.BillID.Equals(billId));
        }

        public IQueryable<TempBillItem> GetTempBillItemsByBIllId(string billId)
        {

            return Entities.TempBillItems.Where(i => i.BillID.Equals(billId));
        }
    }
}

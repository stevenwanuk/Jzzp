using EntitiesDABL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesDABL.BLL
{
    public class TPBillRefBLL
    {

        public ICollection<TPBillRef> GetUnCompletedCallIns(int terminalId)
        {
            ICollection<TPBillRef> result = null;
            using (var entities = new JZZPEntities())
            {

                result = new TPBillRefDAL(entities).GetUnCompletedCallIns(terminalId);
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

        public TPBillRef GetTPBillRefById(long billRefId)
        {
            TPBillRef result = null;
            using (var entities = new JZZPEntities())
            {

                result = new TPBillRefDAL(entities).GetTPBillRefById(billRefId);
            }
            return result;
        }


    }
}

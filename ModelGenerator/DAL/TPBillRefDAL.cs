using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jzzp.Enum;
using ModelGenerator;
using System.Data.SqlClient;

namespace EntitiesDABL.DAL
{
    public class TPBillRefDAL : DALBase
    {

        public TPBillRefDAL (JZZPEntities entities)
        {
            this.Entities = entities;
        }


        public IQueryable<TPBillRef> GetUnCompletedTpBillRefs(int terminalId)
        {

            var query = from br in Entities.TPBillRefs
                        where br.Status < (int)BillRefStatus.Done
                        && br.TPCallIn != null && br.TPCallIn.TerminalId == terminalId
                        select br;
            return query;
        }

        public void UpdateBillRefUser(long billRefId, Guid userId)
        {

            var query = Entities.TPBillRefs.Where(i => i.BillRefId == billRefId).ToList().FirstOrDefault();
            if (query != null)
            {
                query.UserId_FK = userId;
            }
        }

        public IQueryable<TPBillRef> GetTPBillRefById(long billRefId)
        {
            return Entities.TPBillRefs.Where(i => i.BillRefId == billRefId);
        }

        public void SaveUser(long billRefId, Guid userId)
        {
            var billRef = Entities.TPBillRefs.Where(i => i.BillRefId == billRefId).ToList().FirstOrDefault();
            if (billRef != null)
            {

                //Entities.TPUsers.Attach(user);
                billRef.UserId_FK = userId;
            }
        }

        public void SaveUserAddress(long billRefId, long? userAddressId)
        {
            var billRef = Entities.TPBillRefs.Where(i => i.BillRefId == billRefId).ToList().FirstOrDefault();
            if (billRef != null)
            {
                billRef.AddressId_FK = userAddressId;
            }
        }
    }
}

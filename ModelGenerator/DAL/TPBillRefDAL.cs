using System;
using System.Collections.Generic;
using System.Data;
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


        public ICollection<TPBillRef> GetUnCompletedCallIns(int terminalId)
        {

            var query = from br in Entities.TPBillRefs.Include("TPCallIn")
                        where br.Status < (int)BillRefStatus.Done
                        && br.TPCallIn != null && br.TPCallIn.TerminalId == terminalId
                        select br;

            return query.ToList();
        }

        public void UpdateBillRefUser(long billRefId, Guid userId)
        {

            var query = Entities.TPBillRefs.Where(i => i.BillRefId == billRefId).ToList().FirstOrDefault();
            if (query != null)
            {
                query.UserId_FK = userId;
            }
        }

        public TPBillRef GetTPBillRefById(long billRefId)
        {
            return Entities.TPBillRefs.Include("TPUser").Where(i => i.BillRefId == billRefId).ToList().FirstOrDefault();
        }
    }
}

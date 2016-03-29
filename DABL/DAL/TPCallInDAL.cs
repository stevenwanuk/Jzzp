using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jzzp.Common;
using Jzzp.Enum;
using ModelGenerator;

namespace Jzzp.DAL
{
    public class TPCallInDAL
    {

        public ICollection<TPCallIn> GetCallInsByBillStatus()
        {
            var sql = " select * from tp_callin a " +
                         " left join TP_BillRef b on a.CallInId = b.CallInId_FK " +
                         " where b.Status is null or b.Status < @status";
            var result = new List<TPCallIn>();
            using (var command = DatabaseUtils.DbInstance.GetSqlStringCommand(sql))
            {

                DatabaseUtils.DbInstance.AddInParameter(command, "@status", DbType.Int32, (int)BillRefStatus.Done);

                using (var reader = DatabaseUtils.DbInstance.ExecuteReader(command))
                {

                    result = GetDataFromReader(reader);
                }
            }
            return result;
        }

        protected List<TPCallIn> GetDataFromReader(IDataReader reader)
        {
            
            var callIns = new List<TPCallIn>();
            while (reader != null && reader.Read())
            {
                var entity = new TPCallIn()
                {
                    CallInId = reader.GetInt64(reader.GetOrdinal("CallInId")),
                    CellNumber = reader.GetString(reader.GetOrdinal("CellNumber")),
                    StartDate = DALHelper.GetNullableDate(reader, reader.GetOrdinal("StartDate")),
                    EndDate = DALHelper.GetNullableDate(reader, reader.GetOrdinal("EndDate")),
                    TeminalId = reader.GetInt32(reader.GetOrdinal("TeminalId"))
                };

                callIns.Add(entity);
            }
            return callIns;
        }
    }
}

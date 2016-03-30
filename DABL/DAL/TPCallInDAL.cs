using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using Jzzp.Common;
using Jzzp.Enum;
using ModelGenerator;

namespace Jzzp.DAL
{
    public class TPCallInDAL
    {

        public ICollection<TPCallIn> GetUnCompletedCallIns(int terminalId)
        {
            var sql = " select * from tp_callin a " +
                         " left join TP_BillRef b on a.CallInId = b.CallInId_FK " +
                         " where b.Status is null or b.Status < @status and terminalId = @terminal order by a.CallInId";
            var result = new List<TPCallIn>();
            using (var command = DatabaseUtils.DbInstance.GetSqlStringCommand(sql))
            {

                DatabaseUtils.DbInstance.AddInParameter(command, "@status", DbType.Int32, (int)BillRefStatus.Done);
                DatabaseUtils.DbInstance.AddInParameter(command, "@terminal", DbType.Int32, terminalId);

                using (var reader = DatabaseUtils.DbInstance.ExecuteReader(command))
                {

                    while (reader != null && reader.Read())
                    {

                        var tPCallIn = GetSimpleDataFromReader(reader, true);

                        var tPBillRef = TPBillRefDAL.GetSimpleDataFromReader(reader, true);
                        if (tPBillRef != null)
                        {
                            tPCallIn.TPBillRef = new List<TPBillRef>() {tPBillRef};
                        }


                        result.Add(tPCallIn);    
                    }
                }
            }
            return result;
        }

        public static TPCallIn GetSimpleDataFromReader(IDataReader reader, bool isRead = false)
        {

            TPCallIn result = null;

            if (reader != null && (isRead || reader.Read()))
            {
                var id = DALHelper.GetNullableLong(reader, reader.GetOrdinal("CallInId"));
                if (id != null)
                {
                    result = new TPCallIn
                    {
                        CallInId = reader.GetInt64(reader.GetOrdinal("CallInId")),
                        CellNumber = reader.GetString(reader.GetOrdinal("CellNumber")),
                        StartDate = DALHelper.GetNullableDate(reader, reader.GetOrdinal("StartDate")),
                        EndDate = DALHelper.GetNullableDate(reader, reader.GetOrdinal("EndDate")),
                        TerminalId = reader.GetInt32(reader.GetOrdinal("TerminalId"))
                    };
                }
            }

            return result;
        }
    }
}

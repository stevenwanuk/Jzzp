using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jzzp.Common;
using ModelGenerator;

namespace Jzzp.DAL
{
    public class TPDeliverDAL
    {
        public TPDeliver GeTPDeliverById(long tpDeliverId)
        {

            var sql = " select * from tp_deliver a where DeliverId = @deliverId ";
            TPDeliver result = null;
            using (var command = DatabaseUtils.DbInstance.GetSqlStringCommand(sql))
            {

                DatabaseUtils.DbInstance.AddInParameter(command, "@deliverId", DbType.Guid, tpDeliverId);

                using (var reader = DatabaseUtils.DbInstance.ExecuteReader(command))
                {

                    result = GetSimpleDataFromReader(reader, true);
                }
            }
            return result;
        }

        public static TPDeliver GetSimpleDataFromReader(IDataReader reader, bool isRead = false)
        {

            TPDeliver result = null;

            if (reader != null && (isRead || reader.Read()))
            {
                var id = DALHelper.GetNullableLong(reader, reader.GetOrdinal("DeliverId"));
                if (id != null)
                {
                    result = new TPDeliver
                    {
                        DeliverId = reader.GetInt64(reader.GetOrdinal("DeliverId")),
                        DriverId_FK = reader.GetInt64(reader.GetOrdinal("FirstName")),
                        StartDate = DALHelper.GetNullableDate(reader, reader.GetOrdinal("StartDate")),
                        EndDate = DALHelper.GetNullableDate(reader, reader.GetOrdinal("EndDate")),
                        Status = reader.GetInt32(reader.GetOrdinal("Status"))
                    };
                }
            }

            return result;
        }
    }
}

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
    public class TPUserDAL
    {


        public static TPUser GeTPUserById(Guid tPUserId)
        {

            var sql = " select * from tp_user a where UserId = @userId ";
            TPUser result = null;
            using (var command = DatabaseUtils.DbInstance.GetSqlStringCommand(sql))
            {

                DatabaseUtils.DbInstance.AddInParameter(command, "@userId", DbType.Guid, tPUserId);

                using (var reader = DatabaseUtils.DbInstance.ExecuteReader(command))
                {

                    result = GetSimpleDataFromReader(reader, true);
                }
            }
            return result;
        }

        public static TPUser GetSimpleDataFromReader(IDataReader reader, bool isRead = false)
        {

            TPUser result = null;

            if (reader != null && (isRead || reader.Read()))
            {
                var id = DALHelper.GetNullableGuid(reader, reader.GetOrdinal("UserId"));
                if (id != null)
                {
                    result = new TPUser
                    {
                        UserId = reader.GetGuid(reader.GetOrdinal("UserId")),
                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
                        Gender = DALHelper.GetNullableInt(reader, reader.GetOrdinal("Gender"))
                    };
                }
            }

            return result;
        }
    }
}

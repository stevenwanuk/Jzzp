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
    public class TPUserAddressDAL
    {
        public TPUserAddress GeTPUserAddressById(long tPUserAddressId)
        {

            var sql = " select * from tp_useraddress a where UserAddressId = @userAddressId ";
            TPUserAddress result = null;
            using (var command = DatabaseUtils.DbInstance.GetSqlStringCommand(sql))
            {

                DatabaseUtils.DbInstance.AddInParameter(command, "@userAddressId", DbType.Int64, tPUserAddressId);

                using (var reader = DatabaseUtils.DbInstance.ExecuteReader(command))
                {

                    result = GetSimpleDataFromReader(reader, true);
                }
            }
            return result;
        }

        public static TPUserAddress GetSimpleDataFromReader(IDataReader reader, bool isRead = false)
        {

            TPUserAddress result = null;

            if (reader != null && (isRead || reader.Read()))
            {
                var id = DALHelper.GetNullableLong(reader, reader.GetOrdinal("UserAddressId"));
                if (id != null)
                {
                    result = new TPUserAddress
                    {
                        UserAddressId = reader.GetInt64(reader.GetOrdinal("UserAddressId")),
                        UserId_FK = reader.GetGuid(reader.GetOrdinal("UserId_FK")),
                        HouseNumber = reader.GetString(reader.GetOrdinal("HouseNumber")),
                        AddressField1 = reader.GetString(reader.GetOrdinal("AddressField1")),
                        AddressField2 = reader.GetString(reader.GetOrdinal("AddressField2")),
                        AddressField3 = reader.GetString(reader.GetOrdinal("AddressField3")),
                        TownCity = reader.GetString(reader.GetOrdinal("TownCity")),
                        Postcode = reader.GetString(reader.GetOrdinal("Postcode")),
                        Country = reader.GetString(reader.GetOrdinal("Country")),

                    };
                }
            }

            return result;
        }
    }
}

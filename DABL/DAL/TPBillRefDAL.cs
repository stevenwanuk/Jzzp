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
    public class TPBillRefDAL
    {

        public static TPBillRef GetSimpleDataFromReader(IDataReader reader, bool isRead = false)
        {
            TPBillRef result = null;

            if (reader != null && (isRead || reader.Read()))
            {

                var id = DALHelper.GetNullableLong(reader, reader.GetOrdinal("BillRefId"));
                if (id != null)
                {
                    result = new TPBillRef()
                    {
                        BillRefId = reader.GetInt64(reader.GetOrdinal("BillRefId")),
                        CallInId_FK = DALHelper.GetNullableLong(reader, reader.GetOrdinal("CallInId_FK")),
                        UserId_FK = DALHelper.GetNullableGuid(reader, reader.GetOrdinal("UserId_FK")),
                        AddressId_FK = DALHelper.GetNullableLong(reader, reader.GetOrdinal("AddressId_FK")),
                        BillId_FK = reader.GetString(reader.GetOrdinal("BillId_FK")),
                        DeliverMiles = DALHelper.GetNullableDecimal(reader, reader.GetOrdinal("DeliverMiles")),
                        DeliverFee = DALHelper.GetNullableDecimal(reader, reader.GetOrdinal("DeliverFee")),
                        Status = reader.GetInt32(reader.GetOrdinal("Status"))
                    };
                }
            }

            return result;
        }



        public TPBillRef GetUserDetails(long billRefId)
        {

            TPBillRef result = null;
            var sql = " select * from TP_BillRef a " +
                      " left join TP_User b on a.UserId_FK = b.UserId " +
                      " left join TP_UserAddress c on a.AddressId_FK = c.UserAddressId " +
                      " where BillRefId = @billRefId ";

            using (var command = DatabaseUtils.DbInstance.GetSqlStringCommand(sql))
            {

                DatabaseUtils.DbInstance.AddInParameter(command, "@billRefId", DbType.Int32, billRefId);

                using (var reader = DatabaseUtils.DbInstance.ExecuteReader(command))
                {

                    if (reader != null && reader.Read())
                    {

                        result = TPBillRefDAL.GetSimpleDataFromReader(reader, true);
                        var tpUser = TPUserDAL.GetSimpleDataFromReader(reader, true);
                        if (tpUser != null)
                        {
                            result.TPUser = tpUser;
                        }

                        var tpUserAddress = TPUserAddressDAL.GetSimpleDataFromReader(reader, true);
                        if (tpUserAddress != null)
                        {
                            result.TPUserAddress = tpUserAddress;
                        }
                    }
                }
            }
            return result;
        }

        public void FillWithUserDetails(ref TPBillRef tPBillRef)
        {

            if (tPBillRef != null)
            {
                tPBillRef = GetUserDetails(tPBillRef.BillRefId);
            }
            
        }
    }
}

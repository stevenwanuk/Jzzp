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
    public class TPBillRefDAL
    {




        public static ICollection<TPBillRef> GetUnCompletedCallIns(int terminalId)
        {
            var sql = " select * from tp_callin a " +
                         " join TP_BillRef b on a.CallInId = b.CallInId_FK " +
                         " where b.Status is null or b.Status < @status and terminalId = @terminal order by a.CallInId";
            var result = new List<TPBillRef>();
            using (var command = DatabaseUtils.DbInstance.GetSqlStringCommand(sql))
            {

                DatabaseUtils.DbInstance.AddInParameter(command, "@status", DbType.Int32, (int)BillRefStatus.Done);
                DatabaseUtils.DbInstance.AddInParameter(command, "@terminal", DbType.Int32, terminalId);

                using (var reader = DatabaseUtils.DbInstance.ExecuteReader(command))
                {

                    while (reader != null && reader.Read())
                    {

                        var tPBillRef = GetSimpleDataFromReader(reader, true);

                        var tpCallIn = TPCallInDAL.GetSimpleDataFromReader(reader, true);
                        if (tpCallIn != null)
                        {
                            tPBillRef.TPCallIn = tpCallIn;
                        }

                        result.Add(tPBillRef);
                    }
                }
            }
            return result;
        }


        public static TPBillRef GetSimpleDataFromReader(IDataReader reader, bool isRead = false)
        {
            TPBillRef result = null;

            if (reader != null && (isRead || reader.Read()))
            {

                var id = DALHelper.GetNullableLong(reader, reader.GetOrdinal("BillRefId"));
                if (id != null)
                {
                    result = new TPBillRef();
                    result.BillRefId = reader.GetInt64(reader.GetOrdinal("BillRefId"));
                    result.CallInId_FK = DALHelper.GetNullableLong(reader, reader.GetOrdinal("CallInId_FK"));
                    result.UserId_FK = DALHelper.GetNullableGuid(reader, reader.GetOrdinal("UserId_FK"));
                    result.AddressId_FK = DALHelper.GetNullableLong(reader, reader.GetOrdinal("AddressId_FK"));
                    result.BillId_FK = DALHelper.GetNullableString(reader, reader.GetOrdinal("BillId_FK"));
                    result.DeliverMiles = DALHelper.GetNullableDecimal(reader, reader.GetOrdinal("DeliverMiles"));
                    result.DeliverFee = DALHelper.GetNullableDecimal(reader, reader.GetOrdinal("DeliverFee"));
                    result.Status = reader.GetInt32(reader.GetOrdinal("Status"));
                }
            }

            return result;
        }



        public static TPBillRef GetUserDetails(long billRefId)
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

        public static void BindUser(long billRefId, Guid userId)
        {
            var sql = " updateTP_BillRef  " +
                      " set UserId_FK = @userId_FK " +
                      " where BillRefId = @billRefId ";
            using (var command = DatabaseUtils.DbInstance.GetSqlStringCommand(sql))
            {

                DatabaseUtils.DbInstance.AddInParameter(command, "@userId_FK", DbType.Guid, userId);
                DatabaseUtils.DbInstance.AddInParameter(command, "@billRefId", DbType.Int64, billRefId);

                DatabaseUtils.DbInstance.ExecuteNonQuery(command);
            }
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

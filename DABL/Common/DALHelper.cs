using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using System.Reflection;

namespace Jzzp.Common
{
    public class DALHelper
    {
        public static DateTime? GetNullableDate(IDataRecord record, int ordinal)
        {
            if (record.IsDBNull(ordinal))
                return null;
            else
                return record.GetDateTime(ordinal);
        }

        public static int? GetNullableInt(IDataRecord record, int ordinal)
        {
            if (record.IsDBNull(ordinal))
                return null;
            else
                return record.GetInt32(ordinal);
        }

        public static long? GetNullableLong(IDataRecord record, int ordinal)
        {
            if (record.IsDBNull(ordinal))
                return null;
            else
                return record.GetInt64(ordinal);
        }

        public static bool? GetNullableBool(IDataRecord record, int ordinal)
        {
            if (record.IsDBNull(ordinal))
                return null;
            else
                return record.GetBoolean(ordinal);
        }

        public static double? GetNullableDouble(IDataRecord record, int ordinal)
        {
            if (record.IsDBNull(ordinal))
                return null;
            else
                return record.GetDouble(ordinal);
        }

        public static string GetNullableString(IDataRecord record, int ordinal)
        {
            if (record.IsDBNull(ordinal))
                return null;
            else
                return record.GetString(ordinal);
        }

        public static Guid? GetNullableGuid(IDataRecord record, int ordinal)
        {
            if (record.IsDBNull(ordinal))
                return null;
            else
                return record.GetGuid(ordinal);
        }

        public static Expression<Func<TElement, bool>> BuildContainsExpression<TElement,
            TValue>(
            Expression<Func<TElement, TValue>> valueSelector, IEnumerable<TValue>
            values)
        {
            if (null == valueSelector)
            {
                throw new
                    ArgumentNullException("valueSelector");
            }
            if (null == values) { throw new ArgumentNullException("values"); }

            var p = valueSelector.Parameters.Single();
            if (!values.Any())
            {
                return e => false;
            }

            var equals = values.Select(value =>
            (Expression)Expression.Equal(valueSelector.Body, Expression.Constant(value,
            typeof(TValue))));
            var body = equals.Aggregate<Expression>((accumulate, equal) =>
            Expression.Or(accumulate, equal));
            return Expression.Lambda<Func<TElement, bool>>(body, p);
        }


        /// <summary>
        /// fill the DataTable with IEnumerable<T> by reflaction
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="varlist"></param>
        /// <returns></returns>
        public static DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
    }
}

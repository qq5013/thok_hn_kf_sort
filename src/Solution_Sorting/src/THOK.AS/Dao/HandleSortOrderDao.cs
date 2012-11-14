using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.AS.Dao
{
    public class HandleSortOrderDao : BaseDao
    {
        internal int FindCount(string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);

            string sql = "SELECT COUNT(*) FROM AS_HANDLE_SORT_ORDER " + where;
            return (int)ExecuteScalar(sql);
        }

        internal DataTable FindAll(int startRecord, int pageSize, string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);

            string sql = "SELECT * FROM AS_HANDLE_SORT_ORDER " + where;
            return ExecuteQuery(sql, "AS_HANDLE_SORT_ORDER", startRecord, pageSize).Tables[0];
        }

        internal void InsertEntity(string orderDate, string orderId)
        {
            SqlCreate sqlCreate = new SqlCreate("AS_HANDLE_SORT_ORDER", SqlType.INSERT);
            sqlCreate.AppendQuote("ORDERDATE", orderDate);
            sqlCreate.AppendQuote("ORDERID", orderId);
            ExecuteNonQuery(sqlCreate.GetSQL());
        }

        internal void UpdateEntity(string orderDate, string oldOrderId, string newOrderId)
        {
            string sql = "UPDATE AS_HANDLE_SORT_ORDER SET ORDERID = '{0}' WHERE ORDERDATE = '{1}' AND ORDERID = '{2}'";
            ExecuteNonQuery(string.Format(sql,newOrderId,orderDate,oldOrderId));
        }

        internal void DeleteEntity(string orderDate, string orderId)
        {
            string sql = "DELETE FROM AS_HANDLE_SORT_ORDER WHERE ORDERDATE = '{0}' AND ORDERID = '{1}' ";
            ExecuteNonQuery(string.Format(sql,orderDate,orderId));
        }
    }
}

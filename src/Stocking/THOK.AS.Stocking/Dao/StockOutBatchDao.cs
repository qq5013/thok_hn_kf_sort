using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.AS.Stocking.Dao
{
    public class StockOutBatchDao: BaseDao
    {
        public void Delete()
        {
            string sql = "TRUNCATE TABLE AS_STOCK_OUT_BATCH";
            ExecuteNonQuery(sql);
        }

        public void InsertBatch(int batchNo, string lineCode, string sortNo, int quantity, string channelGroup)
        {
            SqlCreate sqlCreate = new SqlCreate("AS_STOCK_OUT_BATCH", SqlType.INSERT);
            sqlCreate.Append("BATCHNO", batchNo);
            sqlCreate.AppendQuote("LINECODE", lineCode);
            sqlCreate.Append("SORTNO", sortNo);
            sqlCreate.Append("CHANNELGROUP", channelGroup);
            sqlCreate.Append("QUANTITY", quantity);
            sqlCreate.Append("OUTQUANTITY", 0);
            string sql = sqlCreate.GetSQL();
            ExecuteNonQuery(sql);
        }

        public DataTable FindAll()
        {
            string sql = "SELECT A.BATCHNO,"+
                            " CASE WHEN A.LINECODE='00' THEN 'ȫ��' ELSE A.LINECODE END LINECODE," +
                            " A.QUANTITY, A.OUTQUANTITY,B.ORDERDATE,B.BATCHNO SORTBATCHNO"+
                            " FROM AS_STOCK_OUT_BATCH A" +
                            " LEFT JOIN AS_SC_SUPPLY B ON A.SORTNO = B.SORTNO "+
                            " GROUP BY A.BATCHNO,A.LINECODE,A.QUANTITY, A.OUTQUANTITY,B.ORDERDATE,B.BATCHNO";
            return ExecuteQuery(sql).Tables[0];
        }

        public int FindMaxBatchNo()
        {
            string sql = "SELECT ISNULL(MAX(BATCHNO),0) FROM AS_STOCK_OUT_BATCH ";
            return Convert.ToInt32(ExecuteScalar(sql));
        }

        public DataTable FindBatch()
        {
            string sql = "SELECT  * FROM AS_STOCK_OUT_BATCH WHERE QUANTITY > OUTQUANTITY ORDER BY BATCHNO";
            return ExecuteQuery(sql).Tables[0];
        }

        public void UpdateBatch(string batchNo, int quantity)
        {
            string sql = "UPDATE AS_STOCK_OUT_BATCH SET OUTQUANTITY = OUTQUANTITY + {0} WHERE BATCHNO = {1}";
            ExecuteNonQuery(string.Format(sql, quantity, batchNo));
        }
    }
}

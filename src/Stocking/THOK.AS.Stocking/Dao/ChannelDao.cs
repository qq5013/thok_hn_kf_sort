using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.AS.Stocking.Dao
{
    public class ChannelDao: BaseDao
    {
        public void Delete()
        {
            ExecuteNonQuery("TRUNCATE TABLE AS_BI_STOCKCHANNEL");

            ExecuteNonQuery("TRUNCATE TABLE AS_SC_STOCKMIXCHANNEL");

            ExecuteQuery("TRUNCATE TABLE AS_SC_CHANNELUSED");
        }

        public void InsertChannel(DataTable channelTable)
        {
            BatchInsert(channelTable, "AS_BI_STOCKCHANNEL");            
        }

        public void InsertChannelUSED(DataTable channelTable)
        {            
            BatchInsert(channelTable, "AS_SC_CHANNELUSED");
        }

        public void InsertMixChannel(DataTable mixTable)
        {
            BatchInsert(mixTable, "AS_SC_STOCKMIXCHANNEL");
        }

        public DataTable FindAll()
        {
            string sql = "SELECT CHANNELCODE,CHANNELNAME,"+
                            " CASE WHEN CHANNELTYPE='3' THEN '����̵�' ELSE '��һ�̵�' END CHANNELTYPE,"+
                            " CIGARETTECODE, CIGARETTENAME "+
                            " FROM AS_BI_STOCKCHANNEL";
            return ExecuteQuery(sql).Tables[0];
        }

        public string FindLed(string channelCode)
        {
            string sql = "SELECT ISNULL(MIN(LEDNO),0) FROM AS_BI_STOCKCHANNEL WHERE CHANNELCODE = '{0}'";
            return ExecuteScalar(string.Format(sql, channelCode)).ToString();
        }

        public DataTable FindChannelForCigaretteCode(string cigaretteCode)
        {
            string sql = "SELECT A.CHANNELCODE,A.CIGARETTECODE,A.CIGARETTENAME,A.ISSTOCKIN,A.REMAINQUANTITY,B.BARCODE "+
                            " FROM AS_BI_STOCKCHANNEL A " +
                            " LEFT JOIN AS_SC_SUPPLY B ON A.CIGARETTECODE = B.CIGARETTECODE "+         
                            " WHERE A.CIGARETTECODE = '{0}' "+
                            " GROUP BY A.CHANNELCODE,A.CIGARETTECODE,A.CIGARETTENAME,A.IsStockIn,A.REMAINQUANTITY,B.BARCODE";
            return ExecuteQuery(string.Format(sql, cigaretteCode)).Tables[0];
        }

        #region �����ּ��̵�        
       
        public DataTable FindChannelUSED(string lineCode, string channelCode)
        {
            string sql = "SELECT * FROM AS_SC_CHANNELUSED WHERE CHANNELCODE='{0}' AND LINECODE = '{1}' ";
            return ExecuteQuery(string.Format(sql, channelCode, lineCode)).Tables[0];
        }

        internal DataTable FindEmptyChannel(string lineCode, string channelCode, object channelGroup, object channelType)
        {
            string sql = "SELECT CHANNELCODE, " +
                            " LINECODE + ' ��  ' + CHANNELNAME + ' ' + CASE CHANNELTYPE WHEN '2' THEN '��ʽ��' WHEN '5' THEN '��ʽ��'  ELSE 'ͨ����' END CHANNELNAME " +
                            " FROM AS_SC_CHANNELUSED " +
                            " WHERE CHANNELTYPE IN ('{0}') AND CHANNELTYPE != '5' AND CHANNELGROUP = {1} AND CHANNELCODE != '{2}' AND LINECODE = '{3}' " +
                            " ORDER BY CHANNELNAME";
            return ExecuteQuery(string.Format(sql, channelType, channelGroup, channelCode, lineCode)).Tables[0];
        }

        public void UpdateChannelUSED(string lineCode, string channelCode, string cigaretteCode, string cigaretteName, int quantity, string sortNo)
        {
            string sql = "UPDATE AS_SC_CHANNELUSED SET CIGARETTECODE='{0}', CIGARETTENAME='{1}', QUANTITY={2}, SORTNO={3} "+
                            " WHERE CHANNELCODE='{4}' AND LINECODE = '{5}'";
            ExecuteNonQuery(string.Format(sql,cigaretteCode, cigaretteName, quantity, sortNo, channelCode, lineCode));
        }

        public DataTable FindChannelUSED()
        {
            string sql = "SELECT CHANNELCODE, CHANNELNAME, "+
                            " CASE CHANNELTYPE WHEN '2' THEN '��ʽ��' WHEN '5' THEN '����̵�' ELSE 'ͨ����' END CHANNELTYPE, " +
                            " LINECODE, CIGARETTECODE, CIGARETTENAME, QUANTITY "+
                            " FROM AS_SC_CHANNELUSED ORDER BY LINECODE, CHANNELNAME";
            return ExecuteQuery(sql).Tables[0];
        }

        #endregion


        /// <summary>
        /// ���Ӳִ����ݿ���ͼ��ѯ���ĳ������ݲ��뵽�������ϵͳ�����ݿ�
        /// </summary>
        public void InsertStockOutData(DataTable StockTempCheckTable)
        {
            ExecuteQuery("TRUNCATE TABLE AS_WMS_STOCK_OUT");
            BatchInsert(StockTempCheckTable, "AS_WMS_STOCK_OUT");
        }
    }
}

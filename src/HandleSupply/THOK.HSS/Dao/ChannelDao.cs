using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using THOK.Util;

namespace THOK.HSS
{
    public class ChannelDao: BaseDao
    {
        public DataTable FindChannel()
        {
            string sql = "SELECT *,CASE CHANNELTYPE WHEN '3' THEN  '通道机' WHEN '5' THEN '混合烟道'  WHEN  '2' THEN '立式机' END CHANNELTYPENAME,QUANTITY / 50 BOXQUANTITY,QUANTITY % 50 ITEMQUANTITY FROM AS_SC_CHANNELUSED";
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindChannelSort(string channelName)
        {
            string sql = string.Format("SELECT A.*,C.CHANNELNAME,B.ROUTENAME,B.CUSTOMERNAME,B.ORDERNO,CASE B.STATUS WHEN 1 THEN '已下单' ELSE '未下单' END AS SORTSTATUS,CASE A.STATUS WHEN 1 THEN '已补货' ELSE '未补货' END AS SUPPLYSTATUS FROM AS_SC_HANDLESUPPLY  A LEFT JOIN (SELECT MIN(SORTNO) AS SORTNO,ORDERID,[STATUS],ROUTENAME,CUSTOMERNAME,ORDERNO FROM AS_SC_PALLETMASTER GROUP BY ORDERDATE,BATCHNO,LINECODE,ORDERID,[STATUS],ROUTENAME,CUSTOMERNAME,ORDERNO) B ON A.ORDERID = B.ORDERID LEFT JOIN AS_SC_CHANNELUSED C ON A.CHANNELCODE = C.CHANNELCODE WHERE C.CHANNELNAME = '{0}' ORDER BY SORTNO", channelName);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindChannelQuantity(string sortNo)
        {
            string sql = string.Format("SELECT *, REMAINQUANTITY / 50 BOXQUANTITY, REMAINQUANTITY % 50 ITEMQUANTITY FROM ( " +
                "SELECT A.CHANNELNAME, A.LEDGROUP, A.LEDNO, CASE CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE, " +
                "A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY, CASE WHEN B.QUANTITY IS NULL THEN 0 ELSE B.QUANTITY END SORTQUANTITY, " +
                "A.QUANTITY -CASE WHEN B.QUANTITY IS NULL THEN 0 ELSE B.QUANTITY END REMAINQUANTITY,A.LED_X,A.LED_Y,A.LED_WIDTH,A.LED_HEIGHT,A.CHANNELADDRESS FROM AS_SC_CHANNELUSED A " +
                "LEFT JOIN(SELECT CHANNELCODE, SUM(QUANTITY) QUANTITY FROM AS_SC_ORDER WHERE SORTNO <= '{0}' GROUP BY CHANNELCODE) B " +
                "ON A.CHANNELCODE = B.CHANNELCODE) C ORDER BY CHANNELNAME", sortNo);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindEmptyChannel()
        {
            string sql = "SELECT CHANNELCODE, CHANNELNAME + ' ' + CASE CHANNELTYPE WHEN '2' THEN '立式机'   WHEN '5' THEN '立式机'  ELSE '通道机' END CHANNELNAME FROM AS_SC_CHANNELUSED WHERE QUANTITY=0 AND STATUS='1' AND CHANNELTYPE IN ('2','3') ORDER BY CHANNELNAME";
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindChannel(string channelCode)
        {
            string sql = string.Format("SELECT * FROM AS_SC_CHANNELUSED WHERE CHANNELCODE='{0}'", channelCode);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindLastSortNo()
        {
            string sql = string.Format("SELECT CHANNELADDRESS,CHANNELCODE,SORTNO FROM AS_SC_CHANNELUSED ORDER BY CHANNELTYPE,CHANNELCODE");
            return ExecuteQuery(sql).Tables[0];
        }

        public void UpdateChannel(string channelCode, string cigaretteCode, string cigaretteName, int quantity, string sortNo)
        {
            string sql = string.Format("UPDATE AS_SC_CHANNELUSED SET CIGARETTECODE='{0}', CIGARETTENAME='{1}', QUANTITY={2}, SORTNO={3} WHERE CHANNELCODE='{4}'",
                cigaretteCode, cigaretteName, quantity, sortNo, channelCode);
            ExecuteNonQuery(sql);
        }

        public void InsertChannel(DataTable channelTable)
        {
            ExecuteQuery("TRUNCATE TABLE AS_SC_CHANNELUSED");
            BatchInsert(channelTable, "AS_SC_CHANNELUSED");
        }
    }
}

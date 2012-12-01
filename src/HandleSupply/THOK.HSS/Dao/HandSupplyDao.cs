using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;
namespace THOK.HSS
{
    public class HandSupplyDao : BaseDao
    {
        public DataTable GetBatchNo()
        {
            string strSql = string.Format("SELECT TOP 1 BATCHNO,ORDERDATE FROM AS_SC_HANDLESUPPLY");
            return ExecuteQuery(strSql).Tables[0];
        }

        public DataTable GetAllHandleSupply()
        {
            string strSql = "SELECT A.SUPPLYNO,A.ORDERDATE,A.BATCHNO,A.LINECODE,A.SORTNO,A.SUPPLYBATCH,A.ORDERID,A.CIGARETTECODE,A.CIGARETTENAME,A.CHANNELCODE,A.QUANTITY,CASE WHEN A.STATUS = '1' THEN'已补货' ELSE '未补货' END STATUS,B.CHANNELNAME FROM AS_SC_HANDLESUPPLY A LEFT JOIN AS_SC_CHANNELUSED B ON A.CHANNELCODE = B.CHANNELCODE ORDER BY SUPPLYBATCH";
            return ExecuteQuery(strSql).Tables[0];
        }

        public DataTable GetAllCigarette()
        {
            string strSql = string.Format("SELECT CIGARETTECODE,CIGARETTENAME,sum(quantity) as quantity,ORDERDATE,LINECODE FROM AS_SC_HANDLESUPPLY GROUP BY LINECODE,ORDERDATE,CIGARETTECODE,CIGARETTENAME ORDER BY SUM(QUANTITY) DESC");
            return ExecuteQuery(strSql).Tables[0];
        }

        public DataTable GetFinishHandSupply()
        {
            string strSql = string.Format("SELECT SUPPLYNO,SORTNO,LINECODE,BATCHNO,ORDERDATE,ORDERID,CIGARETTECODE,CIGARETTENAME,CHANNELCODE,QUANTITY,CASE WHEN STATUS = 1 THEN '已补货' ELSE '未补货' END STATUS FROM AS_SC_HANDLESUPPLY WHERE STATUS='1' ORDER BY SUPPLYNO,QUANTITY DESC ");
            return ExecuteQuery(strSql).Tables[0];
        }

        public DataTable GetUnFinishHandSupply()
        {
            string strSql = string.Format("SELECT SUPPLYNO,SORTNO,LINECODE,BATCHNO,ORDERDATE,ORDERID,CIGARETTECODE,CIGARETTENAME,CHANNELCODE,QUANTITY,CASE WHEN STATUS = 1 THEN '已补货' ELSE '未补货' END STATUS FROM AS_SC_HANDLESUPPLY WHERE STATUS='0' ORDER BY SUPPLYNO,QUANTITY DESC ");
            return ExecuteQuery(strSql).Tables[0];
        }

        public void FinishSupply(string supplyNo,string sortNo,string batchNo,string orderDate)
        {
            string strSql = string.Format("UPDATE AS_SC_HANDLESUPPLY SET STATUS = '1' WHERE SUPPLYNO={0} AND SORTNO = {1} AND BATCHNO = '{2}' AND ORDERDATE ='{3}'", supplyNo, sortNo,batchNo, orderDate);
            ExecuteNonQuery(strSql);
        }

        public DataTable GetCigaretteTable()
        {
            string strSql = string.Format("SELECT CIGARETTECODE,CIGARETTENAME,SUM(QUANTITY) AS QUANTITY FROM AS_SC_HANDLESUPPLY GROUP BY CIGARETTECODE,CIGARETTENAME ");
            return ExecuteQuery(strSql).Tables[0];
        }

        public DataTable GetCigaretteQuantity(string cigaretteCode)
        {
            if (cigaretteCode == "")
            {
                string strSql = string.Format("SELECT UNFINISH=SUM(CASE STATUS WHEN '0' THEN QUANTITY ELSE 0 END),FINISH=SUM(CASE STATUS WHEN '1' THEN QUANTITY ELSE 0 END) FROM AS_SC_HANDLESUPPLY");
                DataTable dt = ExecuteQuery(strSql).Tables[0];
                if (dt.Rows[0]["UNFINISH"].ToString() == "")
                {
                    throw new Exception("手工补货表无数据");
                }
                else
                {
                    return ExecuteQuery(strSql).Tables[0];
                }
            }
            else
            {
                string strSql = string.Format("SELECT CIGARETTECODE,CIGARETTENAME,UNFINISH=SUM(CASE STATUS WHEN '0' THEN QUANTITY ELSE 0 END),FINISH=SUM(CASE STATUS WHEN '1' THEN QUANTITY ELSE 0 END) FROM AS_SC_HANDLESUPPLY WHERE CIGARETTECODE='{0}' GROUP BY CIGARETTECODE , CIGARETTENAME ", cigaretteCode);
                return ExecuteQuery(strSql).Tables[0];
            }
        }

        public DataTable GetCigaretteInformation(string cigaretteCode)
        {
            if (cigaretteCode == "")
            {
                string strSql = string.Format("SELECT SUPPLYNO,CIGARETTECODE,CIGARETTENAME,QUANTITY,CHANNELCODE,CASE WHEN STATUS = 1 THEN '已补货' ELSE '未补货' END STATUS FROM AS_SC_HANDLESUPPLY ");
                return ExecuteQuery(strSql).Tables[0];
            }
            else
            {
                string strSql = string.Format("SELECT SUPPLYNO,CIGARETTECODE,CIGARETTENAME,QUANTITY,CHANNELCODE,CASE WHEN STATUS = 1 THEN '已补货' ELSE '未补货' END STATUS FROM AS_SC_HANDLESUPPLY WHERE CIGARETTECODE='{0}' ORDER BY SUPPLYNO,QUANTITY DESC", cigaretteCode);
                return ExecuteQuery(strSql).Tables[0];
            }
        }

        public DataTable GetHandSupplyBySupplyBatch(int supplyBatch)
        {
            string strSql = string.Format("SELECT A.SUPPLYNO,A.SORTNO,A.CIGARETTECODE,A.BATCHNO,A.CIGARETTENAME,A.LINECODE,A.ORDERDATE,A.SUPPLYBATCH,A.QUANTITY,B.CHANNELNAME CHANNELNAME,CASE WHEN A.STATUS = 1 THEN '已补货' ELSE '未补货' END STATUS FROM AS_SC_HANDLESUPPLY A LEFT JOIN AS_SC_CHANNELUSED B ON A.CHANNELCODE = B.CHANNELCODE WHERE A.SUPPLYBATCH='{0}' ORDER BY A.SUPPLYNO,A.QUANTITY DESC", supplyBatch);
            return ExecuteQuery(strSql).Tables[0];
        }
        
        public int GetHandSupplyCountBySupplyBatch(int supplyBatch)
        {
            string strSql = string.Format("SELECT COUNT(*) FROM AS_SC_HANDLESUPPLY WHERE SUPPLYBATCH='{0}' ", supplyBatch);
            int count=Convert.ToInt32((ExecuteScalar(strSql)));
            if (count == 0)
            {
                return 0;
            }
            else
            {
                return count;
            }
        }
        
        public int GetCurrentSupplyBatch()
        {
            string strSql = string.Format("SELECT TOP 1 SUPPLYBATCH FROM AS_SC_HANDLESUPPLY WHERE STATUS='0' ORDER BY SUPPLYNO,SUPPLYBATCH ");
            string strSql2 = string.Format("SELECT MAX(SUPPLYBATCH) SUPPLYBATCH FROM AS_SC_HANDLESUPPLY ");
            DataTable dt1= ExecuteQuery(strSql).Tables[0];
            //作业已都补完
            if (dt1.Rows.Count == 0)
            {
                DataTable dt2 = ExecuteQuery(strSql2).Tables[0];
                string aa = dt2.Rows[0]["SUPPLYBATCH"].ToString();
                if (dt2.Rows[0]["SUPPLYBATCH"].ToString() == "")
                {
                    throw new Exception("手工补货表无数据");
                }
                else
                {
                    //return Convert.ToInt32(dt2.Rows[0]["SUPPLYBATCH"]);
                    throw new Exception("手工补货任务都已完成");
                }
            }
            else
            {
                return Convert.ToInt32(dt1.Rows[0]["SUPPLYBATCH"]);
            }
        }
        
        public DataTable GetLastSupplyBatch()
        {
            string strSql = string.Format("select * from dbo.AS_SC_HANDLESUPPLY where SUPPLYBATCH in (select top 1 max(SUPPLYBATCH) from AS_SC_HANDLESUPPLY)");
            return ExecuteQuery(strSql).Tables[0];
        }
        public int GetLastSupplyBatchNo()
        {
            string strSql = string.Format("select top 1 SUPPLYBATCH from dbo.AS_SC_HANDLESUPPLY where SUPPLYBATCH in (select top 1 max(SUPPLYBATCH) from AS_SC_HANDLESUPPLY)");
            int count = Convert.ToInt32((ExecuteScalar(strSql)));
            if (count == 0)
            {
                return 0;
            }
            else
            {
                return count;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.AS.OTS.Dao
{
    public class OrderDao: BaseDao
    {

        public DataTable FindOrder()
        {
            string sql = "SELECT LINECODE, BATCHNO, ORDERID,ORDERDATE, CIGARETTECODE, CIGARETTENAME, CHANNELCODE, SUM(QUANTITY) QUANTITY " +
                "FROM AS_SC_ORDER GROUP BY LINECODE, BATCHNO, ORDERID, ORDERDATE, CIGARETTECODE, CIGARETTENAME, CHANNELCODE ORDER BY ORDERID";
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindRoute()
        {
            string sql = "SELECT ORDERDATE,AREACODE,AREANAME,ROUTECODE,ROUTENAME ,MIN(SORTNO) SORTNO ,Count(DISTINCT ORDERID) ORDERCOUNT,SUM(QUANTITY+QUANTITY1) QUANTITY FROM AS_SC_PALLETMASTER " +
                "GROUP BY ORDERDATE,AREACODE,AREANAME,ROUTECODE,ROUTENAME ORDER BY SORTNO";
            return ExecuteQuery(sql).Tables[0];
        }

        //��ѯ��·����ϸ��Ϣ�����ԭ�����ݿ����ͼV_PALLETMASTER�е�SUM(Q0UANTITY) AS QUANTITY�ĳ�SUM(QUANTITY+QUANTITY1) AS QUANTITY���ܵó���ȷ�Ķ�������
        public DataTable FindCustomer()
        {
            string sql = "SELECT SORTNO,ORDERID,ROUTECODE,ROUTENAME,CUSTOMERCODE,CUSTOMERNAME, QUANTITY FROM V_OUTTERMINAL ORDER BY SORTNO ";
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindOrderMaster()
        {
            string sql = "SELECT  ORDERDATE, ORDERID, ROUTECODE, ROUTENAME, CUSTOMERCODE, CUSTOMERNAME,ADDRESS,ORDERNO, QUANTITY ,CASE WHEN PACKQUANTITY=QUANTITY THEN '�ѷ���' ELSE 'δ����' END PACKAGE " +
                "FROM V_PALLETMASTER ORDER BY SORTNO";

            sql = "SELECT SORTNO, ORDERDATE, ORDERID, ROUTECODE, ROUTENAME, CUSTOMERCODE, CUSTOMERNAME, ADDRESS,ORDERNO,QUANTITY ,CASE WHEN PACKQUANTITY=QUANTITY THEN '�ѷ���' ELSE 'δ����' END PACKAGE ," +                    
            "CASE WHEN PRINTSTATUS=1 THEN '�Ѵ�ӡ' ELSE 'δ��ӡ' END PRINTSTATUS FROM V_PALLETMASTER ORDER BY SORTNO";

            sql = "SELECT ROW_NUMBER() OVER(ORDER BY MIN(SORTNO)) AS CUSTOMERNO,MIN(SORTNO) AS SORTNO ,ORDERDATE,ORDERID,ROUTECODE,ROUTENAME,CUSTOMERCODE,CUSTOMERNAME,ADDRESS,ORDERNO,SUM(QUANTITY+QUANTITY1) AS QUANTITY, ABNORMITY_QUANTITY," +
                    "CASE WHEN SUM(PACKQUANTITY)=SUM(QUANTITY) THEN '�ѷ���' ELSE 'δ����' END [PACKAGE], "+
                    "CASE WHEN PRINTSTATUS=1 THEN '�Ѵ�ӡ' ELSE 'δ��ӡ' END PRINTSTATUS "+
                    "FROM AS_SC_PALLETMASTER GROUP BY ORDERDATE,ROUTECODE,ROUTENAME,ORDERID,CUSTOMERCODE,CUSTOMERNAME,ADDRESS,ORDERNO,PRINTSTATUS,ABNORMITY_QUANTITY ORDER BY SORTNO";
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindOrderMaster(string routeCode)
        {
            string sql = string.Format("SELECT * FROM V_PALLETMASTER WHERE ROUTECODE='{0}' ORDER BY SORTNO", routeCode);
            return ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// ����·��ѯ����
        /// </summary>
        /// <returns></returns>
        public DataTable FindOutReport()
        {
            string sql = string.Format("SELECT DISTINCT(ROUTECODE) AS ��·����,ROUTENAME AS ��·����,AREANAME AS ��������,AREACODE AS �������,ORDERDATE AS ��������,BATCHNO AS ����,LINECODE AS �ּ���," +
                                        "(SELECT COUNT(DISTINCT PACKNO) FROM AS_SORT_PACKORDER WHERE EXPORTNO=1 AND ROUTECODE=E.ROUTECODE) AS '1�Ű�װ������'," +
                                        "(SELECT COUNT(DISTINCT PACKNO) FROM AS_SORT_PACKORDER WHERE EXPORTNO=2 AND ROUTECODE=E.ROUTECODE) AS '2�Ű�װ������'," +
                                        "(SELECT COUNT(DISTINCT PACKNO) FROM AS_SORT_PACKORDER WHERE EXPORTNO=0 AND ROUTECODE=E.ROUTECODE) AS '������'" +
                                        "FROM AS_SORT_PACKORDER E");
            return ExecuteQuery(sql).Tables[0];
        }


        //2011.3.14����˲�ѯ��װ����EXPORTNO,�����˰���װ���ŷ���
        public DataTable FindOrderDetail()
        {
            //string sql = "SELECT A.ORDERID, A.CIGARETTECODE,A.CIGARETTENAME, SUM(A.QUANTITY) QUANTITY,EXPORTNO " +
            //    "FROM AS_SC_ORDER A  LEFT JOIN dbo.AS_SC_CHANNELUSED B ON A.CHANNELCODE = B.CHANNELCODE GROUP BY ORDERID,EXPORTNO, A.SORTNO ,B.CHANNELNAME,A.CIGARETTECODE,A.CIGARETTENAME ORDER BY A.SORTNO DESC,B.CHANNELNAME";
            string sql = string.Format("SELECT A.SORTNO, EXPORTNO,ORDERID, B.CHANNELNAME, " +
                                " CASE B.CHANNELTYPE WHEN '2' THEN '��ʽ��' WHEN '5' THEN '��ʽ��' ELSE 'ͨ����' END CHANNELTYPE, " +
                                " A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY ," +
                                " CASE WHEN A.CHANNELGROUP=1 THEN 'A��' ELSE 'B��' END  CHANNELLINE " +
                                " FROM AS_SC_ORDER A " +
                                " LEFT JOIN AS_SC_CHANNELUSED B ON A.CHANNELCODE=B.CHANNELCODE " +
                                " ORDER BY A.CHANNELGROUP,A.SORTNO DESC,B.CHANNELADDRESS DESC");
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindOrderDetail(string orderID)
        {
            string sql = string.Format("SELECT A.CIGARETTENAME, SUM(A.QUANTITY) QUANTITY FROM AS_SC_ORDER A  LEFT JOIN dbo.AS_SC_CHANNELUSED B ON A.CHANNELCODE = B.CHANNELCODE WHERE ORDERID = '{0}' GROUP BY A.SORTNO ,B.CHANNELNAME,A.CIGARETTECODE,A.CIGARETTENAME ORDER BY A.SORTNO,B.CHANNELNAME,A.CHANNELGROUP,B.CHANNELADDRESS DESC", orderID);
            return ExecuteQuery(sql).Tables[0];
        }

        public void UpdateOrderPrintStatus(string orderID)
        {
            string sql = string.Format("UPDATE AS_SC_PALLETMASTER SET PRINTSTATUS = '1' WHERE ORDERID ='{0}' ",orderID);
            ExecuteNonQuery(sql);
        }

        public DataTable FindDetail(string orderID)
        {
            string sql = string.Format("SELECT * FROM AS_SC_ORDER WHERE ORDERID='{0}' ORDER BY SORTNO,CHANNELCODE", orderID);
            return ExecuteQuery(sql).Tables[0];
        }
        public string FindMinSortNo()
        {
            string sql = "SELECT MIN(SORTNO) FROM V_PALLETMASTER WHERE PRINTSTATUS = '0' ";
            return ExecuteScalar(sql).ToString();
        }

        public DataTable FindMasterTable(string sortNo)
        {
            string sql = string.Format("SELECT * FROM V_PALLETMASTER WHERE SORTNO>0 AND SORTNO <= {0} AND PRINTSTATUS = '0'  ORDER BY SORTNO ", sortNo);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindNextMaster(string sortNo)
        {
            string sql = string.Format("SELECT TOP 1 * FROM V_PALLETMASTER WHERE SORTNO > {0} ORDER BY SORTNO ", sortNo);
            return ExecuteQuery(sql).Tables[0];
        }

        //2011.3.21��Ӳ�ѯ��װ�����ݣ������Ų�ѯ��
        public DataTable FindPOneMaster(string packNo)
        {
            string sql = string.Format("SELECT PACKNO,CUSTOMERNAME,SUM(QUANTITY) AS QUANTITY FROM V_SORT_PACKORDER_PACKER_ONE WHERE PACKNO='{0}' GROUP BY PACKNO,CUSTOMERNAME",packNo);
            return ExecuteQuery(sql).Tables[0];
        }
        //2011.3.21��Ӳ�ѯ��װ��������ϸ�������Ų�ѯ��
        public DataTable FindPOneDetail(string packNo)
        {
            string sql = string.Format("SELECT CIGARETTENAME,QUANTITY FROM V_SORT_PACKORDER_PACKER_ONE WHERE PACKNO='{0}'",packNo);
            return ExecuteQuery(sql).Tables[0];
        }

        //�޸ı�ʶ
        public void UpdatePrintStatusInOne(string orderId)
        {
            string sql = string.Format("UPDATE AS_SC_PALLETMASTER SET PRINTSTATUS = 1 WHERE SORTNO <= (SELECT MAX(SORTNO) FROM AS_SC_PALLETMASTER WHERE ORDERID = '{0}')", orderId);
            ExecuteNonQuery(sql);
        }
        //�޸����
        public void UpdatePrintStatusIsZero(string orderId)
        {
            string sql = string.Format("UPDATE AS_SC_PALLETMASTER SET PRINTSTATUS = 0 WHERE SORTNO >=(SELECT MIN(SORTNO) FROM AS_SC_PALLETMASTER WHERE ORDERID = '{0}')", orderId);
            ExecuteNonQuery(sql);
        }

        internal DataTable FindOrderIDAndOrderNoForCacheOrderQuery(int channelGroup, int sortNo)
        {
            string sql = "SELECT ORDERID,ORDERNO FROM AS_SC_ORDER WHERE  CHANNELGROUP = {0} AND SORTNO ={1}";
            return ExecuteQuery(string.Format(sql, channelGroup, sortNo)).Tables[0];
        }
        
        /// <summary>
        /// ��ȡ�ڶ��Ͱ�װ���Ķ�������Ϣ�������ţ��ͻ����ƣ�����
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderNo"></param>
        /// <param name="channelGroup"></param>
        /// <returns></returns>
        public DataTable GetOrderTitle(string orderId, int orderNo, int channelGroup)
        {
            string sql = "SELECT A.ORDERID,C.CUSTOMERNAME,A.QUANTITY "+
                                "FROM AS_SC_ORDER A "+
                                "LEFT JOIN AS_SC_CHANNELUSED B ON A.CHANNELCODE=B.CHANNELCODE "+
                                "LEFT JOIN AS_SC_PALLETMASTER C ON A.SORTNO = C.SORTNO AND A.ORDERID = C.ORDERID AND A.ORDERDATE = C.ORDERDATE "+
                                "WHERE A.ORDERID = '{0}' AND A.ORDERNO = '{1}' AND A.CHANNELGROUP ='{2}'  " +
                                "ORDER BY A.SORTNO DESC,B.CHANNELADDRESS DESC";
            return ExecuteQuery(string.Format(sql,orderId,orderNo,channelGroup)).Tables[0];
        }

        /// <summary>
        /// ��ѯ�ڶ��Ͱ�װ��������Ϣ���������ƣ��̵����ͣ����飬����
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderNo"></param>
        /// <param name="channelGroup"></param>
        /// <returns></returns>
        public DataTable FindDetailForCacheOrderQuery(string orderId, int orderNo, int channelGroup)
        {
            //string sql = "SELECT A.SORTNO,A.ORDERID,C.CUSTOMERNAME,B.CHANNELNAME, " +
            //        " CASE B.CHANNELTYPE WHEN '2' THEN '��ʽ��' WHEN '5' THEN '��ʽ��' ELSE 'ͨ����' END CHANNELTYPE, " +
            //        " A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY ," +
            //        " CASE WHEN A.CHANNELGROUP=1 THEN 'A��' ELSE 'B��' END  CHANNELLINE, " +
            //        " ISNULL((SELECT TOP 1 PACKNO FROM AS_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 0 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO0," +
            //        " ISNULL((SELECT TOP 1 PACKNO FROM AS_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 1 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO1," +
            //        " ISNULL((SELECT TOP 1 PACKNO FROM AS_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 2 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO2 " +
            //        " FROM AS_SC_ORDER A " +
            //        " LEFT JOIN AS_SC_CHANNELUSED B ON A.CHANNELCODE=B.CHANNELCODE " +
            //        " LEFT JOIN AS_SC_PALLETMASTER C ON A.SORTNO = C.SORTNO AND A.ORDERID = C.ORDERID AND A.ORDERDATE = C.ORDERDATE  " +
            //        " WHERE A.ORDERID = {0} AND A.ORDERNO = {1} AND A.CHANNELGROUP = {2} ORDER BY A.SORTNO DESC,B.CHANNELADDRESS DESC";
            string sql = "SELECT CASE B.CHANNELTYPE WHEN '2' THEN '��ʽ��' WHEN '5' THEN '��ʽ��' ELSE 'ͨ����' END CHANNELTYPE," + 
                                "A.CIGARETTENAME, A.QUANTITY ," +
                                "CASE WHEN A.CHANNELGROUP=1 THEN 'A��' ELSE 'B��' END  CHANNELLINE " +
                                "FROM AS_SC_ORDER A " + 
                                "LEFT JOIN AS_SC_CHANNELUSED B ON A.CHANNELCODE=B.CHANNELCODE " +
                                "LEFT JOIN AS_SC_PALLETMASTER C ON A.SORTNO = C.SORTNO AND A.ORDERID = C.ORDERID AND A.ORDERDATE = C.ORDERDATE " + 
                                "WHERE A.ORDERID = {0} AND A.ORDERNO ={1} AND A.CHANNELGROUP ={2} ORDER BY A.SORTNO DESC,B.CHANNELADDRESS DESC";
            return ExecuteQuery(string.Format(sql, orderId, orderNo, channelGroup)).Tables[0];
        }

        public DataTable FindDetailForCacheOrderQuery(int exportNo, int sortNo, string packMode)
        {
            string sql = "";
            switch (packMode)
            {
                case "0":
                    sql = "SELECT A.SORTNO,A.ORDERID,C.CUSTOMERNAME,B.CHANNELNAME, " +
                            " CASE B.CHANNELTYPE WHEN '2' THEN '��ʽ��' WHEN '5' THEN '��ʽ��' ELSE 'ͨ����' END CHANNELTYPE, " +
                            " A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY ," +
                            " CASE WHEN A.CHANNELGROUP = 1 THEN 'A��' ELSE 'B��' END  CHANNELLINE, " +
                            " ISNULL((SELECT TOP 1 PACKNO FROM AS_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 0 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO0," +
                            " ISNULL((SELECT TOP 1 PACKNO FROM AS_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 1 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO1," +
                            " ISNULL((SELECT TOP 1 PACKNO FROM AS_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 2 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO2, " +
                            "  CASE WHEN " +
                            " 	    A.CHANNELGROUP = 1" +
                            "  THEN" +
                            " 	    CASE WHEN " +
                            " 	    (" +
                            " 	    SELECT ISNULL(MAX(SORTNO),0) FROM AS_SC_ORDER " +
                            " 	    WHERE ORDERID = A.ORDERID AND ORDERNO = A.ORDERNO AND CHANNELGROUP = A.CHANNELGROUP" +
                            " 	    ) = (" +
                            " 	    SELECT ISNULL(MAX(SORTNO),0) FROM AS_SC_PALLETMASTER " +
                            " 	    WHERE ORDERID= A.ORDERID AND QUANTITY != 0" +
                            "      )" +
                            " 	    THEN 10000 " +
                            " 	    ELSE A.ORDERNO " +
                            "      END " +
                            "  ELSE" +
                            "      CASE WHEN " +
                            " 	    (" +
                            " 	    SELECT ISNULL(MAX(SORTNO),0) FROM AS_SC_ORDER" +
                            " 	    WHERE ORDERID = A.ORDERID AND ORDERNO = A.ORDERNO AND CHANNELGROUP = A.CHANNELGROUP" +
                            " 	    ) = (" +
                            " 	    SELECT ISNULL(MAX(SORTNO),0) FROM AS_SC_PALLETMASTER" +
                            " 	    WHERE ORDERID= A.ORDERID AND QUANTITY1 != 0" +
                            "      )" +
                            " 	    THEN 10000 " +
                            " 	    ELSE A.ORDERNO " +
                            "      END " +
                            "  END" +
                            "  ORDERNO_PACKNO" +
                            " FROM AS_SC_ORDER A " +
                            " LEFT JOIN AS_SC_CHANNELUSED B ON A.CHANNELCODE=B.CHANNELCODE " +
                            " LEFT JOIN AS_SC_PALLETMASTER C ON A.SORTNO = C.SORTNO AND A.ORDERID = C.ORDERID AND A.ORDERDATE = C.ORDERDATE  " +
                            " WHERE A.EXPORTNO = {0} AND A.SORTNO <= {1}  ORDER BY C.CUSTOMERSORTNO DESC,ORDERNO_PACKNO DESC,A.CHANNELGROUP,A.SORTNO DESC,B.CHANNELADDRESS DESC";
                    sql = string.Format(sql, exportNo, sortNo);
                    break;
                case "1":
                    sql = "SELECT A.SORTNO,A.ORDERID,C.CUSTOMERNAME,B.CHANNELNAME, " +
                            " CASE B.CHANNELTYPE WHEN '2' THEN '��ʽ��' WHEN '5' THEN '��ʽ��' ELSE 'ͨ����' END CHANNELTYPE, " +
                            " A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY ," +
                            " CASE WHEN A.CHANNELGROUP = 1 THEN 'A��' ELSE 'B��' END  CHANNELLINE, " +
                            " ISNULL((SELECT TOP 1 PACKNO FROM AS_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 0 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO0," +
                            " ISNULL((SELECT TOP 1 PACKNO FROM AS_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 1 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO1," +
                            " ISNULL((SELECT TOP 1 PACKNO FROM AS_SORT_PACKORDER WHERE SORTNO = A.SORTNO AND EXPORTNO = 2 AND CHANNELGROUP = A.CHANNELGROUP),0) AS PACKNO2," +
                            "  CASE WHEN " +
                            " 	    A.CHANNELGROUP = 1" +
                            "  THEN" +
                            " 	    CASE WHEN " +
                            " 	    (" +
                            " 	    SELECT ISNULL(MAX(SORTNO),0) FROM AS_SC_ORDER " +
                            " 	    WHERE ORDERID = A.ORDERID AND ORDERNO = A.ORDERNO AND CHANNELGROUP = A.CHANNELGROUP" +
                            " 	    ) = (" +
                            " 	    SELECT ISNULL(MAX(SORTNO),0) FROM AS_SC_PALLETMASTER " +
                            " 	    WHERE ORDERID= A.ORDERID AND QUANTITY != 0" +
                            "      )" +
                            " 	    THEN 10000 " +
                            " 	    ELSE A.ORDERNO " +
                            "      END " +
                            "  ELSE" +
                            "      CASE WHEN " +
                            " 	    (" +
                            " 	    SELECT ISNULL(MAX(SORTNO),0) FROM AS_SC_ORDER" +
                            " 	    WHERE ORDERID = A.ORDERID AND ORDERNO = A.ORDERNO AND CHANNELGROUP = A.CHANNELGROUP" +
                            " 	    ) = (" +
                            " 	    SELECT ISNULL(MAX(SORTNO),0) FROM AS_SC_PALLETMASTER" +
                            " 	    WHERE ORDERID= A.ORDERID AND QUANTITY1 != 0" +
                            "      )" +
                            " 	    THEN 10000 " +
                            " 	    ELSE A.ORDERNO " +
                            "      END " +
                            "  END" +
                            "  ORDERNO_PACKNO" +
                            " FROM AS_SC_ORDER A " +
                            " LEFT JOIN AS_SC_CHANNELUSED B ON A.CHANNELCODE=B.CHANNELCODE " +
                            " LEFT JOIN AS_SC_PALLETMASTER C ON A.SORTNO = C.SORTNO AND A.ORDERID = C.ORDERID AND A.ORDERDATE = C.ORDERDATE  " +
                            " WHERE A.SORTNO <= {0}  ORDER BY C.CUSTOMERSORTNO DESC,ORDERNO_PACKNO DESC,A.CHANNELGROUP,A.SORTNO DESC,B.CHANNELADDRESS DESC";
                    sql = string.Format(sql, sortNo);
                    break;
                default:
                    return (new DataTable());
            }
            return ExecuteQuery(sql).Tables[0];
        }

    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using THOK.Util;

namespace THOK.AS.Sorting.Dao
{
    public class ChannelDao: BaseDao
    {

        public DataTable FindChannel()
        {
            string sql = "SELECT CHANNELCODE, CHANNELNAME, "+
                            " CASE CHANNELTYPE WHEN '2' THEN '��ʽ��' WHEN '5' THEN '��ʽ��' ELSE 'ͨ����' END CHANNELTYPE, " +
                            " LINECODE, CIGARETTECODE, CIGARETTENAME, QUANTITY "+
                            " FROM AS_SC_CHANNELUSED ORDER BY CHANNELNAME";
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// �����̵������ѯ�̵����ͺ��̵����
        /// </summary>
        /// <param name="channelcode">�̵�����</param>
        /// <returns>�����̵����ͺ��̵����</returns>
        public DataTable FindChannelCode(string channelcode)
        {
            string sql = string.Format("SELECT CHANNELADDRESS,CHANNELTYPE,CHANNELGROUP FROM AS_SC_CHANNELUSED WHERE CHANNELCODE='{0}'", channelcode);
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// ������ˮ�Ų�ѯ�̵���Ϣ
        /// </summary>
        /// <param name="sortNo">��ˮ��</param>
        /// <param name="channelGroup">A�߻���B��</param>
        /// <returns>���ز�ѯ�����̵���Ϣ��</returns>
        public DataTable FindChannelQuantity(string sortNo, string channelGroup)
        {
            string sql = string.Format("SELECT *, REMAINQUANTITY / 50 BOXQUANTITY, REMAINQUANTITY % 50 ITEMQUANTITY "+
                                        " FROM (SELECT A.CHANNELNAME, A.LEDGROUP, A.LEDNO, "+
                                                " CASE CHANNELTYPE WHEN '2' THEN '��ʽ��' WHEN '5' THEN '��ʽ��' ELSE 'ͨ����' END CHANNELTYPE," +
                                                " A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY,ISNULL(B.QUANTITY,0) SORTQUANTITY,"+
                                                " A.QUANTITY - ISNULL(B.QUANTITY,0) REMAINQUANTITY,"+
                                                " A.LED_X,A.LED_Y,A.LED_WIDTH,A.LED_HEIGHT,A.CHANNELADDRESS,A.CHANNELGROUP "+
                                                " FROM AS_SC_CHANNELUSED A " +
                                                " LEFT JOIN(SELECT CHANNELCODE, SUM(QUANTITY) QUANTITY "+
                                                            " FROM AS_SC_ORDER WHERE SORTNO <= '{0}' GROUP BY CHANNELCODE) B " +
                                                            " ON A.CHANNELCODE = B.CHANNELCODE) C  "+
                                        " WHERE CHANNELGROUP = {1} ORDER BY CHANNELNAME ",sortNo,channelGroup=="A"?"1":"2");
            return ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// ʵʱ��ѯ�ּ��̵�����ʣ�ľ�������
        /// </summary>
        /// <param name="sortNo">�ּ���ˮ��</param>
        /// <param name="channelGroup">�̵����</param>
        /// <returns>���ز�ѯ���ּ��̵���Ϣ��</returns>
        public DataTable FindChannelRealtimeQuantity(string sortNo, string channelGroup)
        {
            string sql = string.Format(@"SELECT C.*,   
                                             (QUANTITY - SORTQUANTITY) AS NOSORTQUANTITY,            
                                             ( ISNULL(D.OUTQUANTITY,0) + HANDLESUPPLYQUANTITY) ALLQUANTITY,
                                             ((ISNULL(D.OUTQUANTITY,0) + HANDLESUPPLYQUANTITY) - SORTQUANTITY) AS REMAINQUANTITY,                                             
                                             ((ISNULL(D.OUTQUANTITY,0) + HANDLESUPPLYQUANTITY) - SORTQUANTITY) / 50 BOXQUANTITY, 
                                             ((ISNULL(D.OUTQUANTITY,0) + HANDLESUPPLYQUANTITY) - SORTQUANTITY) % 50 ITEMQUANTITY		                                     
                                        FROM (SELECT A.LINECODE,A.CHANNELGROUP,A.CHANNELCODE,A.CHANNELNAME,A.CHANNELADDRESS,
                                                CASE CHANNELTYPE WHEN '2' THEN '��ʽ��' WHEN '5' THEN '��ʽ��' ELSE 'ͨ����' END CHANNELTYPE,
                                                CASE CHANNELTYPE WHEN '2' THEN A.QUANTITY % 50 WHEN '5' THEN A.QUANTITY ELSE A.QUANTITY % 50 END HANDLESUPPLYQUANTITY,
                                                A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY,ISNULL(B.QUANTITY,0) SORTQUANTITY                                                
                                              FROM AS_SC_CHANNELUSED A 
                                                LEFT JOIN(SELECT CHANNELCODE, SUM(QUANTITY) QUANTITY FROM AS_SC_ORDER 
                                                            WHERE SORTNO <= '{0}' GROUP BY CHANNELCODE) B 
                                                ON A.CHANNELCODE = B.CHANNELCODE) C  
                                        LEFT JOIN AS_CHANNEL_CHECK D ON C.CHANNELCODE=D.CHANNELCODE
                                        WHERE C.CHANNELGROUP = '{1}' ORDER BY C.CHANNELNAME", sortNo, channelGroup);
            return ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        ///  �����̵���ź��̵����Ͳ�ѯ���̵���Ϣ��
        /// </summary>
        /// <param name="channelType">�̵�����</param>
        /// <param name="channelGroup">�̵����</param>
        /// <returns>�����̵���Ϣ��</returns>
        public DataTable FindEmptyChannel(string channelCode,string channelType,int channelGroup)
        {
            string sql = string.Format("SELECT CHANNELCODE, "+
                                        " CHANNELNAME + ' ' + CASE CHANNELTYPE WHEN '2' THEN '��ʽ��' WHEN '5' THEN '��ʽ��'  ELSE 'ͨ����' END CHANNELNAME " +
                                        " FROM AS_SC_CHANNELUSED "+
                                        " WHERE CHANNELTYPE IN ('{0}') AND CHANNELTYPE != '5' AND CHANNELGROUP = {1} AND CHANNELCODE != {2} " +
                                        " ORDER BY CHANNELNAME", channelType, channelGroup, channelCode);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindChannel(string channelCode)
        {
            string sql = string.Format("SELECT * FROM AS_SC_CHANNELUSED WHERE CHANNELCODE='{0}'", channelCode);
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// ��ѯ�ղ��̵���Ϣ
        /// </summary>
        /// <returns>���ؿղ���Ϣ��</returns>
        public DataTable FindLastSortNo(int channelgroup)
        {
            string sql = string.Format("SELECT CHANNELADDRESS,CHANNELCODE,SORTNO FROM AS_SC_CHANNELUSED WHERE CHANNELGROUP={0} ORDER BY CHANNELGROUP,CHANNELCODE",channelgroup);
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
            ExecuteQuery("TRUNCATE TABLE AS_SORT_STATUS");
            ExecuteQuery("TRUNCATE TABLE AS_SORT_EFFICIENCY");
            ExecuteQuery("TRUNCATE TABLE AS_SC_CHANNELUSED");
            BatchInsert(channelTable, "AS_SC_CHANNELUSED");
        }

        /// <summary>
        /// ���������ϵͳ�ĳ������ݲ���ּ���ϵͳ���Ѿ���2��ɨ�������Ѿ���3��ɨ�����ľ��̣�
        /// </summary>
        /// <param name="channelCheckTable"></param>
        public void InsertChannelCheck(DataTable channelCheckTable)
        {
            ExecuteQuery("TRUNCATE TABLE AS_CHANNEL_CHECK");
            BatchInsert(channelCheckTable, "AS_CHANNEL_CHECK");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using THOK.Util;

namespace THOK.AS.Stocking.Dao
{
    public class CheckDao:BaseDao
    {
        /// <summary>
        /// 查询已经入库未出库的卷烟的相关信息
        /// </summary>
        /// <returns></returns>
        public DataTable FindChannelRemain()
        {
            string sql = @"SELECT CHANNELCODE,CIGARETTECODE,CIGARETTENAME,BARCODE,COUNT(CIGARETTENAME) AS QUANTITY,
                            GETDATE() AS CHECKTIME
                            FROM dbo.AS_STOCK_IN
                            WHERE  STATE=1 AND STOCKOUTID=0
                            GROUP BY CHANNELCODE,CIGARETTENAME,CIGARETTECODE,BARCODE,STATE
                            ORDER BY CHANNELCODE";
            return ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 查询仓储数据库的出库信息
        /// </summary>
        /// <returns></returns>
        public DataTable FindOutData()
        {
            string sql = "SELECT * FROM V_CHECK_STOCK";
            return ExecuteQuery(sql).Tables[0];
        }

        internal DataTable FindStockTempQuantity()
        {
            string sql = @"SELECT A.CIGARETTECODE, A.CIGARETTENAME,
                            ISNULL(B.QUANTITY,0)*50 - ISNULL(C.QUANTITY,0)*50 - ISNULL(D.QUANTITY,0)*50 - A.QUANTITY AS QUANTITY	                                     
                            FROM (SELECT CIGARETTECODE,CIGARETTENAME,SUM(QUANTITY) QUANTITY
	                              FROM (SELECT LINECODE,CHANNELCODE,CIGARETTECODE,CIGARETTENAME,QUANTITY % 50 QUANTITY                                                
		                                FROM AS_SC_CHANNELUSED WHERE CHANNELTYPE !='5' 
		                                GROUP BY LINECODE,CHANNELCODE,CIGARETTECODE,CIGARETTENAME,QUANTITY
	                             ) T GROUP BY CIGARETTECODE,CIGARETTENAME ) A  
                            LEFT JOIN AS_WMS_STOCK_OUT B ON  A.CIGARETTECODE = B.PRODUCTCODE
                            LEFT JOIN (SELECT A.CIGARETTECODE,A.CIGARETTENAME,COUNT(A.CIGARETTECODE) AS QUANTITY
			                            FROM AS_STOCK_OUT A
			                            WHERE  (A.SCAN_STATE_02 = 1) OR (A.SCAN_STATE_03 = 1)
			                            GROUP BY A.CIGARETTENAME,A.CIGARETTECODE,A.SCAN_STATE_02,A.SCAN_STATE_03 ) C 
	                            ON A.CIGARETTECODE = C.CIGARETTECODE
                            LEFT JOIN (SELECT CIGARETTECODE,CIGARETTENAME,COUNT(CIGARETTECODE) AS QUANTITY
			                            FROM AS_STOCK_IN
			                            WHERE  STATE=1 AND STOCKOUTID=0
			                            GROUP BY CIGARETTECODE,CIGARETTENAME,STATE) D 
	                            ON A.CIGARETTECODE = D.CIGARETTECODE
                            ORDER BY A.CIGARETTECODE";
            return ExecuteQuery(sql).Tables[0];
        }
    }
}

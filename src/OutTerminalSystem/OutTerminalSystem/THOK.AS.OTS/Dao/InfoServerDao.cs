using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using THOK.Util;
namespace THOK.AS.OTS.Dao
{
    public  class InfoServerDao:BaseDao
    {
        public DataTable Find(string orderDate, string batchNo)
        {
            string sql = @"SELECT ROW_NUMBER() OVER (ORDER BY D.SORTID,C.SORTID,A.ORDERID ,B.CIGARETTECODE) AS SORTNO, 
                          A.ORDERID,C.CUSTOMERCODE,C.CUSTOMERNAME, 
                          B.CIGARETTECODE,B.CIGARETTENAME,B.QUANTITY,C.ADDRESS AS ADDRESS,  
                          ISNULL(Z.BATCHNO_ONEPRO,Z.BATCHNO) BATCHNO, 
                          ROW_NUMBER() OVER (ORDER BY D.SORTID,C.SORTID,A.ORDERID ,B.CIGARETTECODE) ORDERNO, 
                          D.ROUTECODE,D.ROUTENAME,F.AREANAME,
                          CONVERT(NVARCHAR(10),A.ORDERDATE,120) ORDERDATE, 
                          CONVERT(NVARCHAR(10),GETDATE(),120) SCDATE, 
                          '1' AS LINECODE,'1' AS ZZBS,F.ORDERNO AS CUSNO 
                          FROM AS_I_ORDERMASTER A  
                          LEFT JOIN AS_I_ORDERDETAIL B  
                          ON A.ORDERID = B.ORDERID  
                          LEFT JOIN AS_BI_BATCH Z 
                          ON A.ORDERDATE = Z.ORDERDATE AND A.BATCHNO = Z.BATCHNO  
                          LEFT JOIN AS_BI_CUSTOMER C  
                          ON A.CUSTOMERCODE = C.CUSTOMERCODE 
                          LEFT JOIN AS_BI_ROUTE D 
                          ON A.ROUTECODE = D.ROUTECODE  
                          LEFT JOIN AS_BI_CIGARETTE E 
                          ON B.CIGARETTECODE = E.CIGARETTECODE 
						 LEFT JOIN (SELECT DISTINCT(CUSTOMERCODE),ORDERNO,AREANAME FROM dbo.AS_SC_PALLETMASTER WHERE ORDERDATE='{0}' AND BATCHNO='{1}') AS F
						  ON A.CUSTOMERCODE=F.CUSTOMERCODE
                          WHERE A.ORDERDATE='{0}' AND A.BATCHNO='{1}' AND B.QUANTITY IS NOT NULL AND E.ISABNORMITY = '1' 
                          ORDER BY LINECODE,SORTNO,CIGARETTECODE ";
            return ExecuteQuery(string.Format(sql,orderDate,batchNo)).Tables[0];

        }
    }
}

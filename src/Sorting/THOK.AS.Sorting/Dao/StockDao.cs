using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using THOK.Util;

namespace THOK.AS.Sorting.Dao
{
    public class StockDao:BaseDao
    {
        /// <summary>
        ///  ��ѯ�������ϵͳ�Ѿ��������ѹ�2��ɨ������3��ɨ�����ľ��̵���Ϣ
        /// </summary>
        /// <param name="Linecode">�ּ��ߴ���</param>
        /// <returns></returns>
        public DataTable FindOutData(string Linecode)
        {
            string sql = string.Format("SELECT * FROM V_STOCKOUT WHERE LINECODE='{0}'",Linecode);
            return ExecuteQuery(sql).Tables[0];
        }
    }
}

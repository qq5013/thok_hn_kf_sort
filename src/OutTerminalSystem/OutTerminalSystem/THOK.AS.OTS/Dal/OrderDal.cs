using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using THOK.AS.OTS.Dao;
using THOK.Util;

namespace THOK.AS.OTS.Dal
{
    public  class OrderDal
    {
        public DataTable GetOrderDetailForCacheOrderQuery(int channelGroup, int sortNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                DataTable table = orderDao.FindOrderIDAndOrderNoForCacheOrderQuery(channelGroup, sortNo);

                if (table.Rows.Count != 0)
                {
                    string orderId = table.Rows[0]["ORDERID"].ToString();
                    int orderNo = Convert.ToInt32(table.Rows[0]["ORDERNO"]);
                    return orderDao.FindDetailForCacheOrderQuery(orderId, orderNo, channelGroup);
                }
                return (new DataTable());
            }
        }

        public DataTable GetOrderOrderTitleForCacheOrderQuery(int channelGroup, int sortNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                DataTable table = orderDao.FindOrderIDAndOrderNoForCacheOrderQuery(channelGroup, sortNo);

                if (table.Rows.Count != 0)
                {
                    string orderId = table.Rows[0]["ORDERID"].ToString();
                    int orderNo = Convert.ToInt32(table.Rows[0]["ORDERNO"]);
                    return orderDao.GetOrderTitle(orderId, orderNo, channelGroup);
                }
                return (new DataTable());
            }
        }
    }
}

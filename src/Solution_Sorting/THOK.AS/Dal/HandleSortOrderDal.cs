using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.AS.Dao;
using THOK.Util;

namespace THOK.AS.Dal
{
    public class HandleSortOrderDal
    {
        public int GetCount(string filter)
        {
            int count = 0;
            using (PersistentManager pm = new PersistentManager())
            {
                HandleSortOrderDao handleSortOrderDao = new HandleSortOrderDao();
                count = handleSortOrderDao.FindCount(filter);
            }
            return count;
        }

        public DataTable GetAll(int pageIndex, int PagingSize, string filter)
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                HandleSortOrderDao handleSortOrderDao = new HandleSortOrderDao();
                table = handleSortOrderDao.FindAll((pageIndex - 1) * PagingSize, PagingSize, filter);
            }
            return table;
        }

        public void Insert(string orderDate, string orderId)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                HandleSortOrderDao handleSortOrderDao = new HandleSortOrderDao();
                handleSortOrderDao.InsertEntity(orderDate, orderId);
            }
        }

        public void Save(string orderDate, string oldOrderId, string newOrderId)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                HandleSortOrderDao handleSortOrderDao = new HandleSortOrderDao();
                handleSortOrderDao.UpdateEntity(orderDate, oldOrderId, newOrderId);
            }
        }

        public void Delete(string orderDate, string orderId)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                HandleSortOrderDao handleSortOrderDao = new HandleSortOrderDao();
                handleSortOrderDao.DeleteEntity(orderDate, orderId);
            }
        }
    }
}

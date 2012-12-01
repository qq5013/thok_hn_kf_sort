using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.HSS.Dal
{
    public class HandSupplyDal
    {
        public DataTable GetAllHandleSupply()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                HandSupplyDao handSupplyDao = new HandSupplyDao();
                return handSupplyDao.GetAllHandleSupply();
            }
        }

        public DataTable GetAllCigarette()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                HandSupplyDao handSupplyDao = new HandSupplyDao();
                return handSupplyDao.GetAllCigarette();
            }
        }

        public DataTable GetHandSupplyBySupplyBatch(int supplyBatch)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                HandSupplyDao handSupplyDao = new HandSupplyDao();
                return handSupplyDao.GetHandSupplyBySupplyBatch(supplyBatch);
            }
        }

        public int GetLastSupplyBatchNo()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                HandSupplyDao handSupplyDao = new HandSupplyDao();
                return handSupplyDao.GetLastSupplyBatchNo();
            }
        }

        public int GetCurrentSupplyBatch()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                HandSupplyDao handSupplyDao = new HandSupplyDao();
                return handSupplyDao.GetCurrentSupplyBatch();
            }
        }

        public int GetHandSupplyCountBySupplyBatch(int supplyBatch)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                HandSupplyDao handSupplyDao = new HandSupplyDao();
                return handSupplyDao.GetHandSupplyCountBySupplyBatch(supplyBatch);
            }
        }
    }
}

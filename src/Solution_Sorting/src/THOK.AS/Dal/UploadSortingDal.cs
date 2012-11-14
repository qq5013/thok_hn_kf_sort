using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;
using THOK.AS.Dao;

namespace THOK.AS.Dal
{
    public class UploadSortingDal
    {
        /// <summary>
        /// ����ʱ���ѯ����������
        /// </summary>
        /// <param name="orderDate"></param>
        /// <returns></returns>
        public DataTable FindOrderMaster(string orderDate)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UploadSortingDao udao = new UploadSortingDao();
                return udao.FindOrderMaster(orderDate);
            }
        }

        /// <summary>
        /// ����ʱ���ѯϸ��������
        /// </summary>
        /// <param name="orderDate"></param>
        /// <returns></returns>
        public DataTable FindOrderDetail(string orderDate)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UploadSortingDao udao = new UploadSortingDao();
                return udao.FindOrderDetail(orderDate);
            }
        }

        /// <summary>
        /// ��ѯ�ּ�������
        /// </summary>
        /// <returns></returns>
        public DataTable FindSortingIdps()
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UploadSortingDao udao = new UploadSortingDao();
                return udao.FindSortingIdps();
            }
        }

        /// <summary>
        /// ��¼�ϱ���Ϣ
        /// </summary>
        /// <param name="userName">�ϱ����û�</param>
        /// <param name="uploadIp">�ϱ���ip</param>
        /// <param name="orderDate">�ϱ��Ķ���ʱ��</param>
        public void InsertUploadRecord(string userName,string uploadIp,string orderDate)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UploadSortingDao udao = new UploadSortingDao();
                string sql = @"INSERT INTO AS_RECORD_UPLOAD(UPLOADORDERDATE,UPLOADDATE,UPLOADIP,UPLOADUSERNAME,UPLOADSTATE)VALUES('{0}','{1}','{2}','{3}','{4}')";
                sql = string.Format(sql, orderDate, DateTime.Now.ToString("yyyyMMddHHmmss"), uploadIp, userName, "1");
                udao.setData(sql);
            }
        }

        /// <summary>
        /// ��ȡ�ϱ�״̬
        /// </summary>
        /// <param name="orderDate"></param>
        /// <returns></returns>
        public DataTable GetUploadState(string orderDate)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UploadSortingDao udao = new UploadSortingDao();
                orderDate = Convert.ToDateTime(orderDate).ToString("yyyy-MM-dd");
                return udao.GetUploadState(orderDate);
            }
        }


        /// <summary>
        /// �ϱ����Ҿַּ���������
        /// </summary>
        /// <param name="masterTable"></param>
        public void UploadSortingOrderMaster(DataTable masterTable)
        {
            //�ϱ��ּ���������
            using (PersistentManager pm = new PersistentManager("DB2Connection"))
            {
                UploadSortingDao udao = new UploadSortingDao();
                udao.SetPersistentManager(pm);
                try
                {
                    pm.BeginTransaction();
                    foreach (DataRow row in masterTable.Rows)
                    {
                        string sql = string.Format("INSERT INTO DWV_IORD_ORDER(ORDER_ID,ORG_CODE,SALE_REG_CODE,ORDER_DATE,ORDER_TYPE," +
                            "CUST_CODE,CUST_NAME,QUANTITY_SUM,AMOUNT_SUM,DETAIL_NUM,ISACTIVE,UPDATE_DATE,IS_IMPORT)VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}',{7},{8},{9},'{10}','{11}','{12}')",
                            row["ORDER_ID"], row["ORG_CODE"], row["SALE_REG_CODE"], Convert.ToDateTime(row["ORDER_DATE"]).ToString("yyyyMMdd"), row["ORDER_TYPE"], row["CUST_CODE"], row["CUST_NAME"],
                            row["QUANTITY_SUM"], row["AMOUNT_SUM"], row["DETAIL_NUM"], row["ISACTIVE"], DateTime.Now.ToString("yyyyMMddHHmmss"), row["IS_IMPORT"]);
                        udao.setData(sql);
                    }
                    pm.Commit();
                }
                catch (Exception exp)
                {
                    pm.Rollback();
                    throw exp;
                }
                
            }
        }

        //�޸ı����ϱ�״̬
        public void UpdateOrderMasterState(string orderDate)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UploadSortingDao udao = new UploadSortingDao();
                string sql = string.Format("UPDATE AS_I_ORDERMASTER SET IS_IMPORT=1 WHERE ORDERDATE='{0}'", orderDate);
                udao.setData(sql);
            }
        }

        /// <summary>
        /// �ϱ����Ҿַּ�����ϸ��
        /// </summary>
        /// <param name="detailTable"></param>
        public void UploadSortingOrderDetail(DataTable detailTable)
        {
            //�ϱ��ּ�ϸ������
            using (PersistentManager pm = new PersistentManager("DB2Connection"))
            {
                UploadSortingDao udao = new UploadSortingDao();
                udao.SetPersistentManager(pm);
                try
                {
                    pm.BeginTransaction();
                    foreach (DataRow row in detailTable.Rows)
                    {
                        string sql = string.Format("INSERT INTO DWV_IORD_ORDER_DETAIL(ORDER_DETAIL_ID,ORDER_ID,BRAND_CODE,BRAND_NAME,BRAND_UNIT_NAME," +
                            "QTY_DEMAND,QUANTITY,PRICE,AMOUNT,IS_IMPORT)VALUES('{0}','{1}','{2}','{3}','{4}',{5},{6},{7},{8},'{9}')",
                            row["ORDER_DETAIL_ID"], row["ORDER_ID"], row["BRAND_CODE"], row["BRAND_NAME"], row["BRAND_UNIT_NAME"], row["QTY_DEMAND"], row["QUANTITY"],
                            row["PRICE"], row["AMOUNT"], row["IS_IMPORT"]);
                        udao.setData(sql);
                    }
                    pm.Commit();
                }
                catch (Exception exp)
                {
                    pm.Rollback();
                    throw exp;
                }
            }
        }

        //�޸ı����ϱ�״̬
        public void UpdateOrderDeatilState(string orderDate)
        {
            using (PersistentManager persistentManager = new PersistentManager())
            {
                UploadSortingDao udao = new UploadSortingDao();
                string sql = string.Format("UPDATE AS_I_ORDERDETAIL SET IS_IMPORT=1 WHERE ORDERDATE='{0}'", orderDate);
                udao.setData(sql);
            }
        }

        /// <summary>
        /// �ϱ����Ҿַּ������ݱ�
        /// </summary>
        /// <param name="idpsTable"></param>
        public void UploadSortingIdps(DataTable idpsTable)
        {
            foreach (DataRow row in idpsTable.Rows)
            {  //�ϱ��ּ�ϸ������
                using (PersistentManager persistentManager = new PersistentManager("DB2Connection"))
                {
                    UploadSortingDao udao = new UploadSortingDao();
                    udao.SetPersistentManager(persistentManager);
                    string sql = string.Format("INSERT INTO DWV_IDPS_SORTING(SORTING_CODE,SORTING_NAME,SORTING_TYPE,ISACTIVE,UPDATE_DATE," +
                    "IS_IMPORT)VALUES('{0}','{1}','{2}','{3}','{4}','{5}')",
                    row["LINECODE"], row["LINENAME"], row["LINETYPE"], row["STATUS"], DateTime.Now.ToString("yyyyMMddHHmmss"), row["IS_IMPORT"]);
                    udao.setData(sql);
                }
                //�޸ı����ϱ�״̬
                using (PersistentManager persistentManager = new PersistentManager())
                {
                    UploadSortingDao udao = new UploadSortingDao();
                    string sql = string.Format("UPDATE AS_BI_LINEINFO SET IS_IMPORT=1 WHERE LINECODE='{0}'", row["LINECODE"]);
                    udao.setData(sql);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using THOK.AS.Stocking.Dao;
using System.Data;
using System.Windows.Forms;
using THOK.Util;

namespace THOK.AS.Stocking.Process
{
    class StockInRequestProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  �������
             * 
             *  stateItem.ItemName ��
             *      Init - ��ʼ����
             *      FirstBatch - ���ɵ�һ�������������
             *      StockInRequest - �������������������
             * 
             *  stateItem.State ������ - ����ľ��̱��롣        
            */
            string cigaretteCode = "";
            try
            {
                switch (stateItem.ItemName)
                {
                    case "Init":
                        break;
                    case "FirstBatch":
                        AddFirstBatch();
                        break;
                    case "StockInRequest":                        
                        cigaretteCode = Convert.ToString(stateItem.State);
                        StockInRequest(cigaretteCode);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Logger.Error("������������������ɴ���ʧ�ܣ�ԭ��" + e.Message);
            }
        }

        private void AddFirstBatch()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                SupplyDao supplyDao = new SupplyDao();              
                ChannelDao channelDao = new ChannelDao();

                DataTable cigaretteTable = supplyDao.FindCigarette();
                if (cigaretteTable.Rows.Count !=0)
                {
                    foreach (DataRow row in cigaretteTable.Rows)
                    {
                        DataTable channelTable = channelDao.FindChannelForCigaretteCode(row["CigaretteCode"].ToString());
                        int stockRemainQuantity = Convert.ToInt32(channelTable.Rows[0]["REMAINQUANTITY"]);

                        if (Convert.ToInt32(row["Quantity"]) + stockRemainQuantity >= 30)
                        {
                            StockInRequest(row["CigaretteCode"].ToString(), 30, stockRemainQuantity);
                        }
                        else if (Convert.ToInt32(row["Quantity"]) + stockRemainQuantity > 0)
                        {
                            StockInRequest(row["CigaretteCode"].ToString(), Convert.ToInt32(row["Quantity"]) + stockRemainQuantity, stockRemainQuantity);
                        }
                    }
                    Logger.Info("������һ�����������ɹ�");
                    WriteToProcess("LEDProcess", "Refresh_01", null);
                }
            }
        }

        private void StockInRequest(string cigaretteCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockInBatchDao stockInBatchDao = new StockInBatchDao();
                SupplyDao supplyDao = new SupplyDao();
                ChannelDao channelDao = new ChannelDao();

                DataTable stockInBatchTable = stockInBatchDao.FindStockInBatch(cigaretteCode);

                DataTable channelTable = channelDao.FindChannelForCigaretteCode(cigaretteCode);
                int stockRemainQuantity = Convert.ToInt32(channelTable.Rows[0]["REMAINQUANTITY"]);

                DataTable cigaretteTable = supplyDao.FindCigarette(cigaretteCode, stockRemainQuantity.ToString());

                if (stockInBatchTable.Rows.Count == 0 && cigaretteTable.Rows.Count != 0 )
                {
                    DataRow row = cigaretteTable.Rows[0];

                    if (Convert.ToInt32(row["Quantity"]) >= 30)
                    {
                        StockInRequest(row["CigaretteCode"].ToString(), 30,0);
                        Logger.Info(row["CigaretteName"].ToString() + "�����������ɹ�");
                    }
                    else if (Convert.ToInt32(row["Quantity"]) > 0)
                    {
                        StockInRequest(row["CigaretteCode"].ToString(), Convert.ToInt32(row["Quantity"]),0);
                        Logger.Info(row["CigaretteName"].ToString() + "�����������ɹ�");
                    }
                    
                    WriteToProcess("LEDProcess", "Refresh_01", null);
                }
            }
        }

        private void StockInRequest(string cigaretteCode, int quantity, int stockRemainQuantity)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                StockInBatchDao stockInBatchDao = new StockInBatchDao();
                StockInDao stockInDao = new StockInDao();
                ChannelDao channelDao = new ChannelDao();
                
                stockInBatchDao.SetPersistentManager(pm);
                stockInDao.SetPersistentManager(pm);
                channelDao.SetPersistentManager(pm);
                
                pm.BeginTransaction();
                try
                {
                    DataTable cigaretteTable = channelDao.FindChannelForCigaretteCode(cigaretteCode);

                    if (cigaretteTable.Rows.Count != 0)
                    {
                        DataRow row = cigaretteTable.Rows[0];
                        bool isStockIn = row["ISSTOCKIN"].ToString() == "1" ? true : false;

                        int batchNo = stockInBatchDao.FindMaxBatchNo() + 1;
                        stockInBatchDao.InsertBatch(batchNo, row["CHANNELCODE"].ToString(), cigaretteCode, row["CIGARETTENAME"].ToString(), quantity, isStockIn ? stockRemainQuantity : 0);
                        
                        int stockInID = stockInDao.FindMaxInID();
                        for (int i = 1; i <= quantity; i++)
                        {
                            stockInID = stockInID + 1;
                            stockInDao.Insert(stockInID, batchNo, row["CHANNELCODE"].ToString(), cigaretteCode, row["CIGARETTENAME"].ToString(), row["BARCODE"].ToString(), (isStockIn && stockRemainQuantity-- > 0) ? "1" : "0");
                        }

                        pm.Commit();
                        
                        try
                        {
                            using (PersistentManager pmWES = new PersistentManager("WESConnection"))
                            {
                                StockInBatchDao stockInBatchDaoWES = new StockInBatchDao();
                                stockInBatchDaoWES.SetPersistentManager(pmWES);
                                stockInBatchDaoWES.InsertBatch(batchNo, row["CHANNELCODE"].ToString(), cigaretteCode, row["CIGARETTENAME"].ToString(), quantity, isStockIn ? stockRemainQuantity : 0);
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Error("�ϴ����ƻ�ʧ�ܣ����飺"+ e.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    pm.Rollback();
                    Logger.Error("�������ƻ�ʧ�ܣ����飺" + ex.Message);
                }
            }
        }
    }
}

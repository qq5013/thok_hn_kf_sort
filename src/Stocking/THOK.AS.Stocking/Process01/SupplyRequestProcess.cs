using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.MCP;
using THOK.Util;
using THOK.AS.Stocking.Dao;

namespace THOK.AS.Stocking.Process
{
    public class SupplyRequestProcess: AbstractProcess
    {
        [Serializable]
        public class OrderInfo
        {
            public string orderDate = "";
            public string batchNo = "";
        }
        private OrderInfo orderInfo = new OrderInfo();
        private bool isOk = false;
        private string orderDate = "";
        private string batchNo = "";

        public override void Initialize(Context context)
        {
            base.Initialize(context);
            orderInfo = Util.SerializableUtil.Deserialize<OrderInfo>(true, @".\orderInfo.sl");
        }
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                bool needNotify = false;
                switch (stateItem.ItemName)
                {
                    case "OrderInfo":
                        object o = stateItem.State;
                        if (o is Array)
                        {
                            Array array = (Array)o;
                            if (array.Length == 2)
                            {
                                string[] orderinfo = new string[2];
                                array.CopyTo(orderinfo, 0);
                                orderInfo.orderDate = orderinfo[0];
                                orderInfo.batchNo = orderinfo[1];
                            }
                        }

                        Util.SerializableUtil.Serialize(true, @".\orderInfo.sl", orderInfo);
                        return;
                        break;
                    case "FirstBatch":
                        needNotify = AddFirstSupply();
                        break;
                    case "SupplyRequest":
                        Dictionary<string, string> parameter = (Dictionary<string, string>)stateItem.State;

                        if ((!isOk || orderDate != orderInfo.orderDate || batchNo != orderInfo.batchNo) && (parameter["OrderDate"] != orderInfo.orderDate || parameter["BatchNo"] != orderInfo.batchNo))
                        {
                            if (parameter["OrderDate"] != "")
                            {
                                Logger.Error("��������������ʧ�ܣ�ԭ�򣺷ּ𶩵������벹���������β�����");
                            }
                            return;
                        }

                        isOk = true;
                        orderDate = orderInfo.orderDate;
                        batchNo = orderInfo.batchNo;

                        needNotify = AddSupply(parameter["LineCode"], parameter["SortNo"], parameter["ChannelGroup"]);
                        break;
                }
                if (needNotify)
                    dispatcher.WriteToProcess("DataRequestProcess", "SupplyRequest", 1);
            }
            catch (Exception e)
            {
                Logger.Error("�����������ɴ���ʧ�ܣ�ԭ��" + e.Message);
            }
        }

        private bool AddFirstSupply()
        {
            bool result = false;
            try
            {                
                using (PersistentManager pm = new PersistentManager())
                {
                    StockOutBatchDao batchDao = new StockOutBatchDao();
                    SupplyDao supplyDao = new SupplyDao();
                    StockOutDao outDao = new StockOutDao();

                    DataTable supplyTable = supplyDao.FindFirstSupply();

                    if (supplyTable.Rows.Count != 0)
                    {
                        try
                        {
                            pm.BeginTransaction();

                            int batchNo = batchDao.FindMaxBatchNo() + 1;
                            batchDao.InsertBatch(batchNo, "00", "0", supplyTable.Rows.Count,"0");

                            int outID = outDao.FindMaxOutID();
                            outDao.Insert(outID, batchNo, supplyTable);

                            pm.Commit();
                            result = true;

                            Logger.Info("������һ���γ�������ɹ�");
                            WriteToProcess("LEDProcess", "Refresh_02", null);
                        }
                        catch (Exception e)
                        {
                            Logger.Error("���ɵ�һ���γ�������ʧ�ܣ�ԭ��" + e.Message);
                            pm.Rollback();
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                Logger.Error("��һ�����������ɴ���ʧ�ܣ�ԭ��" + ee.Message);
            }

            return result;
        }        

        private bool AddSupply(string lineCode, string sortNo,string channelGroup)
        {            
            bool result = false;
            try
            {
                using (PersistentManager pm = new PersistentManager())
                {
                    StockOutBatchDao batchDao = new StockOutBatchDao();
                    SupplyDao supplyDao = new SupplyDao();
                    StockOutDao outDao = new StockOutDao();

                    DataTable table = supplyDao.FindSupplyBatch(lineCode, sortNo,channelGroup);

                    foreach (DataRow row in table.Rows)
                    {
                        string tmpSortNo = row["SORTNO"].ToString();

                        DataTable supplyTable = supplyDao.FindSupply(lineCode, tmpSortNo,channelGroup);

                        if (supplyTable.Rows.Count != 0)
                        {
                            Logger.Info(string.Format("�յ���������������'{0}'����ˮ��'{1}'", lineCode, tmpSortNo));
                            try
                            {
                                pm.BeginTransaction();

                                int batchNo = batchDao.FindMaxBatchNo() + 1;
                                batchDao.InsertBatch(batchNo, lineCode, tmpSortNo, supplyTable.Rows.Count,channelGroup);

                                int outID = outDao.FindMaxOutID();
                                outDao.Insert(outID, batchNo, supplyTable);

                                pm.Commit();
                                result = true;

                                Logger.Info("���ɳ�������ɹ�");
                                WriteToProcess("LEDProcess", "Refresh_02", null);
                            }
                            catch (Exception e)
                            {
                                Logger.Error("���ɳ�������ʧ�ܣ�ԭ��" + e.Message);
                                pm.Rollback();
                            }
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                Logger.Error("�������������ɴ���ʧ�ܣ�ԭ��" + ee.Message);
            }
            return result;
        }
    }
}

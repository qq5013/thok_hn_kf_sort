using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using THOK.MCP;
using THOK.AS.Sorting.Dao;
using THOK.Util;
using THOK.AS.Sorting.Util;

namespace THOK.AS.Sorting.Process
{
    public class OrderRequestProcess: AbstractProcess
    {
        private MessageUtil messageUtil = null;

        public override void Initialize(Context context)
        {
            try
            {
                base.Initialize(context);
                messageUtil = new MessageUtil(context.Attributes);
            }
            catch (Exception e)
            {
                Logger.Error("OrderRequestProcess ��ʼ��ʧ�ܣ�ԭ��" + e.Message);
            }

        }

        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                string channelGroup = "";

                switch (stateItem.ItemName)
                {
                    case "OrderRequestA":
                        channelGroup = "A";
                        break;
                    case "OrderRequestB":
                        channelGroup = "B";
                        break;
                    default:
                        return;
                }
              
                //��ȡA�߶�����ϸ��д��PLC
                object o = ObjectUtil.GetObject(stateItem.State);

                if (o != null && o.ToString() == "1")
                {
                    using (PersistentManager pm = new PersistentManager())
                    {
                        OrderDao orderDao = new OrderDao();
                        try
                        {
                            //Ҫ���ݷּ������ѯ����
                            DataTable masterTable = orderDao.FindSortMaster(channelGroup);

                            if (masterTable.Rows.Count != 0)
                            {
                                //��ǰ��ˮ��
                                string sortNo = masterTable.Rows[0]["SORTNO"].ToString();
                                //��ȡ�������ID����ˮ�ţ��ж��Ƿ񻻻�
                                string maxSortNo = orderDao.FindMaxSortNoFromMasterByOrderID(masterTable.Rows[0]["ORDERID"].ToString(), channelGroup);
                                //��ѯ������ϸ                                
                                DataTable detailTable = orderDao.FindSortDetail(sortNo, channelGroup);
                                //��ѯ���ּ����������ˮ�ţ��ж��Ƿ����
                                string endSortNo = orderDao.FindEndSortNoForChannelGroup(channelGroup);
                                int exportNo = Convert.ToInt32(masterTable.Rows[0]["EXPORTNO" + (channelGroup == "A" ? "" : "1")]);

                                int[] orderData = new int[37];
                                if (detailTable.Rows.Count > 0)
                                {
                                    for (int i = 0; i < detailTable.Rows.Count; i++)
                                    {
                                        orderData[Convert.ToInt32(detailTable.Rows[i]["CHANNELADDRESS"]) - 1] = Convert.ToInt32(detailTable.Rows[i]["QUANTITY"]);
                                    }
                                }

                                //�ּ���ˮ��
                                orderData[30] = Convert.ToInt32(sortNo);
                                //��������
                                orderData[31] = Convert.ToInt32(masterTable.Rows[0]["QUANTITY" + (channelGroup == "A" ? "" : "1")]);
                                //�Ƿ񻻻�
                                orderData[32] = maxSortNo == sortNo ? 1 : 0;
                                //�ͻ��ּ���ˮ��
                                orderData[33] = Convert.ToInt32(masterTable.Rows[0]["CUSTOMERSORTNO"].ToString());
                                //��װ����
                                orderData[34] = exportNo;
                                //���ּ���·�Ƿ����
                                orderData[35] = endSortNo == sortNo ? 1 : 0;
                                //��ɱ�־
                                orderData[36] = 1;
                                if (WriteToService("SortPLC", "OrderData" + channelGroup, orderData))
                                {
                                    orderDao.UpdateOrderStatus(sortNo, "1", channelGroup);
                                    Logger.Info(string.Format(channelGroup + " �� д�������ݳɹ�,�ּ𶩵���[{0}]��", sortNo));
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Error(string.Format(channelGroup + " �� д��������ʧ�ܣ�ԭ��{0}�� {1}", e.Message, "OrderRequestProcess.cs �кţ�100��"));
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                Logger.Error(string.Format("�ּ𶩵��������ʧ�ܣ�ԭ��{0}�� {1}", ee.Message, "OrderRequestProcess.cs �кţ�108��"));
            }
        }
    }
}

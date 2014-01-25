using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.AS.Schedule;
using THOK.AS.Dao;
using THOK.Util;
using THOK.Optimize;
using System.Threading;

namespace THOK.AS.Schedule
{
    public class AutoSchedule
    {
        public event ScheduleEventHandler OnSchedule = null;

        /// <summary>
        /// ���ָ����������
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void ClearSchedule(string orderDate, int batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                //AS_BI_BATCH
                BatchDao batchDao = new BatchDao();
                batchDao.UpdateExecuter("0", "0", orderDate, batchNo);
                batchDao.UpdateIsValid(orderDate, batchNo, "0");

                //AS_SC_CHANNELUSED
                ChannelScheduleDao csDao = new ChannelScheduleDao();
                csDao.DeleteSchedule(orderDate, batchNo);

                //AS_SC_LINE
                LineScheduleDao lsDao = new LineScheduleDao();
                lsDao.DeleteSchedule(orderDate, batchNo);

                //AS_SC_PALLETMASTER,AS_SC_ORDER
                OrderScheduleDao osDao = new OrderScheduleDao();
                osDao.DeleteSchedule(orderDate, batchNo);

                //AS_I_ORDERDETAIL��AS_I_ORDERMASTER
                OrderDao orderDao = new OrderDao();
                orderDao.DeleteOrder(orderDate, batchNo);

                //AS_SC_STOCKMIXCHANNEL
                StockChannelDao scDao = new StockChannelDao();
                scDao.DeleteSchedule(orderDate, batchNo);

                //AS_SC_SUPPLY
                SupplyDao supplyDao = new SupplyDao();
                supplyDao.DeleteSchedule(orderDate, batchNo);

                //AS_SC_HANDLESUPPLY
                HandleSupplyDao handleSupplyDao = new HandleSupplyDao();
                handleSupplyDao.DeleteHandleSupply(orderDate, batchNo);

            }
        }

        public void DownloadData(string orderDate, int batchNo, string historyOrderDate)
        {
            ClearSchedule(orderDate, batchNo);
            DateTime dtHistoryOrderDate = DateTime.Parse(historyOrderDate);
            DateTime dtOrderDate = DateTime.Parse(orderDate);
            using (PersistentManager pm = new PersistentManager())
            {
                pm.BeginTransaction();
                try
                {
                    LineScheduleDao lsDao = new LineScheduleDao();
                    OrderDao orderDao = new OrderDao();

                    //��ѯ���Ż�������·���Խ����ų���
                    string routes = lsDao.FindRoutes(orderDate);

                    //���ض�������
                    DataTable masterTable = orderDao.FindHistoryOrderMaster(dtOrderDate, batchNo, routes, dtHistoryOrderDate);
                    orderDao.BatchInsertMaster(masterTable);
                    if (OnSchedule != null)
                        OnSchedule(this, new ScheduleEventArgs(1, "�������������", 13, 14));

                    //���ض�����ϸ
                    DataTable detailTable = orderDao.FindHistoryOrderDetail(dtOrderDate, batchNo, routes, dtHistoryOrderDate);
                    orderDao.BatchInsertDetail(detailTable);
                    if (OnSchedule != null)
                        OnSchedule(this, new ScheduleEventArgs(1, "�������������", 14, 14));

                    pm.Commit();
                }
                catch (Exception e)
                {
                    pm.Rollback();
                    throw e;
                }                
            }
        }

        /// <summary>
        /// �����ʷ���ݣ����������ݡ�
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void DownloadData(string orderDate, int batchNo)
        {
            try
            {
                DateTime dtOrder = DateTime.Parse(orderDate);
                string historyDate = dtOrder.AddDays(-7).ToShortDateString();
                using (PersistentManager pm = new PersistentManager())
                {
                    BatchDao batchDao = new BatchDao();
                    using (PersistentManager ssPM = new PersistentManager("OuterConnection"))
                    {
                        SalesSystemDao ssDao = new SalesSystemDao();
                        ssDao.SetPersistentManager(ssPM);
                        try
                        {
                            pm.BeginTransaction();

                            //AS_BI_BATCH
                            batchDao.DeleteHistory(historyDate);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 1, 14));

                            //AS_SC_CHANNELUSED
                            ChannelScheduleDao csDao = new ChannelScheduleDao();
                            csDao.DeleteHistory(historyDate);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 2, 14));

                            //AS_SC_LINE
                            LineScheduleDao lsDao = new LineScheduleDao();
                            lsDao.DeleteHistory(historyDate);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 3, 14));

                            //AS_SC_PALLETMASTER ,AS_SC_ORDER
                            OrderScheduleDao osDao = new OrderScheduleDao();
                            osDao.DeleteHistory(historyDate);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 4, 14));

                            //AS_I_ORDERMASTER,AS_I_ORDERDETAIL,
                            OrderDao orderDao = new OrderDao();
                            orderDao.DeleteHistory(historyDate);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 5, 14));

                            //AS_SC_STOCKMIXCHANNEL
                            StockChannelDao scDao = new StockChannelDao();
                            scDao.DeleteHistory(historyDate);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 6, 14));

                            //AS_SC_SUPPLY
                            SupplyDao supplyDao = new SupplyDao();
                            supplyDao.DeleteHistory(historyDate);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 7, 14));

                            //AS_SC_HANDLESUPPLY
                            HandleSupplyDao handleSupplyDao = new HandleSupplyDao();
                            handleSupplyDao.DeleteHistory(historyDate);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 8, 14));

                            ClearSchedule(orderDate, batchNo);

                            //////////////////////////////////////////////////////////////////////////

                            //���������
                            AreaDao areaDao = new AreaDao();
                            DataTable areaTable = ssDao.FindArea();
                            areaDao.SynchronizeArea(areaTable);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 9, 14));

                            //����������·��
                            RouteDao routeDao = new RouteDao();
                            DataTable routeTable = ssDao.FindRoute();
                            routeDao.SynchronizeRoute(routeTable);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 10, 14));

                            //���ؿͻ���
                            CustomerDao customerDao = new CustomerDao();
                            DataTable customerTable = ssDao.FindCustomer(dtOrder);
                            customerDao.SynchronizeCustomer(customerTable);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 11, 14));

                            //���ؾ��̱� ����ͬ��
                            CigaretteDao cigaretteDao = new CigaretteDao();
                            DataTable cigaretteTable = ssDao.FindCigarette(dtOrder);
                            cigaretteDao.SynchronizeCigarette(cigaretteTable);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 12, 14));

                            //��ѯ���Ż�������·���Խ����ų���
                            string routes = lsDao.FindRoutes(orderDate);

                            //���ض�������
                            DataTable masterTable = ssDao.FindOrderMaster(dtOrder, batchNo, routes);
                            orderDao.BatchInsertMaster(masterTable);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 13, 14));

                            //���ض�����ϸ
                            DataTable detailTable = ssDao.FindOrderDetail(dtOrder, batchNo, routes);
                            orderDao.BatchInsertDetail(detailTable);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 14, 14));

                            pm.Commit();
                        }
                        catch (Exception e)
                        {
                            pm.Rollback();
                            throw e;
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                if (OnSchedule != null)
                    OnSchedule(this, new ScheduleEventArgs(OptimizeStatus.ERROR, ee.Message));
                throw ee;
            }
        }

        /// <summary>
        /// ��ʼ�Ż�
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void GenSchedule(string orderDate, int batchNo)
        {
            try
            {
                //�ּ����Ż�
                GenLineSchedule(orderDate, batchNo);                

                //�ּ����̵��Ż�
                GenChannelSchedule(orderDate, batchNo);
                
                //��ֶ���
                GenSplitOrder(orderDate, batchNo);

                //�����Ż�
                GenOrderSchedule(orderDate, batchNo);

                //�����̵��Ż�
                GenStockChannelSchedule(orderDate, batchNo);

                //�����Ż�
                GenSupplySchedule(orderDate, batchNo);

                //�ֹ������Ż�
                GenHandleSupplySchedule(orderDate, batchNo);

                //����Ϊ���Ż�
                using (PersistentManager pm = new PersistentManager())
                {
                    BatchDao batchDao = new BatchDao();
                    if (!batchDao.CheckOrder(orderDate, batchNo))
                    {
                        throw new Exception("�Ż����̳������飡");
                    }
                    batchDao.SelectBalanceIntoHistory(orderDate, batchNo);
                    batchDao.UpdateIsValid(orderDate, batchNo, "1");
                }

                if (OnSchedule != null)
                    OnSchedule(this, new ScheduleEventArgs(OptimizeStatus.COMPLETE));
                
            }
            catch (Exception e)
            {
                ClearSchedule(orderDate, batchNo);
                if (OnSchedule != null)
                    OnSchedule(this, new ScheduleEventArgs(OptimizeStatus.ERROR,"[" + e.TargetSite + "]\n" + e.Message));
            }
        }



        #region  �Ż�����

        /// <summary>
        /// �������Ż�  2008-12-11�޸� 
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void GenLineSchedule(string orderDate, int batchNo)
        {
            using (THOK.Util.PersistentManager pm = new THOK.Util.PersistentManager())
            {
                LineInfoDao lineDao = new LineInfoDao();
                OrderDao detailDao = new OrderDao();
                LineScheduleDao lineScDao = new LineScheduleDao();
                THOK.Optimize.LineOptimize lineSchedule = new THOK.Optimize.LineOptimize();

                DataTable routeTable = detailDao.FindRouteQuantity(orderDate, batchNo).Tables[0];
                DataTable lineTable = lineDao.GetAvailabeLine("2").Tables[0];
                DataTable scLineTable = new DataTable();
                if (lineTable.Rows.Count > 0)
                {
                    scLineTable = lineSchedule.Optimize(routeTable, lineTable, orderDate, batchNo);
                    lineScDao.SaveLineSchedule(scLineTable);
                }
                else
                    throw new Exception("û�п��õķּ��ߣ�");


                routeTable = detailDao.FindRouteQuantity(orderDate, batchNo).Tables[0];
                lineTable = lineDao.GetAvailabeLine("3").Tables[0];
                if (lineTable.Rows.Count > 0)
                {
                    scLineTable = lineSchedule.Optimize(routeTable, lineTable, orderDate, batchNo);
                    lineScDao.SaveLineSchedule(scLineTable);
                }

                if (OnSchedule != null)
                    OnSchedule(this, new ScheduleEventArgs(2, "�������Ż�", 1, 1));
            }     
        }

        /// <summary>
        /// �������̵��Ż�
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void GenChannelSchedule(string orderDate, int batchNo)
        {
            using (THOK.Util.PersistentManager pm = new THOK.Util.PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                OrderDao detailDao = new OrderDao();                
                LineDeviceDao deviceDao = new LineDeviceDao();
                ChannelOptimize channelSchedule = new ChannelOptimize();

                THOK.AS.Dao.SysParameterDao parameterDao = new SysParameterDao();
                Dictionary<string, string> parameter = parameterDao.FindParameters();

                THOK.AS.Dao.LineScheduleDao lineDao = new LineScheduleDao();
                DataTable lineTable = lineDao.FindAllLine(orderDate, batchNo).Tables[0];    
            
                int currentCount = 0;
                int totalCount = lineTable.Rows.Count;

                foreach (DataRow lineRow in lineTable.Rows)
                {
                    string lineCode = lineRow["LINECODE"].ToString();

                    //��ѯ�ּ����̵���
                    DataTable channelTable = channelDao.FindAvailableChannel(lineCode).Tables[0];
                    //��ѯ�ּ����豸������            
                    DataTable deviceTable = deviceDao.FindLineDevice(lineCode).Tables[0];

                    //ͨ���������ּ���Ʒ�Ʊ���һ�£���ʽ�������ּ�������Ҫһ��
                    //ȡ���ж���Ʒ�Ƽ�������
                    DataTable orderTable = detailDao.FindAllCigaretteQuantity(orderDate, batchNo).Tables[0];
                    //ȡÿ���߶���Ʒ�Ƽ�������
                    DataTable lineOrderTable = detailDao.FindLineCigaretteQuantity(orderDate, batchNo, lineCode).Tables[0];

                    channelSchedule.Optimize(orderTable, lineOrderTable, channelTable, deviceTable, parameter);

                    channelDao.SaveChannelSchedule(channelTable, orderDate, batchNo);

                    if (OnSchedule != null)
                        OnSchedule(this, new ScheduleEventArgs(3, "�����Ż�" + lineRow["LINECODE"].ToString() + "�ּ����̵�", ++currentCount, totalCount));
                }

                LineInfoDao lineDao1 = new LineInfoDao();
                DataTable lineTable1 = lineDao1.GetAvailabeLine("3").Tables[0];
                currentCount = 0;
                totalCount = lineTable1.Rows.Count;

                foreach (DataRow lineRow in lineTable1.Rows)
                {
                    string lineCode = lineRow["LINECODE"].ToString();

                    //��ѯ�ּ����̵���
                    DataTable channelTable = channelDao.FindAvailableChannel(lineCode).Tables[0];
                    channelDao.SaveChannelSchedule(channelTable, orderDate, batchNo);

                    if (OnSchedule != null)
                        OnSchedule(this, new ScheduleEventArgs(3, "�����Ż�" + lineRow["LINECODE"].ToString() + "�ּ����̵�", ++currentCount, totalCount));
                }
            }
        }

        /// <summary>
        /// �������2010-07-05
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void GenSplitOrder(string orderDate, int batchNo)
        {
            using (THOK.Util.PersistentManager pm = new THOK.Util.PersistentManager())
            {

                OrderDao orderDao = new OrderDao();
                ChannelDao channelDao = new ChannelDao();
                OrderScheduleDao orderScheduleDao = new OrderScheduleDao();
                LineScheduleDao lineDao = new LineScheduleDao();

                OrderSplitOptimize orderSchedule = new OrderSplitOptimize();

                THOK.AS.Dao.SysParameterDao parameterDao = new SysParameterDao();
                Dictionary<string, string> parameter = parameterDao.FindParameters();

                //��ѯ�ּ��߱�
                DataTable lineTable = lineDao.FindAllLine(orderDate, batchNo).Tables[0];

                //������ʱ��
                orderScheduleDao.ClearSplitOrder();

                LineInfoDao lineDao1 = new LineInfoDao();
                DataTable lineTable1 = lineDao1.GetAvailabeLine("3").Tables[0];
                bool isUseWholePiecesSortLine = lineTable1.Rows.Count > 0;

                foreach (DataRow lineRow in lineTable.Rows)
                {
                    string lineCode = lineRow["LINECODE"].ToString();

                    //��ѯ�̵���Ϣ��
                    DataTable channelTable = channelDao.FindChannelSchedule(orderDate, batchNo, lineCode).Tables[0];
                    //��ѯ��������
                    DataTable masterTable = orderDao.FindOrderMaster(orderDate, batchNo, lineCode).Tables[0];                    
                    //��ѯ������ϸ��
                    DataTable orderTable = orderDao.FindOrderDetail(orderDate, batchNo, lineCode).Tables[0];

                    DataTable orderDetail = null;
                    HashTableHandle hashTableHandle = new HashTableHandle(orderTable);

                    int sortNo = 1;
                    int currentCount = 0;
                    int totalCount = masterTable.Rows.Count;
                    orderSchedule.moveToMixChannelProducts.Clear();

                    foreach (DataRow masterRow in masterTable.Rows)
                    {
                        DataRow[] orderRows = null;

                        orderDetail = hashTableHandle.Select("ORDERID", masterRow["ORDERID"]);
                        orderRows = orderDetail.Select(string.Format("ORDERID = '{0}'", masterRow["ORDERID"]), "QUANTITY");

                        DataSet ds = orderSchedule.Optimize(masterRow, orderRows, channelTable, lineCode, ref sortNo, parameter,isUseWholePiecesSortLine);
                        orderScheduleDao.SaveSplitOrder(ds);

                        if (OnSchedule != null)
                            OnSchedule(this, new ScheduleEventArgs(4, "�����Ż�" + lineRow["LINECODE"].ToString() + "�ּ��߶���", ++currentCount, totalCount));
                    }

                    channelDao.UpdateQuantity(channelTable, Convert.ToBoolean(parameter["IsUseBalance"]));
                }
            }
        }

        /// <summary>
        /// �����Ż�2010-07-07
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void GenOrderSchedule(string orderDate, int batchNo)
        {
            using (THOK.Util.PersistentManager pm = new THOK.Util.PersistentManager())
            {

                OrderDao orderDao = new OrderDao();
                ChannelDao channelDao = new ChannelDao();
                OrderScheduleDao orderScheduleDao = new OrderScheduleDao();                
                SupplyDao supplyDao = new SupplyDao();
                OrderOptimize orderSchedule = new OrderOptimize();

                LineScheduleDao lineDao = new LineScheduleDao();
                DataTable lineTable = lineDao.FindAllLine(orderDate, batchNo).Tables[0];

                foreach (DataRow lineRow in lineTable.Rows)
                {
                    string lineCode = lineRow["LINECODE"].ToString();

                    //��ѯ��������
                    DataTable masterTable = orderDao.FindTmpMaster(orderDate, batchNo, lineCode);
                    //��ѯ������ϸ��
                    DataTable orderTable = orderDao.FindTmpDetail(orderDate, batchNo, lineCode);
                    //��ѯ�ּ��̵���
                    DataTable channelTable = channelDao.FindChannelSchedule(orderDate, batchNo, lineCode).Tables[0];

                    int sortNo = 1;
                    int currentCount = 0;
                    int totalCount = masterTable.Rows.Count;

                    foreach (DataRow masterRow in masterTable.Rows)
                    {
                        DataRow[] orderRows = null;

                        //��ѯ��ǰ������ϸ
                        orderRows = orderTable.Select(string.Format("SORTNO = '{0}'", masterRow["SORTNO"]), "CHANNELGROUP, CHANNELCODE");
                        DataSet ds = orderSchedule.Optimize(masterRow, orderRows, channelTable, ref sortNo);

                        orderScheduleDao.SaveOrder(ds);
                        supplyDao.InsertSupply(ds.Tables["SUPPLY"], lineCode, orderDate, batchNo);

                        if (OnSchedule != null)
                            OnSchedule(this, new ScheduleEventArgs(5, "�����Ż�" + lineRow["LINECODE"].ToString() + "�ּ��߶���", ++currentCount, totalCount));
                    }

                    channelDao.Update(channelTable);
                }

                LineInfoDao lineDao1 = new LineInfoDao();
                DataTable lineTable1 = lineDao1.GetAvailabeLine("3").Tables[0];

                foreach (DataRow lineRow in lineTable1.Rows)
                {
                    string lineCode = lineRow["LINECODE"].ToString();

                    //��ѯ�̵���Ϣ��
                    DataTable channelTable = channelDao.FindChannelSchedule(orderDate, batchNo, lineCode).Tables[0];
                    //��ѯ��������
                    DataTable masterTable = orderDao.FindOrderMaster(orderDate, batchNo, lineCode).Tables[0];
                    //��ѯ������ϸ��
                    DataTable orderTable = orderDao.FindOrderDetail(orderDate, batchNo, lineCode).Tables[0];

                    int sortNo = 1;
                    int currentCount = 0;
                    int totalCount = masterTable.Rows.Count;

                    foreach (DataRow masterRow in masterTable.Rows)
                    {
                        DataRow[] orderRows = null;
                        //��ѯ��ǰ������ϸ
                        orderRows = orderTable.Select(string.Format("ORDERID = '{0}'", masterRow["ORDERID"]), "CHANNELCODE");
                        DataSet ds = orderSchedule.OptimizeUseWholePiecesSortLine(masterRow, orderRows, channelTable, ref sortNo);
                        orderScheduleDao.SaveOrder(ds);

                        if (OnSchedule != null)
                            OnSchedule(this, new ScheduleEventArgs(5, "�����Ż�" + lineRow["LINECODE"].ToString() + "�ּ��߶���", ++currentCount, totalCount));
                    }

                    channelDao.UpdateQuantity(channelTable,false);
                }
            }
        }

        /// <summary>
        /// �����̵��Ż�2010-07-08
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void GenStockChannelSchedule(string orderDate, int batchNo)
        {
            using (THOK.Util.PersistentManager pm = new THOK.Util.PersistentManager())
            {
                StockChannelDao schannelDao = new StockChannelDao();
                OrderDao orderDao = new OrderDao();

                SysParameterDao parameterDao = new SysParameterDao();
                Dictionary<string, string> parameter = parameterDao.FindParameters();
                
                //ÿ��ּ�����󱸻��̵��Ƿ�Ϊ��
                if (parameter["ClearStockChannel"] == "1")
                    schannelDao.ClearCigarette();

                //��ѯ�����̵���
                DataTable channelTable = schannelDao.FindChannel();
                //��ѯͨ��������������Ϣ��
                DataTable orderCTable = orderDao.FindCigaretteQuantityFromChannelUsed(orderDate, batchNo, "3");
                //��ѯ��ʽ������������Ϣ��Ӧ���ϻ���̵����⣩
                DataTable orderTTable = orderDao.FindCigaretteQuantityFromChannelUsed(orderDate, batchNo, "2");

                StockOptimize stockOptimize = new StockOptimize();
                DataTable mixTable = stockOptimize.Optimize(channelTable, orderCTable, orderTTable, orderDate, batchNo);

                schannelDao.UpdateChannel(channelTable);
                schannelDao.InsertStockChannelUsed(orderDate, batchNo,channelTable);
                schannelDao.InsertMixChannel(mixTable);

                if (OnSchedule != null)
                    OnSchedule(this, new ScheduleEventArgs(6, "�����̵��Ż�", 1, 1));
            }
        }

        /// <summary>
        /// �����Ż�2010-04-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void GenSupplySchedule(string orderDate, int batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                ChannelDao channelDao = new ChannelDao();
                LineScheduleDao lineDao = new LineScheduleDao();
                SupplyDao supplyDao = new SupplyDao();
                SupplyOptimize supplyOptimize = new SupplyOptimize();

                SysParameterDao parameterDao = new SysParameterDao();
                Dictionary<string, string> parameter = parameterDao.FindParameters();

                DataTable lineTable = lineDao.FindAllLine(orderDate, batchNo).Tables[0];

                int currentCount = 0;
                int totalCount = lineTable.Rows.Count;

                foreach (DataRow lineRow in lineTable.Rows)
                {
                    string lineCode = lineRow["LINECODE"].ToString();

                    int channelGroup = 1;
                    int channelType = 2;
                    int aheadCount = Convert.ToInt32(parameter[string.Format("SupplyAheadCount-{0}-{1}-{2}",lineCode,channelGroup,channelType)]);                   
                    supplyDao.AdjustSortNo(orderDate, batchNo, lineCode, channelGroup, channelType, aheadCount);

                    channelGroup = 1;
                    channelType = 3;
                    aheadCount = Convert.ToInt32(parameter[string.Format("SupplyAheadCount-{0}-{1}-{2}", lineCode, channelGroup, channelType)]);   
                    supplyDao.AdjustSortNo(orderDate, batchNo, lineCode, channelGroup, channelType, aheadCount);

                    channelGroup = 2;
                    channelType = 2;
                    aheadCount = Convert.ToInt32(parameter[string.Format("SupplyAheadCount-{0}-{1}-{2}", lineCode, channelGroup, channelType)]);       
                    supplyDao.AdjustSortNo(orderDate, batchNo, lineCode, channelGroup, channelType, aheadCount);

                    channelGroup = 2;
                    channelType = 3;
                    aheadCount = Convert.ToInt32(parameter[string.Format("SupplyAheadCount-{0}-{1}-{2}", lineCode, channelGroup, channelType)]);      
                    supplyDao.AdjustSortNo(orderDate, batchNo, lineCode, channelGroup, channelType, aheadCount);

                    DataTable channelTable = channelDao.FindChannelSchedule(orderDate, batchNo, lineCode).Tables[0];
                    DataTable supplyTable = supplyOptimize.Optimize(channelTable);
                    supplyDao.InsertSupply(supplyTable,false);

                    if (OnSchedule != null)
                        OnSchedule(this, new ScheduleEventArgs(7, "�����Ż�" + lineRow["LINECODE"].ToString() + "�����ƻ�", currentCount++, totalCount));
                }

                LineInfoDao lineDao1 = new LineInfoDao();
                DataTable lineTable1 = lineDao1.GetAvailabeLine("3").Tables[0];

                foreach (DataRow lineRow in lineTable1.Rows)
                {
                    string lineCode = lineRow["LINECODE"].ToString();

                    //��ѯ�̵���Ϣ��
                    DataTable channelTable = channelDao.FindChannelSchedule(orderDate, batchNo, lineCode).Tables[0];
                    //��ѯ��������
                    DataTable masterTable = orderDao.FindOrderMaster(orderDate, batchNo, lineCode).Tables[0];
                    //��ѯ������ϸ��
                    DataTable orderTable = orderDao.FindOrderDetail(orderDate, batchNo, lineCode).Tables[0];

                    int serialNo = 1;
                    currentCount = 0;
                    totalCount = masterTable.Rows.Count;

                    foreach (DataRow masterRow in masterTable.Rows)
                    {
                        DataRow[] orderRows = null;
                        //��ѯ��ǰ������ϸ
                        orderRows = orderTable.Select(string.Format("ORDERID = '{0}'", masterRow["ORDERID"]), "CHANNELCODE");
                        DataTable supplyTable = supplyOptimize.Optimize(channelTable, orderRows,ref serialNo);
                        supplyDao.InsertSupply(supplyTable,true);

                        if (OnSchedule != null)
                            OnSchedule(this, new ScheduleEventArgs(7, "�����Ż�" + lineRow["LINECODE"].ToString() + "�ּ��߶���������", ++currentCount, totalCount));
                    }
                }
            }
        }

        /// <summary>
        /// �ֹ������Ż�
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void GenHandleSupplySchedule(string orderDate, int batchNo) 
        {
            using (PersistentManager pm = new PersistentManager())
            {

                HandleSupplyOptimize handleSupplyOptimize = new THOK.Optimize.HandleSupplyOptimize();
                ScOrderDao scOrderDao = new ScOrderDao();
                ChannelDao channelDao = new ChannelDao();

                Dao.LineScheduleDao lineDao = new LineScheduleDao();
                DataTable lineTable = lineDao.FindAllLine(orderDate, batchNo).Tables[0];

                int currentCount = 0;
                int totalCount = lineTable.Rows.Count;

                foreach (DataRow lineRow in lineTable.Rows)
                {
                    string lineCode = lineRow["LINECODE"].ToString();

                    DataTable handSupplyOrders = scOrderDao.FindHandleSupplyOrder(orderDate, batchNo, lineCode);
                    DataTable multiBrandChannel = channelDao.FindMultiBrandChannel(lineCode);

                    AddColumnForChannelTable(multiBrandChannel, multiBrandChannel.Rows.Count);

                    //�����µ��ֹ�����������
                    DataTable newSupplyOrders = handleSupplyOptimize.Optimize(handSupplyOrders, multiBrandChannel);

                    //�����̵��ղ���ҵ��SortNo
                    channelDao.Update(multiBrandChannel,orderDate, batchNo);

                    //ɾ��sc_orderԭ�����ֹ���������
                    scOrderDao.DeleteOldSupplyOrders(orderDate, batchNo, lineCode);
                    //��sc_order�в����µ��ֹ���������
                    scOrderDao.InsertNewSupplyOrders(newSupplyOrders);


                    //��AS_SC_HANDLESUPPLY�в����µ��ֹ���������
                    scOrderDao.InsertHandSupplyOrders(newSupplyOrders);

                    if (OnSchedule != null)
                        OnSchedule(this, new ScheduleEventArgs(8, "�����Ż�" + lineRow["LINECODE"].ToString() + "�ּ����ֹ�������������", ++currentCount, totalCount));
                }
            }
        }

        private DataTable AddColumnForChannelTable(DataTable channel, int channelCount)
        {
            for (int i = 0; i < channelCount; i++)
            {
                channel.Rows[i]["QUANTITY"] = 0;
            }

            return channel;
        } 

        #endregion

    }
}
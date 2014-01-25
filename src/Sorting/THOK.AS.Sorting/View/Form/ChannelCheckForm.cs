using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.MCP;
using THOK.Util;
using THOK.AS.Sorting.Dao;
using THOK.ParamUtil;
using THOK.MCP.Config;

namespace THOK.AS.Sorting.View
{
    public partial class ChannelCheckForm : THOK.AF.View.ToolbarForm 
    {
        public ChannelCheckForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        /// <summary>
        /// �ּ��̵�ʵʱ�̵�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DownLoadData();
            ChannelGroupDialog channelGroupDialog = new ChannelGroupDialog();
            if (channelGroupDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (PersistentManager pm = new PersistentManager())
                    {
                        OrderDao orderDao = new OrderDao();
                        ChannelDao channelDao = new ChannelDao();
                        Report.ChannelCheckReport crp = new THOK.AS.Sorting.Report.ChannelCheckReport();
                        crp.SetDataSource(GetChannelRealtimeQuantity(channelGroupDialog.ChannelGroup));
                        this.crystalReportViewer1.ReportSource = crp;
                        Logger.Info("�ּ��̵�ʵʱ�̵�");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message);
                }
            }
        }

        private DataTable GetChannelRealtimeQuantity(string channelGroup)
        {
            try
            {
                int[] quantity = new int[30];
                object state = null;

                if (channelGroup == "1")
                {
                    state = this.mainFrame.Context.Services["SortPLC"].Read("CheckA");
                }
                else
                {
                    state = this.mainFrame.Context.Services["SortPLC"].Read("CheckB");
                }

                if (state is Array)
                {
                    Array array = (Array)state;
                    if (array.Length == 30)
                    {
                        array.CopyTo(quantity, 0);
                    }
                }

                using (PersistentManager pm = new PersistentManager())
                {
                    OrderDao orderDao = new OrderDao();
                    ChannelDao channelDao = new ChannelDao();

                    string sortNo = "";
                    DataTable channelTable = null;

                    sortNo = orderDao.FindMaxSortedMaster(channelGroup);
                    channelTable = channelDao.FindChannelRealtimeQuantity(sortNo, channelGroup);

                    foreach (DataRow row in channelTable.Rows)
                    {
                        row["SORTQUANTITY"] = Convert.ToInt32(row["SORTQUANTITY"]) - quantity[Convert.ToInt32(row["CHANNELADDRESS"]) - 1];
                        row["NOSORTQUANTITY"] = Convert.ToInt32(row["NOSORTQUANTITY"]) + quantity[Convert.ToInt32(row["CHANNELADDRESS"]) - 1];
                        row["REMAINQUANTITY"] = Convert.ToInt32(row["REMAINQUANTITY"]) + quantity[Convert.ToInt32(row["CHANNELADDRESS"]) - 1];
                        row["BOXQUANTITY"] = Convert.ToInt32(row["REMAINQUANTITY"]) / 50;
                        row["ITEMQUANTITY"] = Convert.ToInt32(row["REMAINQUANTITY"]) % 50;
                    }
                    return channelTable;
                }
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("ChannelCheckForm.GetChannelRealtimeQuantity() ����ʧ�ܣ�ԭ��{0}��", e.Message));
                return null;
            }
        }

        /// <summary>
        /// ���ز���ϵͳ�ĳ������ݲ��뵽�ּ�ϵͳ�����ݿ�
        /// </summary>
        private void DownLoadData()
        {
            try
            {
                using (PersistentManager pm = new PersistentManager())
                {
                    ChannelDao channelDao = new ChannelDao();
                    using (PersistentManager pmStockServer = new PersistentManager("StockServerConnection"))
                    {
                        StockDao stockDao = new StockDao();
                        stockDao.SetPersistentManager(pmStockServer);
                        GetLineCode();
                        DataTable table = stockDao.FindOutData(lineCode);
                        if (table.Rows.Count!=0)
                        {
                            channelDao.InsertChannelCheck(table);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("���ز���ϵͳ��������ʧ�ܣ�ԭ��" + ex.Message);
            }
        }

        string lineCode;
        private Dictionary<string, string> attributes = null;

        /// <summary>
        /// ��ȡ�ּ��ߺ�
        /// </summary>
        private void GetLineCode()
        {
            ConfigUtil configUtil = new ConfigUtil();
            attributes = configUtil.GetAttribute();
            Parameter parameter = new Parameter();
            parameter.LineCode = attributes["LineCode"];
            lineCode = parameter.LineCode;
        }
    }
}
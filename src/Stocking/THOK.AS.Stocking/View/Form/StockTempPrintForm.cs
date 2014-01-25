using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using THOK.MCP;
using THOK.Util;
using THOK.AS.Stocking.Dao;

namespace THOK.AS.Stocking
{
    public partial class StockTempPrintForm : THOK.AF.View.ToolbarForm
    {
        public StockTempPrintForm()
        {
            InitializeComponent();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                DownLoadData();

                using (PersistentManager pm = new PersistentManager())
                {
                    CheckDao checkDao = new CheckDao();
                    Report.StockTempReport ccp = new Report.StockTempReport();
                    ccp.SetDataSource(checkDao.FindStockTempQuantity());
                    this.crvStockTemp.ReportSource = ccp;
                }

                Logger.Info("�ݴ���ʵʱ�̵�ɹ�");
            }
            catch (Exception ex)
            {
                Logger.Error("�ݴ���ʵʱ�̵�ʧ�ܣ�ԭ��"+ex.ToString());
            }
        }

        /// <summary>
        /// ���زִ�ϵͳ�ĳ������ݲ��뵽�������ݿ�
        /// </summary>
        private void DownLoadData()
        {
            try
            {
                using(PersistentManager pm=new PersistentManager())
                {
                    ChannelDao channelDao = new ChannelDao();
                    using (PersistentManager pmWmsServer = new PersistentManager("WMSConnection"))
                    {
                        CheckDao checkDao = new CheckDao();
                        checkDao.SetPersistentManager(pmWmsServer);
                        DataTable table = checkDao.FindOutData();
                        if (table.Rows.Count!=0)
                        {
                            channelDao.InsertStockOutData(table);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("���زִ�ϵͳ��������ʧ�ܣ�ԭ��" + ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }
    }
}
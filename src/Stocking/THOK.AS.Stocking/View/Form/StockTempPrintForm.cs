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

                Logger.Info("暂存区实时盘点成功");
            }
            catch (Exception ex)
            {
                Logger.Error("暂存区实时盘点失败，原因："+ex.ToString());
            }
        }

        /// <summary>
        /// 下载仓储系统的出库数据插入到本地数据库
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
                Logger.Error("下载仓储系统出库数据失败！原因：" + ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }
    }
}
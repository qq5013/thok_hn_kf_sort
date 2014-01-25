using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using THOK.Util;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace THOK.AS.OTS.View
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                crystalReportViewer1.Refresh();
                lbInfo.Text = "";
                lbInfo.Text = "数据加载中……";
                OTS.Dao.OrderDao orderDao = new THOK.AS.OTS.Dao.OrderDao();
                CrystalReportOut cro = new CrystalReportOut();
                cro.SetDataSource(orderDao.FindOutReport());
                crystalReportViewer1.ReportSource = cro;
                lbInfo.Text = "";
            }
            catch (Exception ex)
            {
                THOK.MCP.Logger.Error(ex.Message);
            }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                crystalReportViewer1.Refresh();
                Dao.OrderDao dao = new THOK.AS.OTS.Dao.OrderDao();
                DataTable table=dao.FindOrder();
                using (PersistentManager pmServer = new PersistentManager("InfoServerConnection"))
                {
                    OTS.Dao.InfoServerDao infoDao = new THOK.AS.OTS.Dao.InfoServerDao();
                    infoDao.SetPersistentManager(pmServer);
                    PrintCrystalReport pr = new PrintCrystalReport();
                    pr.SetDataSource(infoDao.Find(table.Rows[0]["ORDERDATE"].ToString(), table.Rows[0]["BATCHNO"].ToString()));
                    //pr.SetDataSource(infoDao.Find("2011-5-30", table.Rows[0]["BATCHNO"].ToString()));
                    crystalReportViewer1.ReportSource = pr;
                }
            }
            catch (Exception EX)
            {
                THOK.MCP.Logger.Error(EX.Message);
            }
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}
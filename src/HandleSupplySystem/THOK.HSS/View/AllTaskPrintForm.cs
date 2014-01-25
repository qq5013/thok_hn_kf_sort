using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using System.Threading;
using THOK.HSS.Report;
using THOK.HSS.Dal;

namespace THOK.HSS.View
{
    public partial class PrintForm : Form
    {
        private HandSupplyDal handSupplyDal = new HandSupplyDal();
        public delegate void PrintHandler(AllTaskReport allTaskDataSet);
        public delegate void LblInfoHandler(string info);
        private AllTaskReport allTaskReport = null;
        Thread t = null;

        public PrintForm()
        {
            InitializeComponent();
            lblInfo.Text = "数据加载中……";
            t = new Thread(new ThreadStart(GetDataSet));
            t.IsBackground = false;
            t.Start();
        }

        private void GetDataSet()
        {
            try
            {
                allTaskReport = new AllTaskReport();
                DataTable table = handSupplyDal.GetAllHandleSupply();
                if (table.Rows.Count == 0)
                    throw new Exception("没有数据");
               
                allTaskReport.SetDataSource(table);
                SetReportToCrv(allTaskReport);
            }
            catch (Exception ex)
            {
                SetLblInfo(ex.Message);
            }
            finally
            {
                t.Abort();
            }
        }

        private void SetReportToCrv(AllTaskReport allTaskReport)
        {
            if (crvAllTaskReport.InvokeRequired)
            {
                crvAllTaskReport.Invoke(new PrintHandler(SetReportToCrv), allTaskReport);
            }
            else
            {
                lblInfo.Visible = false;
                crvAllTaskReport.ReportSource = allTaskReport;
            }
        }

        private void SetLblInfo(string info)
        {
            if (lblInfo.InvokeRequired)
            {
                crvAllTaskReport.Invoke(new LblInfoHandler(SetLblInfo), info);
            }
            else
            {
                lblInfo.Visible = true;
                lblInfo.Text = info;
            }
        }
    }
}
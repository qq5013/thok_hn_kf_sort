using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.HSS.Report;
using System.Threading;
using THOK.HSS.Dal;

namespace THOK.HSS.View
{
    public partial class SortChannelPrintForm : Form
    {
        private ChannelDal channelDal = new ChannelDal();
        public delegate void PrintHandler(SortChannelReport allTaskDataSet);
        public delegate void LblInfoHandler(string info);
        private  SortChannelReport channelReport = null;
        Thread t = null;

        public SortChannelPrintForm()
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
                channelReport = new SortChannelReport();
                DataTable table = channelDal.FindChannel();
                if(table.Rows.Count ==0 )
                    throw new Exception("没有数据");
                channelReport.SetDataSource(table);
                SetReportToCrv(channelReport);
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

        private void SetReportToCrv(SortChannelReport allTaskReport)
        {
            if (crvChannelReport.InvokeRequired)
            {
                crvChannelReport.Invoke(new PrintHandler(SetReportToCrv), allTaskReport);
            }
            else
            {
                lblInfo.Visible = false;
                crvChannelReport.ReportSource = allTaskReport;
            }
        }

        private void SetLblInfo(string info)
        {
            if (lblInfo.InvokeRequired)
            {
                crvChannelReport.Invoke(new LblInfoHandler(SetLblInfo), info);
            }
            else
            {
                lblInfo.Text = info;
            }
        }
    }
}
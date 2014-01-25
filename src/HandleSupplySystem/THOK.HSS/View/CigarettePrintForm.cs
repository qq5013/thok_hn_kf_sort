using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.HSS.Report;
using THOK.HSS.Dal;
using System.Threading;

namespace THOK.HSS.View
{
    public partial class CigarettePrintForm : Form
    {
        private HandSupplyDal handSupplyDal = new HandSupplyDal();
        public delegate void PrintHandler(CigaretteReport cigaretteReport);
        public delegate void LblInfoHandler(string info);
        private CigaretteReport cigaretteReport = null;
        Thread t = null;

        public CigarettePrintForm()
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
                cigaretteReport = new CigaretteReport();
                DataTable table = handSupplyDal.GetAllCigarette();
                if (table.Rows.Count == 0)
                    throw new Exception("没有数据");

                cigaretteReport.SetDataSource(table);
                SetReportToCrv(cigaretteReport);

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

        private void SetReportToCrv(CigaretteReport cigaretteReport)
        {
            if (crvCigaretteReport.InvokeRequired)
            {
                crvCigaretteReport.Invoke(new PrintHandler(SetReportToCrv), cigaretteReport);
            }
            else
            {
                lblInfo.Visible = false;
                crvCigaretteReport.ReportSource = cigaretteReport;
            }
        }

        private void SetLblInfo(string info)
        {
            if (lblInfo.InvokeRequired)
            {
                crvCigaretteReport.Invoke(new LblInfoHandler(SetLblInfo), info);
            }
            else
            {
                lblInfo.Visible = true;
                lblInfo.Text = info;
            }
        }
    }
}
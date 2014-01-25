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
    public partial class CacheChannelPrintForm :THOK.AF.View.ToolbarForm
    {
        public CacheChannelPrintForm()
        {
            InitializeComponent();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                using (PersistentManager pm = new PersistentManager())
                {
                    CheckDao checkDao = new CheckDao();
                    Report.CacheChannelReport ccp = new THOK.AS.Stocking.Report.CacheChannelReport();
                    ccp.SetDataSource(checkDao.FindChannelRemain());
                    crvCache.ReportSource = ccp;
                    Logger.Info("�����̵�ʵʱ�̵�ɹ�");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("�����̵�ʵʱ�̵�ʧ�ܣ�ԭ��"+ex.ToString());
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }
    }
}
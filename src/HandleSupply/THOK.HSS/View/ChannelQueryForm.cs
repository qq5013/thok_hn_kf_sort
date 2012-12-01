using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.AF.View;
using THOK.HSS.Dal;

namespace THOK.HSS.View
{
    public partial class ChannelQueryForm : ToolbarForm
    {
        private ChannelDal channelDal = new ChannelDal();

        public ChannelQueryForm()
        {
            InitializeComponent();
            this.CHANNELNAME.FilteringEnabled = true;
            this.CIGARETTECODE.FilteringEnabled = true;
            this.CIGARETTENAME.FilteringEnabled = true;
            this.CHANNELTYPENAME.FilteringEnabled = true;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (bsMain.DataSource == null)
                bsMain.DataSource = channelDal.FindChannel();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            SortChannelPrintForm printForm = new SortChannelPrintForm();
            printForm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Exit();
        }

    
    }
}
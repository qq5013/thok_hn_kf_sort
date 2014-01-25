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
    public partial class ChannelSortQueryForm : ToolbarForm
    {
        private ChannelDal channelDal = new ChannelDal();

        public ChannelSortQueryForm()
        {
            InitializeComponent();           
            this.SUPPLYBATCH.FilteringEnabled = true;
            this.SUPPLYNO.FilteringEnabled = true;
            this.SORTNO.FilteringEnabled = true;
            this.ORDERID.FilteringEnabled = true;
            this.CIGARETTENAME.FilteringEnabled = true;
            this.CHANNELNAME.FilteringEnabled = true;
            this.ROUTENAME.FilteringEnabled = true;
            this.SUPPLYSTATUS.FilteringEnabled = true;
            this.CUSTOMERNAME.FilteringEnabled = true;
            this.SORTSTATUS.FilteringEnabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Exit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ChannelNameDialog channelNameDialog = new ChannelNameDialog();
            if (channelNameDialog.ShowDialog() == DialogResult.OK)
            {
                bsChannelSort.DataSource = channelDal.FindChannelSort(channelNameDialog.ChannelName);
            }
        }

        private void ChannelSortQueryForm_Load(object sender, EventArgs e)
        {
            this.dgvChannelSort.AutoGenerateColumns = false;
        }
    }
}
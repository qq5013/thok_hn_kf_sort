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
    public partial class AllTaskQueryForm : ToolbarForm
    {
        HandSupplyDal handSupplyDal = new HandSupplyDal();
        private DataTable handSupplyTable = null;
        private int supplyBatch = 0;
        private int lastPage = 0;

        public AllTaskQueryForm()
        {
            InitializeComponent();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            supplyBatch = 1;
            lastPage = handSupplyDal.GetLastSupplyBatchNo();

            handSupplyTable = handSupplyDal.GetHandSupplyBySupplyBatch(supplyBatch);
            if (handSupplyTable.Rows.Count == 0)
                MessageBox.Show("没有数据显示");
            else
                dgvHandSupply.DataSource = handSupplyTable;
        }

        private void UpPage_Click(object sender, EventArgs e)
        {
            if (supplyBatch > 1)
            {
                handSupplyTable = handSupplyDal.GetHandSupplyBySupplyBatch(--supplyBatch);
                dgvHandSupply.DataSource = handSupplyTable;
            }
        }

        private void DownPage_Click(object sender, EventArgs e)
        {
            if (supplyBatch < lastPage)
            {
                handSupplyTable = handSupplyDal.GetHandSupplyBySupplyBatch(++supplyBatch);
                dgvHandSupply.DataSource = handSupplyTable;
            }
        }

        private void Print_Click(object sender, EventArgs e)
        {
            PrintForm printForm = new PrintForm();
            printForm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                supplyBatch = int.Parse(txtSupplyBatch.Text);
                DataTable batchTaskTable = handSupplyDal.GetHandSupplyBySupplyBatch(supplyBatch);
                dgvHandSupply.DataSource = batchTaskTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
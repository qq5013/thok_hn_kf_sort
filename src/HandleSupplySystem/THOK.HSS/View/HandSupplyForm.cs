using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.AF.View;
using THOK.HSS;
using THOK.HSS.Dal;

namespace THOK.HSS.View
{
    public partial class HandSupplyForm : ToolbarForm
    {
        private HandSupplyDao  handSupplyDao = new HandSupplyDao()
            ;
        private HandSupplyDal handSupplyDal = new HandSupplyDal();
        private DataTable handSupplyTable = null;

        private int supplyBatch = 0;
        private int dataCount = 0;

        public HandSupplyForm()
        {
            InitializeComponent();
        }

        //public  void RefreshData()
        //{
        //    try
        //    {
        //        int iSupplyBatch = handSupplyDao.GetCurrentSupplyBatch();
        //        supplyBatch = iSupplyBatch;

 
        //        handSupplyTable = handSupplyDao.GetHandSupplyBySupplyBatch(supplyBatch);
        //        dataCount = handSupplyDao.GetHandSupplyCountBySupplyBatch(supplyBatch);
        //        dgvHandSupply.DataSource = handSupplyTable;
        //        LoadColor(dgvHandSupply);
        //    }
        //    catch (Exception ee)
        //    {
        //        MessageBox.Show(ee.Message);
        //    }
        //}

        private void LoadColor(DataGridView dgvHandSupply)
        {

            for (int i = 0; i < dgvHandSupply.Rows.Count; i++)
            {
                dgvHandSupply.Rows[i].Height = 25;
                if (dgvHandSupply.Rows[i].Cells["STATUS"].Value.ToString() == "已补货")
                {
                    dgvHandSupply.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);
                    dgvHandSupply.Rows[i].Cells["SetSupplyStatus"].Value = true;
                    dgvHandSupply.Rows[i].Cells["SetSupplyStatus"].ReadOnly = true;
                }
                else if (dgvHandSupply.Rows[i].Cells["STATUS"].Value.ToString() == "未补货")
                {
                    if (i >= 1)
                    {
                        if (dgvHandSupply.Rows[i - 1].Cells["STATUS"].Value.ToString() == "已补货")
                        {
                            dgvHandSupply.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                        else
                        {
                            dgvHandSupply.Rows[i].Cells["SetSupplyStatus"].Value = false;
                            dgvHandSupply.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                    else
                    {
                        if (dgvHandSupply.Rows[i].Cells["STATUS"].Value.ToString() == "未补货")
                        {
                            dgvHandSupply.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }
                }
            }

        }
        private void dgvHandSupply_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)//当单击复选框
            {
                DataGridViewCell cell = dgvHandSupply.Rows[e.RowIndex].Cells[e.ColumnIndex];
                //判断前面是否有未补货的数据
                for (int i = 0; i < e.RowIndex; i++)
                {
                    if (dgvHandSupply.Rows[i].Cells["STATUS"].Value.ToString() == "未补货")
                    {
                        for (int j = 0; j < dataCount - 1; j++)
                        {
                            if (dgvHandSupply.Rows[j].Cells["STATUS"].Value.ToString() == "未补货")
                            {
                                dgvHandSupply.Rows[j].Cells["SetSupplyStatus"].Value = 0;
                            }
                            else
                            {
                                dgvHandSupply.Rows[j].Cells["SetSupplyStatus"].Value = 1;
                                dgvHandSupply.Rows[j].Cells["SetSupplyStatus"].ReadOnly = true;
                            }
                        }
                        LoadColor(dgvHandSupply);
                        dgvHandSupply.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                        MessageBox.Show("前面还有卷烟未进行补货,请先补前面未补货的卷烟!");
                        //dgvHandSupply.Rows.SharedRow(0);

                        DataGridViewCheckBoxCell ck = (DataGridViewCheckBoxCell)dgvHandSupply.Rows[e.RowIndex].Cells["SetSupplyStatus"];
                        ck.Selected = false;
                        ck.EditingCellFormattedValue = false;
                        //LoadColor(dgvHandSupply);
                        dgvHandSupply.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                        return;
                    }
                }
                bool ifcheck1 = Convert.ToBoolean(cell.FormattedValue);
                bool ifcheck2 = Convert.ToBoolean(cell.EditedFormattedValue);
                if (ifcheck1 != ifcheck2)
                {
                    string checkMsg = dgvHandSupply.Rows[e.RowIndex].Cells["STATUS"].Value.ToString();
                    string supplyNo = dgvHandSupply.Rows[e.RowIndex].Cells["SUPPLYNO"].Value.ToString();
                    string sortNo = dgvHandSupply.Rows[e.RowIndex].Cells["SORTNO"].Value.ToString();
                    string lineCode = dgvHandSupply.Rows[e.RowIndex].Cells["LINECODE"].Value.ToString();
                    string batchNo = dgvHandSupply.Rows[e.RowIndex].Cells["BATCHNO"].Value.ToString();
                    string orderDate = dgvHandSupply.Rows[e.RowIndex].Cells["ORDERDATE"].Value.ToString();
                    if (checkMsg == "已补货")
                    {
                        if (e.RowIndex != dataCount - 1)
                        {
                            for (int k = 0; k < dataCount - 1; k++)
                            {
                                if (dgvHandSupply.Rows[k].Cells["STATUS"].Value.ToString() == "未补货")
                                {
                                    dgvHandSupply.Rows[k].Cells["SetSupplyStatus"].Value = 0;
                                }
                                else
                                {
                                    dgvHandSupply.Rows[k].Cells["SetSupplyStatus"].Value = 0;
                                }
                            }
                            LoadColor(dgvHandSupply);
                            dgvHandSupply.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                            return;
                        }
                    }
                    if (checkMsg == "未补货")
                    {
                        //更新为已补货
                        dgvHandSupply.Rows[e.RowIndex].Cells["STATUS"].Value = "已补货";
                        dgvHandSupply.Rows[e.RowIndex].Cells["SetSupplyStatus"].Value = 1;
                        handSupplyDao.FinishSupply(supplyNo, sortNo, batchNo, orderDate);
                        //dgvHandSupply.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red ;
                        LoadColor(dgvHandSupply);
                    }
                    if (e.RowIndex == dataCount - 1)
                    {
                        handSupplyTable = handSupplyDao.GetHandSupplyBySupplyBatch(++supplyBatch);
                        if (handSupplyTable.Rows.Count == 0)
                        {
                            handSupplyTable = handSupplyDao.GetHandSupplyBySupplyBatch(--supplyBatch);
                            dataCount = handSupplyDao.GetHandSupplyCountBySupplyBatch(supplyBatch);
                            dgvHandSupply.DataSource = handSupplyTable;
                            LoadColor(dgvHandSupply);
                            MessageBox.Show("已经完成全部手工补货！");
                        }
                        else
                        {
                            dataCount = handSupplyDao.GetHandSupplyCountBySupplyBatch(supplyBatch);
                            dgvHandSupply.DataSource = handSupplyTable;
                            LoadColor(dgvHandSupply);
                        }
                    }
                }

            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                int iSupplyBatch = handSupplyDal.GetCurrentSupplyBatch();
                supplyBatch = iSupplyBatch;


                handSupplyTable = handSupplyDal.GetHandSupplyBySupplyBatch(supplyBatch);
                dataCount = handSupplyDal.GetHandSupplyCountBySupplyBatch(supplyBatch);
                dgvHandSupply.DataSource = handSupplyTable;
                LoadColor(dgvHandSupply);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void HandSupplyForm_Load(object sender, EventArgs e)
        {
            this.dgvHandSupply.AutoGenerateColumns = false;
        }


        private void dgvHandSupply_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
            //this.dgvHandSupply
        }
    }
}
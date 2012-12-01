using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace THOK.AS.OTS.View
{
    public partial class RouteForm : Form
    {
        private DataTable masterTable = null;
        private DataTable detailTable = null;

        public RouteForm()
        {
            InitializeComponent();
            GetData();
            bsMaster.DataSource = masterTable;
            dgvDetail.DataSource = detailTable;
            //计算出本次分拣的总条数
            int count = 0;
            for (int i = 0; i < dgvMaster.Rows.Count; i++)
            {
                count = Convert.ToInt32(dgvMaster.Rows[i].Cells[6].Value)+count;
            }
            lbTotalCount.Text = count.ToString() + "条";
        }

        private void GetData()
        {
            try
            {
                using (THOK.Util.PersistentManager pm = new THOK.Util.PersistentManager())
                {
                    Dao.OrderDao orderDao = new THOK.AS.OTS.Dao.OrderDao();
                    masterTable = orderDao.FindRoute();
                    detailTable = orderDao.FindCustomer();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        private void dgvMaster_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detailTable.DefaultView.RowFilter = string.Format("ROUTECODE = '{0}'", dgvMaster.Rows[e.RowIndex].Cells[3].Value);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        //打印
        //private void btnPrint_Click(object sender, EventArgs e)
        //{
        //    Printer printer = new Printer();
        //    using (THOK.Util.PersistentManager pm = new THOK.Util.PersistentManager())
        //    {
        //        Dao.OrderDao orderDao = new Dao.OrderDao();
        //        for (int i = dgvMaster.SelectedRows.Count - 1; i >= 0; i--)
        //        {
        //            DataGridViewRow row = dgvMaster.SelectedRows[i];
        //            DataTable master = orderDao.FindOrderMaster(row.Cells["ROUTECODE"].Value.ToString());
        //            foreach (DataRow masterRow in master.Rows)
        //            {
        //                DataTable table = orderDao.FindOrderDetail(masterRow["ORDERID"].ToString());
        //                int quantity = Convert.ToInt32(table.Compute("SUM(QUANTITY)", ""));
        //                printer.Print(masterRow, table.Select(), quantity);
        //            }
        //        }
        //    }
        //}
    }
}
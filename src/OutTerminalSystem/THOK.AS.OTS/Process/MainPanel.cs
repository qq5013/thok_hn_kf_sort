using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace THOK.AS.OTS.Process
{
    internal delegate void RefreshDataEventHandler(string orderID);

    public partial class MainPanel : THOK.MCP.View.ProcessControl
    {
        public MainPanel()
        {
            InitializeComponent();
        }

        public override void Initialize(THOK.MCP.Context context)
        {
            base.Initialize(context);
            RefreshData("-1");
        }

        public override void Process(THOK.MCP.StateItem stateItem)
        {
            RefreshData(stateItem.State.ToString());
        }

        private void RefreshData(string sortNo)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new RefreshDataEventHandler(RefreshData), sortNo);
            }
            else
            {
                try
                {
                    string idFirst = "", idNext = "";
                    
                    //读数据库取明细数据
                    using (THOK.Util.PersistentManager pm = new THOK.Util.PersistentManager())
                    {
                        Dao.OrderDao orderDao = new THOK.AS.OTS.Dao.OrderDao();

                        if (sortNo.Equals("-1"))
                        {
                            sortNo = orderDao.FindMinSortNo();
                            if (sortNo.Equals(""))
                            {
                                THOK.MCP.Logger.Info("没有刷新数据或者已全部打印完成！");
                                return;
                            }
                        }

                        DataTable masterTable = orderDao.FindMasterTable(sortNo);
                        if (masterTable.Rows.Count != 0)
                        {
                            orderIDFirst.Text = masterTable.Rows[masterTable.Rows.Count -1]["ORDERID"].ToString();
                            customerNameFirst.Text = masterTable.Rows[masterTable.Rows.Count - 1]["CUSTOMERNAME"].ToString();
                            idFirst = masterTable.Rows[masterTable.Rows.Count - 1]["ORDERID"].ToString();                            

                            masterTable = orderDao.FindNextMaster(sortNo);
                            if (masterTable.Rows.Count != 0)
                            {
                                orderIDNext.Text = masterTable.Rows[0]["ORDERID"].ToString();
                                customerNameNext.Text = masterTable.Rows[0]["CUSTOMERNAME"].ToString();
                                idNext = masterTable.Rows[0]["ORDERID"].ToString();
                            }
                            else
                            {
                                orderIDNext.Text = "";
                                customerNameNext.Text = "";
                            }

                            DataTable firstTable = orderDao.FindOrderDetail(idFirst);
                            dgvFirst.DataSource = firstTable;
                            int quantity = 0;
                            if (firstTable.Rows.Count != 0)
                                quantity = Convert.ToInt32(firstTable.Compute("SUM(QUANTITY)", ""));
                            quantityFirst.Text = quantity.ToString();
                            packageFirst.Text = Convert.ToString(quantity % 25 == 0 ? quantity / 25 : quantity / 25 + 1);

                            DataTable nextTable = orderDao.FindOrderDetail(idNext);
                            dgvNext.DataSource = nextTable;
                            if (nextTable.Rows.Count != 0)//当前订单为最后一张订单时已经没有下一张订单了
                            {

                                quantity = Convert.ToInt32(nextTable.Compute("SUM(QUANTITY)", ""));
                                quantityNext.Text = quantity.ToString();
                                packageNext.Text = Convert.ToString(quantity % 25 == 0 ? quantity / 25 : quantity / 25 + 1);
                            }
                            else
                            {
                                quantityNext.Text = "0";
                                packageNext.Text = "0";
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    THOK.MCP.Logger.Error("错误，原因：" + e.Message);
                }
            }

        }
    }
}


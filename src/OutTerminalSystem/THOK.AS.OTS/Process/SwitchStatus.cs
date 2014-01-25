using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using THOK.MCP;
using THOK.AS.OTS.Dal;

namespace THOK.AS.OTS.Process
{
    internal delegate void statuechangeEventHandler(int sortNoOne, int channelGroupOne, int sortNoTwo, int channelGroupTwo);

    public partial class SwitchStatus:THOK.MCP.View.ProcessControl
    {
        private OrderDal orderDal = new OrderDal();

        public SwitchStatus()
        {
            InitializeComponent();
            //SwitchStatusChange(28,1,7,2);
        }

        public override void Initialize(THOK.MCP.Context context)
        {
            base.Initialize(context);
        }

        public override void Process(THOK.MCP.StateItem stateItem)
        {
            base.Process(stateItem);
            Dictionary<string, string> parameter = (Dictionary<string, string>)stateItem.State;

            string sortNoOne = parameter["SwitchOneSortNo"];
            string sortNoTwo = parameter["SwitchTwoSortNo"];
            string channelGroupOne = parameter["SwitchOneChannelGroup"];
            string channelGroupTwo = parameter["SwitchTwoChannelGroup"];
            SwitchStatusChange(Convert.ToInt32(sortNoOne), Convert.ToInt32(channelGroupOne), Convert.ToInt32(sortNoTwo), Convert.ToInt32(channelGroupTwo));
        }

        //绑定数据
        public void SwitchStatusChange(int sortNoOne, int channelGroupOne, int sortNoTwo, int channelGroupTwo)
        {
            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new statuechangeEventHandler(SwitchStatusChange), sortNoOne, channelGroupOne, sortNoTwo, channelGroupTwo);
                }
                else
                {
                    THOK.AS.OTS.Dao.OrderDao dao = new THOK.AS.OTS.Dao.OrderDao();
                    int sumQutity = 0;

                    //摆动1
                    DataTable tableTitle = orderDal.GetOrderOrderTitleForCacheOrderQuery(channelGroupOne, sortNoOne);
                    DataTable table= orderDal.GetOrderDetailForCacheOrderQuery(channelGroupOne, sortNoOne);
                    if (tableTitle.Rows.Count!=0)
                    {
                        OrderIDSOne.Text = tableTitle.Rows[0]["ORDERID"].ToString();
                        CustomerNameSOne.Text = tableTitle.Rows[0]["CUSTOMERNAME"].ToString();
                        QuantitySOne.Text = tableTitle.Compute("SUM(QUANTITY)", "").ToString();
                        //packageSOne.Text = "";
                    }

                    else
                    {
                        OrderIDSOne.Text = null;
                        CustomerNameSOne.Text = null;
                        QuantitySOne.Text = null;
                    }

                    if (table.Rows.Count != 0)
                    {
                        //dgvSwitchOne.Rows.Clear();
                        dgvSwitchOne.DataSource = table;
                    }

                    //摆动2
                    DataTable tableTitleTwo = orderDal.GetOrderOrderTitleForCacheOrderQuery(channelGroupTwo, sortNoTwo);
                    DataTable tableTwo = orderDal.GetOrderDetailForCacheOrderQuery(channelGroupTwo, sortNoTwo);

                    //sumQutity = Convert.ToInt32(tableTwo.Compute("SUM(QUANTITY)", ""));

                    if (tableTitleTwo.Rows.Count != 0)
                    {
                        OrderIDSTwo.Text = tableTitleTwo.Rows[0]["ORDERID"].ToString();
                        CustomerNameSTwo.Text = tableTitleTwo.Rows[0]["CUSTOMERNAME"].ToString();
                        QuantitySTwo.Text = tableTitleTwo.Compute("SUM(QUANTITY)", "").ToString();
                        //packageSTwo.Text = "";
                    }
                    else
                    {
                        OrderIDSTwo.Text = null;
                        CustomerNameSTwo.Text = null;
                        QuantitySTwo.Text = null;
                    }

                    if (tableTwo.Rows.Count != 0)
                    {
                        //dgvSwitchTwo.Rows.Clear();
                        dgvSwitchTwo.DataSource = tableTwo;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("摆动缓存段："+ex.Message);
            }
        }
    }
}

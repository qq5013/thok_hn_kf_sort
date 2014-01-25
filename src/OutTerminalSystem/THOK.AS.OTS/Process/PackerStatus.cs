using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using THOK.AS.OTS.Dal;

namespace THOK.AS.OTS.Process
{
    internal delegate void packstatuechangeEventHandler(int sortNoOne, int channelGroupOne, int sortNoTwo, int channelGroupTwo);

    public partial class PackerStatus :THOK.MCP.View.ProcessControl
    {
        private OrderDal orderDal = new OrderDal();
        public PackerStatus()
        {
            InitializeComponent();
            //PackerStatusChange(1,1,1,2);   
        }

        public override void Process(THOK.MCP.StateItem stateItem)
        {
            base.Process(stateItem);
            Dictionary<string, string> parameter = (Dictionary<string, string>)stateItem.State;

            string sortNoOne = parameter["PackerOneSortNo"];
            string sortNoTwo = parameter["PackerTwoSortNo"];
            string channelGroupOne = parameter["PackerOneChannelGroup"];
            string channelGroupTwo = parameter["PackerTwoChannelGroup"];
            PackerStatusChange(Convert.ToInt32(sortNoOne),Convert.ToInt32(channelGroupOne),Convert.ToInt32(sortNoTwo),Convert.ToInt32(channelGroupTwo));
        }

        //绑定数据
        public void PackerStatusChange(int sortNoOne, int channelGroupOne, int sortNoTwo, int channelGroupTwo)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new packstatuechangeEventHandler(PackerStatusChange), sortNoOne, channelGroupOne, sortNoTwo, channelGroupTwo);
            }
            else
            {
                try
                {
                    int sumQutity = 0;

                    //1号包装机
                    DataTable tableTitle = orderDal.GetOrderOrderTitleForCacheOrderQuery(channelGroupOne, sortNoOne);
                    DataTable table = orderDal.GetOrderDetailForCacheOrderQuery(channelGroupOne, sortNoOne);

                    sumQutity = Convert.ToInt32(table.Compute("SUM(QUANTITY)", ""));

                    OrderIDPOne.Text = tableTitle.Rows[0]["ORDERID"].ToString(); ;
                    CustomerNamePOne.Text = tableTitle.Rows[0]["CUSTOMERNAME"].ToString();
                    QuantityPOne.Text = tableTitle.Compute("SUM(QUANTITY)", "").ToString();
                    //packagePOne.Text = "";
                    if (table.Rows.Count != 0)
                    {
                        dgvPackerOne.DataSource = table;
                    }

                    //2号包装机
                    DataTable tableTitleTwo = orderDal.GetOrderOrderTitleForCacheOrderQuery(channelGroupTwo, sortNoTwo);
                    DataTable tableTwo = orderDal.GetOrderDetailForCacheOrderQuery(channelGroupTwo, sortNoTwo);

                    sumQutity = Convert.ToInt32(tableTwo.Compute("SUM(QUANTITY)", ""));

                    OrderIDPTwo.Text = tableTitleTwo.Rows[0]["ORDERID"].ToString();
                    CustomerNamePTwo.Text = tableTitleTwo.Rows[0]["CUSTOMERNAME"].ToString();
                    QuantityPTwo.Text = tableTitleTwo.Compute("SUM(QUANTITY)", "").ToString();
                    //packagePTwo.Text = "";
                    if (table.Rows.Count != 0)
                    {
                        dgvPackerTwo.DataSource = tableTwo;
                    }
                }
                catch (Exception ex)
                {
                    THOK.MCP.Logger.Error("包装缓存段："+ex.Message);
                }
            }
        }
    }
}

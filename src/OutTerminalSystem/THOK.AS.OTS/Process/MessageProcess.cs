using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.AS.OTS.Process
{
    public class MessageProcess: THOK.MCP.AbstractProcess
    {
        private Printer printer = new Printer();

        protected override void StateChanged(THOK.MCP.StateItem stateItem, THOK.MCP.IProcessDispatcher dispatcher)
        {
            //Dictionary<string, string> parameter = (Dictionary<string, string>)stateItem.State;
            //dispatcher.WriteToProcess("switchStatus1", "SortNo", parameter["SwitchOneSortNo"]);
            //WriteToProcess("PackerStatus", stateItem.ItemName, stateItem.State);
            dispatcher.WriteToProcess("switchStatus", stateItem.ItemName, stateItem.State);
            dispatcher.WriteToProcess("packerStatus", stateItem.ItemName, stateItem.State);
            return;
            //Dictionary<string, string> parameter = (Dictionary<string, string>)stateItem.State;
            ////转发给mainPanel刷新显示数据
            //dispatcher.WriteToProcess("mainPanel", "SortNo", parameter["SORTNO"]);
            ////dispatcher.WriteToProcess("SwitchStatus", "SortNo", parameter["SORTNO"]);

            ////打印客户标签
            //if (Context.Attributes["PrintLabel"].ToString().ToUpper().Equals("TRUE"))
            //{
            //    using (PersistentManager pm = new PersistentManager())
            //    {
            //        Dao.OrderDao orderDao = new Dao.OrderDao();

            //        DataTable masterTable = orderDao.FindMasterTable(parameter["SORTNO"]);
            //        if (masterTable.Rows.Count != 0)
            //        {
            //            foreach (DataRow  row in masterTable.Rows)
            //            {
            //                string orderID = row["ORDERID"].ToString();
            //                THOK.MCP.Logger.Info("打印客户标签.订单号：" + orderID);
            //                DataTable detailTable = orderDao.FindOrderDetail(orderID);

            //                int quantity = Convert.ToInt32(detailTable.Compute("SUM(QUANTITY)", ""));

            //                printer.Print(row, detailTable.Select(), quantity);

            //                orderDao.UpdateOrderPrintStatus(orderID);
            //            }
            //        }
            //    }
            //}
        }
    }
}

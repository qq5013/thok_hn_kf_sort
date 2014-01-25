using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using THOK.AS.Stocking.Dao;
using System.Data;
using System.Windows.Forms;
using THOK.Util;

namespace THOK.AS.Stocking.Process
{
    class SupplyCarRequestProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                object o = ObjectUtil.GetObject(stateItem.State);
                if (o == null || o.ToString() == "0")
                {
                    return;
                }

                //��ѯAS_STOCK_OUT���·�����Ŀ���̵���PLC
                using (PersistentManager pm = new PersistentManager())
                {
                    StockOutDao outDao = new StockOutDao();
                    string spupplyCarCode = stateItem.ItemName.Substring(stateItem.ItemName.Length - 2, 2);

                    DataTable outTable = outDao.FindCigaretteForSupplyCar(spupplyCarCode);
                    if (outTable.Rows.Count != 0)
                    {
                        string text = "";
                        switch (o.ToString())
                        {
                            case "1":
                                int channelAddress = Convert.ToInt32(outTable.Rows[0]["GROUPNO"]);
                                dispatcher.WriteToService("StockPLC", stateItem.ItemName + "_Data", channelAddress);
                                break;
                            case "2"://�ɹ���ɲ�������״̬                               
                                outDao.UpdateSupplyCarStatus(outTable.Rows[0]["STOCKOUTID"].ToString());
                                break;
                            case "4"://�Զ�����ʧ�ܣ�������ֹ���������ҲҪ����״̬
                                text = string.Format("{0} ��С��������ǰ����Ʒ��Ϊ��{1}�����̵���Ϊ ��{2}���ţ� ��ȷ�ϣ�", spupplyCarCode, outTable.Rows[0]["CIGARETTENAME"], outTable.Rows[0]["GROUPNO"]);
                                MessageBox.Show(text, "ѯ��", MessageBoxButtons.OK , MessageBoxIcon.Question);
                                break;
                            default:
                                break;
                        }
                        
                    }
                }
            }
            catch (Exception ee)
            {
                Logger.Error("����С������������ʧ�ܣ�ԭ��" + ee.Message);
            }
        }
    }
}

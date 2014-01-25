using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;

namespace THOK.AS.Stocking.Process
{
    public class DynamicShowProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                if (stateItem.ItemName == "DynamicShow")
                {
                    if (stateItem.State is Array)
                    {
                        Array array = (Array)stateItem.State;
                        if (array.Length == 115)
                        {
                            dispatcher.WriteToProcess("monitorView", "������", array);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error("�����Ϣ����ʧ�ܣ�ԭ��" + e.Message);
            }

        }
    }
}

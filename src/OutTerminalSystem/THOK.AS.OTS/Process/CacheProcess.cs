using System;
using System.Collections.Generic;
using System.Text;

using THOK.MCP;

namespace THOK.AS.OTS.Process
{
    public class CacheProcess:AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            WriteToProcess("PackerStatus", stateItem.ItemName, stateItem.State);
            WriteToProcess("SwitchStatus", stateItem.ItemName, stateItem.State);
        }
    }
}

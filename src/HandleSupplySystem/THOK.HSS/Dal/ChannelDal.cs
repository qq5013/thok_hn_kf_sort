using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.HSS.Dal
{
    public class ChannelDal
    {
        public DataTable FindChannel()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                return channelDao.FindChannel();
            }
        }

        public DataTable FindChannelSort(string channelName)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao channelDao = new ChannelDao();
                return channelDao.FindChannelSort(channelName);
            }
        }
    }
}

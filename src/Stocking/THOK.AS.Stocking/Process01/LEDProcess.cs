using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.MCP;
using THOK.Util;
using THOK.AS.Stocking.Dao;
using THOK.AS.Stocking.Util;

namespace THOK.AS.Stocking.Process
{
    public class LEDProcess: AbstractProcess
    {
        private LEDUtil ledUtil = new LEDUtil();
        private Dictionary<int, string> isActiveLeds = new Dictionary<int, string>();

        public override void Release()
        {
            try
            {
                ledUtil.Release();
                base.Release();
            }
            catch (Exception e)
            {
                Logger.Error("LEDProcess ��Դ�ͷ�ʧ�ܣ�ԭ��" + e.Message);
            }
        }

        public override void Initialize(Context context)
        {
            base.Initialize(context);

            Microsoft.VisualBasic.Devices.Network network = new Microsoft.VisualBasic.Devices.Network();
            string[] ledConfig = context.Attributes["IsActiveLeds"].ToString().Split(';');

            foreach (string led in ledConfig)
            {
                if (network.Ping(led.Split(',')[1]))
                {
                    isActiveLeds.Add(Convert.ToInt32(led.Split(',')[0]), led.Split(',')[1]);
                }
                else
                {
                    Logger.Error(Convert.ToInt32(led.Split(',')[0]) + "��LED�����ϣ����飡IP:[" + led.Split(',')[1] + "]");
                }
            }

            ledUtil.isActiveLeds = isActiveLeds;
        }

        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  �������
             *  Init����ʼ��
             *  Refresh��ˢ��LED����
             *      ��01����һ���� ��ʾ�������������Ϣ
             *      ��02���������� ��ʾ���󲹻��Ļ���̵�����˳����Ϣ
             */
            string cigaretteName = "";

            switch (stateItem.ItemName)
            {
                case "Refresh":
                    this.Refresh();
                    break;
                case "Refresh_01":
                    this.Refresh_01();
                    break;
                case "Refresh_02":
                    this.Refresh_02();
                    break;
                case "Refresh_02_MoveNext":
                    this.Refresh_02_MoveNext();
                    break;
                case "StockInRequestShow":
                    cigaretteName = Convert.ToString(stateItem.State);
                    this.StockInRequestShow(cigaretteName);
                    break;
                case "Show_Scanner_01":
                    break;
                case "Show_Scanner_02":
                    cigaretteName = Convert.ToString(stateItem.State);
                    this.Show("1","02", cigaretteName);
                    break;
                case "Show_Scanner_03":
                    cigaretteName = Convert.ToString(stateItem.State);
                    this.Show("2", "03",cigaretteName);
                    break;
                case "Show_Scanner_04":
                    cigaretteName = Convert.ToString(stateItem.State);
                    this.Show("3", "04",cigaretteName);
                    break;
                case "Show_Scanner_05":
                    cigaretteName = Convert.ToString(stateItem.State);
                    this.Show("3","05",cigaretteName);
                    break;
                default:
                    break;
            }        
        }

        private void Refresh()
        {
            //ˢ��1����
            Refresh_01();
            //ˢ��2����
            Refresh_02();
        }

        private void Refresh_01()
        {
            //ˢ��1����
            using (PersistentManager pm = new PersistentManager())
            {
                StockInBatchDao stockInBatchDao = new StockInBatchDao();

                DataTable batchTable = stockInBatchDao.FindStockInTopAnyBatch();
                ledUtil.RefreshStockInLED(batchTable, "1");
            }
        }

        private void Refresh_02()
        {
            //ˢ��2����
            using (PersistentManager pm = new PersistentManager())
            {
                StockOutDao outDao = new StockOutDao();
                ChannelDao channelDao = new ChannelDao();

                string channelCode = Context.Attributes["LED_02_CHANNELCODE"].ToString();
                DataTable table = outDao.FindLEDData(channelCode);
                string ledNo = channelDao.FindLed(channelCode);
                ledUtil.Refresh(table, ledNo);
            }
        }

        private void Refresh_02_MoveNext()
        {
            //�����ѳ�һ����״̬��ˢ��2����
            using (PersistentManager pm = new PersistentManager())
            {
                StockOutDao outDao = new StockOutDao();
                ChannelDao channelDao = new ChannelDao();

                string channelCode = Context.Attributes["LED_02_CHANNELCODE"].ToString();

                string stockOutID = outDao.FindMinStockOutID(channelCode);
                outDao.UpdateLEDStatus(stockOutID);

                DataTable table = outDao.FindLEDData(channelCode);
                string ledNo = channelDao.FindLed(channelCode);

                ledUtil.Refresh(table, ledNo);
            }
        }

        private void StockInRequestShow(string cigaretteName)
        {
            ledUtil.RefreshStockInLED("1",cigaretteName);
            Logger.Info("ȱ�����ѣ������" + cigaretteName);
        }

        private void Show(string ledNo,string sccanerCode,string cigaretteName)
        {
            ledUtil.RefreshScannerLED(ledNo, sccanerCode, cigaretteName);
            Logger.Info(ledNo + "������ʾ��" + sccanerCode + "��ɨ������ǰ����Ϊ��" + cigaretteName);
        }
    }
}
 
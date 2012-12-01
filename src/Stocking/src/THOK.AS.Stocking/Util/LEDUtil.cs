using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace THOK.AS.Stocking.Util
{
    public class LEDUtil
    {
        internal class LedItem
        {
            public string Name;
            public int Count = 0;
            public override string ToString()
            {
                return string.Format("{0}-{1}", Count.ToString().PadLeft(2, ' '), Name);
            }
        }

        private LedCollection leds = new LedCollection();
        public Dictionary<int, string> isActiveLeds = new Dictionary<int, string>();
        public LEDUtil()
        {
            //��ʼ��LED��            
            leds.DelAllProgram();

        }

        public void Release()
        {
            //�ͷ�LED����Դ
            leds = null;
        } 

        public void Refresh(DataTable table, string ledNo)
        {
            int cardNum = Convert.ToInt32(ledNo);
            if (!IsOnLineLed(cardNum))
            {
                return;
            }
            Stack<LedItem> data = new Stack<LedItem>();
            foreach (DataRow row in table.Rows)
            {
                if (data.Count != 0)
                {
                    LedItem item = data.Pop();
                    if (item.Name == row["CIGARETTENAME"].ToString())
                    {
                        item.Count++;
                        data.Push(item);
                    }
                    else
                    {
                        data.Push(item);//���ԭ��

                        if (data.Count >= 10)
                        {
                            break;
                        }

                        //����µ�
                        item = new LedItem();
                        item.Name = row["CIGARETTENAME"].ToString();
                        item.Count = 1;
                        data.Push(item);
                    }

                }
                else
                {
                    LedItem item = new LedItem();
                    item.Name = row["CIGARETTENAME"].ToString();
                    item.Count = 1;
                    data.Push(item);
                }

            }

            LedItem[] ledItems = data.ToArray();
            Array.Reverse(ledItems);

            leds.DelAllProgram();

            int i = 1;
            if (ledItems.Length > 0 )
            {
                foreach (LedItem item in ledItems)
                {
                    leds.AddTextToProgram(cardNum, 0, (i - 1) * 16, 16, 128, item.ToString(),i == 1 ? LED2008.GREEN:LED2008.RED,false);
                    i++;
                }
            }else
                leds.AddTextToProgram(cardNum, 0, (i - 1) * 16, 16, 128, "��ǰ���޲�������", LED2008.GREEN, false);
  
            leds.SendToScreen();
        }

        public void RefreshStockInLED(DataTable table,string ledNo)
        {
            int cardNum = Convert.ToInt32(ledNo);

            if (!IsOnLineLed(cardNum))
            {
                return;
            }

            int i = 1;
            leds.DelAllProgram();
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    leds.AddTextToProgram(cardNum, 0, (i - 1) * 16, 16, 128, row["QUANTITY"].ToString() + "-" + row["CIGARETTENAME"].ToString(),i == 1 ? LED2008.GREEN : LED2008.RED, false);
                    i++;
                }
            }
            else
            {
                leds.AddTextToProgram(cardNum, 0, (i - 1) * 16, 16, 128, "��ǰ������������", LED2008.GREEN, false);
            }       
                
            leds.SendToScreen();
        }

        public void RefreshStockInLED( string ledNo,string cigaretteName )
        {
            int cardNum = Convert.ToInt32(ledNo);

            if (!IsOnLineLed(cardNum))
            {
                return;
            }

            int i = 1;
            leds.DelAllProgram();

            leds.AddTextToProgram(cardNum, 0, (i - 1) * 16, 16, 128, "ȱ������,����⣺", LED2008.RED , false);
            i++;
            leds.AddTextToProgram(cardNum, 0, (i - 1) * 16, 16, 128, cigaretteName, LED2008.GREEN, false);

            leds.SendToScreen();
        }

        public void RefreshScannerLED(string ledNo, string sccanerCode, string cigaretteName)
        {
            int cardNum = Convert.ToInt32(ledNo);

            if (!IsOnLineLed(cardNum))
            {
                return;
            }

            int i = 1;
            leds.DelAllProgram();

            leds.AddTextToProgram(cardNum, 0, (i - 1) * 16, 16, 128, sccanerCode + "��ɨ������ǰ���̣�", LED2008.RED, false);
            i++;
            leds.AddTextToProgram(cardNum, 0, (i - 1) * 16, 16, 128, cigaretteName, LED2008.GREEN, false);

            leds.SendToScreen();
        }

        private bool IsOnLineLed(int ledNo)
        {
            if (isActiveLeds.ContainsKey(ledNo))
            {
                Microsoft.VisualBasic.Devices.Network network = new Microsoft.VisualBasic.Devices.Network();
                if (!network.Ping(isActiveLeds[ledNo]))
                {
                    THOK.MCP.Logger.Error(ledNo + "��LED�����ϣ����飡IP:[" + isActiveLeds[ledNo] + "]");
                    return false;
                }
                else
                    return true;
            }
            else 
                return false;
        }
    }
}

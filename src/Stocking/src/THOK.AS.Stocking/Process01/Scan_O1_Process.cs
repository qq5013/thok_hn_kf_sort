using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using THOK.AS.Stocking.Dao;
using THOK.AS.Stocking.Util;
using THOK.MCP;
using THOK.Util;

namespace THOK.AS.Stocking.Process
{
    public class Scan_O1_Process : AbstractProcess
    {      
        [Serializable]
        public class SerializableScannerParameters
        {
            private Dictionary<string, Dictionary<string, object>> Parameters = new Dictionary<string, Dictionary<string, object>>();

            public void SetParameter(string scannerCode,string parameterName,object parameterValue)
            {
                Dictionary<string, object> param = null;
                if (Parameters.ContainsKey(scannerCode))
                {
                    param = Parameters[scannerCode];
                }
                else
                {
                    param = new Dictionary<string, object>();
                }

                param[parameterName] = parameterValue;

                Parameters[scannerCode] = param;

                Serialize();
            }

            public object GetParameter(string scannerCode,string parameterName)
            {
                Dictionary<string, object> param = null;
                if (Parameters.ContainsKey(scannerCode))
                {
                    param = Parameters[scannerCode];
                    if (param.ContainsKey(parameterName))
                    {
                        return param[parameterName];
                    }
                    else
                        return null;
                }
                else
                    return null;
            }

            private void Serialize()
            {
                SerializableUtil.Serialize(true, @".\Scan_O1_ProcessParameters.sl", this);
            }

            public static SerializableScannerParameters Deserialize()
            {
                return SerializableUtil.Deserialize<SerializableScannerParameters>(true, @".\Scan_O1_ProcessParameters.sl"); 
            }

            public void Init()
            {
                Parameters.Clear();
                Serialize();
            }
        }

        private SerializableScannerParameters scannerParameters = new SerializableScannerParameters();

        public override void Initialize(Context context)
        {
            base.Initialize(context);
            scannerParameters = SerializableScannerParameters.Deserialize();
        }

        private string scan = "01";
        private bool isProcessingError = false;
        private int processingOrderNo = 0;
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  �������
             *  stateItem.Name �� "" - ������Ϣ��SickScan - ɨ������Ϣ�� StockPLC_01 - ���⻺����PLC��Ϣ
             *  stateItem.ItemName ��00 ��ʼ��ɨ���������� 01~05 �ֱ��Ӧ5��ɨ���������д���
             *  stateItem.State ��������
             *      Barcode������/NOREAD/RESCAN_OK      
             *      OrderNo����ǰ����˳��ţ����ϻָ�ʱ�����������������Ҫ��
            */

            try
            {
                string scannerCode = "";
                string barcode = "";
                int orderNo = 0;

                switch (stateItem.Name)
                {
                    case "":
                        switch (stateItem.ItemName)
                        {
                            case "Init":
                                scannerParameters.Init();
                                return;
                            case "01":
                                scannerCode = stateItem.ItemName;
                                if (stateItem.State != null && stateItem.State is Dictionary<string, string>)
                                {
                                    barcode = ((Dictionary<string, string>)stateItem.State)["barcode"];
                                    if (barcode == "NOREAD")
                                    {
                                        Logger.Error(string.Format("{0} ������ɨ�账��ʧ�ܣ����飺δɨ�����룡", scannerCode));
                                        return;
                                    }
                                    if (processingOrderNo == (scannerParameters.GetParameter(scannerCode, "OrderNo") != null ? (int)scannerParameters.GetParameter(scannerCode, "OrderNo") : 0) + 1)
                                    {
                                        processingOrderNo = 0;
                                        Scanner_Process_StockIn(scannerCode, barcode);
                                    } 
                                }
                                return;
                            default:
                                return;
                        }
                    case "SickScan":
                        switch (stateItem.ItemName)
                        {
                            case "01":
                                scannerCode = stateItem.ItemName;
                                if (stateItem.State != null && stateItem.State is Dictionary<string, string>)
                                {
                                    barcode = ((Dictionary<string, string>)stateItem.State)["barcode"];
                                    if (barcode == "NOREAD")
                                    {
                                        Logger.Error(string.Format("{0} ������ɨ�账��ʧ�ܣ����飺δɨ�����룡", scannerCode));
                                        return;
                                    }
                                    Scanner_Process_StockIn(scannerCode, barcode);
                                }
                                return;
                            default:
                                return;
                        }
                    case "StockPLC_01":
                        scannerCode = stateItem.ItemName.Split("_"[0])[1];  //���������ɨ������
                        string info = stateItem.ItemName.Split("_"[0])[0];  //
                        orderNo = Convert.ToInt32(THOK.MCP.ObjectUtil.GetObject(stateItem.State));
                        
                        object state = THOK.MCP.ObjectUtil.GetObject(Context.Services["StockPLC_01"].Read("ErrTag_01"));
                        int errTag = state != null ? Convert.ToInt32(state) : 0;

                        if (info == "ErrTag")
                        {
                            if (orderNo == 1)
                            {
                                return;
                            }
                            else
                            {
                                return;
                            }
                        }

                        int supplyAddress = scannerParameters.GetParameter(scannerCode, "SupplyAddress") != null ? (int)scannerParameters.GetParameter(scannerCode, "SupplyAddress") : 0;
                        int change = scannerParameters.GetParameter(scannerCode, "Change") != null ? (int)scannerParameters.GetParameter(scannerCode, "Change") : 0;
                        int orderNo_sl = scannerParameters.GetParameter(scannerCode, "OrderNo") != null ? (int)scannerParameters.GetParameter(scannerCode, "OrderNo") : 0;

                        if (orderNo == orderNo_sl)
                        {
                            int[] data = new int[3];
                            data[0] = supplyAddress;
                            data[1] = change;
                            data[2] = orderNo_sl;
                            WriteToService("StockPLC_01", "Scanner_DirectoryData_" + scannerCode, data);
                        }
                        else if (orderNo == orderNo_sl + 1)
                        {
                            if (!isProcessingError && errTag == 1)
                            {
                                isProcessingError = true;
                                processingOrderNo = orderNo_sl + 1;
                                WriteToProcess("buttonArea", "SimulateDialog", "01");
                            }
                        }
                        else if (orderNo != 0)
                        {
                            Logger.Error(string.Format(scannerCode + "��ɨ���������ϻָ�����ʧ�ܣ�ԭ��PLC��¼��ǰ����������ˮ��{0},��λ����¼��ǰ��ˮ��ӦΪ{1},���˹�ȷ�ˣ�", orderNo, orderNo_sl + 1));
                            ShowMessageBox(string.Format(scannerCode + "��ɨ���������ϻָ�����ʧ�ܣ�ԭ��PLC��¼��ǰ����������ˮ��{0},��λ����¼��ǰ��ˮ��ӦΪ{1},���˹�ȷ�ˣ�", orderNo, orderNo_sl + 1), "ѯ��", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        }
                        return;
                    default:
                        return;
                }
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("{0} ������ɨ�账��ʧ�ܣ�ԭ��{1} ",this.scan ,e.Message));
            }
        }

        private void Scanner_Process_StockIn(string scannerCode, string barcode)
        {
            if (barcode.Length == 32)
            {
                barcode = barcode.Substring(2, 6);
            }

            if (barcode.Length != 6)
            {
                string text = scannerCode + "������ɨ��������ʧ�ܣ����飺�����ʽ����ȷ��";
                ShowMessageBox(text, "ѯ��", MessageBoxButtons.OK, MessageBoxIcon.Question);
                Logger.Error(text);
                return;
            }

            using (PersistentManager pm = new PersistentManager())
            {
                StockInDao stockInDao = new StockInDao();
                StockInBatchDao stockInBatch = new StockInBatchDao();

                DataTable stockInTable = null;

                int pQuantity = scannerParameters.GetParameter(scannerCode, "Quantity") != null ? (int)scannerParameters.GetParameter(scannerCode, "Quantity") : 0;
                string pBarcode = scannerParameters.GetParameter(scannerCode, "Barcode") != null ? (string)scannerParameters.GetParameter(scannerCode, "Barcode") : "";

                stockInTable = stockInDao.FindCigarette();

                if (stockInTable.Rows.Count == 0)
                {
                    //��ʾ��ǰ���������������
                    string text = scannerCode + "������ɨ��������ʧ�ܣ����飺��ǰ���������������";
                    ShowMessageBox(text, "ѯ��", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    Logger.Error(text);
                    return;
                }

                if (barcode != pBarcode && pBarcode != string.Empty)
                {
                    //�Ƿ�ɻ�Ʒ�ƣ��������
                    if (pQuantity == 5)
                    {
                        //�ɻ�Ʒ����⣨δ��ɵ�Ʒ�ƿɣ��´�������⣩
                        stockInTable = stockInDao.FindCigarette(barcode);
                    }
                    else
                    {
                        //���ɻ�Ʒ����⡣
                        //������ʾ����Ҫ���˹�ȷ�ϡ����а�ؾ��̣���ԭ�ƻ��������̡�
                        //���˹�ǿ�и���Ʒ�ƣ���δ��ɵ����Ʒ�ƣ�ǿ�н���������������⣩��
                        stockInTable = stockInDao.FindCigarette(pBarcode);
                        if (stockInTable.Rows.Count != 0)
                        {
                            string text = string.Format("��ǰ�ƻ�������Ʒ��Ϊ��{0}�����Ƿ�ǿ�Ƹ�������Ʒ�ƣ���������Yes������������No��", stockInTable.Rows[0]["CIGARETTENAME"]);
                            if (DialogResult.Yes == MessageBox.Show(Application.OpenForms["MainForm"],text, "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            {
                                //ǿ�и���Ʒ�ơ�                          
                                stockInBatch.UpdateState(stockInTable.Rows[0]["BATCHNO"].ToString());

                                stockInTable = stockInDao.FindCigarette(barcode);

                                scannerParameters.SetParameter(scannerCode, "Barcode", "");
                                scannerParameters.SetParameter(scannerCode, "Quantity", 5);
                            }
                            else
                            {
                                //��ʾ���а�ؾ��̣���ԭ�ƻ��������̡����˳�����ɨ�����̡�
                                text = scannerCode + "������ɨ��������ʧ�ܣ����飺���ؾ��̣���ԭ�ƻ��������̣�";
                                ShowMessageBox(text, "ѯ��", MessageBoxButtons.OK, MessageBoxIcon.Question);
                                Logger.Error(text);
                                return;
                            }
                        }
                        else
                            stockInTable = stockInDao.FindCigarette(barcode);
                    }
                }
                else
                {
                    stockInTable = stockInDao.FindCigarette(barcode);
                }

                
                //�жϵ�ǰɨ�����������Ƿ�����������������û������ʾ�����а�ؾ��̣���ԭ�ƻ��������̡����˳�����ɨ�����̡�
                if (stockInTable.Rows.Count == 0)
                {
                    //��ʾ��ǰɨ��������̣����������������
                    string text = scannerCode + "������ɨ��������ʧ�ܣ����飺��ǰ�������������������";
                    Logger.Error(text);
                    ShowMessageBox(text, "ѯ��", MessageBoxButtons.OK, MessageBoxIcon.Question);                    

                    text = scannerCode + "������ɨ��������ʧ�ܣ����飺��ǰ���̿�����Ҫ���¸����룬��ȷ�ϣ�";
                    string cigaretteCode = "";
                    Scan(text, cigaretteCode, barcode);
                    return;
                }

                if (barcode == stockInTable.Rows[0]["BARCODE"].ToString())
                {
                    int [] data = new int [3];                

                    int supplyAddress = Convert.ToInt32(stockInTable.Rows[0]["CHANNELCODE"].ToString());                    
                    int change = Convert.ToInt32(stockInTable.Rows[0]["QUANTITY"]) <= 1 ? 1 : 0;
                    int orderNo = scannerParameters.GetParameter(scannerCode, "OrderNo") != null ? (int)scannerParameters.GetParameter(scannerCode, "OrderNo") : 0;

                    data[0] = supplyAddress;                    
                    data[1] = change;
                    data[2] = orderNo + 1;

                    if (WriteToService("StockPLC_01", "Scanner_DirectoryData_" + scannerCode, data))
                    {                       
                        scannerParameters.SetParameter(scannerCode, "SupplyAddress",supplyAddress);
                        scannerParameters.SetParameter(scannerCode, "Change", change);
                        scannerParameters.SetParameter(scannerCode, "OrderNo", orderNo + 1);

                        //�������ݿ�״̬
                        pm.BeginTransaction();
                        //����Ϊ����⣬�����µ�ǰ���������������
                        stockInDao.UpdateScanStatus(stockInTable.Rows[0]["STOCKINID"].ToString());
                        stockInBatch.UpdateQuantityForBatch(stockInTable.Rows[0]["BATCHNO"].ToString());
                        if (Convert.ToInt32(stockInTable.Rows[0]["QUANTITY"]) <= 1)
                        {
                            stockInBatch.UpdateState(stockInTable.Rows[0]["BATCHNO"].ToString());                            
                        }
                        pm.Commit();
                        //����ɨ����������Ϣ
                        if (pBarcode == barcode)
                        {
                            if (pQuantity < 5)
                            {
                                scannerParameters.SetParameter(scannerCode, "Quantity", pQuantity + 1);
                            }
                            else
                            {
                                scannerParameters.SetParameter(scannerCode, "Quantity", 1);
                                WriteToProcess("DataRequestProcess", "StockInRequest", 1);
                            }
                        }
                        else
                        {
                            scannerParameters.SetParameter(scannerCode, "Barcode", barcode);
                            scannerParameters.SetParameter(scannerCode, "Quantity", 1);
                            WriteToProcess("DataRequestProcess", "StockInRequest", 1);
                        }

                        if (Convert.ToInt32(stockInTable.Rows[0]["QUANTITY"]) <= 1)
                        {
                            scannerParameters.SetParameter(scannerCode, "Barcode", "");
                            scannerParameters.SetParameter(scannerCode, "Quantity", 5);
                            WriteToProcess("DataRequestProcess", "StockInRequest", 1);
                        }

                        WriteToProcess("LEDProcess", "Refresh_01", null);
                        Logger.Info(string.Format(scannerCode + "������ɨ�裬д�������ݣ���������:{0}��Ŀ��:{1} ��", stockInTable.Rows[0]["CIGARETTENAME"], Convert.ToInt32(stockInTable.Rows[0]["CHANNELCODE"].ToString())));
                    }
                    return;
                }
            }
        }

        private void Scan(string text, string cigaretteCode, string barcode)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("text", text);
            parameters.Add("cigaretteCode", cigaretteCode);
            parameters.Add("barcode", barcode);
            WriteToProcess("buttonArea", "ScanDialog", parameters);
        }

        private void ShowMessageBox(string msg, string title, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("msg", msg);
            parameters.Add("title", title);
            parameters.Add("messageBoxButtons", messageBoxButtons);
            parameters.Add("messageBoxIcon", messageBoxIcon);
            WriteToProcess("buttonArea", "MessageBox", parameters);
        }
    }
}

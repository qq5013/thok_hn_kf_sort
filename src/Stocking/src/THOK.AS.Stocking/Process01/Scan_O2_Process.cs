using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using THOK.MCP;
using THOK.Util;
using THOK.AS.Stocking.Dao;

namespace THOK.AS.Stocking.Process
{
    public class Scan_O2_Process : AbstractProcess
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
                Util.SerializableUtil.Serialize(true, @".\SerializableScannerParameters.sl", this);
            }

            public static SerializableScannerParameters Deserialize()
            {
                return Util.SerializableUtil.Deserialize<SerializableScannerParameters>(true, @".\SerializableScannerParameters.sl"); 
            }

            public void Init()
            {
                Parameters.Clear();
                Serialize();
            }
        }

        private string scan = "";
        private SerializableScannerParameters scannerParameters = new SerializableScannerParameters();
        public static bool isProcessingError = false;

        public override void Initialize(Context context)
        {
            base.Initialize(context);
            scannerParameters = SerializableScannerParameters.Deserialize();
        }

        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  �������
             *  stateItem.ItemName ��00 ��ʼ��ɨ���������� 01~05 �ֱ��Ӧ5��ɨ���������д���
             *  stateItem.State ��������
             *      barcode������/NOREAD/RESCAN_OK      
             *      OrderNo����ǰ����˳��ţ����ϻָ�ʱ�����������������Ҫ��
            */

            try
            {
                Dictionary<string, string> parameters = null;
                string scannerCode = "";
                string barcode = "";
                int orderNo = 0;
                switch (stateItem.Name)
                {
                    case "":
                        if (stateItem.ItemName == "ErrReset")
                        {
                            isProcessingError = false;
                            return;
                        }

                        scannerCode = stateItem.ItemName;
                        if (stateItem.State != null && stateItem.State is Dictionary<string, string>)
                        {
                            parameters = (Dictionary<string, string>)stateItem.State;
                            barcode = parameters["barcode"];
                        }
                        break;
                    case "SickScan":
                        scannerCode = stateItem.ItemName;
                        parameters = (Dictionary<string, string>)stateItem.State;
                        barcode = parameters["barcode"];
                        break;
                    case "StockPLC_01":
                        scannerCode = stateItem.ItemName.Split("_"[0])[1];
                        string info = stateItem.ItemName.Split("_"[0])[0];

                        orderNo = Convert.ToInt32(THOK.MCP.ObjectUtil.GetObject(stateItem.State));
                        object state = THOK.MCP.ObjectUtil.GetObject(Context.Services["StockPLC_01"].Read("ErrTag_01"));
                        int errTag = state != null ? Convert.ToInt32(state) : 0;

                        if (info == "ErrTag" && orderNo == 1)
                        {
                            //ShowMessageBox("ɨ��ʧ�ܣ����ֹ���λ��", "ѯ��", MessageBoxButtons.OK, MessageBoxIcon.Question);                        
                            return;
                        }
                        else if(info == "ErrTag")
                        {
                            return;
                        }


                        int supplyAddress = scannerParameters.GetParameter(scannerCode, "SupplyAddress") != null ? (int)scannerParameters.GetParameter(scannerCode, "SupplyAddress") : 0;
                        int change = scannerParameters.GetParameter(scannerCode, "Change") != null ? (int)scannerParameters.GetParameter(scannerCode, "Change") : 0;
                        int orderNo_sl = scannerParameters.GetParameter(scannerCode, "OrderNo") != null ? (int)scannerParameters.GetParameter(scannerCode, "OrderNo") : 0;

                        if (orderNo == orderNo_sl)
                        {
                            int [] data = new int [3];
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
                                WriteToProcess("buttonArea", "SimulateDialog", "01");
                            }
                        }
                        else if (orderNo != 0)
                        {
                            Logger.Error(string.Format(scannerCode + "��ɨ���������ϻָ�����ʧ�ܣ�ԭ��PLC��¼��ǰ����������ˮ��{0},��λ����¼��ǰ��ˮ��ӦΪ{1},���˹�ȷ�ˣ�", orderNo, orderNo_sl + 1));
                            ShowMessageBox(string.Format(scannerCode + "��ɨ���������ϻָ�����ʧ�ܣ�ԭ��PLC��¼��ǰ����������ˮ��{0},��λ����¼��ǰ��ˮ��ӦΪ{1},���˹�ȷ�ˣ�", orderNo, orderNo_sl + 1), "ѯ��", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        }
                        return;
                        break;
                    case "StockPLC_02":
                        scannerCode = stateItem.ItemName.Split("_"[0])[1];
                        barcode = stateItem.ItemName.Split("_"[0])[0];
                        orderNo = Convert.ToInt32(THOK.MCP.ObjectUtil.GetObject(stateItem.State));
                        if (orderNo == 0)
                        {
                            WriteToProcess("LEDProcess", "Refresh_01", null);
                            return;
                        }
                        break;
                    default:
                        return;
                        break;
                }

                switch (scannerCode)
                {
                    case "Init":
                        scannerParameters.Init();
                        return;
                        break;
                    case "01":
                        if (barcode == "NOREAD")
                        {
                            Logger.Error(scannerCode + "������ɨ�账��ʧ�ܣ����飺δɨ�����룡");
                            return;
                        }
                        Scanner_Process_StockIn(scannerCode, barcode);
                        break;
                    default:
                        if (barcode == "NOREAD")
                        {
                            Logger.Error(scannerCode + "������ɨ��������ʧ�ܣ����飺δɨ�����룡");
                            return;
                        }
                        if (barcode == "ReScanOk" || barcode == "Show")
                        {
                            int orderNo_sl = scannerParameters.GetParameter(scannerCode, "OrderNo") != null ? (int)scannerParameters.GetParameter(scannerCode, "OrderNo") : 0;
                            int supplyAddress = scannerParameters.GetParameter(scannerCode, "SupplyAddress") != null ? (int)scannerParameters.GetParameter(scannerCode, "SupplyAddress") : 0;
                            string cigaretteName = scannerParameters.GetParameter(scannerCode, "CigaretteName") != null ? (string)scannerParameters.GetParameter(scannerCode, "CigaretteName") : "";
                            if (orderNo == orderNo_sl && supplyAddress != 0 && cigaretteName != "")
                            {
                                if (barcode == "ReScanOk")
                                {
                                    int[] data = new int[2];
                                    data[0] = supplyAddress;
                                    data[1] = orderNo;

                                    WriteToService("StockPLC_02", "Scanner_DirectoryData_" + scannerCode, data);
                                }
                                else
                                {
                                    WriteToProcess("LEDProcess", "Show_Scanner_" + scannerCode, cigaretteName);
                                }
                                return;
                            }
                            else if (orderNo != orderNo_sl + 1)
                            {
                                Logger.Error(string.Format(scannerCode + "��ɨ���������ϻָ�����ʧ�ܣ�ԭ��PLC��¼��ǰ����������ˮ��{0},��λ����¼��ǰ��ˮ��ӦΪ{1},���˹�ȷ�ˣ�", orderNo, orderNo_sl + 1));
                                ShowMessageBox(string.Format(scannerCode + "��ɨ���������ϻָ�����ʧ�ܣ�ԭ��PLC��¼��ǰ����������ˮ��{0},��λ����¼��ǰ��ˮ��ӦΪ{1},���˹�ȷ�ˣ�", orderNo, orderNo_sl + 1), "ѯ��", MessageBoxButtons.OK, MessageBoxIcon.Question);
                                return;
                            }
                        }
                        Scanner_Process_StockOut(scannerCode, barcode);
                        break;
                }

            }
            catch (Exception e)
            {
                Logger.Error("��������ɨ�账��ʧ�ܣ�ԭ��" + e.Message);
            }
        }

        private void Scanner_Process_StockIn(string scannerCode, string barcode)
        {
            if (barcode.Length == 32)
            {
                barcode = barcode.Substring(2, 6);
                Logger.Info(barcode);
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

                    text = scannerCode + "������ɨ��������ʧ�ܣ����飺��ǰ������Ҫ���¸����룬��ȷ�ϣ�";
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

        private void Scanner_Process_StockOut(string scannerCode, string barcode)
        {
            //ȡ������,����ѯ AS_STOCK_OUT ���Ƿ��뵱ǰӦ��ͨ���ľ���һ�¡���һ���·����ݸ�PLC���粻��ȷ��¼��¼��־��
            using (PersistentManager pm = new PersistentManager())
            {
                StockOutDao stockOutDao = new StockOutDao();

                DataTable outTable = stockOutDao.FindCigaretteForScanner(scannerCode);

                if (outTable.Rows.Count != 0)
                {
                    if (barcode.Length ==32)
                    {
                        barcode = barcode.Substring(2, 6);
                    }

                    if (barcode == "ReScanOk")
                    {
                        barcode = outTable.Rows[0]["BARCODE"].ToString();
                        Logger.Info(scannerCode + "������ɨ���������ϻָ���");
                    }

                    if (barcode == "Show")
                    {
                        WriteToProcess("LEDProcess", "Show_Scanner_" + scannerCode, outTable.Rows[0]["CIGARETTENAME"]);
                        return;
                    }

                    if (barcode != outTable.Rows[0]["BARCODE"].ToString())
                    {
                        string text = string.Format(scannerCode + "ɨ��������ǰ����Ʒ��Ϊ��{0}����ɨ������Ϊ ��{1}�� ,������Ϊ ��{2}����", outTable.Rows[0]["CIGARETTENAME"], barcode, outTable.Rows[0]["BARCODE"].ToString());
                        WriteToProcess("LEDProcess", "Show_Scanner_" + scannerCode,"ɨ�������ȷ�ϣ�");

                        Logger.Error(scannerCode + "������ɨ�账��ʧ�ܣ����飺" + text);
                        string cigaretteCode = outTable.Rows[0]["CIGARETTECODE"].ToString();
                        Scan(text, cigaretteCode,barcode);
                        return;
                    }

                    if (barcode == outTable.Rows[0]["BARCODE"].ToString())
                    {
                        try
                        {
                            //дPLC       
                            int lineCode = Convert.ToInt32(outTable.Rows[0]["LINECODE"]);

                            switch (Context.Attributes["SupplyToSortLine"].ToString())
                            {
                                case "01":
                                    lineCode = 1;
                                    break;
                                case "02":
                                    lineCode = 2;
                                    break;
                                default:
                                    break;
                            }

                            int supplyAddress = Convert.ToInt32(lineCode.ToString() + outTable.Rows[0]["SUPPLYADDRESS"].ToString().PadLeft(2, "0"[0]));
                            int OrderNo = scannerParameters.GetParameter(scannerCode, "OrderNo") != null ? (int)scannerParameters.GetParameter(scannerCode, "OrderNo") : 0;

                            int[] data = new int[2];
                            data[0] = supplyAddress;
                            data[1] = OrderNo +1;

                            if (WriteToService("StockPLC_02", "Scanner_DirectoryData_" + scannerCode, data))
                            {
                                pm.BeginTransaction();
                                //����Ϊ��ɨ��
                                stockOutDao.UpdateScanStatus(outTable.Rows[0]["STOCKOUTID"].ToString(),scannerCode);
                                pm.Commit();
                                
                                scannerParameters.SetParameter(scannerCode, "OrderNo", OrderNo + 1);
                                scannerParameters.SetParameter(scannerCode, "SupplyAddress", supplyAddress);
                                scannerParameters.SetParameter(scannerCode, "CigaretteName", outTable.Rows[0]["CIGARETTENAME"]);

                                if (scannerCode == "03")
                                {
                                    WriteToProcess("LEDProcess", "Refresh_02_MoveNext", null);
                                }                                

                                Logger.Info(string.Format( scannerCode + "������ɨ�裬д�������ݳɹ�����������:{0}��Ŀ��:{1} ��", outTable.Rows[0]["CIGARETTENAME"], data[0].ToString() + data[1].ToString()));
                            }
                            else
                                Logger.Error(string.Format(scannerCode + "������ɨ�裬д��������ʧ�ܣ���������:{0}��Ŀ��:{1} ��", outTable.Rows[0]["CIGARETTENAME"], data));
                        }
                        catch (Exception e)
                        {
                            Logger.Error(scannerCode + "������ɨ�裬д��������ʧ�ܣ�ԭ��" + e.Message);
                            pm.Rollback();
                        }
                    }
                }
                else
                {
                    Logger.Error(scannerCode + "������ɨ�裬û�в�����������PLC�˶Բ�����Ϣ��");
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

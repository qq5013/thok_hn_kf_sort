using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.MCP.Config;
using THOK.Util;
using THOK.ParamUtil;

namespace THOK.AS.Stocking.View
{
    public partial class ParameterForm : THOK.AF.View.ToolbarForm
    {
        private Parameter parameter = new Parameter();
        private DBConfigUtil config = new DBConfigUtil("DefaultConnection", "SQLSERVER");
        private DBConfigUtil serverConfig = new DBConfigUtil("ServerConnection", "SQLSERVER");
        private THOK.MCP.Service.UDP.Config.Configuration udpConfig = new THOK.MCP.Service.UDP.Config.Configuration("UDP.xml");
        private THOK.MCP.Service.Sick.Config.Configuration sickConfig = new THOK.MCP.Service.Sick.Config.Configuration("Scanner.xml");

        private Dictionary<string, string> attributes = null;

        public ParameterForm()
        {
            InitializeComponent();
            ReadParameter();
        }

        private void ReadParameter()
        {
            //��ȡContext�����ļ�LED��ʾ������
            ConfigUtil configUtil = new ConfigUtil();
            attributes = configUtil.GetAttribute();
            parameter.LED_01_CHANNELCODE = attributes["LED_01_CHANNELCODE"];
            parameter.LED_02_CHANNELCODE = attributes["LED_02_CHANNELCODE"];
            parameter.SupplyToSortLine = attributes["SupplyToSortLine"];

            //�������ݿ����Ӳ���
            parameter.ServerName = config.Parameters["server"].ToString();
            parameter.DBName = config.Parameters["database"].ToString();
            parameter.DBUser = config.Parameters["uid"].ToString();
            parameter.Password = config.Parameters["password"].ToString();

            //���������ݿ����Ӳ���
            parameter.RemoteServerName = serverConfig.Parameters["server"].ToString();
            parameter.RemoteDBName = serverConfig.Parameters["database"].ToString();
            parameter.RemoteDBUser = serverConfig.Parameters["uid"].ToString();
            parameter.RemotePassword = serverConfig.Parameters["password"].ToString();

            //��ȡUDP�����ļ�����
            parameter.IP = udpConfig.IP;
            parameter.Port = udpConfig.Port;

            //��ȡScanner.xml�����ļ�����
            parameter.PortName = sickConfig.PortName.ToString();
            parameter.BaudRate = sickConfig.BaudRate.ToString();
            parameter.DataBits = sickConfig.DataBits.ToString();
            parameter.Parity = sickConfig.Parity.ToString();
            parameter.StopBits = sickConfig.StopBits.ToString();            

            propertyGrid.SelectedObject = parameter;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //����Context�����ļ�LED��ʾ������
                attributes["LED_01_CHANNELCODE"] = parameter.LED_01_CHANNELCODE;
                attributes["LED_02_CHANNELCODE"] = parameter.LED_02_CHANNELCODE;
                attributes["SupplyToSortLine"] = parameter.SupplyToSortLine;

                ConfigUtil configUtil = new ConfigUtil();
                configUtil.Save(attributes);

                //���汾�����ݿ����Ӳ���
                config.Parameters["server"] = parameter.ServerName;
                config.Parameters["database"] = parameter.DBName;
                config.Parameters["uid"] = parameter.DBUser;
                config.Parameters["Password"] = config.Parameters["Password"].ToString() == parameter.Password?parameter.Password: THOK.Util.Coding.Encoding(parameter.Password);
                config.Save();

                //������������ݿ����Ӳ���
                serverConfig.Parameters["server"] = parameter.RemoteServerName;
                serverConfig.Parameters["database"] = parameter.RemoteDBName;
                serverConfig.Parameters["uid"] = parameter.RemoteDBUser;
                serverConfig.Parameters["Password"] = serverConfig.Parameters["Password"].ToString() == parameter.RemotePassword ? parameter.RemotePassword:THOK.Util.Coding.Encoding(parameter.RemotePassword);
                serverConfig.Save();   

                //����UDP����
                udpConfig.IP = parameter.IP;
                udpConfig.Port = parameter.Port;
                udpConfig.Save();

                //����ɨ��������
                sickConfig.PortName = parameter.PortName;
                sickConfig.BaudRate = Convert.ToInt32(parameter.BaudRate);
                sickConfig.DataBits = Convert.ToInt32(parameter.DataBits) > 8 || Convert.ToInt32(parameter.DataBits) < 5 ? 8 : Convert.ToInt32(parameter.DataBits);
                sickConfig.Parity = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), parameter.Parity);
                sickConfig.StopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits), parameter.StopBits);
                sickConfig.Save();

                MessageBox.Show("ϵͳ��������ɹ���������������ϵͳ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                MessageBox.Show("����ϵͳ���������г����쳣��ԭ��" + exp.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.Util;
using THOK.MCP.Config;

namespace THOK.AS.OTS.View
{
    public partial class ParameterForm : Form
    {
        private Parameter parameter = new Parameter();
        private DBConfigUtil config = new DBConfigUtil("DefaultConnection", "SQLSERVER");
        private DBConfigUtil serverConfig = new DBConfigUtil("InfoServerConnection", "SQLSERVER");
        private ConfigUtil configUtil = new ConfigUtil();
        private THOK.MCP.Service.UDP.Config.Configuration udpConfig = new THOK.MCP.Service.UDP.Config.Configuration("UDP.xml");

        Dictionary<string, string> sysParam = null;

        public ParameterForm()
        {
            InitializeComponent();
            ReadParameter();
        }

        private void ReadParameter()
        {
            //�ּ����ݿ����Ӳ���
            parameter.ServerName = config.Parameters["server"].ToString();
            parameter.DBName = config.Parameters["database"].ToString();
            parameter.DBUser = config.Parameters["uid"].ToString();
            parameter.Password = config.Parameters["password"].ToString();

            //������Ϣϵͳ���Ӳ���
            parameter.InfoSystemServerName=serverConfig.Parameters["server"].ToString();
            parameter.InfoDBName=serverConfig.Parameters["database"].ToString();
            parameter.InfoDBuser=serverConfig.Parameters["uid"].ToString();
            parameter.InfoPassword=serverConfig.Parameters["password"].ToString();

            sysParam = configUtil.GetAttribute();
            parameter.PrintLabel = Convert.ToBoolean(sysParam["PrintLabel"]);

            parameter.IP = udpConfig.IP;
            parameter.Port = udpConfig.Port;

            propertyGrid.SelectedObject = parameter;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //����ּ����ݿ����
                config.Parameters["server"] = parameter.ServerName;
                config.Parameters["database"] = parameter.DBName;
                config.Parameters["uid"] = parameter.DBUser;
                config.Parameters["password"] = config.Parameters["Password"].ToString() == parameter.Password ? parameter.Password : THOK.Util.Coding.Encoding(parameter.Password);
                config.Save();

                //���������Ϣϵͳ���ݿ����
                serverConfig.Parameters["server"]=parameter.InfoSystemServerName;
                serverConfig.Parameters["database"]=parameter.InfoDBName;
                serverConfig.Parameters["uid"]=parameter.InfoDBuser;
                serverConfig.Parameters["password"] = serverConfig.Parameters["Password"].ToString() == parameter.Password ? parameter.Password : THOK.Util.Coding.Encoding(parameter.Password);
                serverConfig.Save();

                sysParam["PrintLabel"] = parameter.PrintLabel.ToString();
                configUtil.Save(sysParam);

                udpConfig.IP = parameter.IP;
                udpConfig.Port = parameter.Port;
                udpConfig.Save();
                MessageBox.Show("ϵͳ��������ɹ���������������ϵͳ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                MessageBox.Show("����ϵͳ���������г����쳣��ԭ��" + exp.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
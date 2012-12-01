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
    public partial class SortingLedParameterForm : THOK.AF.View.ToolbarForm
    {
        private SortingLedParameter parameter = new SortingLedParameter();
        private THOK.MCP.Service.UDP.Config.Configuration udpConfig = new THOK.MCP.Service.UDP.Config.Configuration("UDP.xml");

        private Dictionary<string, string> attributes = null;

        public SortingLedParameterForm()
        {
            InitializeComponent();
            ReadParameter();
        }

        private void ReadParameter()
        {
            //��ȡContext�����ļ�����
            ConfigUtil configUtil = new ConfigUtil();
            attributes = configUtil.GetAttribute();
            parameter.MaxLEDFormTitle = attributes["MaxLEDFormTitle"];
            parameter.MaxLEDFormTop = attributes["MaxLEDFormTop"];
            parameter.MaxLEDFormLeft = attributes["MaxLEDFormLeft"];
            parameter.MaxLEDFormWidth = attributes["MaxLEDFormWidth"];
            parameter.MaxLEDFormHeight = attributes["MaxLEDFormHeight"];

            //��ȡUDP�����ļ�����
            parameter.IP = udpConfig.IP;
            parameter.Port = udpConfig.Port;        

            propertyGrid.SelectedObject = parameter;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //����Context����
                attributes["MaxLEDFormTitle"] = parameter.MaxLEDFormTitle;
                attributes["MaxLEDFormTop"] = parameter.MaxLEDFormTop;
                attributes["MaxLEDFormLeft"] = parameter.MaxLEDFormLeft;
                attributes["MaxLEDFormWidth"] = parameter.MaxLEDFormWidth;
                attributes["MaxLEDFormHeight"] = parameter.MaxLEDFormHeight;

                ConfigUtil configUtil = new ConfigUtil();
                configUtil.Save(attributes);

                //����UDP����
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }
    }
}


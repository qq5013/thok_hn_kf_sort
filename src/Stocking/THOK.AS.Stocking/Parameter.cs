using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using THOK.ParamUtil;

namespace THOK.AS.Stocking
{
    public class Parameter: BaseObject
    {
        private string serverName;

        [CategoryAttribute("�������ݿ����Ӳ���"), DescriptionAttribute("���ݿ����������"), Chinese("����������")]
        public string ServerName
        {
            get { return serverName; }
            set { serverName = value; }
        }

        private string dbName;

        [CategoryAttribute("�������ݿ����Ӳ���"), DescriptionAttribute("���ݿ�����"), Chinese("���ݿ���")]
        public string DBName
        {
            get { return dbName; }
            set { dbName = value; }
        }

        private string dbUser;

        [CategoryAttribute("�������ݿ����Ӳ���"), DescriptionAttribute("���ݿ������û���"), Chinese("�û���")]
        public string DBUser
        {
            get { return dbUser; }
            set { dbUser = value; }
        }
        private string password;

        [CategoryAttribute("�������ݿ����Ӳ���"), DescriptionAttribute("���ݿ���������"), Chinese("����")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string remoteServerName;

        [CategoryAttribute("���������ݿ����Ӳ���"), DescriptionAttribute("���ݿ����������"), Chinese("����������")]
        public string RemoteServerName
        {
            get { return remoteServerName; }
            set { remoteServerName = value; }
        }

        private string remoteDBName;

        [CategoryAttribute("���������ݿ����Ӳ���"), DescriptionAttribute("���ݿ�����"), Chinese("���ݿ���")]
        public string RemoteDBName
        {
            get { return remoteDBName; }
            set { remoteDBName = value; }
        }

        private string remoteDBUser;

        [CategoryAttribute("���������ݿ����Ӳ���"), DescriptionAttribute("���ݿ������û���"), Chinese("�û���")]
        public string RemoteDBUser
        {
            get { return remoteDBUser; }
            set { remoteDBUser = value; }
        }
        private string remotePassword;

        [CategoryAttribute("���������ݿ����Ӳ���"), DescriptionAttribute("���ݿ���������"), Chinese("����")]
        public string RemotePassword
        {
            get { return remotePassword; }
            set { remotePassword = value; }
        }

        private int port;

        [CategoryAttribute("����ϵͳͨ�Ų���"), DescriptionAttribute("����ϵͳ�����˿�"), Chinese("�����˿�")]
        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        private string ip;

        [CategoryAttribute("����ϵͳͨ�Ų���"), DescriptionAttribute("����ϵͳIP��ַ"), Chinese("IP��ַ")]
        public string IP
        {
            get { return ip; }
            set { ip = value; }
        }

        private string portName;

        [CategoryAttribute("ɨ����ͨ�Ų���"), DescriptionAttribute("ɨ�������ں�"), Chinese("���ں�")]
        public string PortName
        {
            get { return portName; }
            set { portName = value; }
        }

        private string baudRate;

        [CategoryAttribute("ɨ����ͨ�Ų���"), DescriptionAttribute("ɨ����������"), Chinese("������")]
        public string BaudRate
        {
            get { return baudRate; }
            set { baudRate = value; }
        }

        private string parity;

        [CategoryAttribute("ɨ����ͨ�Ų���"), DescriptionAttribute("ɨ��������λ"), Chinese("����λ")]
        public string Parity
        {
            get { return parity; }
            set { parity = value; }
        }

        private string dataBits;

        [CategoryAttribute("ɨ����ͨ�Ų���"), DescriptionAttribute("ɨ��������λ"), Chinese("����λ")]
        public string DataBits 
        {
            get { return dataBits; }
            set { dataBits = value; }
        }

        private string stopBits;

        [CategoryAttribute("ɨ����ͨ�Ų���"), DescriptionAttribute("ɨ����ֹͣλ"), Chinese("ֹͣλ")]
        public string StopBits 
        {
            get { return stopBits; }
            set { stopBits = value; }
        }

        private string led_01_ChannelCode = "";

        [CategoryAttribute("LED��ʾ������"), DescriptionAttribute("һ�����̵�����"), Chinese("һ�����̵�����")]
        public string LED_01_CHANNELCODE
        {
            get { return led_01_ChannelCode; }
            set { led_01_ChannelCode = value; }
        }

        private string led_02_ChannelCode = "";

        [CategoryAttribute("LED��ʾ������"), DescriptionAttribute("�������̵�����"), Chinese("�������̵�����")]
        public string LED_02_CHANNELCODE
        {
            get { return led_02_ChannelCode; }
            set { led_02_ChannelCode = value; }
        }

        private string supplyToSortLine = "";

        [CategoryAttribute("��������ǿ�Ƹ�Ԥ����"), DescriptionAttribute("�ּ��ߴ����00���Զ�������Ԥ����01��һ���ߣ�02�������ߣ���"), Chinese("�ּ��ߴ����00���Զ�������Ԥ����01��һ���ߣ�02�������ߣ���")]
        public string SupplyToSortLine
        {
            get { return supplyToSortLine; }
            set { supplyToSortLine = value; }
        }
    }
}

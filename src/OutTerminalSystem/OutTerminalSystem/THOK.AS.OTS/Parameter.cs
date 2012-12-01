using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using THOK.ParamUtil;

namespace THOK.AS.OTS
{
    public class Parameter: BaseObject
    {
        private string serverName;

        [CategoryAttribute("���ݿ����Ӳ���"), DescriptionAttribute("���ݿ����������"), Chinese("����������")]
        public string ServerName
        {
            get { return serverName; }
            set { serverName = value; }
        }

        private string dbName;

        [CategoryAttribute("���ݿ����Ӳ���"), DescriptionAttribute("���ݿ�����"), Chinese("���ݿ���")]
        public string DBName
        {
            get { return dbName; }
            set { dbName = value; }
        }

        private string dbUser;

        [CategoryAttribute("���ݿ����Ӳ���"), DescriptionAttribute("���ݿ������û���"), Chinese("�û���")]
        public string DBUser
        {
            get { return dbUser; }
            set { dbUser = value; }
        }
        private string password;

        [CategoryAttribute("���ݿ����Ӳ���"), DescriptionAttribute("���ݿ���������"), Chinese("����")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        //������Ϣϵͳ���ݿ����Ӳ���
        private string infoSystemServerName;

        [CategoryAttribute("������Ϣϵͳ���ݿ����Ӳ���"), DescriptionAttribute("���ݿ����������"), Chinese("����������")]
        public string InfoSystemServerName
        {
            get { return infoSystemServerName; }
            set { infoSystemServerName = value; }
        }

        private string infoDBName;

        [CategoryAttribute("������Ϣϵͳ���ݿ����Ӳ���"), DescriptionAttribute("���ݿ�����"), Chinese("���ݿ���")]
        public string InfoDBName
        {
            get { return infoDBName; }
            set { infoDBName = value; }
        }

        private string infoDBuser;

        [CategoryAttribute("������Ϣϵͳ���ݿ����Ӳ���"), DescriptionAttribute("���ݿ������û���"), Chinese("�û���")]
        public string InfoDBuser
        {
            get { return infoDBuser; }
            set { infoDBuser = value; }
        }

        private string infoPassword;

        [CategoryAttribute("������Ϣϵͳ���ݿ����Ӳ���"), DescriptionAttribute("���ݿ���������"), Chinese("����")]
        public string InfoPassword
        {
            get { return infoPassword; }
            set { infoPassword = value; }
        }

        private bool printLabel;

        [CategoryAttribute("��ǩ��ӡ����"), DescriptionAttribute("�Ƿ�ʵʱ��ӡ�ͻ���ǩ"), Chinese("ʵʱ��ӡ��ǩ")]
        public bool PrintLabel
        {
            get { return printLabel; }
            set { printLabel = value; }
        }

        private string ip;
        [CategoryAttribute("ͨ�Ų���"), DescriptionAttribute("������ַ"), Chinese("IP��ַ")]
        public string IP
        {
            get { return ip; }
            set { ip = value; }
        }

        private int port;
        [CategoryAttribute("ͨ�Ų���"), DescriptionAttribute("�����˿�"), Chinese("�˿�")]
        public int Port
        {
            get { return port; }
            set { port = value; }
        }
    }
}

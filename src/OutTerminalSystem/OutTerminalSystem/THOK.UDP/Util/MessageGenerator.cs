using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace THOK.UDP.Util
{
    /// <summary>
    /// ��Ϣ����
    /// </summary>
    public class MessageGenerator
    {
        private string command = null;
        private string sender = null;
        private List<string> receivers = new List<string>();
        private Dictionary<string, string> parameters = new Dictionary<string, string>();

        public MessageGenerator(string command, string sender)
        {
            this.command = command;
            this.sender = sender;
        }

        /// <summary>
        /// ������Ϣ������
        /// </summary>
        /// <param name="sender">��Ϣ������</param>
        public void SetSender(string sender)
        {
            this.sender = sender;
        }

        /// <summary>
        /// ������Ϣ����
        /// </summary>
        /// <param name="command">��Ϣ����</param>
        public void SetCommand(string command)
        {
            this.command = command;
        }

        /// <summary>
        /// �����Ϣ������
        /// </summary>
        /// <param name="receiver">��Ϣ������</param>
        public void AddReceiver(string receiver)
        {
            if (!receivers.Contains(receiver))
                receivers.Add(receiver);
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="paramName">��������</param>
        /// <param name="paramValue">����ֵ</param>
        public void AddParameter(string paramName, string paramValue)
        {
            parameters.Add(paramName, paramValue);
        }

        /// <summary>
        /// ���������Ϣ
        /// </summary>
        public void Clear()
        {
            command = null;
            sender = null;
            receivers.Clear();
            parameters.Clear();
        }

        /// <summary>
        /// �������ɵ���Ϣ
        /// </summary>
        /// <returns>���ɵ���Ϣ</returns>
        public string GetMessage()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rootNode = doc.CreateElement("Message");
            //���Sender�ڵ�
            rootNode.AppendChild(GetSender(doc));

            //���Receivers�ڵ�
            rootNode.AppendChild(GetReceiver(doc));

            //���Command�ڵ�
            rootNode.AppendChild(GetCommand(doc));

            //���Parameters�ڵ�
            rootNode.AppendChild(GetParameter(doc));

            doc.AppendChild(rootNode);
            return doc.OuterXml;
        }

        private XmlElement GetSender(XmlDocument doc)
        {
            XmlElement node = null;
            if (sender == null)
            {
                throw new Exception("δ������Ϣ�����ߣ�����������Ϣ��");
            }
            else
            {
                node = doc.CreateElement("Sender");
                node.InnerText = sender;
            }
            return node;
        }

        private XmlElement GetCommand(XmlDocument doc)
        {
            XmlElement node = null;
            if (command == null)
            {
                throw new Exception("δ�����������������Ϣ��");
            }
            else
            {
                node = doc.CreateElement("Command");
                node.InnerText = command;
            }
            return node;
        }

        private XmlElement GetReceiver(XmlDocument doc)
        {
            XmlElement rootNode = doc.CreateElement("Receivers");
            for (int i = 0; i < receivers.Count; i++)
            {
                XmlElement node = doc.CreateElement("Receiver");
                node.InnerText = receivers[i].ToString();
                rootNode.AppendChild(node);
            }
            return rootNode;
        }

        private XmlElement GetParameter(XmlDocument doc)
        {
            XmlElement rootNode = doc.CreateElement("Parameters");
            foreach (string key in parameters.Keys)
            {
                string v = parameters[key];
                XmlElement node = doc.CreateElement(key);
                node.InnerText = v;
                rootNode.AppendChild(node);
            }
            return rootNode;
        }

    }
}

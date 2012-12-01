using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace THOK.UDP.Util
{
    /// <summary>
    /// 消息生成
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
        /// 设置消息发送者
        /// </summary>
        /// <param name="sender">消息发送者</param>
        public void SetSender(string sender)
        {
            this.sender = sender;
        }

        /// <summary>
        /// 设置消息命令
        /// </summary>
        /// <param name="command">消息命令</param>
        public void SetCommand(string command)
        {
            this.command = command;
        }

        /// <summary>
        /// 添加消息接收者
        /// </summary>
        /// <param name="receiver">消息接收者</param>
        public void AddReceiver(string receiver)
        {
            if (!receivers.Contains(receiver))
                receivers.Add(receiver);
        }

        /// <summary>
        /// 添加命令参数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="paramValue">参数值</param>
        public void AddParameter(string paramName, string paramValue)
        {
            parameters.Add(paramName, paramValue);
        }

        /// <summary>
        /// 清除所有信息
        /// </summary>
        public void Clear()
        {
            command = null;
            sender = null;
            receivers.Clear();
            parameters.Clear();
        }

        /// <summary>
        /// 返回生成的消息
        /// </summary>
        /// <returns>生成的消息</returns>
        public string GetMessage()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rootNode = doc.CreateElement("Message");
            //添加Sender节点
            rootNode.AppendChild(GetSender(doc));

            //添加Receivers节点
            rootNode.AppendChild(GetReceiver(doc));

            //添加Command节点
            rootNode.AppendChild(GetCommand(doc));

            //添加Parameters节点
            rootNode.AppendChild(GetParameter(doc));

            doc.AppendChild(rootNode);
            return doc.OuterXml;
        }

        private XmlElement GetSender(XmlDocument doc)
        {
            XmlElement node = null;
            if (sender == null)
            {
                throw new Exception("未设置消息发送者，不能生成消息。");
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
                throw new Exception("未设置命令，不能生成消息。");
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

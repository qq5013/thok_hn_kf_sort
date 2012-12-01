using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace THOK.UDP.Util
{
    public class MessageParser
    {
        public Message Parse(string msg)
        {
            Message message = null;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(msg);
                string sender = GetSender(doc);
                string command = GetCommand(doc);
                List<string> receivers = GetReceivers(doc);
                Dictionary<string, string> parameters = GetParameters(doc);
                message = new Message(msg, sender, command, receivers, parameters);
            }
            catch 
            {
                
            }
            return message;
        }

        private string GetCommand(XmlDocument doc)
        {
            XmlNodeList nodes = doc.GetElementsByTagName("Command");
            if (nodes.Count == 0)
                throw new Exception("消息格式不正确，不能进行解析。\n" + doc.OuterXml);
            return nodes[0].InnerText;

        }

        private string GetSender(XmlDocument doc)
        {
            XmlNodeList nodes = doc.GetElementsByTagName("Sender");
            if (nodes.Count == 0)
                throw new Exception("消息格式不正确，不能进行解析。\n" + doc.OuterXml);
            return nodes[0].InnerText;
        }

        private List<string> GetReceivers(XmlDocument doc)
        {
            XmlNodeList nodes = doc.GetElementsByTagName("Receivers");
            if (nodes.Count == 0)
                throw new Exception("消息格式不正确，不能进行解析。\n" + doc.OuterXml);
            List<string> receivers = new List<string>();
            foreach (XmlNode node in nodes[0].ChildNodes)
            {
                if (node.Name.Equals("Receiver"))
                    receivers.Add(node.InnerText);
                else
                    throw new Exception("消息格式不正确，不能进行解析。\n" + doc.OuterXml);
            }
            return receivers;
        }

        private Dictionary<string, string> GetParameters(XmlDocument doc)
        {
            XmlNodeList nodes = doc.GetElementsByTagName("Parameters");
            if (nodes.Count == 0)
                throw new Exception("消息格式不正确，不能进行解析。\n" + doc.OuterXml);
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            foreach (XmlNode node in nodes[0].ChildNodes)
            {
                parameters.Add(node.Name, node.InnerText);
            }
            return parameters;
        }
    }
}

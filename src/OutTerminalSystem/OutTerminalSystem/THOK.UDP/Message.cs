using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.UDP
{
    public class Message
    {
        private string msg = null ;
        private string sender = null;
        private string command = null;
        private List<string> receivers = new List<string>();
        private Dictionary<string, string> parameters = new Dictionary<string, string>();

        public string Msg
        {
            get { return msg; }
        }

        public string Sender
        {
            get { return sender; }
        }

        public string Command
        {
            get { return command; }
        }

        public List<string> Receivers
        {
            get { return receivers; }
        }

        public Dictionary<string, string> Parameters
        {
            get { return parameters; }
        }

        public Message(string msg, string sender, string command, List<string> receivers, Dictionary<string, string> parameters)
        {
            this.msg = msg;
            this.sender = sender;
            this.receivers = receivers;
            this.command = command;
            this.parameters = parameters;
        }
    }
}

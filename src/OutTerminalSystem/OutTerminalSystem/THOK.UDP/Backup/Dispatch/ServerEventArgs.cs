using System;
using System.Data;

namespace THOK.UDP.Dispatch
{
	/// <summary>
	/// ServerEventArgs ��ժҪ˵����
	/// </summary>
	public class ServerEventArgs
	{
		private string clientName = null;
		private string clientIP = null;
		private int clientPort = 0;
		
		public ServerEventArgs(string clientName, string clientIP, int clientPort)
		{
			this.clientName = clientName;
			this.clientIP = clientIP;
			this.clientPort = clientPort;
		}

		public string ClientName
		{
			get
			{
				return clientName;
			}
		}

		public string ClientIP
		{
			get
			{
				return clientIP;
			}
		}

		public int ClientPort
		{
			get
			{
				return clientPort;
			}
		}
	}
}

using System;
using System.Net;
using THOK.UDP.Util;

namespace THOK.UDP.Dispatch
{
	/// <summary>
	/// UDP客户端，比Huike.Udp.Client多了Register和Unregister两个方法，
	/// Register用来向消息转发服务器注册自己的IP地址和监听端口
	/// Unregister用来向消息转发服务器注销
	/// </summary>
    public class DispatchClient : Client
	{
		private string dispatchServerName = "Dispatcher";
		private string name = "Client";

		/// <summary>
		/// 消息转发服务器名称
		/// </summary>
		public string DispatchServerName
		{
			get
			{
				return dispatchServerName;
			}
		}

		/// <summary>
		/// 当前客户端名称
		/// </summary>
		public string Name
		{
			get
			{
				return name;
			}
		}

		public DispatchClient(string dispatchServerName, string hostIP, int port, string name): base(hostIP, port)
		{
			this.dispatchServerName = dispatchServerName;
			this.name = name;
		}

        public DispatchClient(string dispatchServerName, string hostIP, int port)
            : base(hostIP, port)
		{
			this.dispatchServerName = dispatchServerName;
			this.name = System.Net.Dns.GetHostName();
		}

		/// <summary>
		/// 向消息转发服务器注册
		/// </summary>
		public void Register(string ip, int port)
		{
			MessageGenerator generator = new MessageGenerator("REG", name);
			generator.AddReceiver(dispatchServerName);
			generator.AddReceiver(name);
			generator.AddParameter("IP", ip);
			generator.AddParameter("Port", port.ToString());
			Send(generator.GetMessage());
		}

		/// <summary>
		/// 用系统第一个IP地址作为UDP服务器的IP地址向消息转发服务器注册
		/// </summary>
		/// <param name="port"></param>
		public void Register(int port)
		{
			string ip = "127.0.0.1";
            
			IPHostEntry ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());
			if (ipHostEntry.AddressList.Length != 0)
				ip = ipHostEntry.AddressList[0].ToString();
			Register(ip, port);
			
		}

		/// <summary>
		/// 向消息转发服务器注销
		/// </summary>
		public void Unregister()
		{
			MessageGenerator generator = new MessageGenerator("UNREG", name);
			generator.AddReceiver(dispatchServerName);
			Send(generator.GetMessage());
		}

		/// <summary>
		/// 获取在转发服务器上已注册的所有客户及注册日期
		/// </summary>
		public void GetRegistedClient()
		{
			MessageGenerator generator = new MessageGenerator("CLIENTS", name);
			generator.AddReceiver(dispatchServerName);
			Send(generator.GetMessage());
		}
	}
}

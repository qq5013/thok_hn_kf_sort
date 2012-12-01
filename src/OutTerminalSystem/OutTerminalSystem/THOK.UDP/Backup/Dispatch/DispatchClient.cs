using System;
using System.Net;
using THOK.UDP.Util;

namespace THOK.UDP.Dispatch
{
	/// <summary>
	/// UDP�ͻ��ˣ���Huike.Udp.Client����Register��Unregister����������
	/// Register��������Ϣת��������ע���Լ���IP��ַ�ͼ����˿�
	/// Unregister��������Ϣת��������ע��
	/// </summary>
    public class DispatchClient : Client
	{
		private string dispatchServerName = "Dispatcher";
		private string name = "Client";

		/// <summary>
		/// ��Ϣת������������
		/// </summary>
		public string DispatchServerName
		{
			get
			{
				return dispatchServerName;
			}
		}

		/// <summary>
		/// ��ǰ�ͻ�������
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
		/// ����Ϣת��������ע��
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
		/// ��ϵͳ��һ��IP��ַ��ΪUDP��������IP��ַ����Ϣת��������ע��
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
		/// ����Ϣת��������ע��
		/// </summary>
		public void Unregister()
		{
			MessageGenerator generator = new MessageGenerator("UNREG", name);
			generator.AddReceiver(dispatchServerName);
			Send(generator.GetMessage());
		}

		/// <summary>
		/// ��ȡ��ת������������ע������пͻ���ע������
		/// </summary>
		public void GetRegistedClient()
		{
			MessageGenerator generator = new MessageGenerator("CLIENTS", name);
			generator.AddReceiver(dispatchServerName);
			Send(generator.GetMessage());
		}
	}
}

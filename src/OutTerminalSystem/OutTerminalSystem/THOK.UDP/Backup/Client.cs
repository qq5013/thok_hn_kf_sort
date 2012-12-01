using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace THOK.UDP
{
	/// <summary>
	/// UDP客户
	/// </summary>
	public class Client
	{
		private IPEndPoint endPoint = null;
		private Socket socket = null;

		/// <summary>
		/// 创建UDP客户端实例
		/// </summary>
		/// <param name="hostAddress">服务器的IP地址</param>
		/// <param name="port">服务器的端口</param>
		public Client(string hostAddress, int port)
		{
			endPoint = new IPEndPoint(IPAddress.Parse(hostAddress), port);
			socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
		}

		/// <summary>
		/// 发送消息
		/// </summary>
		/// <param name="message">发送的信息</param>
		public void Send(string message)
		{
			byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);
			socket.SendTo(buffer, endPoint);
		}

        public void Send(object message)
        {
            byte[] buffer = null;
            IFormatter formatter = new BinaryFormatter();
            Stream memStream = new MemoryStream();
            formatter.Serialize(memStream, message);
            memStream.Read(buffer, 0, (int) memStream.Length);
            socket.SendTo(buffer, endPoint);
        }

		/// <summary>
		/// 释放资源
		/// </summary>
		public void Release()
		{
			if (socket != null)
			{
				socket.Close();
				socket = null;
			}
		}
	}
}

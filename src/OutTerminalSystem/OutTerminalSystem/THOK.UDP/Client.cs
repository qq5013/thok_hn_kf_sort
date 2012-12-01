using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace THOK.UDP
{
	/// <summary>
	/// UDP�ͻ�
	/// </summary>
	public class Client
	{
		private IPEndPoint endPoint = null;
		private Socket socket = null;

		/// <summary>
		/// ����UDP�ͻ���ʵ��
		/// </summary>
		/// <param name="hostAddress">��������IP��ַ</param>
		/// <param name="port">�������Ķ˿�</param>
		public Client(string hostAddress, int port)
		{
			endPoint = new IPEndPoint(IPAddress.Parse(hostAddress), port);
			socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
		}

		/// <summary>
		/// ������Ϣ
		/// </summary>
		/// <param name="message">���͵���Ϣ</param>
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
		/// �ͷ���Դ
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

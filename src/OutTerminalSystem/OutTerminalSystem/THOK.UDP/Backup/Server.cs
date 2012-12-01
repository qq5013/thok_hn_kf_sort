using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace THOK.UDP
{
	public delegate void ReceiveEventHandler(object sender, ReceiveEventArgs e);

	/// <summary>
	/// UDP·þÎñÆ÷
	/// </summary>
	public class Server
	{
		private Socket server = null;
		private string address = null;
		private int port = 0;
		private Thread listenThread = null;

		public event ReceiveEventHandler OnReceive = null;

		/// <summary>
		/// ¼àÌýµØÖ·
		/// </summary>
		public string Address
		{
			get
			{
				return address;
			}
			set
			{
				address = value;
			}
		}

		/// <summary>
		/// ¼àÌý¶Ë¿Ú
		/// </summary>
		public int Port
		{
			get
			{
				return port;
			}
			set
			{
				port = value;
			}
		}


		public Server()
		{
			IPHostEntry ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());
			if (ipHostEntry.AddressList.Length != 0)
				address = ipHostEntry.AddressList[0].ToString();
			else
				address = "127.0.0.1";
			port = 1000;
		}

		/// <summary>
		/// ¿ªÊ¼¼àÌý
		/// </summary>
		public void StartListen()
		{
			server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			server.Bind(new IPEndPoint(IPAddress.Parse(address), port));
			listenThread = new Thread(new ThreadStart(Listen));
			listenThread.IsBackground = true;
			listenThread.Name = "¼àÌýÏß³Ì";
			listenThread.Start();
		}

		/// <summary>
		/// ¿ªÊ¼¼àÌý
		/// </summary>
		/// <param name="address">¼àÌýµØÖ·</param>
		/// <param name="port">¼àÌý¶Ë¿Ú</param>
		public void StartListen(string address, int port)
		{
			this.address = address;
			this.port = port;
			StartListen();
		}

		/// <summary>
		/// Í£Ö¹¼àÌý
		/// </summary>
		public void StopListen()
		{
			if (listenThread != null)
				listenThread.Abort();
			if (server != null)
				server.Close();
		}

		private void Listen()
		{
			while (true)
			{
				byte[] buffer = new byte[2048];
				EndPoint remote = (EndPoint)new IPEndPoint(IPAddress.Any, 0);
				try
				{
					int receiveCount = server.ReceiveFrom(buffer, ref remote);
					string message = Encoding.UTF8.GetString(buffer, 0, receiveCount);
					if (OnReceive != null)
						OnReceive(this, new ReceiveEventArgs(remote.ToString(), message));
				}
				catch (Exception e)
				{
					System.Diagnostics.Debug.WriteLine(e.Message);
				}
			}
		}
	}
}

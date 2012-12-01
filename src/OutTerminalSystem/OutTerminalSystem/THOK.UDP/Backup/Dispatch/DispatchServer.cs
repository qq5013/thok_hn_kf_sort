using System;
using System.Collections.Generic;
using System.Data;
using THOK.UDP.Util;

namespace THOK.UDP.Dispatch
{
	public delegate void ReceiveEventHandler(object sender, Message e);
	public delegate void ServerEventHandler(object sender, ServerEventArgs e);
	/// <summary>
	/// 消息转发服务器
	/// </summary>
	public class DispatchServer
	{
		private string name = null;
        private THOK.UDP.Server server = null;
		private DataSet clientSet = new DataSet("ClientSet");
		public event ReceiveEventHandler OnReceive = null;
		public event ServerEventHandler OnClientConnect = null;
		public event ServerEventHandler OnClientDisconnect = null;

		public DataSet Clients
		{
			get
			{
				return clientSet;
			}
		}

		public string Name
		{
			get
			{
				return name;
			}
		}

        public DispatchServer(string name)
		{
			this.name = name;
            server = new THOK.UDP.Server();
            server.OnReceive += new THOK.UDP.ReceiveEventHandler(server_OnReceive);

			LoadTable();
		}

		/// <summary>
		/// 开始监听
		/// </summary>
		public void StartListen()
		{
			server.StartListen();
		}

		/// <summary>
		/// 开始监听
		/// </summary>
		/// <param name="address">监听地址</param>
		/// <param name="port">监听端口</param>
		public void StartListen(string address, int port)
		{
			server.StartListen(address, port);
		}

		/// <summary>
		/// 停止监听
		/// </summary>
		public void StopListen()
		{
			server.StopListen();
		}

		public DataTable GetRegistedClient()
		{
			return clientSet.Tables["Client"].Copy();
		}

		/// <summary>
		/// 强行注销客户
		/// </summary>
		/// <param name="clientName"></param>
		public void UnregisterClient(string clientName)
		{
			DataTable clientTable = clientSet.Tables["Client"];
			DataRow[] clientRows = clientTable.Select(string.Format("Name='{0}'", clientName));
			if (clientRows.Length != 0)
			{
				string clientIP = clientRows[0]["IP"].ToString();
				int clientPort = Convert.ToInt32(clientRows[0]["Port"]);

				clientRows[0].Delete();
				SaveTable();

				//触发用户注销事件
				if (OnClientDisconnect != null)
					OnClientDisconnect(this, new ServerEventArgs(clientName, clientIP, clientPort));
			}
		}

		/// <summary>
		/// 发送消息到名称为clientName的UDP服务器
		/// </summary>
		/// <param name="clientName"></param>
		/// <param name="message"></param>
		public void Send(string clientName, string message)
		{
			DataRow[] clientRows = clientSet.Tables["Client"].Select(string.Format("Name='{0}'", clientName));
			if (clientRows.Length != 0)
			{
				//在消息转发服务器中注册的用户处理
				DataRow clientRow = clientRows[0];
                Client client = new Client(clientRow["IP"].ToString(), Convert.ToInt32(clientRow["Port"]));
				client.Send(message);
				client.Release();
			}
			else
			{
				if (OnReceive != null)
				{
					MessageParser parser = new MessageParser();
					OnReceive(this, parser.Parse(message));
				}
			}
		}

		private void server_OnReceive(object sender, ReceiveEventArgs e)
		{
			try
			{
				MessageParser parser = new MessageParser();
                Message message = parser.Parse(e.Message);
				List<string> receivers = message.Receivers;
				for (int i = 0; i < receivers.Count; i++)
				{
					string receiverName = receivers[i].ToString();
					if ( receiverName.ToUpper() == name.ToUpper())
					{
						//当前收到的消息需要消息转发服务器处理
						ProcessMessage(message);
					}
					else
					{
						//当前收到的消息转发到其他计算机
						Send(receiverName, message.Msg);
					}
				}
			}
			catch
			{
				//记录日志
			}
		}

		/// <summary>
		/// 处理应由消息转发服务器处理的消息
		/// </summary>
		/// <param name="parser"></param>
		private void ProcessMessage(Message message)
		{
            switch (message.Command)
			{
				case "REG":
					RegisterClient(message);
					break;
				case "UNREG":
                    UnregisterClient(message.Sender);
					break;
				case "CLIENTS":
					ReturnClients(message);
					break;
				default://接收者为消息转发服务器，但又不是需要处理的命令
					if (OnReceive != null)
                        OnReceive(this, message);
					break;
			}
		}

		/// <summary>
		/// 在消息转发服务器注册客户
		/// </summary>
		/// <param name="parser"></param>
		private void RegisterClient(Message message)
		{
			DataTable clientTable = clientSet.Tables["Client"];
			try
			{
                Dictionary<string, string> parameters = message.Parameters;

				string clientName = message.Sender;
				string clientIP = parameters["IP"];
				int clientPort = Convert.ToInt32(parameters["Port"]);

				if (message.Sender != null || message.Sender.Trim().Length != 0)
				{
					DataRow[] clientRows = clientTable.Select(string.Format("Name='{0}'", message.Sender));
					if (clientRows.Length == 0)
					{
						//如果当前客户未注册则在clientTable添加
						DataRow clientRow = clientTable.NewRow();
						clientRow["Name"] = clientName;
						clientRow["IP"] = clientIP;
						clientRow["Port"] = clientPort;
						clientRow["Date"] = DateTime.Now.ToShortDateString();
						clientTable.Rows.Add(clientRow);
					}
					else
					{
						//如果当前客户已注册则更新clientTable中当前客户的信息
						clientRows[0]["Name"] = clientName;
						clientRows[0]["IP"] = clientIP;
						clientRows[0]["Port"] = clientPort;
						clientRows[0]["Date"] = DateTime.Now.ToShortDateString();
					}
					SaveTable();//保存clientTable，以便程序重启后恢复

					//触发用户注册成功事件
					if (OnClientConnect != null)
						OnClientConnect(this, new ServerEventArgs(clientName, clientIP, clientPort));

				}

			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				//记录日志
			}
		}

		/// <summary>
		/// 返回所有已注册的客户及注册日期
		/// </summary>
		/// <param name="parser"></param>
		private void ReturnClients(Message message)
		{
			MessageGenerator generator = new MessageGenerator("CLIENTS", name);
            generator.AddReceiver(message.Sender);
			foreach (DataRow clientRow in clientSet.Tables["Client"].Rows)
			{
				generator.AddParameter("Client", clientRow["Name"].ToString());
			}
            Send(message.Sender, generator.GetMessage());
		}

		/// <summary>
		/// 保存clientTable到Client.xml文件
		/// </summary>
		private void SaveTable()
		{
			clientSet.AcceptChanges();
			clientSet.WriteXml(@".\Client.xml");
		}

		/// <summary>
		/// 从Client.xml文件中恢复数据
		/// </summary>
		private void LoadTable()
		{
			System.IO.FileInfo clientFile = new System.IO.FileInfo(@".\Client.xml");
			if (clientFile.Exists)
			{
				clientSet.ReadXml(@".\Client.xml");
				if (clientSet.Tables.Count == 0)
					clientSet.Tables.Add(GenerateTable());
			}
			else
			{
				//添加日志
				clientSet.Tables.Add(GenerateTable());
			}
		}

		/// <summary>
		/// 生成用来记录已注册客户的DataTable
		/// </summary>
		/// <returns></returns>
		private DataTable GenerateTable()
		{
			DataTable clientTable = new DataTable("Client");
			clientTable.Columns.Add("Name");
			clientTable.Columns.Add("IP");
			clientTable.Columns.Add("Port");
			clientTable.Columns.Add("Date");
			return clientTable;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Data;
using THOK.UDP.Util;

namespace THOK.UDP.Dispatch
{
	public delegate void ReceiveEventHandler(object sender, Message e);
	public delegate void ServerEventHandler(object sender, ServerEventArgs e);
	/// <summary>
	/// ��Ϣת��������
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
		/// ��ʼ����
		/// </summary>
		public void StartListen()
		{
			server.StartListen();
		}

		/// <summary>
		/// ��ʼ����
		/// </summary>
		/// <param name="address">������ַ</param>
		/// <param name="port">�����˿�</param>
		public void StartListen(string address, int port)
		{
			server.StartListen(address, port);
		}

		/// <summary>
		/// ֹͣ����
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
		/// ǿ��ע���ͻ�
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

				//�����û�ע���¼�
				if (OnClientDisconnect != null)
					OnClientDisconnect(this, new ServerEventArgs(clientName, clientIP, clientPort));
			}
		}

		/// <summary>
		/// ������Ϣ������ΪclientName��UDP������
		/// </summary>
		/// <param name="clientName"></param>
		/// <param name="message"></param>
		public void Send(string clientName, string message)
		{
			DataRow[] clientRows = clientSet.Tables["Client"].Select(string.Format("Name='{0}'", clientName));
			if (clientRows.Length != 0)
			{
				//����Ϣת����������ע����û�����
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
						//��ǰ�յ�����Ϣ��Ҫ��Ϣת������������
						ProcessMessage(message);
					}
					else
					{
						//��ǰ�յ�����Ϣת�������������
						Send(receiverName, message.Msg);
					}
				}
			}
			catch
			{
				//��¼��־
			}
		}

		/// <summary>
		/// ����Ӧ����Ϣת���������������Ϣ
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
				default://������Ϊ��Ϣת�������������ֲ�����Ҫ���������
					if (OnReceive != null)
                        OnReceive(this, message);
					break;
			}
		}

		/// <summary>
		/// ����Ϣת��������ע��ͻ�
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
						//�����ǰ�ͻ�δע������clientTable���
						DataRow clientRow = clientTable.NewRow();
						clientRow["Name"] = clientName;
						clientRow["IP"] = clientIP;
						clientRow["Port"] = clientPort;
						clientRow["Date"] = DateTime.Now.ToShortDateString();
						clientTable.Rows.Add(clientRow);
					}
					else
					{
						//�����ǰ�ͻ���ע�������clientTable�е�ǰ�ͻ�����Ϣ
						clientRows[0]["Name"] = clientName;
						clientRows[0]["IP"] = clientIP;
						clientRows[0]["Port"] = clientPort;
						clientRows[0]["Date"] = DateTime.Now.ToShortDateString();
					}
					SaveTable();//����clientTable���Ա����������ָ�

					//�����û�ע��ɹ��¼�
					if (OnClientConnect != null)
						OnClientConnect(this, new ServerEventArgs(clientName, clientIP, clientPort));

				}

			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				//��¼��־
			}
		}

		/// <summary>
		/// ����������ע��Ŀͻ���ע������
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
		/// ����clientTable��Client.xml�ļ�
		/// </summary>
		private void SaveTable()
		{
			clientSet.AcceptChanges();
			clientSet.WriteXml(@".\Client.xml");
		}

		/// <summary>
		/// ��Client.xml�ļ��лָ�����
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
				//�����־
				clientSet.Tables.Add(GenerateTable());
			}
		}

		/// <summary>
		/// ����������¼��ע��ͻ���DataTable
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

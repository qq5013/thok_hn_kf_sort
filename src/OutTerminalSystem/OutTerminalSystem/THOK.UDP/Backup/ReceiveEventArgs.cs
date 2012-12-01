using System;

namespace THOK.UDP
{
	/// <summary>
	/// ΪOnReceive�¼��ṩ����
	/// </summary>
	public class ReceiveEventArgs: EventArgs
	{
		private string remoteAddress = null;
		private string message = null;

		/// <summary>
		/// Զ�̼������IP��ַ�Ͷ˿�
		/// </summary>
		public string RemoteAddress
		{
			get
			{
				return remoteAddress;
			}
		}

		/// <summary>
		/// ��ǰ�յ�����Ϣ
		/// </summary>
		public string Message
		{
			get
			{
				return message;
			}
		}

		public ReceiveEventArgs(string remoteAddress, string message)
		{
			this.remoteAddress = remoteAddress;
			this.message = message;
		}
	}
}

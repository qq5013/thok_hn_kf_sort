using System;

namespace THOK.UDP
{
	/// <summary>
	/// 为OnReceive事件提供数据
	/// </summary>
	public class ReceiveEventArgs: EventArgs
	{
		private string remoteAddress = null;
		private string message = null;

		/// <summary>
		/// 远程计算机的IP地址和端口
		/// </summary>
		public string RemoteAddress
		{
			get
			{
				return remoteAddress;
			}
		}

		/// <summary>
		/// 当前收到的消息
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

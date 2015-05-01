using System;
using System.Collections.Generic;
using Talker.DAL;

namespace Talker.BL
{
	public class Message : Object
	{
		public User Sender { get; set; }
		public User Receiver { get; set; }
		public string Text { get; set; }
		public bool HasRead { get; set; }

		public Message ( User pSender, User pReceiver, string pText, bool pHasRead )
		{
			this.Sender = pSender;
			this.Receiver = pReceiver;
			this.Text = pText;
			this.HasRead = pHasRead;
		}
	}
}
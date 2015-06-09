using System;
using System.Collections.Generic;

namespace Talker.ML
{
	public class Message : Object
	{
		public string SenderID { get; set; }
		public string ReceiverID { get; set; }
		public string Text { get; set; }
		public bool HasRead { get; set; }

		public Message ( string pSender, string pReceiver, string pText, bool pHasRead )
		{
			this.SenderID = pSender;
			this.ReceiverID = pReceiver;
			this.Text = pText;
			this.HasRead = pHasRead;
		}

        public Message()
        {
        }
	}
}
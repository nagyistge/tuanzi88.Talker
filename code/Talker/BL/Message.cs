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

		public Message ( User pSender, User pReceiver, string pText )
		{
			this.Sender = pSender;
			this.Receiver = pReceiver;
			this.Text = pText;
		}

		public Message()
		{}
	}

	public static class MessageManager
	{
		public static Message GetMessage(int id)	
		{
			return DAL.MessageRepository.GetMessage(id);
		}

		public static List<Message> GetMessages (User pUser)
		{
			return DAL.MessageRepository.GetMessages(pUser);
		}

		public static void SaveMessage(int id)	
		{
			DAL.MessageRepository.GetMessage(id);
		}

		public static void SaveMessage (Message pMessage)
		{
			DAL.MessageRepository.SaveMessage(pMessage);
		}

		public static void DeleteMessage(Message pMessage)
		{
			DAL.MessageRepository.DeleteMessage(pMessage);
		}
	}
}
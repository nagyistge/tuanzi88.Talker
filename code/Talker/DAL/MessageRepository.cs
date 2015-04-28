using System;
using System.Collections.Generic;
using Talker.BL;
using Talker.DL;

namespace Talker.DAL
{
	public static class MessageRepository
	{
		static MessageRepository()
		{
		}

		public static void SaveMessage (Message pItem)
		{
			DataAccessManager.LocalDB.SaveItem<Message>(pItem);
		}

		public static Message GetMessage (int pId)
		{
			return DataAccessManager.LocalDB.Get<Message> (pId);
		}

		public static void DeleteMessage (Message pItem)
		{
			DataAccessManager.LocalDB.Delete<Message> (pItem);
		}

		public static List<Message> GetMessages(User pUser)
		{
			return DataAccessManager.LocalDB.GetItems<Message> (pUser.ID);
		}
	}
}


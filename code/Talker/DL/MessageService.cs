using System;
using System.Collections.Generic;
using Talker.BL;

namespace Talker.DAL
{
	public interface IMessageDatabase
	{
		void SaveMessage (Message pItem);
		void DeleteMessage (Message pItem);
		List<Message> GetMessages (int pUserId);
		Message GetMessage (int pMessageId);
	}

}


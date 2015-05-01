using System;
using System.Collections.Generic;
using Talker.BL;
using Talker.DL;

/* 
 * Note: 
 * Talker.DAL are the interface between business layer and Data layer,
 * which means you don't want to change this layer frequently, and should 
 * give notification and enough reason to change this layer.  
 */
namespace Talker.DAL
{
	public interface IMessageDatabase
	{
		void SaveMessage (Message pItem);
		void DeleteMessage (Message pItem);
		List<Message> GetMessages (int pUserId);
		Message GetMessage (int pMessageId);
	}
		
	public interface IUserDatabase
	{
		bool IsThisUserExisted (string pName, string pPassword);
		void SaveUser (User pItem);
		User GetSender (int pMessageId);
		User GetReceiver (int pMessageId);
		User GetUser (int pUserId);
	}
}


using System;
using System.Collections.Generic;
using System.Linq;
using Talker.BL;
using Talker.DAL;

namespace Talker.DL
{
	public class LocalDatabase : SQLiteConnection, IMessageDatabase, IUserDatabase
	{
		static object mLocker = new object ();

		public LocalDatabase (string path) : base (path)
		{
			// create the tables
			CreateTable<User> ();
			// CreateTable<Message> ();   // YIKANG P1: Why cannot create another table?
		}

		#region IUserDatabase implementation

		bool IUserDatabase.IsThisUserExisted (string pName, string pPassword)
		{
			lock (mLocker) 
			{
				IEnumerable<User> users = from i in Table<User> ()
						where (i.Name == pName && i.Password == pPassword)
					select i;
				List<User> userList = users.ToList ();
				if (userList.Count () == 1)
					return true;
				return false;
			}
		}

		void IUserDatabase.SaveUser (User pItem)
		{
			lock (mLocker) {	
				SaveItem<User> (pItem);
			}
		}

		User IUserDatabase.GetSender (int pMessageId)
		{
			throw new NotImplementedException ();
		}

		User IUserDatabase.GetReceiver (int pMessageId)
		{
			throw new NotImplementedException ();
		}

		User IUserDatabase.GetUser (int pUserId)
		{
			throw new NotImplementedException ();
		}

		#endregion

		#region IMessageDatabase implementation

		public void SaveMessage (Message pItem)
		{
			throw new NotImplementedException ();
		}

		public void DeleteMessage (Message pItem)
		{
			throw new NotImplementedException ();
		}

		public List<Message> GetMessages (int pUserId)
		{
			throw new NotImplementedException ();
		}

		public Message GetMessage (int pMessageId)
		{
			throw new NotImplementedException ();
		}

		#endregion

		public IEnumerable<T> GetItems<T> () where T : BL.Object, new()
		{
			lock (mLocker) {
				return (from i in Table<T> ()
				        select i).ToList ();
			}
		}

		public List<T> GetItems<T> (int pUserId) where T : BL.Message, new()
		{
			lock (mLocker) {
				IEnumerable<T> messages = from i in Table<T> ()
				                          where i.Sender.ID == pUserId
				                          select i;
				List<T> messagesList = messages.ToList ();
				return messagesList;
			}
		}

		public T GetItem<T> (int id) where T : BL.Object, new()
		{
			lock (mLocker) {
				return Table<T> ().FirstOrDefault (x => x.ID == id);
				// Following throws NotSupportedException - thanks aliegeni
				//return (from i in Table<T> ()
				//        where i.ID == id
				//        select i).FirstOrDefault ();
			}
		}

		public int SaveItem<T> (T item) where T : BL.Object
		{
			lock (mLocker) {
				if (item.ID != 0) {
					Update (item);
					return item.ID;
				} else {
					return Insert (item);
				}
			}
		}

		public int DeleteItem<T> (int id) where T : BL.Object, new()
		{
			lock (mLocker) {
				return Delete<T> (new T () { ID = id });
			}
		}
	}


	public class CloudDatabase : IMessageDatabase, IUserDatabase
	{
		#region IUserDatabase implementation

		public bool IsThisUserExisted (string pName, string pPassword)
		{
			throw new NotImplementedException ();
		}

		public void SaveUser (User pItem)
		{
			throw new NotImplementedException ();
		}

		public User GetSender (int pMessageId)
		{
			throw new NotImplementedException ();
		}

		public User GetReceiver (int pMessageId)
		{
			throw new NotImplementedException ();
		}

		public User GetUser (int pUserId)
		{
			throw new NotImplementedException ();
		}

		#endregion

		#region IMessageDatabase implementation

		public void SaveMessage (Message pItem)
		{
			throw new NotImplementedException ();
		}

		public void DeleteMessage (Message pItem)
		{
			throw new NotImplementedException ();
		}

		public List<Message> GetMessages (int pUserId)
		{
			throw new NotImplementedException ();
		}

		public Message GetMessage (int pMessageId)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}


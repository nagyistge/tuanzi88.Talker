using System;
using System.Collections.Generic;
using System.Linq;
using Talker.BL;
using Talker.DAL;
using System.Threading.Tasks;

namespace Talker.DL
{
	public class LocalDatabase : SQLiteConnection, IMessageDatabase, IUserDatabase
	{
		static object mLocker = new object ();
		//private IEnumerable<User> mUserList;    

		public LocalDatabase (string path) : base (path)
		{
			// create the tables
			CreateTable<User> ();
			// CreateTable<Message> ();   // YIKANG P1: Why crash when create another table?
		}

		#region IUserDatabase implementation

		public Task<User> GetUser (string pName, string pPassword)
		{
			throw new NotImplementedException ();
		}

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

		public IEnumerable<T> GetItems<T> () where T : BL.ObjectLocal, new()
		{
			lock (mLocker) {
				return (from i in Table<T> ()
				        select i).ToList ();
			}
		}

		public List<T> GetItems<T> (string pUserId) where T : BL.Message, new()
		{
			lock (mLocker) {
				IEnumerable<T> messages = from i in Table<T> ()
				                          where i.Sender.ID == pUserId
				                          select i;
				List<T> messagesList = messages.ToList ();
				return messagesList;
			}
		}

		public T GetItem<T> (int id) where T : BL.ObjectLocal, new()
		{
			lock (mLocker) {
				return Table<T> ().FirstOrDefault (x => x.ID == id);
				// Following throws NotSupportedException - thanks aliegeni
				//return (from i in Table<T> ()
				//        where i.ID == id
				//        select i).FirstOrDefault ();
			}
		}

		public int SaveItem<T> (T item) where T : BL.ObjectLocal
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

		public int DeleteItem<T> (int id) where T : BL.ObjectLocal, new()
		{
			lock (mLocker) {
				return Delete<T> (new T () { ID = id });
			}
		}
	}



}


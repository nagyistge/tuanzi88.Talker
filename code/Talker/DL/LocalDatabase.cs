using System;
using System.Collections.Generic;
using System.Linq;
using Talker.BL;

namespace Talker.DL
{
	public class LocalDatabase : SQLiteConnection
	{
		static object mLocker = new object ();

		public LocalDatabase (string path) : base (path)
		{
			// create the tables
			CreateTable<Message>();
			CreateTable<User> ();
		}

		public IEnumerable<T> GetItems<T> () where T : BL.Object, new ()
		{
			lock (mLocker) {
				return (from i in Table<T> () select i).ToList ();
			}
		}
			
		public List<T> GetItems<T>(int pUserId) where T : BL.Message, new()
		{
			lock (mLocker) 
			{
				IEnumerable<T> messages = from i in Table<T> () where i.Sender.ID == pUserId select i;
				List<T> messagesList = messages.ToList ();
				return messagesList;
			}
		}

		public T GetItem<T> (int id) where T : BL.Object, new ()
		{
			lock (mLocker) {
				return Table<T>().FirstOrDefault(x => x.ID == id);
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

		public int DeleteItem<T>(int id) where T : BL.Object, new ()
		{
			lock (mLocker) {
				return Delete<T> (new T () { ID = id });
			}
		}
	}
}


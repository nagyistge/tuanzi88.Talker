using System;
using System.Collections.Generic;
using Talker.DAL;
using Talker.BL;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace Talker.DL
{
	public class CloudDatabase : IMessageDatabase, IUserDatabase
	{
		private readonly MobileServiceClient mClient;
		private bool mIsThisUserExisted = false;
		private List<User> UserList = null;

        public CloudDatabase (string pAppUrl, string pAppKey)
		{
			CurrentPlatform.Init();
            mClient = new MobileServiceClient (pAppUrl, pAppKey);
		}

		public async Task IsThisUserExisted1 (string pName, string pPassword)
		{
			IMobileServiceTable<User> userTable = mClient.GetTable<User>();
			List<User> items = await userTable
				//.Where(userItem => userItem.ReadOnly == false)
				.ToListAsync();
			if (items.Count >= 1) 
			{
				items = await userTable
					.Where(userItem => userItem.Name == pName && userItem.Password == pPassword)
					.ToListAsync();

				mIsThisUserExisted |= items.Count == 1;
			}

			mIsThisUserExisted = false;
		}

		#region IUserDatabase implementation

		public async Task<User> GetUser (string pName, string pPassword)
		{
			try 
			{
				// This code refreshes the entries in the list view by querying the User table.
				// The query excludes completed TodoItems
				UserList = await mClient.GetTable<User>()
					.Where (user => user.Name == pName && user.Password == pPassword).ToListAsync();
			} 
			catch (MobileServiceInvalidOperationException e) 
			{
				Console.Error.WriteLine (@"ERROR {0}", e.Message);
				return null;
			}

			if (UserList.Count >= 1)
				return UserList [0];
			else
				return null;
		}

		public bool IsThisUserExisted (string pName, string pPassword)
		{
			try
			{
				Task task = IsThisUserExisted1 (pName, pPassword);
				task.Wait();
				return mIsThisUserExisted;
			}
			catch(MobileServiceInvalidOperationException e)
			{
				Console.WriteLine ("This is the bug"+e.Message);
				throw e;
			}
		}

		public async void SaveUser (User pItem)
		{
			try
			{
				await mClient.GetTable<User>().InsertAsync(pItem);
			}
			catch(MobileServiceInvalidOperationException e)
			{
				Console.WriteLine ("This is the bug"+e.Message);
				throw e;
			}
		}

		public Talker.BL.User GetSender (int pMessageId)
		{
			throw new NotImplementedException ();
		}

		public Talker.BL.User GetReceiver (int pMessageId)
		{
			throw new NotImplementedException ();
		}

		public Talker.BL.User GetUser (int pUserId)
		{
			throw new NotImplementedException ();
		}

		#endregion

		#region IMessageDatabase implementation

		public void SaveMessage (Talker.BL.Message pItem)
		{
			throw new NotImplementedException ();
		}

		public void DeleteMessage (Talker.BL.Message pItem)
		{
			throw new NotImplementedException ();
		}

		public System.Collections.Generic.List<Talker.BL.Message> GetMessages (int pUserId)
		{
			throw new NotImplementedException ();
		}

		public Talker.BL.Message GetMessage (int pMessageId)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}


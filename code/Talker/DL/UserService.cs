using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

using Talker.BL;

/*
 * Note:
 * YIKANG P3:
 */
namespace Talker.DL
{
    public class UserService : BaseService, IUserService
    {
        protected static UserService mInstance = new UserService();
        private IMobileServiceSyncTable<User> mUserTable;

        private UserService ()
            : base()
		{
            // Define table for local db
            DatabaseService.Instance.LocalStore.DefineTable<User>();

            // Create user table for local db
            mUserTable = mClient.GetSyncTable<User>();
		}

        public static UserService Default
        {
            get { 
                return mInstance;
            }
        }

        #region IUserService implementation
        public List<User> UserList { get; private set; }

        public async Task SyncAsync()
        {
            try {
                await mClient.SyncContext.PushAsync();
                await mUserTable.PullAsync("allUsers", mUserTable.CreateQuery()); // query ID is used for incremental sync
            }
            catch (MobileServiceInvalidOperationException e) {
                Console.Error.WriteLine(@"Sync Failed: {0}", e.Message);
            }
        }

        public async Task<List<User>> RefreshDataAsync ()
        {
            try {
                // update the local store
                // all operations on todoTable use the local database, call SyncAsync to send changes
                await SyncAsync();                          

                // This code refreshes the entries in the list view by querying the local TodoItems table.
                // The query excludes completed TodoItems
                //UserList = await mUserTable
                //    .Where (user => user.Complete == false).ToListAsync ();
                UserList = await mUserTable.ToListAsync ();
                
            } catch (MobileServiceInvalidOperationException e) {
                Console.Error.WriteLine (@"ERROR {0}", e.Message);
                return null;
            }

            return UserList;
        }

        public async Task InsertUserAsync (User pUser)
        {
            try {
                await mUserTable.InsertAsync (pUser); // Insert a new TodoItem into the local database. 
                await SyncAsync(); // send changes to the mobile service

                UserList.Add (pUser); 
            } catch (MobileServiceInvalidOperationException e) {
                Console.Error.WriteLine (@"ERROR {0}", e.Message);
            }
        }

        public async Task<User> GetUser (string pName, string pPassword, string pType)
        {
            await RefreshDataAsync();

            foreach (User one in UserList)
            {
                if (one.Name == pName && one.Password == pPassword && one.Type == pType)
                    return one;
            }
            return null;
        }
        #endregion
	}
}


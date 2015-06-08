using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

using Talker.ML;
using Talker.DAL;

/*
 * Note:
 * YIKANG P3:
 */
namespace Talker.DL
{
    public class UserDB : IUserDB
    {
        private static UserDB mInstance = new UserDB();
        private IMobileServiceSyncTable<User> mUserTable;

        private UserDB ()
            : base()
		{
            // Define table for local db
            DatabaseService.Instance.LocalDB.DefineTable<User>();

            // Create user table for local db
            mUserTable = DatabaseService.Instance.CloudDB.GetSyncTable<User>();
		}

        public static UserDB Instance
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
                await DatabaseService.Instance.CloudDB.SyncContext.PushAsync();
                await mUserTable.PullAsync("allUsers", mUserTable.CreateQuery()); // query ID is used for incremental sync
            }
            catch (MobileServiceInvalidOperationException e) {
                Debug.WriteLine (@"Sync Failed: {0}", e.Message);
            }
        }

        public async Task<List<User>> RefreshDataAsync ()
        {
            try {
                // update the local store
                // all operations on userTable use the local database, call SyncAsync to send changes
                await SyncAsync();                          

                // This code refreshes the entries in the list view by querying the local table.
                UserList = await mUserTable.ToListAsync ();
                
            } catch (MobileServiceInvalidOperationException e) {
                Debug.WriteLine (@"ERROR {0}", e.Message);
                return null;
            }

            return UserList;
        }

        // YIKANG P2: Need to check the insert conflict
        public async Task InsertUserAsync (User pUser)
        {
            try {
                await mUserTable.InsertAsync (pUser); // Insert a new TodoItem into the local database. 
                await SyncAsync(); // send changes to the mobile service

                UserList.Add (pUser); 
            } catch (MobileServiceInvalidOperationException e) {
                Debug.WriteLine (@"ERROR {0}", e.Message);
            }
        }

        public async Task<User> GetUser (string pName, string pPassword, UserType pType)
        {
            await RefreshDataAsync();

            foreach (User one in UserList)
            {
                if (one.Name == pName && one.Password == pPassword && one.Type == pType)
                    return one;
            }
            return null;
        }

        public void InitFriends()
        {            
            if (GlobalManager.Instance.Friends.Count == 0 && GlobalManager.Instance.CurrentUser != null )
            {
                foreach (User one in UserList)
                {
                    if (one.ID != GlobalManager.Instance.CurrentUser.ID)
                    {
                        GlobalManager.Instance.Friends.Add(one);
                    }
                }
            }
        }
        #endregion
	}
}


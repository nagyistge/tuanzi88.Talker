using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

namespace Talker
{
    public class DatabaseService
    {
        static DatabaseService mInstance = new DatabaseService();

        // YIKANG P2: move below constants to Constants class
        const string applicationURL = @"https://talker.azure-mobile.net/";
        const string applicationKey = @"tHduMiKBYdscDusiKwEQHzOiPmQHQj75";
        const string localDbPath    = "localstore.db";

        private MobileServiceClient mClient;
        private MobileServiceSQLiteStore mLocalStore;

        private DatabaseService()
        {
            CurrentPlatform.Init ();
            SQLitePCL.CurrentPlatform.Init(); 

            // Initialize the Mobile Service client with your URL and key
            if (mClient == null)
                mClient = new MobileServiceClient (applicationURL, applicationKey);

            // Initialize the local database with database path
            if (mLocalStore == null)
                mLocalStore = new MobileServiceSQLiteStore(localDbPath);

            // Initial SyncContext
            //mClient.SyncContext.InitializeAsync(mLocalStore);  // YIKANG P1: why cannot init it here?
        }

        public static DatabaseService Instance
        {
            get {
                return mInstance;
            }
        }

        public MobileServiceClient Client
        {
            get {                
                return mClient;
            }
        }

        public MobileServiceSQLiteStore LocalStore
        {
            get {
                return mLocalStore;
            }
        }
    }
}


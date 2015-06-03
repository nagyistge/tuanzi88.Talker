using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices;
using Talker.ML;
using Talker.DAL;

namespace Talker.DL
{
    public class BaseService : IBaseService
    {
        protected MobileServiceClient mClient;
        private bool mIsStoreInitialized;

        public BaseService()
        {
            // For cloud db
            mClient = DatabaseService.Instance.Client;

            // YIKANG P3: Can init once from outside?
            mIsStoreInitialized = false;
        }

        public async Task InitializeStoreAsync()
        {
            // YIKANG P1: Must have better way to init it
            if (!mIsStoreInitialized) {
                // Uses the default conflict handler, which fails on conflict
                // To use a different conflict handler, pass a parameter to InitializeAsync. For more details, see http://go.microsoft.com/fwlink/?LinkId=521416
                await mClient.SyncContext.InitializeAsync(DatabaseService.Instance.LocalStore);
                mIsStoreInitialized = true;
            }
        }
    }
}


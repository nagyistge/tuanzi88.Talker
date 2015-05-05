using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

using Talker.BL;
using Talker.DAL;

namespace Talker.DL
{
    public class MessageService : BaseService, IMessageService
    {
        private static MessageService mInstance = new MessageService();
        private IMobileServiceSyncTable<Message> mMessageTable;

        public MessageService()
            :base()
        {
            // Define table for local db
            DatabaseService.Instance.LocalStore.DefineTable<Message>();

            // Create user table for local db
            mMessageTable = mClient.GetSyncTable<Message>();
        }

        public static MessageService Instance
        {
            get 
            { 
                return mInstance;
            }
        }

        #region IMessageService implementation
        public List<Message> MessageList { get; private set; }

        public async Task SyncAsync()
        {
            try 
            {
                await mClient.SyncContext.PushAsync();
                await mMessageTable.PullAsync("allMessages", mMessageTable.CreateQuery()); // query ID is used for incremental sync
            }
            catch (MobileServiceInvalidOperationException e) {
                Console.Error.WriteLine(@"Sync Failed: {0}", e.Message);
            }
        }

        public async Task<List<Message>> RefreshDataAsync(string pUserID)
        {
            try 
            {
                // update the local store
                // all operations on messageTable use the local database, call SyncAsync to send changes
                await SyncAsync();                          

                // This code refreshes the entries in the list view by querying the local table.
                MessageList = await mMessageTable
                    .Where (message => message.SenderID == pUserID).ToListAsync ();
            } 
            catch (MobileServiceInvalidOperationException e) 
            {
                Console.Error.WriteLine (@"ERROR {0}", e.Message);
                return null;
            }

            return MessageList;
        }

        public async Task InsertMessageAsync(Message pMessage)
        {
            try 
            {
                await mMessageTable.InsertAsync (pMessage); // Insert a new TodoItem into the local database. 
                await SyncAsync(); // send changes to the mobile service

                MessageList.Add (pMessage); 
            } 
            catch (MobileServiceInvalidOperationException e) 
            {
                Console.Error.WriteLine (@"ERROR {0}", e.Message);
            }
        }

        public async Task DeleteMessage(string pMessageID)
        {
            foreach (Message one in MessageList)
            {
                if (one.ID == pMessageID)
                {
                    try
                    {
                        await mMessageTable.DeleteAsync(one);
                        await SyncAsync();

                        MessageList.Remove(one);
                    }
                    catch (MobileServiceInvalidOperationException e) 
                    {
                        Console.Error.WriteLine (@"ERROR {0}", e.Message);
                    }
                    return;
                }
            }
        }
        #endregion
    }
}


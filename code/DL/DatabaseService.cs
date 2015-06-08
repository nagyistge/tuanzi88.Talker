using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

namespace Talker.DL
{
    public class DatabaseService
    {
        static DatabaseService mInstance = new DatabaseService();
        private MobileServiceClient mCloudDB;
        private MobileServiceSQLiteStore mLocalDB;

        private DatabaseService()
        {            
            // Initialize the Mobile Service client with your URL and key
            if (mCloudDB == null)
                mCloudDB = new MobileServiceClient (Constants.gCloudApplicationURL, Constants.gCloudApplicationKey);

            // Initialize the local database with database path
            if (mLocalDB == null)
                mLocalDB = new MobileServiceSQLiteStore(Constants.gLocalDBPath);
        }

        public static DatabaseService Instance
        {
            get {
                return mInstance;
            }
        }

        public MobileServiceClient CloudDB
        {
            get {                
                return mCloudDB;
            }
        }

        public MobileServiceSQLiteStore LocalDB
        {
            get {
                return mLocalDB;
            }
        }

        public static async Task InitCloundDB()
        {
            // Fails if being executed for twice!!!
            // To use a different conflict handler, pass a parameter to InitializeAsync, 
            // details in http://go.microsoft.com/fwlink/?LinkId=521416
            await DatabaseService.Instance.CloudDB.SyncContext.InitializeAsync(DatabaseService.Instance.LocalDB);
        }
    }
}


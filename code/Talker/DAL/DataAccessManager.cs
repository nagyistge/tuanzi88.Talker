using System;
using System.IO;
using Talker.DL;
using Talker.BL;
using System.Threading.Tasks;

/*
 * Note:
 * DataAccessManager is the global access for cloud and local database
 * but hide the complexity for configurating them, with following steps
 * to read and write data:
 * Step#1: sync data from cloud database to local database
 * Step#2: read / write data to local database
 * Step#3: sync data from local database to cloud database
 */
namespace Talker.DAL
{
    public class DataAccessCloudManager
    {
        private static readonly DataAccessCloudManager mSelf;
        private static DL.CloudDatabase mDB;

        static DataAccessCloudManager()
        {
            if (mSelf == null) {
                mSelf = new DataAccessCloudManager ();
            }               
        }

        private DataAccessCloudManager()
        {
            // instantiate the database
            if (mDB == null) {
                mDB = new DL.CloudDatabase (Constants.gApplicationURL, Constants.gApplicationKey);
            }
        }

    }

    public class LocalDataAccessManager
    {
        private static readonly LocalDataAccessManager mSelf;
        private readonly DL.LocalDatabase mDB;

        static LocalDataAccessManager()
        {
            if (mSelf == null) {
                mSelf = new LocalDataAccessManager ();
            }               
        }

        private LocalDataAccessManager()
        {
            // instantiate the database
            if (mDB == null) {
                mDB = new DL.LocalDatabase (DatabaseFilePath);
            }
        }

        private static string DatabaseFilePath {
            get {
                #if SILVERLIGHT
                // Windows Phone expects a local path, not absolute
                var path = Talker.BL.Constants.gSQLiteFilename;
                #else

                #if __ANDROID__
                // Just use whatever directory SpecialFolder.Personal returns
                string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                #else
                // we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
                // (they don't want non-user-generated data in Documents)
                string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
                string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
                #endif

                var path = Path.Combine (libraryPath, Constants.gSQLiteFilename);
                #endif

                return path;    
            }
        }

        public static bool IsThisUserExisted (string pName, string pPassword)
        {
            return mDB.IsThisUserExisted (pName, pPassword);
        }

        public static void SaveUser (User pUser)
        {
            mDB.SaveUser (pUser);
        }

        public static void SaveUser (string pName, string pPassword)
        {
            User one = new User (pName, pPassword);
            mDB.SaveUser (one);
        }

        public static async Task<User> GetUser (string pName, string pPassword)
        {
            return await mDB.GetUser (pName, pPassword);
        }
    }

    public class DataAccessManager
    {
        protected static DataAccessManager me;
        protected static DL.LocalDatabase mLocalDB;
        protected static DL.CloudDatabase mCloudDB;

        static DataAccessManager ()
        {
            me = new DataAccessManager ();
        }

        protected DataAccessManager ()
        {
            // instantiate the database
            if (mLocalDB == null) {
                mLocalDB = new DL.LocalDatabase (DatabaseFilePath);
            }
            if (mCloudDB == null) {
                mCloudDB = new DL.CloudDatabase ();
            }
        }

        public static void Sync()
        {
            // YIKANG P1: Sync from cloud database to local database with the time comparision


            // YIKANG P1: Sync from local database to cloud database with the time comparision


            // YIKANG P1: be able to solve the conflict
        }

        public static IMessageDatabase GetMessageDB (bool pIsLocal = true)
        {
            if (pIsLocal)
                return mLocalDB;
            else
                return mCloudDB;
        }

        public static IUserDatabase GetUserDB (bool pIsLocal = true)
        {
            if (pIsLocal)
                return mLocalDB;
            else
                return mCloudDB;
        }

    }
}


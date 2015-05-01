using System;
using System.IO;

using Talker.BL;
using Talker.DL;

/*
 * Note:
 * DataAccessManager is the global access for cloud and local database
 * but hide the complexity for configurating them.
 */
namespace Talker.DAL
{
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
			mLocalDB = new DL.LocalDatabase (DatabaseFilePath);
			mCloudDB = new DL.CloudDatabase ();
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

		public static string DatabaseFilePath {
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

				var path = Path.Combine (libraryPath, Talker.BL.Constants.gSQLiteFilename);
				#endif

				return path;	
			}
		}
	}
}


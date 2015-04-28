using System;
using Talker.BL;
using Talker.DL;
using System.IO;

namespace Talker.DAL
{
	// DataAccessManager is used to decide whether use cloud or local database
	public class DataAccessManager
	{
		protected static DataAccessManager me;
		public static DL.LocalDatabase LocalDB { get; set; }
		//private DL.CloudDatabase mCloudDB = null;

		static DataAccessManager ()
		{
			me = new DataAccessManager ();
		}

		protected DataAccessManager ()
		{
			// instantiate the database	
			LocalDB = new Talker.DL.LocalDatabase (DatabaseFilePath);
		}

		public static string DatabaseFilePath 
		{
			get {
				#if SILVERLIGHT
				// Windows Phone expects a local path, not absolute
					var path = Talker.BL.Constants.gSQLiteFilename;
				#else

				#if __ANDROID__
						// Just use whatever directory SpecialFolder.Personal returns
						string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
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


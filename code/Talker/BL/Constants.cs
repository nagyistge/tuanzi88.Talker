using System;

namespace Talker.BL
{
	// DESIGN#2:  Constants will contain all constant values in whole softwares
	public static class Constants
	{
		// Local SQL database
		public const string gSQLiteFilename = "Talker.db3";

		// Azure app specific URL and key
		public const string gApplicationURL = @"https://YOUR_APP_NAME.azure-mobile.net/";	// YIKANG P1
		public const string gApplicationKey = @"YOUR_APP_KEY";

		// RULE#4: the global parameter will start with a "g" as prefix
		// Max object ID, each object has an unique ID
		// YIKANG P1: save it in database table
		public static int gMaxObjectID = 0;
	}
}


using System;

namespace Talker.DL
{
	// DESIGN#2:  Constants will contain all constant values in whole softwares
	public static class Constants
	{
		// Local SQL database
        public const string gLocalDBPath = "localstore.db";

		// Azure app specific URL and key
        public const string gCloudApplicationURL = "https://talker.azure-mobile.net/";
		public const string gCloudApplicationKey = "xCKJqrBjIaEApwNmJYPDdWXZWOsevh29";
	}
}


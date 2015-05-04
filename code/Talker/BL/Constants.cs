using System;

namespace Talker.BL
{
	// DESIGN#2:  Constants will contain all constant values in whole softwares
	public static class Constants
	{
		// Local SQL database
		public const string gSQLiteFilename = "Talker.db3";

		// Azure app specific URL and key
		public const string gApplicationURL = @"https://taskymobile.azure-mobile.net/";	
		public const string gApplicationKey = @"AGyohxaIXDUHGYnMYolDNAeEVmhHze96";

		// Max object ID, each object has an unique ID
		// YIKANG P1: if auto-incremental cannot be used, then use gMaxObjectID to generate ID
		public static int gMaxObjectID = 0;
	}
}


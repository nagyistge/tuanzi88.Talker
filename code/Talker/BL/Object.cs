using System;
using Talker.DL;

namespace Talker.BL
{
	// DESIGN#1: All data objects will inherit from Object class
	public abstract class ObjectLocal
	{
		// Local ID
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		protected ObjectLocal ()
		{
			// YIKANG P3: IF autoIncrement works for cloud, then delete below
			//Constants.gMaxObjectID++;
			//ID = Constants.gMaxObjectID;
		}
	}

	public abstract class ObjectCloud
	{
		// Cloud ID
		public string ID { get; set; }
	}
}

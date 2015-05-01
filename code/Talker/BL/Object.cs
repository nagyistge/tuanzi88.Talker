using System;
using Talker.DL;

namespace Talker.BL
{
	// DESIGN#1: All data objects will inherit from Object class
	public abstract class Object
	{
		// RULE#1: Put all the properties and members in the bottom of class

		/// <summary>
		/// Gets or sets the Database ID.
		/// </summary>
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		// RULE#2: Put constructor in the top of class
		protected Object ()
		{
			// YIKANG P3: IF autoIncrement works for cloud, then delete below
			//Constants.gMaxObjectID++;
			//ID = Constants.gMaxObjectID;
		}
	}
}

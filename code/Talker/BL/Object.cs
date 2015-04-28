using System;

namespace Talker.BL
{
	// DESIGN#1: All data objects will inherit from Object class
	public abstract class Object
	{
		// RULE#1: Put all the properties and members in the bottom of class
		public int ID { get; set; }

		// RULE#2: Put constructor in the top of class
		protected Object ()
		{
			Constants.gMaxObjectID++;
			ID = Constants.gMaxObjectID;
		}
	}
}

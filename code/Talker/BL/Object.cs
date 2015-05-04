using System;

namespace Talker.BL
{
	// DESIGN#1: All data objects will inherit from Object class
	public abstract class Object
	{
		/// <summary>
		/// Gets or sets the Database ID.
		/// </summary>
		public string ID { get; set; }

		protected Object ()
		{
		}
	}
}

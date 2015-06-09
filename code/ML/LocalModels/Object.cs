using System;

namespace Talker.ML
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

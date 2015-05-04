using System;
using System.Collections.Generic;
using Talker.DAL;

namespace Talker.BL
{
	public enum UserType {
		None = 0,
		Teacher = 1,
		Student = 2,
	}

	public class User : Object 
	{
		public string Name { get; set; }
		public string Password { get; set; }
		//public UserType Type { get; set; }

		// RULE#3: All function paremeters will start with a "p" as prefix.
		public User (string pLoginName, string pPassword) //, UserType pType )
		{
			this.Name = pLoginName;
			this.Password = pPassword;
			//Type = pType;
		}

		public User()
		{}

		public void SendMessage( User pReceiver, string pText )
		{
			Message one = new Message (this, pReceiver, pText, false);
			MessageManager.SaveMessage (one);
		}
	}
}


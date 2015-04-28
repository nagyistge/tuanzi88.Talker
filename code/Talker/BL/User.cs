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
		public string LoginName { get; set; }
		public string LoginPassword { get; set; }
		public UserType Type { get; set; }
		public List<Message> Messages { get; set; }

		// RULE#3: All function paremeters will start with a "p" as prefix.
		public User ( string pLoginName, string pPassword, UserType pType )
		{
			this.LoginName = pLoginName;
			this.LoginPassword = pPassword;
			Type = pType;
		}

		public void SendMessage( User pReceiver, string pText )
		{
			Message one = new Message (this, pReceiver, pText);
			Messages.Add (one);
		}
	}
}


using System;
using Talker.DAL;

namespace Talker.BL
{
	public static class UserManager
	{
		static readonly IUserDatabase mDB;

		static UserManager ()
		{
			mDB = DataAccessManager.GetUserDB ();
		}

		public static bool IsThisUserExisted (string pName, string pPassword)
		{
			return mDB.IsThisUserExisted (pName, pPassword);
		}

		public static void SaveUser (User pUser)
		{
			mDB.SaveUser (pUser);
		}

		public static void SaveUser (string pName, string pPassword)
		{
			User one = new User (pName, pPassword);
			mDB.SaveUser (one);
		}
	}
}


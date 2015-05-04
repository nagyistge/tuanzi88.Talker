using System;
using System.Collections.Generic;
using Talker.DAL;
using System.Threading.Tasks;

namespace Talker.BL
{
	public static class UserLocalManager
	{
		static readonly IUserDatabase mDB = null;

        static UserLocalManager ()
		{
			if (mDB == null) {
				mDB = DataAccessManager.GetUserDB (false);
			}
		}


	}
}


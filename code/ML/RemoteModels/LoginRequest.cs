using System;
using Newtonsoft.Json;

namespace Talker.ML
{
	public class LoginRequest : WebObject
	{
		[JsonProperty("mUserName")]
		public string mUserName { get; set; }

		[JsonProperty("mPassword")]
		public string mPassword { get; set; }

		public LoginRequest(string pUserName, string pPassword)
		{
			this.mUserName = pUserName;
			this.mPassword = pPassword;
		}
	}
}


using System;
using Newtonsoft.Json;

namespace Talker.ML
{
	public class RegisterRequest : WebObject
	{
		[JsonProperty("mUserName")]
		public string mUserName { get; set;}

		[JsonProperty("mPassword")]
		public string mPassword { get; set;}

		[JsonProperty("mUserType")]
		public string mUserType { get; set;}

		public RegisterRequest(string pUserName, string pPassword, UserType pUserType)
		{
			this.mUserName = pUserName;
			this.mPassword = pPassword;
			this.mUserType = pUserType.ToString ();
		}
	}
}


using System;
using Newtonsoft.Json;

namespace ML
{
	public class RegisterRequest
	{
		[JsonProperty("mUserName")]
		public string mUserName { get; set; }

		[JsonProperty("mPassword")]
		public string mPassword { get; set; }

		[JsonProperty("mUserType")]
		public string mUserType { get; set; }
	}
}


using System;
using Newtonsoft.Json;

namespace ML
{
	public class LoginRequest
	{
		[JsonProperty("mUserName")]
		public string mUserName { get; set; }

		[JsonProperty("mPassword")]
		public string mPassword { get; set; }
	}
}


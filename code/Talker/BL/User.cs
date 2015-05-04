using System.Collections.Generic;
using Newtonsoft.Json;
using Talker.DAL;

namespace Talker.BL
{
	public enum UserType {
		None = 0,
		Teacher = 1,
		Student = 2,
	}

	public class User : ObjectCloud
	{
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set;}

		[JsonProperty(PropertyName = "password")]
		public string Password { get; set;}

		public User (string name, string password)
		{
			this.Name = name;
			this.Password = password;
		}

	}

	public class UserLocal : ObjectLocal
	{
		public string Name { get; set; }
		public string Password { get; set;}

		public UserLocal (string name, string password)
		{
			this.Name = name;
			this.Password = password;
		}

		public UserLocal()
		{}
	}
}


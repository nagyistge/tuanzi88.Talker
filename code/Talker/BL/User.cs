using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Talker.BL
{
	public enum UserType {
		Admin,
		Teacher,
		Student,
	}

	public class User : Object 
	{
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "password")]
		public string Password { get; set; }

        [JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

        public User (string pLoginName, string pPassword, string pType )
		{
			this.Name = pLoginName;
			this.Password = pPassword;
			this.Type = pType;
		}

        public User()
        {
            this.Name = "";
            this.Password = "";
            this.Type = "Student";
        }
	}
}


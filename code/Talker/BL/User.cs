using System;
using System.Collections.Generic;
using Talker.DAL;
using Newtonsoft.Json;

namespace Talker.BL
{
	public enum UserType {
		None = 0,
		Teacher = 1,
		Student = 2,
	}

	public class User : Object 
	{
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "password")]
		public string Password { get; set; }

        //[JsonProperty(PropertyName = "type")]
		//public UserType Type { get; set; }

        public User (string pLoginName, string pPassword)//, UserType pType )
		{
			this.Name = pLoginName;
			this.Password = pPassword;
			//this.Type = pType;
		}

        public User()
        {
            this.Name = "";
            this.Password = "";
        }
	}
}


using System;
using DAL;
using ML;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DL
{
	public class UserDBRemote : IUserDBRemote
	{
		public UserDBRemote ()
		{
		}
			
		public async Task<JObject> GetUserRemote(string pName, string pPassword)
		{
			/*
			WebConnectUtility webUtil = new WebConnectUtility ();

			//Completed: Functionalize GetUser
			LoginRequest loginRequest = new LoginRequest (pName, pPassword); 
			JObject loginResult = await webUtil.WebAzurePost("LoginRequest",loginRequest);
			return loginResult;
			*/
		}

		public async Task RegisterUserRemote(string pName, string pPassword, string pType)
		{
			
		}
	}
}


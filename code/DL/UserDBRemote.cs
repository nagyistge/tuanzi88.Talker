using System;
using DAL;
using ML;
using System.Threading.Tasks;

namespace DL
{
	public class UserDBRemote : IUserDBRemote
	{
		public UserDBRemote ()
		{
		}

		/*
		public async Task<JObject> GetUserRemote(string pName, string pPassword)
		{
			WebConnectUtility webUtil = new WebConnectUtility ();

			//Completed: Functionalize GetUser
			LoginRequest loginRequest = new LoginRequest (pName, pPassword); 
			JObject loginResult = await webUtil.WebAzurePost("LoginRequest",loginRequest);
			Console.WriteLine (loginResult.ToString ());
			return loginResult;
		}
		*/
	}
}


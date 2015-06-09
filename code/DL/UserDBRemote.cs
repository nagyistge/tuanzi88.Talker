using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.MobileServices;
using System.Diagnostics;
using Talker.ML;
using Talker.DAL;
using System.Net;

namespace Talker.DL
{
	public class UserDBRemote : IUserDBRemote
	{
		//Singleton Implementation
		private static MobileServiceClient mMobileService;
		private static UserDBRemote mUserDBRemote;

		private UserDBRemote()
		{
			if (mMobileService == null) 
			{
				mMobileService = new MobileServiceClient (new Uri(Constants.gCloudApplicationURL),Constants.gCloudApplicationKey);
			}
		}

		public static UserDBRemote mInstance
		{
			get
			{
				if (mUserDBRemote == null) 
				{
					mUserDBRemote = new UserDBRemote ();
				}
				return mUserDBRemote;
			}
		}
			
		public async Task<JObject> LogInUserRemote(string pName, string pPassword)
		{
			JObject loginResult;
			LoginRequest loginRequest = new LoginRequest (pName, pPassword); 

			try
			{
				loginResult = await mMobileService.InvokeApiAsync<WebObject,JObject>("LoginRequest",loginRequest);
			}
			catch(MobileServiceInvalidOperationException e)
			{
				string json = @"{Exception:'" + e.Message + "'}";
				loginResult = JObject.Parse (json);
			}

			return loginResult;
		}

		public async Task<JObject> RegisterUserRemote(string pName, string pPassword, UserType pUserType)
		{
			JObject registerResult;
			RegisterRequest registerRequest = new RegisterRequest (pName, pPassword, pUserType);

			try
			{
				await mMobileService.InvokeApiAsync<WebObject,JObject>("RegisterRequest",registerRequest);
				string json = @"{Created:''}";
				registerResult = JObject.Parse (json);
			}
			catch(MobileServiceInvalidOperationException e)
			{
				string json = @"{Exception:'" + e.Message + "'}";
				registerResult = JObject.Parse (json);
			}

			return registerResult;
		}
	}
}


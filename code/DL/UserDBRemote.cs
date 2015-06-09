using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.MobileServices;
using System.Diagnostics;
using Talker.ML;
using Talker.DAL;

namespace Talker.DL
{
	public class UserDBRemote : IUserDBRemote
	{
		//Singleton Implementation
		private static MobileServiceClient mMobileService = new MobileServiceClient(Constants.gCloudApplicationKey);
		private static UserDBRemote mUserDBRemote;

		private UserDBRemote()
		{
			
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
				Debug.WriteLine (e.Message);
                return null;
            }

			return loginResult;
		}

		public async Task RegisterUserRemote(string pName, string pPassword, UserType pUserType)
		{
			RegisterRequest registerRequest = new RegisterRequest (pName, pPassword, pUserType);

			try
			{
				await mMobileService.InvokeApiAsync<WebObject,JObject>("RegisterRequest",registerRequest);
			}
			catch(MobileServiceInvalidOperationException e)
			{
				Debug.WriteLine (e.Message);
				return;
			}
		}
	}
}


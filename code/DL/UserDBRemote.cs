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
		public MobileServiceClient mMobileService;

		public UserDBRemote () : base()
		{
			if (mMobileService == null) 
			{
				mMobileService = new MobileServiceClient ("");
			}
		}
			
		public async Task<JObject> GetUserRemote(string pName, string pPassword)
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


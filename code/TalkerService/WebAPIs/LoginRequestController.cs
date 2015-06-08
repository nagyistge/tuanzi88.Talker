using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using TalkerService.WebAPIs.ApiObjects;
using System.Security.Claims;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using TalkerService.ServiceProvider;
using TalkerService.Models;
using TalkerService.DataObjects;
using TalkerService.Security;

namespace TalkerService.WebAPIs
{
    public class LoginRequestController : ApiController
    {
        public ApiServices Services { get; set; }
        public IServiceTokenHandler handler { get; set; }

        public HttpResponseMessage Post(LoginRequest pLoginRequest)
        {
            TalkerContext talkerContext = new TalkerContext();
            User user = talkerContext.Users.Where(a => a.mName == pLoginRequest.mUserName).SingleOrDefault();

            if (user != null)
            {
                byte[] incomingPd = PasswordUtility.hash(pLoginRequest.mPassword, user.mSalt);
                if(PasswordUtility.slowEquals(incomingPd,user.mSaltedAndHashedPd))
                {
                    ClaimsIdentity claimsId = new ClaimsIdentity();
                    claimsId.AddClaim(new Claim(ClaimTypes.NameIdentifier, pLoginRequest.mUserName));
                    LoginResult loginResult = new CustomLoginProvider(handler).CreateLoginResult(claimsId, Services.Settings.MasterKey);
                    return this.Request.CreateResponse(HttpStatusCode.OK, loginResult);
                }
            }

            return this.Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid Username or Password");
        }

    }
}

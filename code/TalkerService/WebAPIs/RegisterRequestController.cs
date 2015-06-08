using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using TalkerService.WebAPIs.ApiObjects;
using TalkerService.Security;
using System.Text.RegularExpressions;
using TalkerService.DataObjects;
using TalkerService.Models;

namespace TalkerService.WebAPIs
{
    public class RegisterRequestController : ApiController
    {
        //Shuran: Notice that a pop-out window will still appear in this case to input username and password, just click cancel.
        //POST api/LoginRequest
        public HttpResponseMessage Post(RegisterRequest pRegisterRequest)
        {
            //Check if the Username is valid
            if (!Regex.IsMatch(pRegisterRequest.mUserName, "^[a-zA-Z0-9]{4,}$"))
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Username");
            }
            //Check if the Password is valid
            else if (pRegisterRequest.mPassword.Length < 8)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Password");
            }
            //Check if the user exists already
            TalkerContext context = new TalkerContext();
            User user = context.Users.Where(a => a.mName == pRegisterRequest.mUserName).SingleOrDefault();
            if (user != null)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, "User already exists");
            }
            //Register the user
            else
            {
                byte[] salt = PasswordUtility.generateSalt();

                User newUser = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    mName = pRegisterRequest.mUserName,
                    mSalt = salt,
                    mSaltedAndHashedPd = PasswordUtility.hash(pRegisterRequest.mPassword, salt),
                    mUserType = pRegisterRequest.mUserType
                };

                context.Users.Add(newUser);
                context.SaveChanges();
                //Return the success code
                return this.Request.CreateResponse(HttpStatusCode.Created);
            }
        }

    }
}

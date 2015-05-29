using Microsoft.WindowsAzure.Mobile.Service.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace TalkerService.ServiceProvider
{
    public class CustomLoginProvider : LoginProvider
    {
        public const string ProviderName = "talker";
        public CustomLoginProvider(IServiceTokenHandler tokenHandler)
            : base(tokenHandler)
        {
            this.TokenLifetime = new TimeSpan(30, 0, 0, 0);
        }

        //Shuran: Add in the override functions to inherit from the abstract class
        public override string Name
        {
            get { return ProviderName; }
        }

        public override ProviderCredentials CreateCredentials(ClaimsIdentity claimsIdentity)
        {
            if (claimsIdentity == null)
            {
                throw new ArgumentNullException("claimsIdentity");
            }
            return new CustomLoginProviderCredentials
            {
                UserId = this.TokenHandler.CreateUserId(this.Name, claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value)
            };
        }

        public override void ConfigureMiddleware(Owin.IAppBuilder appBuilder, Microsoft.WindowsAzure.Mobile.Service.ServiceSettingsDictionary settings)
        {
            return;
        }

        public override ProviderCredentials ParseCredentials(Newtonsoft.Json.Linq.JObject serialized)
        {
            if (serialized == null)
            {
                throw new ArgumentNullException("serialized");
            }

            return serialized.ToObject<CustomLoginProviderCredentials>();
        }
    }
}

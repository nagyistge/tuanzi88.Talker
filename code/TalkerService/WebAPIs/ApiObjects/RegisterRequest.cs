using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkerService.WebAPIs.ApiObjects
{
    public class RegisterRequest
    {
        public string mUserName { get; set; }
        public string mPassword { get; set; }

        public string mUserType { get; set; }
    }
}

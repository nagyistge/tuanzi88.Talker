using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Talker.ML;

namespace Talker.DAL
{
	public interface IUserDBRemote
	{
		Task<JObject> LogInUserRemote(string pName, string pPassword);
		Task RegisterUserRemote (string pName, string pPassword, UserType pUserType);
	}
}


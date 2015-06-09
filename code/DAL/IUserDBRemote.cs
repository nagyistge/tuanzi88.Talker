using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DAL
{
	public interface IUserDBRemote
	{
		Task<JObject> GetUserRemote(string pName, string pPassword);
		Task RegisterUserRemote (string pName, string pPassword, string pType);
	}
}


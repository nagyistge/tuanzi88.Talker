using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Talker.BL
{
    public interface IUserService : IBaseService
    {
        List<User> UserList { get; }
        Task SyncAsync();
        Task<List<User>> RefreshDataAsync();
        Task InsertUserAsync(User pUser);
        Task<User> GetUser(string pName, string pPassword, string pType);
    }
}


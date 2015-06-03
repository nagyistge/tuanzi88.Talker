using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Talker.ML;

namespace Talker.DAL
{
    public interface IMessageService : IBaseService
	{
        Task SyncAsync();
        Task RefreshDataAsync(string pUserID);
        Task InsertMessageAsync(Message pMessage);
        Task DeleteMessage(string pMessageID);
	}
}


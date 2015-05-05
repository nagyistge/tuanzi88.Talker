using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Talker.BL;

namespace Talker.DAL
{
    public interface IMessageService : IBaseService
	{
        List<Message> MessageList { get; }
        Task SyncAsync();
        Task<List<Message>> RefreshDataAsync(string pUserID);
        Task InsertMessageAsync(Message pMessage);
        Task DeleteMessage(string pMessageID);
	}
}


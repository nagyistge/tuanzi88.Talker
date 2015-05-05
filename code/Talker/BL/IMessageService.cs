using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Talker.DL;

namespace Talker.BL
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


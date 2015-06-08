using System;
using System.Threading.Tasks;

using Talker.ML;

namespace Talker.DAL
{
    public interface IMessageDB
	{
        Task SyncAsync();
        Task RefreshDataAsync(string pUserID);
        Task InsertMessageAsync(Message pMessage);
        Task DeleteMessage(string pMessageID);
	}
}


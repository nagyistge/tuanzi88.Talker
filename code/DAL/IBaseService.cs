using System;
using System.Threading.Tasks;

namespace Talker.DAL
{
    public interface IBaseService
    {
        Task InitializeStoreAsync();
    }
}


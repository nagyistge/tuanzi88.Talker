using System;
using System.Threading.Tasks;

namespace Talker.BL
{
    public interface IBaseService
    {
        Task InitializeStoreAsync();
    }
}


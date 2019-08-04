using System.Collections.Generic;
using System.Threading.Tasks;
using Zcore.Dto.Interfaces;
using Zcore.Service;

namespace LibCore
{
    public interface ILogicBatchService<T> : ILogicService<T> where T : class, new()
    {
        Task<IPostBatchModel> Post(IUserSession userSession, T[] collection);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using LibCore;
using Zcore.Dto.Interfaces;

namespace Zcore.Service
{
    public interface ILogicService
    {
        Task<IEnumerable<object>> GetAll(IUserSession userSession);
        Task<object> GetOne(IUserSession userSession, long id);
        Task<object> Put(IUserSession userSession, long id, object value);
        Task<IPostResponseModel> Post(IUserSession userSession, object value);
        Task Delete(IUserSession userSession, long id);
    }

    public interface ILogicService<T> : ILogicService where T : class, new()
    {
       new Task<IEnumerable<T>> GetAll(IUserSession userSession);
       new Task<T> GetOne(IUserSession userSession, long id);
       Task<T> Put(IUserSession userSession, long id, T value);
       Task<IPostResponseModel> Post(IUserSession userSession, T value);
    }
}

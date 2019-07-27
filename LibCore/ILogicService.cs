using System.Collections.Generic;
using System.Threading.Tasks;
using Zcore.Dto.Interfaces;

namespace Zcore.Service
{
    public interface ILogicService
    {
        Task<IEnumerable<object>> GetAll(IUserSession userSession);
        Task<object> GetOne(IUserSession userSession, int id);
        Task<object> Put(IUserSession userSession, int id, object value);
        Task<int> Post(IUserSession userSession, object value);
        Task Delete(IUserSession userSession, int id);
    }

    public interface ILogicService<T> : ILogicService where T : class, new()
    {
       new Task<IEnumerable<T>> GetAll(IUserSession userSession);
       new Task<T> GetOne(IUserSession userSession, int id);
       Task<T> Put(IUserSession userSession, int id, T value);
       Task<int> Post(IUserSession userSession, T value);
       new Task Delete(IUserSession userSession, int id);
    }
}

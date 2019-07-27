using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zcore.Dto.Interfaces;

namespace Zcore.Service
{
    public class SensorDataService : IService
    {
        public Task<IEnumerable<object>> GetAll(IUserSession userSession)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetOne(IUserSession userSession, int id)
        {
            throw new NotImplementedException();
        }

        public Task<object> Put(IUserSession userSession, int id, object value)
        {
            throw new NotImplementedException();
        }

        public Task Delete(IUserSession userSession, int id)
        {
            throw new NotImplementedException();
        }
    }
}

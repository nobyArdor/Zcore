using System;
using System.Threading.Tasks;
using Zcore.Dto;
using Zcore.Dto.Interfaces;

namespace Zcore.Tools
{
    public class ByPassAuthManager : IUserManager
    {
        public async Task <IUserSession> Auth(object authData)
        {
            if (authData is string model)
                return await Auth(model);
            return new NoAuthSession();
        }

        public async Task<IUserSession> Auth(string authData)
        {
            throw new NotImplementedException();
        }

        public async Task<IUserSession> CheckAuth(object authToken)
        {
            if (authToken is string model)
                return await CheckAuth(model);
            return new NoAuthSession();
        }

        public async Task<IUserSession> CheckAuth(string authToken)
        {
            throw new NotImplementedException();
        }
    }
}

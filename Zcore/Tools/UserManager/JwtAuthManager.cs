using System;
using System.Threading.Tasks;
using Zcore.Dto;
using Zcore.Dto.Interfaces;
using Zcore.NetModels;

namespace Zcore.Tools
{
    public class JwtAuthManager : IUserManager
    {
        public async Task<IUserSession> Auth(object authData)
        {
            if (authData is ClassicUserLoginModel model)
                return await Auth(model);

            return new NoAuthSession();
        }

        protected async Task<IUserSession> Auth(ClassicUserLoginModel loginModel)
        {
            throw new NotImplementedException();
        }


        public async Task <IUserSession> CheckAuth(object authToken)
        {
            if (authToken is string model)
                return await CheckAuth(model);

            return new NoAuthSession();
        }

        protected Task<IUserSession> CheckAuth(string authToken)
        {
            throw new NotImplementedException();
        }
    }
}

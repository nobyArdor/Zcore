using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zcore.Dto;
using Zcore.Dto.Interfaces;

namespace Zcore.Tools
{
    public class ByPassAuthManager : IUserManager
    {
        public IUserSession Auth(object authData)
        {
            if (authData is string model)
                return Auth(model);
            return new NoAuthSession();
        }

        public IUserSession Auth(string authData)
        {
            throw new NotImplementedException();
        }

        public IUserSession CheckAuth(object authToken)
        {
            if (authToken is string model)
                return CheckAuth(model);
            return new NoAuthSession();
        }

        public IUserSession CheckAuth(string authToken)
        {
            throw new NotImplementedException();
        }
    }
}

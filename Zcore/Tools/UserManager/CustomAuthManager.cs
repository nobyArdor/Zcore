using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DbCore;
using Microsoft.EntityFrameworkCore;
using Zcore.Dto;
using Zcore.Dto.Interfaces;

namespace Zcore.Tools
{
    public class CustomAuthManager : IUserManager
    {
        private readonly BDContext _bdContext;

        public CustomAuthManager(BDContext bdContext)
        {
            _bdContext = bdContext;
        }
            public async Task<IUserSession> Auth(object authData)
            {
                if (authData is string model)
                    return await Auth(model);
                return new NoAuthSession();
            }

            public async Task<IUserSession> Auth(string authData)
            {
                if (string.IsNullOrEmpty(authData))
                    return new NoAuthSession();

                return await CheckAuth(authData);
            }

            public async Task<IUserSession> CheckAuth(object authToken)
            {
                if (authToken is string model)
                    return await CheckAuth(model);
                return new NoAuthSession();
            }

            public async Task<IUserSession> CheckAuth(string authToken)
            {
                authToken = authToken.Trim('\"', '\'', ' ');
               var session = await _bdContext.Sessions.FirstOrDefaultAsync(x => x.Token == authToken);
                if (session == null || DateTime.Now > session.Expire)
                    return new NoAuthSession();

                return new UserSession(true, session.UserId);
            }
        }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
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

        // TODO передалать на БД.
        private readonly Dictionary<string, int> _byPassDictionary = new Dictionary<string, int>()
        {
            {"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiMTIzNDU2Nzg5MCIsIm5hbWUiOiJQYXJlbnQiLCJpYXQiOjE1MTYyMzkwMjJ9.uOk3GDxGriOcxczL-Q1Z6EW7Tbs2vDXlMUEINSA64gk", 1},
            {"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiMTIzNDU2Nzg5MCIsIm5hbWUiOiJDaGlsZCIsImlhdCI6MTUxNjIzOTAyMn0.WWS9CvvPLv94pqfbzDjXRrAic6YiTV4bdwGBJcPU7y4", 2 }
        };
        public async Task<IUserSession> CheckAuth(string authToken)
        {
            authToken = HttpUtility.UrlDecode(authToken).Trim('\"', '\'', ' ');
           if (!_byPassDictionary.ContainsKey(authToken))
               return new NoAuthSession();

           return new UserSession(true, _byPassDictionary[authToken]);
        }
    }
}

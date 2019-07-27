using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zcore.Dto.Interfaces;

namespace Zcore.Tools
{
    public interface IUserManager
    {
        Task <IUserSession> Auth(object authData);
        Task <IUserSession> CheckAuth(object authToken);
    }
}

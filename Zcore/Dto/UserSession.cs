using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zcore.Dto.Interfaces;

namespace Zcore.Dto
{
    public class UserSession : IUserSession
    {
        public bool State { get; }
        public int UserId { get; }

        public UserSession(bool state, int userId)
        {
            State = state;
            UserId = userId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zcore.Dto.Interfaces;

namespace Zcore.Dto
{
    public class NoAuthSession : IUserSession
    {
        public bool State { get; } = false;
        public long UserId { get; } = long.MinValue;
    }
}

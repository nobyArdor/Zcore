using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zcore.Dto.Interfaces
{
    public interface IUserSession
    {
        bool State { get; }
        long UserId { get; }
    }
}

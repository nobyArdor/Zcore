using Zcore.Dto.Interfaces;

namespace Zcore.Dto
{
    public class UserSession : IUserSession
    {
        public bool State { get; }
        public long UserId { get; }

        public UserSession(bool state, long userId)
        {
            State = state;
            UserId = userId;
        }
    }
}

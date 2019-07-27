using DbCore;

namespace Zcore.Service
{
    public abstract class DbLogicService
    {
        protected readonly BDContext DbContext;

        protected DbLogicService(BDContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}

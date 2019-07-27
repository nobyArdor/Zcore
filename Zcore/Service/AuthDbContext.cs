using System;
using System.Linq.Expressions;
using DbCore;
using LibCore;
using Zcore.Tools;

namespace Zcore.Service
{
    public abstract class AuthDbContext<T> : DbLogicService where T: class, IPrimaryKeyContainer, new()
    {
        protected AuthDbContext(BDContext dbContext) : base(dbContext)
        {

        }

        protected abstract Expression<Func<T, bool>> ByAuth(object value);

        protected Expression<Func<T, bool>> ById(object id)
        {
            T typeKeeper = null;
            return LambdaExpressionGenerator.CaptureFuncParameter<T>(nameof(typeKeeper.Id), id);
        }
    }
}

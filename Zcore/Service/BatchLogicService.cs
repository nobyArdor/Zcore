using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbCore;
using LibCore;
using Microsoft.EntityFrameworkCore;
using Zcore.Dto.Interfaces;
using Zcore.NetModels;
using Zcore.Tools;

namespace Zcore.Service
{
    public abstract class BatchLogicService <T> : AuthDbService<T>, ILogicBatchService<T> where T : class, IPrimaryKeyContainer, new()
    {
        protected BatchLogicService(BDContext dbContext) : base(dbContext)
        {
        }

        public async Task<IPostBatchModel> Post(IUserSession userSession, T[] collection)
        {
            var query = new LinkedList<T>();
            foreach (var primaryKeyContainer in collection)
            {
                if (primaryKeyContainer is IAuthAffectedModel affected)
                    affected.UserId = userSession.UserId;

                var exist = await AllReadyExist(primaryKeyContainer);
                var res = exist ?? (await Container.AddAsync(primaryKeyContainer)).Entity;
                query.AddLast(res);
            }
            await DbContext.SaveChangesAsync();
            return new PostBatchModel() { Ids = query.Select(x => (IPostResponseModel) new PostBaseModel(){Id = x.Id }).ToArray() };
        }
    }
}

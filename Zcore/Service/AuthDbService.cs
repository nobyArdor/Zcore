using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DbCore;
using LibCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Zcore.Dto.Interfaces;
using Zcore.NetModels;
using Zcore.Tools;

namespace Zcore.Service
{
    public abstract class AuthDbService<T> : DbLogicService, ILogicService<T> where T: class, IPrimaryKeyContainer, new()
    {
        protected AuthDbService(BDContext dbContext) : base(dbContext)
        {

        }

        protected static readonly IPostResponseModel EmptyPostResponseModel = new PostBaseModel();

        protected abstract Expression<Func<T, bool>> ByAuth(object value);

        protected Expression<Func<T, bool>> ById(object id)
        {
            T typeKeeper = null;
            return LambdaExpressionT.CaptureFuncParameter<T>(nameof(typeKeeper.Id), id);
        }

        protected abstract LambdaExpressionT.Updater<T> Updater { get; }

        protected abstract DbSet<T> Container { get; }

        public async Task<IEnumerable<T>> GetAll(IUserSession userSession)
        {
            return await Container.Where(ByAuth(userSession)).ToListAsync();
        }

        public async Task<T> GetOne(IUserSession userSession, long id)
        {
            return await Container.Where(ByAuth(userSession)).FirstOrDefaultAsync(ById(id));
        }

        public async Task<T> Put(IUserSession userSession, long id, T value)
        {
            var finded = await Container.Where(ByAuth(userSession)).FirstOrDefaultAsync(ById(id));
            if (finded == null)
                return null;

            if (value is IAuthAffectedModel affected)
                affected.UserId = userSession.UserId;

            Updater(value, finded);
            await DbContext.SaveChangesAsync();
            return finded;
        }

        protected async Task<T> AllReadyExist(T value)
        {
            var expression = LambdaExpressionT.CreateSameObjectChecker(value);
            var exist = await Container.FirstOrDefaultAsync(expression);
            return exist;
        }

        public virtual async Task<IPostResponseModel> Post(IUserSession userSession, T value)
        {
            if (value is IAuthAffectedModel affected)
                affected.UserId = userSession.UserId;

            var res = await AllReadyExist (value) ?? (await Container.AddAsync(value)).Entity;
            await DbContext.SaveChangesAsync();
            return new PostBaseModel() {Id = res.Id };
        }

        public async Task Delete(IUserSession userSession, long id)
        {
            var finded = await Container.Where(ByAuth(userSession)).FirstOrDefaultAsync(ById(id));
            if (finded != null)
                Container.Remove(finded);
            
            await DbContext.SaveChangesAsync();
        }

        async Task<IEnumerable<object>> ILogicService.GetAll(IUserSession userSession)
        {
            return await GetAll(userSession);
        }
        async Task<object> ILogicService.GetOne(IUserSession userSession, long id)
        {
            return await GetOne(userSession, id);
        }

        async Task<IPostResponseModel> ILogicService.Post(IUserSession userSession, object value)
        {
            var tData = ConvertValue<T>(value);
            if (tData != null)
                return await Post(userSession, tData);

            return EmptyPostResponseModel;
        }

        async Task<object> ILogicService.Put(IUserSession userSession, long id, object value)
        {
            var tData = ConvertValue<T>(value);
            if (tData != null)
                return await Put(userSession, id, tData);

            return null;
        }

        protected TU ConvertValue<TU>(object value) where TU : class, new ()
        {
            if (value is JObject jObject)
                value = jObject.ToObject<TU>();

            if (value is TU tData)
                return tData;

            return null;
        }
    }
}

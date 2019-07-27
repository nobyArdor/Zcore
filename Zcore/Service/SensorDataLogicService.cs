using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DbCore;
using DbCore.Models;
using Microsoft.EntityFrameworkCore;
using Zcore.Dto.Interfaces;
using Zcore.Tools;

namespace Zcore.Service
{
    public class SensorDataLogicService : AuthDbContext<SensorData>, ILogicService<SensorData>
    {
        protected override Expression<Func <SensorData, bool>> ByAuth(object value)
        {
            if (value is IUserSession userSession)
                return LambdaExpressionGenerator.CaptureFuncParameter<SensorData>(nameof(SensorData.UserId),
                    userSession.UserId);

            return x => false;
        }

        public SensorDataLogicService(BDContext dbContext) : base(dbContext)
        {
        }


       async Task<IEnumerable<object>> ILogicService.GetAll(IUserSession userSession)
        {
            return await GetAll(userSession);
        }

        public async Task<SensorData> GetOne(IUserSession userSession, int id)
        {
            return await DbContext.SensorData.Where(ByAuth(userSession)).FirstOrDefaultAsync(ById(id));
        }

        public async Task<SensorData> Put(IUserSession userSession, int id, SensorData value)
        {
            var finded = await DbContext.SensorData.Where(ByAuth(userSession)).FirstOrDefaultAsync(ById(id));
            if (finded == null)
                return null;

            finded.UserId = value.UserId;
            finded.Type = value.Type;

        }

        public Task<SensorData> Post(IUserSession userSession, SensorData value)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SensorData>> GetAll(IUserSession userSession)
        {
            return await DbContext.SensorData.Where(ByAuth(userSession)).ToListAsync();
        }

        public Task<object> GetOne(IUserSession userSession, int id)
        {
            throw new NotImplementedException();
        }

        public Task<object> Put(IUserSession userSession, int id, object value)
        {
            throw new NotImplementedException();
        }

        public Task<object> Post(IUserSession userSession, object value)
        {
            throw new NotImplementedException();
        }

        public Task Delete(IUserSession userSession, int id)
        {
            throw new NotImplementedException();
        }
    }
}

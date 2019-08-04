using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DbCore;
using DbCore.Models;
using LibCore;
using Microsoft.EntityFrameworkCore;
using Zcore.Dto.Interfaces;
using Zcore.NetModels;
using Zcore.Tools;

namespace Zcore.Service
{
    public class SensorDataLogicService : AuthDbService<SensorData>, ILogicService<SensorData>
    {
        protected override Expression<Func<SensorData, bool>> ByAuth(object value)
        {
            if (value is IUserSession userSession)
                return LambdaExpressionT.CaptureFuncParameter<SensorData>(nameof(SensorData.UserId),
                    userSession.UserId);

            return x => false;
        }

        protected override LambdaExpressionT.Updater<SensorData> Updater { get; } = LambdaExpressionT
            .CreateUpdater<SensorData>();

        protected override DbSet<SensorData> Container => DbContext.SensorData;

        public SensorDataLogicService(BDContext dbContext) : base(dbContext)
        {
        }

        async Task<IPostResponse> ILogicService.Post(IUserSession userSession, object value)
        {
            var tuData = ConvertValue<SensorMarkedData>(value) ?? ConvertValue<SensorData>(value);
            if (tuData != null)
                return await Post(userSession, tuData);

            return EmptyPostResponse;
        }

        private async Task<SensorData> AllReadyExist(IUserSession userSession, SensorData value)
        {
            value.UserId = userSession.UserId;
            var result = await Container.FirstOrDefaultAsync(x => x.UserId == value.UserId &&
                                                                  x.Date == value.Date &&
                                                                  x.Type == value.Type &&
                                                                  x.Value == value.Value);
            return result;
        }

        public override async Task<IPostResponse> Post(IUserSession userSession, SensorData value)
        {
            var existed = await AllReadyExist(userSession, value);
            var result = existed != null ? new PostBaseModel {Id = existed.Id} : null;
            return result ?? await base.Post(userSession, value);
        }
    }
}
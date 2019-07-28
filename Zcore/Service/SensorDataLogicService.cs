using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DbCore;
using DbCore.Models;
using Microsoft.EntityFrameworkCore;
using Zcore.Dto.Interfaces;
using Zcore.NetModels;
using Zcore.Tools;

namespace Zcore.Service
{
    public class SensorDataLogicService : AuthDbService<SensorData>, ILogicService<SensorData>
    {
        protected override Expression<Func <SensorData, bool>> ByAuth(object value)
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

        async Task<long> ILogicService.Post(IUserSession userSession, object value)
        {
            var tuData = ConvertValue<SensorMarkedData>(value) ?? ConvertValue<SensorData>(value);
            if (tuData != null)
                return await Post(userSession, tuData);

            return 0L;
        }

        public override async Task<long> Post(IUserSession userSession, SensorData value)
        {
            if (!(value is SensorMarkedData markedData) || !markedData.IsNotify)
                return await base.Post(userSession, value);

            var notifyList =  await DbContext.UserRelations.Where(x => x.UserSourceId == userSession.UserId).ToListAsync();

            var notifies = notifyList.Select(x => new NotifyRecords()
            {
                Notification = $"У пользователя {x.Name} критическое понижение давления. Свяжитесь с ней!",
                State = int.MaxValue,
                UserId = x.UserDestId
            });
            DbContext.NotifyRecords.AddRange(notifies);
            await DbContext.SaveChangesAsync();
            return await base.Post(userSession, value);

        }
    }
}

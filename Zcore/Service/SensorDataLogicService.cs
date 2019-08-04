using System;
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
    public class SensorDataLogicService : BatchLogicService<SensorData>, ILogicService<SensorData>, ILogicBatchService<SensorData>
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
    }
}
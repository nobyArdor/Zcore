using System;
using System.Linq.Expressions;
using DbCore;
using DbCore.Models;
using Microsoft.EntityFrameworkCore;
using Zcore.Dto.Interfaces;
using Zcore.Tools;

namespace Zcore.Service
{
    public class NotifyRecordsLogicService : AuthDbService<NotifyRecords> 

    {
        public NotifyRecordsLogicService(BDContext dbContext) : base(dbContext)
        {
        }

        protected override Expression<Func<NotifyRecords, bool>> ByAuth(object value)
        {
            if (value is IUserSession session)
                return LambdaExpressionT.CaptureFuncParameter<NotifyRecords>(nameof(NotifyRecords.UserId), session.UserId);

            return x => false;
        }

        protected override LambdaExpressionT.Updater<NotifyRecords> Updater { get; } =
            LambdaExpressionT.CreateUpdater<NotifyRecords>();
        protected override DbSet<NotifyRecords> Container => DbContext.NotifyRecords;
    }
}

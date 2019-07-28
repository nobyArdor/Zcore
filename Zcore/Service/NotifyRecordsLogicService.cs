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
    public class NotifyRecordsLogicService : AuthDbService<NotifyRecords> 

    {
        public NotifyRecordsLogicService(BDContext dbContext) : base(dbContext)
        {
        }

        protected override Expression<Func<NotifyRecords, bool>> ByAuth(object value)
        {
            if (value is IUserSession session)
                LambdaExpressionT.CaptureFuncParameter<NotifyRecords>(nameof(NotifyRecords.UserId), session.UserId);

            return x => false;
        }

        protected override LambdaExpressionT.Updater<NotifyRecords> Updater { get; } =
            LambdaExpressionT.CreateUpdater<NotifyRecords>();
        protected override DbSet<NotifyRecords> Container => DbContext.NotifyRecords;
    }
}

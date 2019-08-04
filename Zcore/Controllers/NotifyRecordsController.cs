using DbCore.Models;
using Zcore.Service;
using Zcore.Tools;

namespace Zcore.Controllers
{
    public class NotifyRecordsController : CommonController<NotifyRecords>
    {
        public NotifyRecordsController(IUserManager manager, ILogicService<NotifyRecords> logicService) : base(manager, logicService)
        {
        }
    }
}
using DbCore.Models;
using LibCore;
using Zcore.Service;
using Zcore.Tools;

namespace Zcore.Controllers
{
    public class SensorDataController : BatchController<SensorData>
    {
        public SensorDataController(IUserManager manager, ILogicService<SensorData> logicService, ILogicBatchService<SensorData> batchService) : base(manager, logicService, batchService)
        {
        }
    }
}
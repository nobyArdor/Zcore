using System.Diagnostics;
using System.Threading.Tasks;
using LibCore;
using Microsoft.AspNetCore.Mvc;
using Zcore.NetModels;
using Zcore.Service;
using Zcore.Tools;

namespace Zcore.Controllers
{
    public class BatchController<T> : CommonController<T> where T : class, new()
    {
        private readonly ILogicBatchService<T> _logicBatchService;

        public BatchController(IUserManager manager, ILogicService<T> logicService, ILogicBatchService<T> logicBatchService) : base(manager, logicService)
        {
            Debug.Assert(logicBatchService != null, "logicBatchService == null");
            _logicBatchService = logicBatchService;
        }

        [Route("batch"), HttpPost ]
        public virtual async Task<IActionResult> Post([FromHeader] string authorization, [FromBody] BatchModel<T> value)
        {
            var user = await CheckAuth(authorization);
            if (!user.State)
                return ForbidResult;

            return Ok(await _logicBatchService.Post(await CheckAuth(authorization), value.Collection));
        }
    }
}

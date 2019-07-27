using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zcore.Service;
using Zcore.Tools;

namespace Zcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController<T> : AuthenticatedController where T: class, new()
    {
        private readonly ILogicService<T> _logicService;

        public CommonController(IUserManager manager, ILogicService<T> logicService) : base(manager)
        {
            Debug.Assert(logicService != null, "logicService == null");
            _logicService = logicService;
        }
        public override async Task<IActionResult> GetCollection(string authorization)
        {
            var value  = await base.GetCollection(authorization);
            return value ?? Ok(await _logicService.GetAll( await CheckAuth(authorization)));
        }

       
        public override async Task<IActionResult> GetOne(string authorization, int id)
        {
            var value = await base.GetOne(authorization, id);
            return value ?? Ok(await _logicService.GetOne(await CheckAuth(authorization), id));
        }

        public override async Task<IActionResult> Put(string authorization, int id, object value)
        {
            var updated = await base.Put(authorization, id, value);
            return updated ?? Ok(await _logicService.Put(await CheckAuth(authorization), id, value));
        }

        public override async Task<IActionResult> Delete(string authorization, int id)
        {
            var value = await base.Delete(authorization, id);
            if (value != null)
                return value;

            await _logicService.Delete(await CheckAuth(authorization), id);
            return Ok();
        }

        public override async Task<IActionResult> Post(string authorization, object value)
        {
            var saved = await base.Post(authorization, value);
            return saved ?? Ok(await _logicService.Post(await CheckAuth(authorization), value));
        }
    }
}

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
        private readonly IService<T> _service;

        public CommonController(IUserManager manager, IService<T> service) : base(manager)
        {
            Debug.Assert(service != null, "service == null");
            _service = service;
        }
        public override async Task<IActionResult> GetCollection(string authorization)
        {
            var value  = await base.GetCollection(authorization);
            return value ?? Ok(await _service.GetAll( await CheckAuth(authorization)));
        }

       
        public override async Task<IActionResult> GetOne(string authorization, int id)
        {
            var value = await base.GetOne(authorization, id);
            return value ?? Ok(await _service.GetOne(await CheckAuth(authorization), id));
        }

        public override async Task<IActionResult> Put(string authorization, int id, object value)
        {
            var updated = await base.Put(authorization, id, value);
            return updated ?? Ok(await _service.Put(await CheckAuth(authorization), id, value));
        }

        public override async Task<IActionResult> Delete(string authorization, int id)
        {
            var value = await base.Delete(authorization, id);
            if (value != null)
                return value;

            await _service.Delete(await CheckAuth(authorization), id);
            return Ok();
        }

        public override async Task<IActionResult> Post(string authorization, object value)
        {
            var saved = await base.Post(authorization, value);
            return saved ?? Ok(await _service.Post(await CheckAuth(authorization), value));
        }
    }
}

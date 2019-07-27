using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zcore.Dto.Interfaces;
using Zcore.Tools;

namespace Zcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class AuthenticatedController : ControllerBase
    {
        private readonly IUserManager _userManager;

        protected AuthenticatedController(IUserManager userManager)
        {
            Debug.Assert(userManager != null, "userManager == null");
            _userManager = userManager;
        }

        protected async Task <IUserSession> CheckAuth([FromHeader] string authorization )
        {
           return await _userManager.CheckAuth(authorization);
        }

        [HttpGet]
        public virtual async Task <IActionResult> GetCollection([FromHeader] string authorization)
        {
            var user = await CheckAuth(authorization);
            return !user.State ? new ForbidResult() : null;
        }

        [HttpGet("{id}")]
        public virtual async Task <IActionResult> GetOne([FromHeader] string authorization, [FromQuery] int id)
        {
            var user =  await CheckAuth(authorization);
            return !user.State ? new ForbidResult() : null;
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put([FromHeader] string authorization, [FromQuery] int id, [FromBody] object value)
        {
            var user = await CheckAuth(authorization);
            return !user.State ? new ForbidResult() : null;
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromHeader] string authorization, [FromBody] object value)
        {
            var user = await CheckAuth(authorization);
            return !user.State ? new ForbidResult() : null;
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete([FromHeader] string authorization, [FromQuery] int id)
        {
            var user = await CheckAuth(authorization);
            return !user.State ? new ForbidResult() : null;
        }
    }
}

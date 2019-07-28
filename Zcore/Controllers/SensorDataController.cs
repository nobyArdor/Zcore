using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zcore.Service;
using Zcore.Tools;

namespace Zcore.Controllers
{
    [Route("api/[controller]")]
    public class SensorDataController : CommonController<SensorData>
    {
        public SensorDataController(IUserManager manager, ILogicService<SensorData> logicService) : base(manager, logicService)
        {
        }
    }
}
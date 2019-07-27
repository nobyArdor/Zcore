using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zcore.NetModels;

namespace Zcore.Controllers
{
    [Route("api/zmagic")]
    public class RegisterController :BaseController
    {
        [HttpGet("/parent")]
        DumbassLoginModel GetParent()
        {
            return new DumbassLoginModel() { AuthToken = ""} ;
        }

        [HttpGet("/child")]
        DumbassLoginModel GetChild()
        {
            return new DumbassLoginModel() { AuthToken = "" };
        }
    }
}

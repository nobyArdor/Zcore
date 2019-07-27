using System;
using Microsoft.AspNetCore.Mvc;
using Zcore.NetModels;

namespace Zcore.Controllers
{
    public class ZmagicController : BaseController
    {
        [HttpGet("parent")]
        public IActionResult ParentGet()
        {
            return Ok(new DumbassLoginModel
            {
                AuthToken =
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiMTIzNDU2Nzg5MCIsIm5hbWUiOiJQYXJlbnQiLCJpYXQiOjE1MTYyMzkwMjJ9.uOk3GDxGriOcxczL-Q1Z6EW7Tbs2vDXlMUEINSA64gk"
            });
        }

        [HttpGet("child")]
        public IActionResult ChildGet()
        {
            return Ok(new DumbassLoginModel
            {
                AuthToken =
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiMTIzNDU2Nzg5MCIsIm5hbWUiOiJDaGlsZCIsImlhdCI6MTUxNjIzOTAyMn0.WWS9CvvPLv94pqfbzDjXRrAic6YiTV4bdwGBJcPU7y4"
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody] object value)
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            rnd.Next();
            rnd.Next();
            return Ok(rnd.Next());
        }
    }
}
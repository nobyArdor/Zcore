using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zcore.NetModels;

namespace Zcore.Controllers
{
    //TODO Занимить на обычный OAuth по Jwt
    public class ZmagicController : BaseController
    {
        private const string Secret = "Zabota+";
        //TODO убрать.
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegOmsModel value)
        {
            var oms = value.Oms.Trim('"', '\'', ' ');
            var date = DateTime.ParseExact(value.BirthDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var birthdate = $"{date:dd.MM.yyyy}";
            var preseqence = oms + birthdate;
            var bytes = Encoding.UTF8.GetBytes(preseqence);
            var base64 = Convert.ToBase64String(bytes);
            var checkValue = oms + birthdate + $"{DateTime.Today:dd.MM.yyyy}" + Secret;
            var challengeVerification = string.Empty; 
            using (var sha2 = new SHA256Managed())
            {
                var data = Encoding.UTF8.GetBytes(checkValue);
                var sha = sha2.ComputeHash(data);
                challengeVerification = BitConverter.ToString(sha).Replace("-","").ToLowerInvariant();
            }

            var model = new TestShaModel
            {
                Base64Value = base64, ChallengeVerification = challengeVerification, CheckValue = checkValue,
                Preseqence = preseqence,
                Challenge = $"{base64}.{challengeVerification}"
            };

            return Ok(model);
            //  = Hash.CreateSHA256()
        }


    }
}
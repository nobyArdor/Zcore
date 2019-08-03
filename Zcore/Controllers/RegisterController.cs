using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DbCore;
using DbCore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Zcore.NetModels;

namespace Zcore.Controllers
{
    public class RegisterController : BaseController
    {
        private const string Secret = "Zabota+";
        private readonly BDContext _bdContext;

        public RegisterController(BDContext bdContext)
        {
            _bdContext = bdContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] object obj)
        {
            var value = ConvertValue<RegisterChallengModel>(obj);


            Console.WriteLine(value.Challenge);
           var arr =  value.Challenge.Split('.', StringSplitOptions.RemoveEmptyEntries);
           var base64 = arr[0];
           var challenge = arr[1];
           var secret = Convert.FromBase64String(base64);;
           var preseqence = Encoding.UTF8.GetString(secret);
           var checkValue = preseqence + $"{DateTime.Today:dd.MM.yyyy}" + Secret;
           var challengeVerification = string.Empty;
           using (var sha2 = new SHA256Managed())
           {
               var data = Encoding.UTF8.GetBytes(checkValue);
               var sha = sha2.ComputeHash(data);
               challengeVerification = BitConverter.ToString(sha).Replace("-", "").ToLowerInvariant();
           }

           if (!string.Equals(challengeVerification, challenge))
               return Forbid();

           var bytes = new byte[(152 / 8) * 6];
           var rand = new Random((int) DateTime.Now.ToFileTime());
           rand.NextBytes(bytes);
           var auth = Convert.ToBase64String(bytes);

           var user = new Users()
           {
               LastLogin = DateTime.Now,
               RegSecret = secret
           };

           _bdContext.Users.Add(user);
           var session = new Sessions()
           {
               Expire = DateTime.Today.AddYears(1),
               Token = auth,
               User = user
           };

           _bdContext.Sessions.Add(session);
           await _bdContext.SaveChangesAsync();
           return Ok(new RegisterResponse {Auth = auth});
        }

        protected TU ConvertValue<TU>(object value) where TU : class, new()
        {
            Console.WriteLine(value);
            if (value is JObject jObject)
            {
                Console.WriteLine(jObject.ToString());
                value = jObject.ToObject<TU>();
            }

            if (value is TU tData)
                return tData;

            return null;
        }
    }
}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Caching.Memory;
namespace WebApplication3.Controllers
{
     [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthendicationController : ControllerBase
    {

        private readonly Ilogin _Ilogin;
        private readonly IMemarycached _memoryCache;
        public AuthendicationController(Ilogin login, IMemarycached memoryCache)
        {
           _Ilogin = login;
           _memoryCache = memoryCache;
        }
    
        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult>  Login(Loginuser loginuser)
        {
                    return Ok(new
                    {
                        token = await _Ilogin.Login(loginuser)
                    });
        }
    
        [Authorize(Roles = "user")]
        [Route("GetResult")]
        [HttpGet]
        public async   Task<IActionResult> GetResult()
        {
            
          return Ok(await Task.FromResult(new { sucess = "Authorized succefully" }));
        }


        // 403 forbidden issue  due to  role  issue
        [Authorize(Roles = "Admin")]
        [Route("Result")]
        [HttpGet]
        public async Task<IActionResult> Result()
        {

            return Ok(await Task.FromResult(new { sucess = "Authorized succefully" }));
        }

    }
    
}

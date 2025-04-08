using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Re_Backend.Domain.UserDomain.Entity;
using Re_Backend.Domain.UserDomain.IServices;
using System.Threading.Tasks;

namespace Re_Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register(string email,string passwd,string username,string? nickname)
        {
            User user = new User()
            {
                Email=email,
                UserName=username,
                Password=passwd,
                NickName=nickname
            };
            string ressult = await _loginService.Register(user);
            return Ok("Success");
        }
    }
}

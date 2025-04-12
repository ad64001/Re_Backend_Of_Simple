using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Re_Backend.Common;
using Re_Backend.Domain.UserDomain.Entity;
using Re_Backend.Domain.UserDomain.Entity.Vo;
using Re_Backend.Domain.UserDomain.IServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Re_Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public LoginController(ILoginService loginService,IUserService userService,IJwtService jwtService)
        {
            _loginService = loginService;
            _userService = userService;
            _jwtService = jwtService;
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

        [HttpGet("/login")]
        public async Task<IActionResult> Login(string? email,string passwd,string? username)
        {
            User user = new User() { Email = email, Password = passwd ,UserName = username };
            string token = await _loginService.Login(user);
            if (token != "UserNotInDatabase")
            {
                var userId = _jwtService.ParseToken(token);
                UserRoleVo userRoleVo = await _userService.GetUserInfo(int.Parse(userId));
                userRoleVo.UserV.LastLoginTime = DateTime.Now.AddDays(-1);
                await _userService.UpdateUserInfo(userRoleVo.UserV);
            }
            
            return Ok(token);

        }
    }
}

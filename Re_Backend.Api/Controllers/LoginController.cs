﻿using Microsoft.AspNetCore.Mvc;
using Re_Backend.Common.enumscommon;
using Re_Backend.Common.Jwt;
using Re_Backend.Common.OtherEntity;
using Re_Backend.Domain.UserDomain.Entity;
using Re_Backend.Domain.UserDomain.Entity.Vo;
using Re_Backend.Domain.UserDomain.IServices;

namespace Re_Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public LoginController(ILoginService loginService, IUserService userService, IJwtService jwtService)
        {
            _loginService = loginService;
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            User user2 = new User()
            {
                Email = user.Email,
                UserName = user.UserName,
                Password = user.Password,
                NickName = user.NickName
            };
            string ressult = await _loginService.Register(user2);
            return Ok(new Result<Object> { Code = ResultEnum.Success, Data = null });
        }

        [HttpGet]
        public async Task<IActionResult> Login(string? email, string passwd, string? username)
        {
            User user = new User() { Email = email, Password = passwd, UserName = username };
            string token = await _loginService.Login(user);
            if (token != "UserNotInDatabase")
            {
                var userId = _jwtService.ParseToken(token);
                UserRoleVo userRoleVo = await _userService.GetUserInfo(int.Parse(userId));
                userRoleVo.UserV.LastLoginTime = DateTime.Now.AddDays(-1);
                await _userService.UpdateUserInfo(userRoleVo.UserV);
                return Ok(new Result<Object> { Code = ResultEnum.Success, Data = new { Token = token } });
            }
            else
            {
                return Ok(new Result<Object> { Code = ResultEnum.Fail, Data = null });
            }


        }
    }
}

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Re_Backend.Common;
using Re_Backend.Common.enumscommon;
using Re_Backend.Common.SqlConfig;
using Re_Backend.Domain;
using Re_Backend.Domain.UserDomain.Entity;
using Re_Backend.Domain.UserDomain.IRespository;
using Re_Backend.Domain.UserDomain.IServices;
using System.Threading.Tasks;

namespace Re_Backend.Api.Controllers.Other;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly ILoginService _loginService;
    private readonly IJwtService _jwtService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,ILoginService loginService,IJwtService jwtService)
    {
        _logger = logger;
        _loginService = loginService;
        _jwtService = jwtService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IActionResult Get()
    {
        return Ok(Ok(new Result<Object> { Code = ResultEnum.Success, Data = new { Message = "成功" } }));
    }

    //[HttpGet("/login")]
    //public async Task<IActionResult> LoginT(string? userName,string? password,string? email)
    //{
    //    User user = new User { UserName = userName, Password = password ,Email = email };
    //    var userName2 = await _loginService.Login(user);
    //    // 这里添加你的用户验证逻辑
    //    // 假设验证通过，返回token
    //    var token = _jwtService.GenerateToken(userName2);
    //    return Ok(new { token });
    //}

    
}

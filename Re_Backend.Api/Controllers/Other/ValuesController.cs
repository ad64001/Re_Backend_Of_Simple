using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Re_Backend.Api.Controllers.Other
{
    //[Authorize(Roles = "Admin,SuperAdmin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // 获取当前用户 ID
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // 模拟抛出异常
            //throw new Exception("这是一个测试异常");

            return Ok(new { message = $"Hello {userId}" });
        }
    }
}

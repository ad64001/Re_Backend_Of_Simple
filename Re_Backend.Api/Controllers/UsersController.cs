using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Re_Backend.Common.enumscommon;
using Re_Backend.Common;
using Re_Backend.Domain.UserDomain.Entity;
using Re_Backend.Domain.UserDomain.Entity.Vo;
using Re_Backend.Domain.UserDomain.IServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Re_Backend.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            // 获取当前用户ID
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            UserRoleVo userRole = await _userService.GetUserInfo(int.Parse(userId));
            return Ok(new Result<UserRoleVo> { Code = ResultEnum.Success, Data = userRole });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserInfo([FromBody]User user)
        {
            await _userService.UpdateUserInfo(user);
            return Ok(new Result<Object> { Code = ResultEnum.Success, Data = null });
        }


    }
}

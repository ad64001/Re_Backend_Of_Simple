using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Re_Backend.Common.enumscommon;
using Re_Backend.Common;
using Re_Backend.Domain.UserDomain.Entity;
using Re_Backend.Domain.UserDomain.Entity.Vo;
using Re_Backend.Domain.UserDomain.IServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Re_Backend.Domain.UserDomain.Entity.Dto;

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
        public async Task<IActionResult> UpdateUserInfo([FromBody]UserDto userdto)
        {
            User user = new User()
            {
                Id = userdto.Id.Value,
                NickName = userdto.NickName,
                Email = userdto.Email,  
                Password = userdto.Password,
                RoleId = userdto.RoleId.Value,
            };
            await _userService.UpdateUserInfo(user);
            return Ok(new Result<Object> { Code = ResultEnum.Success, Data = null });
        }

        [HttpGet("/api/UsersPage")]
        public async Task<IActionResult> GetUserPage(int size,int pageNumb)
        {
            var users = await _userService.GetUserPages(size, pageNumb);
            var count = await _userService.GetUserCount();
            return Ok(new Result<Object> { Code = ResultEnum.Success, Data = new { Users = users, Count = count } });
        }

        [HttpGet("/api/UserInfo")]
        public async Task<IActionResult> GetUserInfoById(int id)
        {
            UserRoleVo userRole = await _userService.GetUserInfo(id);
            return Ok(new Result<UserRoleVo> { Code = ResultEnum.Success, Data = userRole });
        }

        [HttpPost("/api/DeleteUser")]
        public async Task<IActionResult> DeleteByid([FromBody]UserDto userDto)
        {
            await _userService.DeleteByid(userDto.Id.Value);
            return Ok(new Result<Object> { Code = ResultEnum.Success, Data = null });
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Re_Backend.Common;
using Re_Backend.Common.enumscommon;
using Re_Backend.Domain.UserDomain.Entity;
using Re_Backend.Domain.UserDomain.IServices;

namespace Re_Backend.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            List<Role> roles = await _roleService.GetRoles();
            return Ok(new Result<List<Role>>() { Code = ResultEnum.Success, Data = roles });
        }
    }
}

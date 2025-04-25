using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Re_Backend.Common;
using Re_Backend.Common.enumscommon;
using Re_Backend.Domain.CommonDomain.Entity;
using Re_Backend.Domain.CommonDomain.IServices;
using System.Security.Claims;

namespace Re_Backend.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        private readonly IPageService _pageService;

        public PagesController(IPageService pageService)
        {
            _pageService = pageService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPages()
        {
            var roleName = User.FindFirst(ClaimTypes.Role)?.Value;
            if (!string.IsNullOrEmpty(roleName))
            {
                List<Page> pages = await _pageService.GetPages(roleName);
                return Ok(new Result<List<Page>>() { Code = ResultEnum.Success, Data = pages });
            }
            return Ok(new Result<Object>() { Code = ResultEnum.Fail, Data = null });
        }
    }
}

using System;
using System.Linq;
using System.Security.Claims;
using Domain.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<GameDataDto> GetGameData()
        {
            var userIds = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return _userService.GetGameDataByUserId(Guid.Parse(userIds ?? ""));
        }
        
        [HttpPost]
        public ActionResult SetGameData([FromBody] GameDataDto gameDataDto)
        {
            var userIds = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            var res = _userService.SetGameDataByUserId(Guid.Parse(userIds ?? ""), gameDataDto);
            return res ? Ok(): BadRequest();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Domain.MyCalendar;
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
        
        [HttpPost]
        public ActionResult<List<MyDayDto>> UpdateDate([FromBody] SwitchDateDto switchDateDto)
        {
            var userIds = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var res = _userService.SwitchDateByUserId(Guid.Parse(userIds ?? ""), switchDateDto);
            return Ok(res);
        }

        [HttpPost]
        public ActionResult<List<MyDayDto>> SetDateVolume([FromQuery] Guid dateId, int volume)
        {
            var userIds = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var res = _userService.SetDayVolume(dateId, volume, Guid.Parse(userIds ?? ""));
            return Ok(res);
        }

        [HttpGet]
        public ActionResult<List<MyDayDto>> GetStartDates()
        {
            var userIds = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var res = _userService.GetStartDates(Guid.Parse(userIds ?? ""));
            return res != null ? Ok(res): BadRequest();
        }

        [HttpGet]
        public ActionResult<GameDataDto> GetState()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var gameData = _userService.GetGameDataByUserId(Guid.Parse(userId ?? ""));
            return gameData;
        }

        [HttpPost]
        public ActionResult<List<MyDayDto>> GetNextDates([FromBody] SwitchDateDto dto)
        {
            var userIds = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var res = _userService.GetNextDates(Guid.Parse(userIds ?? ""), dto);
            return res != null ? Ok(res): BadRequest();
        }
        
        [HttpPost("{value}")]
        public ActionResult<List<MyDayDto>> SetCycle(int value, [FromBody] SwitchDateDto dto)
        {
            var userIds = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var res = _userService.SetCycleByUserId(Guid.Parse(userIds ?? ""), value, dto);
            return Ok(res);
        }
        
        [HttpPost("{value}")]
        public ActionResult<List<MyDayDto>> SetTerm(int value, [FromBody] SwitchDateDto dto)
        {
            var userIds = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var res = _userService.SetTermByUserId(Guid.Parse(userIds ?? ""), value, dto);
            return Ok(res);
        }


        // [HttpGet]
        // public ActionResult<GameDataDto> GetGameData()
        // {
        //     var userIds = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        //     return _userService.GetGameDataByUserId(Guid.Parse(userIds ?? ""));
        // }
        //
        // [HttpPost]
        // public ActionResult SetGameData([FromBody] GameDataDto gameDataDto)
        // {
        //     var userIds = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        //     
        //     var res = _userService.SetGameDataByUserId(Guid.Parse(userIds ?? ""), gameDataDto);
        //     return res ? Ok(): BadRequest();
        // }

    }
}
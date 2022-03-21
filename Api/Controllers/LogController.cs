using System;
using System.Linq;
using System.Security.Claims;
using Domain.Log;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService) 
         {
             _logService = logService;
         }
        
         [HttpGet("{message}")]
         public ActionResult Log(string message)
         {
             var userIds = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
             _logService.Log(Guid.Parse(userIds ?? ""), message);
             return Ok();
         }

        // [HttpPost]
        // public ActionResult<GetTokenResponse> SignInGamer([FromBody] SignInDto modelDto)
        // {
        //    // var resp = _authService.SignIn(modelDto);
        //     return  new GetTokenResponse();
        // }
    }
}
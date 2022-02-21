using Domain.Auth;
using Domain.User;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        
         public AuthController(IAuthService authService, IUserService userService) 
         {
             _authService = authService;
             _userService = userService;
         }
        
         [HttpGet("{deviceId}")]
         public ActionResult<GetTokenResponse> SignInAnonGamer(string deviceId)
         {
             var token = _authService.GetTokenByDeviceId(deviceId);
             var userDto = _userService.GetUserByDeviceId(deviceId);
             var gameData = _userService.GetGameDataByUserId(userDto.Id);
             var resp = new GetTokenResponse
             {
                 Token = token,
                 GameData = gameData
             };
             return resp;
         }

        // [HttpPost]
        // public ActionResult<GetTokenResponse> SignInGamer([FromBody] SignInDto modelDto)
        // {
        //    // var resp = _authService.SignIn(modelDto);
        //     return  new GetTokenResponse();
        // }
    }
}
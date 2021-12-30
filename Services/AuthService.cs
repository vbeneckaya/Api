using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Domain.Auth;
using Domain.Error;
using Domain.User;
using Microsoft.IdentityModel.Tokens;

namespace Services
{
    public class AuthService : IAuthService
    {
      //  private readonly AuthConfig _authConfig;
        private readonly IErrorService _errorService;
        private readonly IUserService _userService;

        public AuthService(
        //    AuthConfig authConfig,
            IErrorService errorHandlerService,
            IUserService userService
        )
        {
          //  _authConfig = authConfig;
            _errorService = errorHandlerService;
            _userService = userService;
        }

        public string SignIn(SignInDto modelDto)
        {
            var res = _userService.IsNicNameExist(modelDto.NicName);
            return "";
        }
        
        public string GetTokenByDeviceId(string deviceId)
        {
            var user = _userService.GetUserByDeviceId(deviceId);

            if (user == null)
            {
                user = new UserDto()
                {
                    Id = Guid.NewGuid(),
                    DeviceId = deviceId,
                    Role = 1,
                    Level = 0,
                    Score = 0
                };
                _userService.AddNewAnonUser(user);
            }
            
            var accessTokenIdentity = CreateClaimsIdentity(user.Id, user.NicName ?? "", user.Role.ToString());
            var accessToken = CreateJwtToken(accessTokenIdentity);
            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }

        private static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthOptions.SignKey));
        }

        private static ClaimsIdentity CreateClaimsIdentity(Guid userId, string userName, string userRole)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userRole),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };
            

            var claimsIdentity =
            new ClaimsIdentity(
                claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
                );

            return claimsIdentity;
        }

        
        // public async Task<AuthResponseModel> LoginAsync(AuthRequestModel authRequestModel)
        // {
        //     var user = await _userService.GetUserByLoginAsync(authRequestModel.UserName, authRequestModel.Password);
        //     if (user == null)
        //     {
        //         return new AuthResponseModel
        //         {
        //             Error = _errorService.GetError(ErrorCodes.InvalidLoginPassword)
        //         };
        //     }
        //     
        //     return await CreateAuthResponse(DateTime.UtcNow, user);
        // }
        
        // public async Task<AuthResponseModel> RefreshTokenAsync(RefreshTokenModel refreshTokenModel, Guid userId)
        // {
        //     var user = await _userService.GetUserByIdAsync(userId);
        //     if (user == null)
        //     {
        //         return new AuthResponseModel
        //         {
        //             Error = _errorService.GetError(ErrorCodes.InvalidLoginPassword)
        //         };
        //     }
        //
        //     var refreshTokenHash = ComputeTokenHash(refreshTokenModel.RefreshToken);
        //
        //     // RefreshToken должен совпадать с сохранённым
        //     // для предотвращения повторного логина
        //     if (user.RefreshToken != refreshTokenHash)
        //     {
        //         return new AuthResponseModel
        //         {
        //             Error = _errorService.GetError(ErrorCodes.AlreadyLoggedIn)
        //         };
        //     }
        //
        //     return await CreateAuthResponse(DateTime.UtcNow, user);
        // }
        //
   
        private static JwtSecurityToken CreateJwtToken(ClaimsIdentity identity)
        {
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.SignIssuer,
                audience: AuthOptions.SignAudience,
                claims: identity.Claims,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthOptions.SignKey)),
                    SecurityAlgorithms.HmacSha256));

            return jwt;
            
            // return new JwtSecurityToken(
            //     claims: identity.Claims,
            //     signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            //     );
        }

        private string CalculateTokenHash(string str)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            using (SHA256 hashCalculator = SHA256.Create())
            {
                var hashBytes = hashCalculator.ComputeHash(encoding.GetBytes(str));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

    }
}

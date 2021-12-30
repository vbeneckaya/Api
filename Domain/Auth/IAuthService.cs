using System;

namespace Domain.Auth
{
    public interface IAuthService
    {
        string SignIn (SignInDto modelDto);
        string GetTokenByDeviceId(string deviceId);
    }
}
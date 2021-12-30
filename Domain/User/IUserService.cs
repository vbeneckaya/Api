using System;

namespace Domain.User
{
    public interface IUserService
    {
        bool IsNicNameExist(string nicName);
        bool AddNewAnonUser(UserDto dto);
        UserDto GetUserByDeviceId(string deviceId);
        GameDataDto GetGameDataByUserId(Guid userId);
        bool SetGameDataByUserId(Guid userId, GameDataDto dto);
    }
}
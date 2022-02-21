using System;
using System.Collections.Generic;
using Domain.MyCalendar;

namespace Domain.User
{
    public interface IUserService
    {
        bool IsNicNameExist(string nicName);
        bool AddNewAnonUser(UserDto dto);
        UserDto GetUserByDeviceId(string deviceId);
        GameDataDto GetGameDataByUserId(Guid userId);
        bool SetGameDataByUserId(Guid userId, GameDataDto dto);
        public List<MyDayDto> SwitchDateByUserId(Guid userId, SwitchDateDto dto);
        public List<MyDayDto> SetCycleByUserId(Guid userId, int value, SwitchDateDto dto);
        public List<MyDayDto> SetTermByUserId(Guid userId, int value, SwitchDateDto dto);
        public List<MyDayDto> GetStartDates(Guid userId);
        public List<MyDayDto> GetNextDates(Guid userId, SwitchDateDto dto);
        public List<MyDayDto> SetDayVolume(Guid dayId, int volume, Guid userId);
    }
}
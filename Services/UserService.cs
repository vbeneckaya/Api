using System;
using System.Linq;
using DAL.Models;
using DAL.Services;
using Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly DbSet<User> _userRepository;
        private readonly ICommonDataService _db;
        //private readonly ISecurityService _securityService;

        public UserService(
            ICommonDataService dataService
           // ISecurityService security
            )
        {
            _userRepository = dataService.GetDbSet<User>();
            _db = dataService;
            // _securityService = security;
        }

        public bool IsNicNameExist(string nicName)
        {
            return _userRepository.FirstOrDefault(e => e.NicName == nicName) != null;
        }

        public bool AddNewAnonUser(UserDto model)
        {
            try
            {
                _userRepository.Add(new User()
                {
                    Id = Guid.NewGuid(),
                    DeviceId = model.DeviceId,
                    Level = 0,
                    Score = 0,
                    Role = 1
                });
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public UserDto GetUserByDeviceId(string deviceId)
        {
            var user = _userRepository.FirstOrDefault(e => e.DeviceId == deviceId);
            return TransModelToDto(user);
        }

        public GameDataDto GetGameDataByUserId(Guid userId)
        {
            var user = _userRepository.First(e => e.Id == userId);
            return new GameDataDto()
            {
                UserId = user.Id,
                Level = user.Level,
                Score = user.Score
            };
        }

        public bool SetGameDataByUserId(Guid userId, GameDataDto dto)
        {
            try
            {
                var user = _userRepository.First(e => e.Id == userId);
                user.Level = dto.Level;
                user.Score = dto.Score;

                _userRepository.Update(user);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private UserDto TransModelToDto(User model)
        {
            return model == null ? null : new UserDto()
            {
                Id = model.Id,
                DeviceId = model.DeviceId,
                Level = model.Level,
                NicName = model.NicName,
                Score = model.Score,
                Role = model.Role
            };
        }
        


        // public async Task<int> CreateUserAsync(UserRequestModel requestModel)
        // {
        //     
        //     return await _userRepository
        //         .InsertUserAsync(requestModel, _securityService.GetHashPbkdf2(requestModel.Password));
        // }
        //
    
 
    }
}

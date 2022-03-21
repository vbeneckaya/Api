using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL.Services;
using Domain.MyCalendar;
using Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly DbSet<User> _userRepository;
        private readonly DbSet<MyDay> _myDayRepository;

        private readonly ICommonDataService _db;
        private Guid _userId;

        //private readonly ISecurityService _securityService;

        public UserService(
            ICommonDataService dataService
            // ISecurityService security
        )
        {
            _userRepository = dataService.GetDbSet<User>();
            _myDayRepository = dataService.GetDbSet<MyDay>();
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
                    Id = model.Id,
                    DeviceId = model.DeviceId,
                    Term = model.Term,
                    Cycle = model.Cycle,
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
                Term = user.Term,
                Cycle = user.Cycle,
                IsModeratedTerm = !HasAnyDate(userId),
                IsModeratedCycle = !HasAnyCycle(userId),
                NeedShowExample = NeedShowExample(userId)
            };
        }

        public bool SetGameDataByUserId(Guid userId, GameDataDto dto)
        {
            try
            {
                var user = _userRepository.First(e => e.Id == userId);
                user.Term = dto.Term;
                user.Cycle = dto.Cycle;

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

        public List<MyDayDto> SwitchDateByUserId(Guid userId, SwitchDateDto dto)
        {
            try
            {
                _userId = userId;
                var myDay = _myDayRepository.FirstOrDefault(
                    e =>
                        e.UserId == userId &&
                        e.Year == dto.Year && e.Month == dto.Month && e.Day == dto.Day
                );
                if (myDay != null)
                {
                    _myDayRepository.Remove(myDay);
                }
                else
                {
                    myDay = new MyDay()
                    {
                        UserId = userId,
                        Date = new DateOnly(dto.Year, dto.Month, dto.Day),
                        Year = dto.Year,
                        Month = dto.Month,
                        Day = dto.Day,
                        Volume = dto.Volume == 0 ? DefineInitialVolume() : dto.Volume
                    };
                    _myDayRepository.Add(myDay);
                }

                _db.SaveChanges();

                CalcNewCycle();
                CalcNewTerm();

                List<int> r = new List<int>();
                var t = r.Count;

                return
                    FillDates(userId
                        , new DateOnly(dto.MinDay.Year, dto.MinDay.Month, dto.MinDay.Day)
                        , new DateOnly(dto.MaxDay.Year, dto.MaxDay.Month, dto.MaxDay.Day))
                    ;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<MyDayDto>();
            }
        }

        private int DefineInitialVolume()
        {
            return 10;
        }

        public List<MyDayDto> UpdateDateByUserId(Guid userId, SwitchDateDto dto)
        {
            try
            {
                _userId = userId;
                var myDay = _myDayRepository.FirstOrDefault(
                    e =>
                        e.UserId == userId &&
                        e.Year == dto.Year && e.Month == dto.Month && e.Day == dto.Day
                );
                if (myDay != null)
                {
                    myDay.Volume = dto.Volume;
                    _myDayRepository.Update(myDay);
                }
                else
                {
                    myDay = new MyDay()
                    {
                        UserId = userId,
                        Date = new DateOnly(dto.Year, dto.Month, dto.Day),
                        Year = dto.Year,
                        Month = dto.Month,
                        Day = dto.Day,
                        Volume = dto.Volume
                    };
                    _myDayRepository.Add(myDay);
                }

                _db.SaveChanges();

                CalcNewCycle();
                CalcNewTerm();

                return
                    FillDates(userId
                        , new DateOnly(dto.MinDay.Year, dto.MinDay.Month, dto.MinDay.Day)
                        , new DateOnly(dto.MaxDay.Year, dto.MaxDay.Month, dto.MaxDay.Day))
                    ;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<MyDayDto>();
            }
        }

        public List<MyDayDto> SetCycleByUserId(Guid userId, int value, SwitchDateDto dto)
        {
            var user = _userRepository.Find(userId);

            if (!HasAnyCycle(userId))
            {
                user.Cycle = value;
                _userRepository.Update(user);
                _db.SaveChanges();
            }

            return FillDates(userId
                    , new DateOnly(dto.MinDay.Year, dto.MinDay.Month, dto.MinDay.Day)
                    , new DateOnly(dto.MaxDay.Year, dto.MaxDay.Month, dto.MaxDay.Day))
                ;
        }

        public List<MyDayDto> SetTermByUserId(Guid userId, int value, SwitchDateDto dto)
        {
            var user = _userRepository.Find(userId);
            if (!HasAnyDate(userId))
            {
                user.Term = value;
                _userRepository.Update(user);
                _db.SaveChanges();
            }

            return FillDates(userId
                    , new DateOnly(dto.MinDay.Year, dto.MinDay.Month, dto.MinDay.Day)
                    , new DateOnly(dto.MaxDay.Year, dto.MaxDay.Month, dto.MaxDay.Day))
                ;
        }

        public List<MyDayDto> GetStartDates(Guid userId)
        {
            var minMonth = DateTime.Today.Month - 2;
            var maxMonth = DateTime.Today.Month + 2;

            var minYear = minMonth <= 0 ? DateTime.Today.Year - 1 : DateTime.Today.Year;
            minMonth = minMonth <= 0 ? minMonth + 12 : minMonth;

            var maxYear = maxMonth > 12 ? DateTime.Today.Year + 1 : DateTime.Today.Year;
            maxMonth = maxMonth > 12 ? maxMonth - 12 : maxMonth;

            var minDay = new DateOnly(minYear, minMonth, 1);
            var maxDay = new DateOnly(maxYear, maxMonth, DateTime.DaysInMonth(maxYear, maxMonth));

            return FillDates(userId, minDay, maxDay);
        }

        public List<MyDayDto> GetExampleDates(Guid userId)
        {
            var dayCount = 4;
            var min = DateTime.Today.AddMonths(-1).AddDays(-7);
            var max = min.AddDays(dayCount);

            var minDay = new DateOnly(min.Year, min.Month, min.Day);
            var maxDay = new DateOnly(max.Year, max.Month, max.Day);

            var part1 = FillDates(userId, minDay, maxDay);
            
            part1[0].Volume = 50;
            part1[1].Volume = 100;
            part1[2].Volume = 100;
            part1[3].Volume = 50;
            part1[4].Volume = 10;

            min = min.AddDays(28);
            max = min.AddDays(dayCount);

            minDay = new DateOnly(min.Year, min.Month, min.Day);
            maxDay = new DateOnly(max.Year, max.Month, max.Day);

            var part2 = FillDates(userId, minDay, maxDay);
            
            part2[0].Volume = 50;
            part2[1].Volume = 100;
            part2[2].Volume = 100;
            part2[3].Volume = 50;
            part2[4].Volume = 10;

            part1.AddRange(part2);
            part1.ForEach(e => e.Filled = true);

            return part1;
        }

        public List<MyDayDto> GetNextDates(Guid userId, SwitchDateDto dto)
        {
            var day = new DateOnly(dto.Year, dto.Month, dto.Day);
            var minDay = day;
            var maxDay = day;

            switch (day.Day)
            {
                case 1:
                    maxDay = day.AddDays(-1);
                    minDay = new DateOnly(maxDay.Year, maxDay.Month, 1); //day.AddDays(-5 * 7));
                    return FillDates(userId, minDay, maxDay);

                default:
                    minDay = day.AddDays(1);
                    maxDay = new DateOnly(minDay.Year, minDay.Month,
                        DateTime.DaysInMonth(minDay.Year, minDay.Month)); //day.AddDays(5 * 7);
                    return FillDates(userId, minDay, maxDay);
            }
        }

        public void SaveNotifyToken(Guid userId, string token)
        {
            var user = _userRepository.Find(userId);
            user.FcmToken = token;
            _userRepository.Update(user);
            _db.SaveChanges();
        }

        public void SaveJwtToken(Guid userId, string token)
        {
            var user = _userRepository.Find(userId);
            user.JwtToken = token;
            _userRepository.Update(user);
            _db.SaveChanges();
        }

        public bool NeedShowExample(Guid userDtoId)
        {
            return !_myDayRepository.Any(e => e.UserId == userDtoId);
        }

        private bool HasAnyDate(Guid userId)
        {
            return _myDayRepository.Select(e => e).Any(e => e.UserId == userId);
        }

        private bool HasAnyCycle(Guid userId)
        {
            return GetFirstDays(userId).Count >= 2;
        }

        private int CalcNewTerm()
        {
            // первые дни в периодах (максимально из четырех периодов)
            var firstDays = GetFirstDays(_userId).OrderByDescending(e => e.Date).Take(4);
            // последние дни в периодах (максимально из четырех периодов)
            var lastDays = GetLastDays(_userId).OrderByDescending(e => e.Date).Take(4);
            
            var initialPeriodsCount = firstDays.Count();
            var first3Days = new List<MyDay>();
            var last3Days = new List<MyDay>();
            
            if (_myDayRepository.Select(e =>
                e).Any(e => e.UserId == _userId &&
                            e.Date.CompareTo(new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)) ==
                            0) 
                && initialPeriodsCount > 1)
            {
                // исключить из расчета период, примыкающий к текущей дате, если он короче высчитанного ранее
                var ld = lastDays.Take(1).ToArray()[0];
                var fd = firstDays.Take(1).ToArray()[0];
                var nearTodayPeriodTerm = new DateTime(ld.Year, ld.Month, ld.Day)
                    .Subtract(new DateTime(fd.Date.Year, fd.Date.Month,
                        fd.Date.Day)).Days + 1;
                var oldTerm = _userRepository.Find(_userId).Term;
                if (nearTodayPeriodTerm < oldTerm)
                {
                    first3Days = firstDays.TakeLast(initialPeriodsCount - 1).ToList();
                    last3Days = lastDays.TakeLast(initialPeriodsCount - 1).ToList();
                }
                else
                {
                    first3Days = firstDays.Take(initialPeriodsCount).ToList();
                    last3Days = lastDays.Take(initialPeriodsCount).ToList();
                }
            }
            else
            {
                first3Days = firstDays.Take(initialPeriodsCount).ToList();
                last3Days = lastDays.Take(initialPeriodsCount).ToList();
            }

            var term = 0;
            var count = 0; // количество периодов
            foreach (var lastDay in last3Days)
            {
                term += new DateTime((int)lastDay.Year, (int)lastDay.Month, (int)lastDay.Day)
                    .Subtract(new DateTime((int)first3Days[count].Date.Year, (int)first3Days[count].Date.Month,
                        (int)first3Days[count].Date.Day)).Days + 1;
                count++;
            }

            if (count == 0)
            {
                return _userRepository.Find(_userId).Term;
            }

            var user = _userRepository.Find(_userId);
            user.Term = term / count;
            _userRepository.Update(user);
            _db.SaveChanges();

            return user.Term;
        }

        private int CalcNewCycle()
        {
            var firstDays = GetFirstDays(_userId).OrderByDescending(e => e.Date).Take(4);
            //var initialCount = firstDays.Count();
            var first3Days = firstDays.ToList();

            // if (_myDayRepository.Select(e =>
            //     e).Any(e => e.UserId == _userId &&
            //                 e.Date.CompareTo(new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)) ==
            //                 0)
            // )
            // {
            //     first3Days = firstDays.TakeLast(initialCount - 1).ToList();
            // }
            // else
            // {
            //     first3Days = firstDays.Take(initialCount).ToList();
            // }

            var cycle = 0;
            var count = 0;
            MyDay prevDay = null;
            foreach (var firstDay in first3Days)
            {
                count++;

                if (count >= 2)
                {
                    cycle = new DateTime(prevDay.Date.Year, prevDay.Date.Month, prevDay.Date.Day)
                                .Subtract(new DateTime(firstDay.Date.Year, firstDay.Date.Month, firstDay.Date.Day))
                                .Days +
                            cycle;
                }

                prevDay = firstDay;
            }

            if (count < 2)
            {
                return _userRepository.Find(_userId).Cycle;
            }

            var user = _userRepository.Find(_userId);
            user.Cycle = cycle / (count - 1);
            ;
            _userRepository.Update(user);
            _db.SaveChanges();
            return user.Cycle;
        }

        private List<MyDay> GetFirstDays(Guid userId)
        {
            return _myDayRepository.FromSqlRaw(
                    $"SELECT MD2.* from (SELECT *, MD.\"Date\" - interval '1 day' as PrevDay FROM \"MyDays\" as MD where MD.\"UserId\"=\'{userId}\') as MD2 left join \"MyDays\" as MD0 on MD0.\"UserId\" = MD2.\"UserId\" and MD2.PrevDay = MD0.\"Date\" where MD0.\"Date\" is null;")
                .ToList();
        }

        private List<MyDay> GetLastDays(Guid userId)
        {
            return _myDayRepository.FromSqlRaw(
                    $"SELECT MD2.* from (SELECT *, MD.\"Date\" + interval '1 day' as NextDay FROM \"MyDays\" as MD where MD.\"UserId\"=\'{userId}\') as MD2 left join \"MyDays\" as MD0 on MD0.\"UserId\" = MD2.\"UserId\" and MD2.NextDay = MD0.\"Date\" where MD0.\"Date\" is null;")
                .ToList();
        }

        private List<MyDayDto> FillDates(Guid userId, DateOnly minDay, DateOnly maxDay)
        {
            var today = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            var myList = new List<MyDayDto>();
            var filledDays = _myDayRepository.Select(e => e).Where(
                e =>
                    e.UserId == userId
                    && e.Date <= maxDay
                    && e.Date >= minDay
            ).OrderBy(e => e.Date).ToList();
            var dayToAdd = minDay;
            var count = (maxDay.ToDateTime(new TimeOnly()) - minDay.ToDateTime(new TimeOnly())).Days;


            var lastFirstDay = GetFirstDays(userId).OrderBy(e => e.Date).LastOrDefault()?.Date;

            var prognoseDays = new List<MyDayDto>();
            var countPd = lastFirstDay == null
                ? 0
                : (maxDay.ToDateTime(new TimeOnly()) - lastFirstDay?.ToDateTime(new TimeOnly()))?.Days;

            var cCycle = 0;
            var user = _userRepository.Find(userId);
            var cCycleMax = user.Cycle;
            var cDayMax = user.Term;
            var d = lastFirstDay ?? new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);

            for (var i = 0; i < countPd; i++)
            {
                cCycle = cCycle == cCycleMax ? 1 : cCycle + 1;
                if (cCycle <= cDayMax)
                {
                    prognoseDays.Add(new MyDayDto
                    {
                        Year = d.Year,
                        Month = d.Month,
                        Day = d.Day,
                        Week = 0,
                        DayOfWeek = 0,
                        Today = false,
                        Filled = false,
                        PreFilled = true,
                        MonthName = null,
                        Volume = 100
                    });
                }

                d = d.AddDays(1);
            }


            DateOnly prevBaseDay;
            DateOnly prevFilledDay;
            DateOnly prevPreFilledDay;
            MyDayDto insertedDay;
            int fieldNumber = 0;
            int preFieldNumber = 0;
            int baseNumber = 0;
            for (var i = 0; i <= count; i++)
            {
                var volume = filledDays.FirstOrDefault(e =>
                    e.Year == dayToAdd.Year && e.Month == dayToAdd.Month && e.Day == dayToAdd.Day)?.Volume;

                insertedDay = new MyDayDto()
                {
                    Year = dayToAdd.Year,
                    Month = dayToAdd.Month,
                    Day = dayToAdd.Day,
                    Today = dayToAdd.Equals(today),
                    DayOfWeek = (int)dayToAdd.DayOfWeek == 0 ? 7 : (int)dayToAdd.DayOfWeek,
                    Filled = filledDays.Any(e =>
                        e.Year == dayToAdd.Year && e.Month == dayToAdd.Month && e.Day == dayToAdd.Day),
                    Volume = volume ?? 0,
                    PreFilled = prognoseDays.Any(e =>
                        e.Year == dayToAdd.Year && e.Month == dayToAdd.Month && e.Day == dayToAdd.Day),
                    Week = (dayToAdd.DayOfYear + Delta(dayToAdd.Year)) / 7 + 1
                };
                myList.Add(insertedDay);

                if (prevFilledDay.AddDays(1).CompareTo(dayToAdd) == 0)
                {
                    insertedDay.FilledNumber = ++fieldNumber;
                    if (fieldNumber == 1)
                    {
                        // make reverse numeration in range of 
                    }
                }
                else
                {
                    fieldNumber = 0;
                }

                if (prevPreFilledDay.AddDays(1).CompareTo(dayToAdd) == 0)
                {
                    insertedDay.PreFilledNumber = ++preFieldNumber;
                }
                else
                {
                    preFieldNumber = 0;
                }

                if (prevBaseDay.AddDays(1).CompareTo(dayToAdd) == 0)
                {
                    insertedDay.BaseNumber = ++baseNumber;
                    if (baseNumber == 1)
                    {
                        // make reverse numeration in range of preFieldNumber and fieldNumber
                        for (int j = 1; j <= fieldNumber; j++)
                        {
                            myList[i - j].FilledNumberReverse = j;
                        }

                        for (int j = 1; j <= preFieldNumber; j++)
                        {
                            myList[i - j].PreFilledNumberReverse = j;
                        }
                    }
                }
                else
                {
                    baseNumber = 0;
                }

                prevBaseDay = !insertedDay.Filled && !insertedDay.PreFilled
                    ? new DateOnly(dayToAdd.Year, dayToAdd.Month, dayToAdd.Day)
                    : DateOnly.MinValue;
                prevFilledDay = insertedDay.Filled
                    ? new DateOnly(dayToAdd.Year, dayToAdd.Month, dayToAdd.Day)
                    : DateOnly.MinValue;
                prevPreFilledDay = insertedDay.PreFilled
                    ? new DateOnly(dayToAdd.Year, dayToAdd.Month, dayToAdd.Day)
                    : DateOnly.MinValue;


                dayToAdd = dayToAdd.AddDays(1);
            }

            return myList;
        }

        private UserDto TransModelToDto(User model)
        {
            return model == null
                ? null
                : new UserDto()
                {
                    Id = model.Id,
                    DeviceId = model.DeviceId,
                    Term = model.Term,
                    NicName = model.NicName,
                    Cycle = model.Cycle,
                    Role = model.Role
                };
        }

        private int Delta(int Year)
        {
            return (int)new DateOnly(Year, 1, 1).DayOfWeek - 2;
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
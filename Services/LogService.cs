using System;
using DAL.Models;
using DAL.Services;
using Domain.Log;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class LogService : ILogService
    {
        private readonly DbSet<LogRecord> _logRepository;
        private readonly ICommonDataService _db;
        
        public LogService(
            ICommonDataService dataService
            // ISecurityService security
        )
        {
            _logRepository = dataService.GetDbSet<LogRecord>();
            _db = dataService;

            // _securityService = security;
        }

        public void Log(Guid userId, string message)
        {
            _logRepository.Add(new LogRecord()
            {
                UserId = userId,
                DateTime = DateTime.Now,
                Message = message
            });
            
            _db.SaveChanges();
        }
    }
}
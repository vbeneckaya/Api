using System;

namespace Domain.Log
{
    public interface ILogService
    {
        public void Log(Guid userId, string message);
    }
}
using System;

namespace DAL.Models
{
    public class LogRecord : Base
    {
        public Guid UserId { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
    }
}
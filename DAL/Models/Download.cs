using System;

namespace DAL.Models
{
    public class Download: Base
    {
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string Version { get; set; }
        public string IP { get; set; }
    }
}
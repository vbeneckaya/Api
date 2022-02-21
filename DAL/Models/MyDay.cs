using System;

namespace DAL.Models
{
    public class MyDay : Base
    {
        public Guid UserId { get; set; }
        public Guid MyDaysGroupId { get; set; }
        public DateOnly Date { get; set; }
        public Int64 Year { get; set; }
        public Int64 Month { get; set; }
        public Int64 Day { get; set; }
        public Int64 Number { get; set; }
        public Int64 Volume { get; set; } = 100;
    }
}
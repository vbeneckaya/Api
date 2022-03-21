using System;

namespace DAL.Models
{
    public class MyDay : Base
    {
        public Guid UserId { get; set; }
        public Guid MyDaysGroupId { get; set; }
        public DateOnly Date { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Number { get; set; }
        public int Volume { get; set; } = 100;
    }
}
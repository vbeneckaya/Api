using System;

namespace Domain.MyCalendar
{
    public class SwitchDateDto : BaseDay
    {
        public BaseDay MinDay { get; set; }
        public BaseDay MaxDay { get; set; }
        public int Volume { get; set; }
    }
}
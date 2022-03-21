using System;

namespace Domain.User
{
    public class GameDataDto
    {
        public Guid UserId { get; set; }
        public int Term { get; set; }
        public bool IsModeratedTerm { get; set; }
        public int Cycle { get; set; }
        public bool IsModeratedCycle { get; set; }
        public bool NeedShowExample { get; set; }
        //public List<MyDayDto> Dates { get; set; }
    }
}
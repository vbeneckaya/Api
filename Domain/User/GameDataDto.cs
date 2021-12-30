using System;

namespace Domain.User
{
    public class GameDataDto
    {
        public Guid UserId { get; set; }
        public int Level { get; set; }
        public int Score { get; set; }
    }
}
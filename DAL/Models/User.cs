using System;

namespace DAL.Models
{
    public class User : IPersistable
    {
        public Guid Id { get; set; }
        public int Role { get; set; }
        public string PasswordHash { get; set; }
        public string DeviceId { get; set; }
        public string NicName { get; set; }
        
        public int Level { get; set; }
        public int Score { get; set; }

        public override string ToString()
        {
            return NicName;
        }
    }
}
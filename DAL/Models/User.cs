using System;

namespace DAL.Models
{
    public class User : Base
    {
        public int Role { get; set; }
        public string PasswordHash { get; set; }
        public string DeviceId { get; set; }
        public string NicName { get; set; }
        
        public int Term { get; set; }
        public int Cycle { get; set; }
        public string FcmToken { get; set; }
        public string JwtToken { get; set; }

        public override string ToString()
        {
            return NicName;
        }
    }
}
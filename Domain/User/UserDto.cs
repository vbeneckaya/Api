using System;
using Newtonsoft.Json;

namespace Domain.User
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string DeviceId { get; set; }
        
        public int Role { get; set; }
        
        [JsonIgnore]
        public string Password { get; set; }

        public string NicName { get; set; }

        public int Term { get; set; }
        
        public int Cycle { get; set; }
        
    }
}

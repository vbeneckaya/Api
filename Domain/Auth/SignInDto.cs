using System.ComponentModel.DataAnnotations;
using Common.Enums;

namespace Domain.Auth
{
    public class SignInDto
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string Password { get; set; }
        
        [Required]
        public string NicName { get; set; }
    }
}
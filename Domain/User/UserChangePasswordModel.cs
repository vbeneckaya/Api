using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.User
{
    public class UserChangePasswordModel
    {
        [Required]
        public Guid? Id { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.User
{
    public class UserCreateRequestModel
    {
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string UserName { get; set; }
    
        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        [MaxLength(25)]
        [MinLength(6)]
        public string Password { get; set; }
      
        /// <summary>
        /// Фамилия
        /// </summary>
        [MaxLength(50)]
        public string LastName { get; set; }
    
        /// <summary>
        /// Имя
        /// </summary>
        [MaxLength(50)]
        public string FirstName { get; set; }
       
        /// <summary>
        /// Электронная почта
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
      
        /// <summary>
        /// Роль
        ///  </summary>
        [Required]
        public string Role { get; set; }
    }
}

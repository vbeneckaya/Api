using System.ComponentModel.DataAnnotations;

namespace Domain.User
{
    public class PagingUserModel
    {
        [Required]
        public int CurrentPage { get; set; }
        [Required]
        public int PageSize { get; set; }
    }
}

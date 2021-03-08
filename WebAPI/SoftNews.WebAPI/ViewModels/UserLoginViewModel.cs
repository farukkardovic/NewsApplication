using System.ComponentModel.DataAnnotations;

namespace SoftNews.WebAPI.ViewModels
{
    public  class UserLoginViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 8 character")]
        public string Password { get; set; }
    }
}

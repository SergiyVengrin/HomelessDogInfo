using System.ComponentModel.DataAnnotations;

namespace CommentsWebApi.Models
{
    public sealed class UserDTO
    {
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "The email adress is invalid")]
        public string Email { get; set; }


        [Required(ErrorMessage = "The password is required")]
        [MinLength(8)]
        [MaxLength(50)]
        public string Password { get; set; }


        [Required(ErrorMessage = "User name is required")]
        [MinLength(2)]
        public string UserName { get; set; }


    }
}

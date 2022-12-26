using System.ComponentModel.DataAnnotations;

namespace CommentsWebApi.DTOs
{
    public sealed class EmailDTO
    {
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "The email adress is invalid")]
        public string ToEmail { get; set; }

        [Required(ErrorMessage ="The subject is required")]
        [MinLength(2)]
        [MaxLength(125)]
        public string Subject { get; set; }

        [MinLength(2)]
        public string Body { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace CommentsWebApi.DTOs
{
    public sealed class CommentDTO
    {

        [Required(ErrorMessage = "The DogID is required")]
        public int DogID { get; set; }


        [Required(ErrorMessage = "The User is required")]
        public UserDTO User { get; set; }


        [Required(ErrorMessage ="Text is required")]
        [StringLength(200, MinimumLength = 1, ErrorMessage ="The length of Text should be between 1 and 200")]
        public string Text { get; set; }


        [Required(ErrorMessage = "Rating is required")]
        [Range(1,5)]
        public int Rating { get; set; }


        public int VoteCounts { get; set; }


        public DateTime? DateTime { get; set; }
    }
}

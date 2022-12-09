using System;

namespace BLL.Models
{
    public sealed class CommentModel
    {
        public int CommentID { get; set; }
        public int DogID { get; set; }
        public UserModel User { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public int VoteCounts { get; set; }
        public DateTime? DateTime { get; set; }
    }
    
}

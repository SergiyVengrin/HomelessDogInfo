using System;

namespace DAL.Entities
{
    public sealed class Comment
    {
        public int CommentID { get; set; }
        public int DogID { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public int VoteCounts { get; set; } 
        public DateTime? DateTime { get; set; }
    }
    
}

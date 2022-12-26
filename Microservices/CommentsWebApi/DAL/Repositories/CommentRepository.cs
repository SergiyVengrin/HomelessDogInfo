using System.Linq;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
using DAL.Exceptions;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public sealed class CommentRepository : ICommentRepository
    {
        private readonly CommentContext _db;

        public CommentRepository(CommentContext commentContext)
        {
            _db = commentContext;
        }


        public async Task AddAsync(Comment comment)
        {
            _db.Update(comment);
            await _db.SaveChangesAsync();
        }


        public async Task<Comment> DeleteAsync(int commentID)
        {
            var comment = await _db.Comments.Where(c => c.CommentID == commentID).SingleAsync();

            if (comment is null)
            {
                throw new NotFoundException(commentID);
            }

            _db.Comments.Remove(comment);
            _db.SaveChanges();


            return comment;
        }


        public async Task<object> GetByDogIdAsync(int dogID)
        {
            var dog = await (from c in _db.Comments
                             where c.DogID == dogID
                             select new
                             {
                                 CommentID = c.CommentID,
                                 DogID = c.DogID,
                                 User = new User
                                 {
                                     Email = c.User.Email,
                                     Password = c.User.Password,
                                     Username = c.User.Username
                                 },
                                 Text = c.Text,
                                 Rating = c.Rating,
                                 VoteCounts = c.VoteCounts,
                                 DateTime = c.DateTime
                             }).ToListAsync();

            if (!dog.Any())
            {
                throw new NotFoundException();
            }

            return dog;
        }


        public async Task<Comment> GetByCommentIdAsync(int commentID)
        {
            var comment = await _db.Comments.Where(c => c.CommentID == commentID).SingleOrDefaultAsync();

            if (comment is null)
            {
                throw new NotFoundException(commentID);
            }

            return comment;
        }

        public async Task Upvote(int commentID)
        {
            var comment = await GetByCommentIdAsync(commentID);

            if (comment is null)
            {
                throw new NotFoundException(commentID);
            }

            comment.VoteCounts += 1;
            await _db.SaveChangesAsync();
        }

        public async Task Downvote(int commentID)
        {
            var comment = await GetByCommentIdAsync(commentID);

            if (comment is null)
            {
                throw new NotFoundException(commentID);
            }

            comment.VoteCounts -= 1;
            await _db.SaveChangesAsync();
        }
    }
}

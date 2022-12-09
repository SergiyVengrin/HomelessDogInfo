using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface ICommentRepository
    {
        Task AddAsync(Comment comment);
        Task<Comment> DeleteAsync(int commentID);
        Task<object> GetByDogIdAsync(int dogID);
        Task<Comment> GetByCommentIdAsync(int commentID);
        Task Upvote(int commentID);
        Task Downvote(int commentID);
    }
}

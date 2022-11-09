using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface ICommentService
    {
        Task Add(CommentModel comment);
        Task<CommentModel> Delete(int commentID);
        //IEnumerable<Comment> DeleteAllByArticleId(int articleId);
        Task<CommentModel> GetByCommentId(int commentID);
        Task<object> GetByDogId(int dogID);
        Task Upvote(int commentID);
        Task Downvote(int commentID);

    }
}

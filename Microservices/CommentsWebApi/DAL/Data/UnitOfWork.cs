using DAL.Interfaces;
using DAL.Repositories;

namespace DAL.Data
{
    public class UnitOfWork : IUnitOfWork
    {

        public ICommentRepository CommentRepository =>new CommentRepository();

        public IUserRepository UserRepository => new UserRepository();
    }
}

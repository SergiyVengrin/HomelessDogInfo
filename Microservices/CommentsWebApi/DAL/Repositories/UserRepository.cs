using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
using DAL.Exceptions;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly CommentContext _db;

        public UserRepository(CommentContext commentContext)
        {
            _db = commentContext;
        }


        public async Task AddAsync(User user)
        {

            await _db.Users.AddAsync(user);
            _db.SaveChanges();
        }


        public async Task<User> GetUserAsync(User u)
        {
            var user = await _db.Users.Where(x=> x.Email == u.Email).SingleOrDefaultAsync();

            if (user is null)
            {
                throw new NotFoundException(user.Email);
            }

            return user;
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
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
            try
            {
                await _db.Users.AddAsync(user);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetUserAsync(User user) 
        {
            return await _db.Users.Where(u => u.Email == user.Email).SingleAsync();
        }
    }
}

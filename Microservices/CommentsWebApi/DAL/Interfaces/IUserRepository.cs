using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User> GetUserAsync(User user);
    }
}

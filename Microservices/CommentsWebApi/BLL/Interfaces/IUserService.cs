using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task Register(UserModel userModel);
        Task<bool> Login(UserModel userModel);
    }
}

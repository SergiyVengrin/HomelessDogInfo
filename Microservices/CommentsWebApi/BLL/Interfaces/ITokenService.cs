using BLL.Models;

namespace BLL.Interfaces
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, UserModel user);
        bool ValidateToken(string key, string issuer, string token);

    }
}

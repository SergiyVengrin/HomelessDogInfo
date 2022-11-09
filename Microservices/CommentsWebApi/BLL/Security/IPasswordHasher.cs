using System;


namespace BLL.Security
{
    interface IPasswordHasher
    {
        static string HashPassword(string password) => throw new NotImplementedException();
        static bool VerifyPassword(string password, string hash) => throw new NotImplementedException();
    }
}

using DAL.Entities;

namespace BLL.Models
{
    public sealed class UserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public Role AccessLevel { get; set; }
    }
}

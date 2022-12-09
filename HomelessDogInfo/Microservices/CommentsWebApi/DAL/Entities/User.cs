namespace DAL.Entities
{
    public sealed class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public Role AccessLevel { get; set; }
    }
}

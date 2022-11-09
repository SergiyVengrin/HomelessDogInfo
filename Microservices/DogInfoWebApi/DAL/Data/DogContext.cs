using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DAL.Data
{
    public sealed class DogContext:DbContext
    {
        private readonly string _dbUsername;
        private readonly string _dbPassword;

        private readonly string _connectionString;

        public DogContext()
        {
            _dbUsername = Environment.GetEnvironmentVariable("CommentsDbUsername", EnvironmentVariableTarget.User) ?? "username";
            _dbPassword = Environment.GetEnvironmentVariable("CommentsDbPassword", EnvironmentVariableTarget.User) ?? "password";

            _connectionString = $"Host=localhost;Port=5432;Database=DogDb;Username={_dbUsername};Password={_dbPassword}";
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}

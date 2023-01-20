using Dapper;
using Dapper.Contrib.Extensions;
using WebApi.Models;
using WebApi.Repositories.Interfaces;
using WebApi.Services.Interfaces;

namespace WebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseService _db;

        public UserRepository(IDatabaseService db)
        {
            _db = db;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            var query = "SELECT Id, Username, PasswordHash FROM Users WHERE Username = @username";

            using var connection = _db.CreateConnection();
            var user = await connection.QueryFirstOrDefaultAsync<User>(query, new {username});
            return user;
        }
    }
}

using WebApi.Models;

namespace WebApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
    }
}

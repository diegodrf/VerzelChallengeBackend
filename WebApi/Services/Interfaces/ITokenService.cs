using WebApi.Models;

namespace WebApi.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}

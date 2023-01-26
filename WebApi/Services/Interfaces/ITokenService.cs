using WebApi.Models;

namespace WebApi.Services.Interfaces
{
    public interface ITokenService
    {
        AccessToken GenerateToken(User user);
    }
}

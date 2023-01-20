using System.Data;

namespace WebApi.Services.Interfaces
{
    public interface IDatabaseService
    {
        IDbConnection CreateConnection();
    }
}

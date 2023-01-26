using WebApi.Models;
using WebApi.Models.Request;

namespace WebApi.Repositories.Interfaces
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> GetAllAsync(string? value, bool orderAscendant);
        Task<Vehicle?> GetByIdAsync(int id);
        Task DeleteAsync(Vehicle vehicle);
        Task<int> CreateAsync(VehicleRequest vehicle, string? filename);
        Task<bool> UpdateAsync(int id, VehicleRequest vehicle, string? filename);
        Task<IEnumerable<Brand>> GetAllBrandsAsync();
    }
}

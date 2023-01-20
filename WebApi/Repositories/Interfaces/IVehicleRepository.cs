using WebApi.Models;

namespace WebApi.Repositories.Interfaces
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> GetAllAsync(int? brandId, string? vehicleName);
        Task<Vehicle> GetByIdAsync(int id);
        Task<bool> DeleteAsync(Vehicle vehicle);
        Task<int> CreateAsync(Vehicle vehicle);
        Task<bool> UpdateAsync(Vehicle vehicle);
    }
}

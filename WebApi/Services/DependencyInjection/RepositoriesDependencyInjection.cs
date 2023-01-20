using WebApi.Repositories.Interfaces;
using WebApi.Repositories;

namespace WebApi.Services.DependencyInjection
{
    public static class RepositoriesDependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            return services;
        }
    }
}

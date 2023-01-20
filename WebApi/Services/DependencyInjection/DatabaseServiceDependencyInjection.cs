using WebApi.Services.Interfaces;

namespace WebApi.Services.DependencyInjection
{
    public static class DatabaseServiceDependencyInjection
    {
        public static IServiceCollection AddDatabaseService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDatabaseService>(_ =>
            {
                var connectionString = configuration.GetConnectionString("Default");
                return new DatabaseService(connectionString!);
            });

            return services;
        }
    }
}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.ApplicationConstants;
using WebApi.Services.Interfaces;

namespace WebApi.Services.DependencyInjection
{
    public static class AuthenticationServiceDependencyInjection
    {
        public static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
           services.AddScoped<ITokenService, TokenService>();

            var jwtSecret = configuration.GetValue<string>(ConfigurationConstants.JwtSecret);
            var key = Encoding.ASCII.GetBytes(jwtSecret!);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}

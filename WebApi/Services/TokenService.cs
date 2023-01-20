using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.ApplicationConstants;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class TokenService: ITokenService
    {
        private readonly string _jwtSecret;
        private readonly int _jwtExperisInSeconds;

        public TokenService(IConfiguration configuration)
        {
            _jwtSecret = configuration.GetValue<string>(ConfigurationConstants.JwtSecret)
                ?? throw new ArgumentException(nameof(ConfigurationConstants.JwtSecret));

            _jwtExperisInSeconds = configuration.GetValue<int>(ConfigurationConstants.JwtExperisInSeconds);
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddSeconds(_jwtExperisInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

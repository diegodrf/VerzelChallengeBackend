using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories.Interfaces;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthenticationController(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(User user)
        {
            var thisUser = await _userRepository.GetByUsernameAsync(user.Username);
            if (thisUser is null || thisUser?.PasswordHash != user.PasswordHash)
            {
                var message = "Inavalid user or password";
                return Unauthorized(message);
            }
            
            var token = _tokenService.GenerateToken(thisUser);
            
            return Ok(new { 
                accessToken = token
            });
        }
    }
}

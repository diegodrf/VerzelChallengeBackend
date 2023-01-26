using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.Request;
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
        public async Task<ActionResult<AccessToken>> Login(UserRequest userRequest)
        {
            var user = await _userRepository.GetByUsernameAsync(userRequest.Username);
            if (user is null 
                || !Models.User.IsCorrectPassoword(user.PasswordHash, userRequest.Password))
            {
                var message = "Inavalid userRequest or password";
                return Unauthorized(message);
            }
            
            var token = _tokenService.GenerateToken(user);
            
            return Ok(token);
        }
    }
}

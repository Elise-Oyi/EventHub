using EventHub.Dal;
using EventHub.Jwt;
using EventHub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace EventHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _authentication;
        private readonly ITokenManager _tokenManager;

        public AuthenticationController(IAuthenticationRepository authentication, ITokenManager tokenManager)
        {
            _authentication = authentication;
            _tokenManager = tokenManager;
        }

        //--register user
        [HttpPost("register-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> RegisterUser(User user)
        {
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = hashPassword;

            var result = _authentication.RegisterUser(user);
            if(result > 0)
            {
                return Ok(user);
            }
            return BadRequest();
        }

        //--Check credentials
        [HttpPost("check-credentials")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> GetDetails(User user)
        {
            var authUser = _authentication.CheckCredentials(user);
            if (authUser == null) return NotFound();
            if (authUser != null && !BCrypt.Net.BCrypt.Verify(user.Password, authUser.Password))
            {
                return BadRequest("Incorrect password!");
            }

            var roleName = _authentication.GetUserRole(authUser.RoleId);

            var authResponse = new AuthResponse()
            {
                IsAuthenticated = true,
                Role = roleName,
                Token = _tokenManager.GenerateToken(authUser,roleName)
            };

            return Ok(authResponse);
        }

        //--get all users
        [HttpGet]
          [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var users = _authentication.GetUsers();
            return Ok(users);
        }
    }
}

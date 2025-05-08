using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CRM.Application.Helpers;
using CRM.Application.Services;
using CRM.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;
        private readonly LdapService _ldapService;

        public UserController(UserService userService, IConfiguration configuration, LdapService ldapService)
        {
            _userService = userService;
            _configuration = configuration;
            _ldapService = ldapService;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userService.GetAllUsersAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User?>> GetUserByIdAsync(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAsync(User user)
        {
            await _userService.AddUserAsync(user);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync(User user)
        {
            await _userService.UpdateUserAsync(user);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                await _userService.DeleteUserAsync(id);
                return Ok();
            }
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            var user = HttpContext.User;
            if (user.Identity != null && user.Identity.IsAuthenticated)
            {
                var userName = user.Identity.Name;
                var claims = user.Claims.Select(c => new { c.Type, c.Value });

                return Ok(new
                {
                    UserName = userName,
                    Claims = claims
                });
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _userService.ValidateUserAsync(loginModel.Username, loginModel.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var expiration = DateTime.Now.AddHours(24);
            var token = GenerateJwtToken(user,expiration);
            return Ok(new { 
                User = user,
                Token = token,
                Expiration = expiration
            });
        }

        [AllowAnonymous]
        [HttpGet("test-ldap-connection")]
        public async Task<IActionResult> TestLdapConnection()
        {
            try
            {
                var isValid = _ldapService.ValidateUser("rodrigo.leanos", ".R0drig030.");
                var tmpUser = (await _userService.GetAllUsersAsync()).FirstOrDefault();
                var expiration = DateTime.Now.AddMinutes(30);
                var token = GenerateJwtToken(tmpUser, expiration);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        private string GenerateJwtToken(User user, DateTime expirationDate)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }

    public class LoginModel
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}

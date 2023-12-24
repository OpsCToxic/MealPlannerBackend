using MealPlanBackend.Models;
using MealPlanBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MealPlanBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public static User createdUser = new User();

        public AuthController(IUserService userService, IConfiguration coniguration)
        {
            _userService = userService;
            _configuration = coniguration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            //Check if the user already exists in database
            var existingUser = await _userService.GetUserByUsernameAsync(request.Username);
            
            if (existingUser != null)
            {
                return Conflict("User already exists");
            }

            // Salt makes sure that the hash value is never the same even though
            // the plaintext password is the same
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            createdUser.Username = request.Username;
            createdUser.PasswordHash = passwordHash;

            await _userService.CreateUserAsync(createdUser);

            return Ok(createdUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserDto request)
        {
            var existingUser = await _userService.GetUserByUsernameAsync(request.Username);

            if (existingUser == null) 
            {
                return BadRequest("Username not found or password is wrong.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, existingUser.PasswordHash)) 
            {
                return BadRequest("Username not found or password is wrong.");
            }
            string token = CreateToken(existingUser);

            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}

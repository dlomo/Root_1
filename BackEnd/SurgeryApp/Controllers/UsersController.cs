using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SurgeryApp.Models;
using SurgeryApp.Data;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using SurgeryApp.Services;

namespace SurgeryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SurgeryAppUsersDbContext _context;
        private readonly AuthService _authService;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public UsersController(
            SurgeryAppUsersDbContext context,
            AuthService authService,
            IUserService userService,
            IConfiguration config)
        {
            _context = context;
            _userService = userService;
            _authService = authService;
            _config = config;
        }

        // ================================
        // POST: api/users/login
        // ================================
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { message = "Invalid request." });
            }

            // SINGLE SOURCE OF USER
            var user = await _authService.ValidateUserAsync(
                request.Username,
                request.Password);

            if (user == null)
                return Unauthorized(new { message = "Invalid username or password." });

            if (user.UserId <= 0)
                return StatusCode(500, "Invalid user record.");

            var jwtKey = _config["Jwt:Key"];
            var jwtIssuer = _config["Jwt:Issuer"];
            var jwtAudience = _config["Jwt:Audience"];

            if (string.IsNullOrEmpty(jwtKey))
                return StatusCode(500, "JWT key not configured.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
        new Claim(ClaimTypes.Role, user.RoleName ?? "User")
    };

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );


            return Ok(new
            {
                success = true,
                message = "Login successful",
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }


        // ================================
        // GET: api/users/profile (Protected)
        // ================================
        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value ?? "User";

            return Ok(new
            {
                Id = userId,
                UserName = username,
                Email = email,
                Role = role
            });
        }

        // ================================
        // POST: api/users/register
        // ================================
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUser model)
        {
            if (string.IsNullOrWhiteSpace(model.Username) ||
                string.IsNullOrWhiteSpace(model.Password))
            {
                return BadRequest(new { message = "Username and password are required." });
            }

            var result = await _userService.RegisterUserAsync(model.Username, model.Password);
            
            if (result == RegisterResult.Success)
            {
                return Created("", new
                {
                    message = "User registered successfully",
                    username = model.Username
                });
            }

            //var createdUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == username);
            // return createdUser != null ? RegisterResult.Success    : RegisterResult.Failed;

            if (result == RegisterResult.UserExists)
            {
                return Conflict(new { message = "User already exists." });
            }

            return BadRequest(new { message = "Failed to register user." });

          
        }
    }

    // DTO for login
    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

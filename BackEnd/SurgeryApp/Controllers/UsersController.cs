using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using System.Text;

namespace SurgeryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // POST: api/users/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request body is missing or invalid JSON.");
            }
            Console.WriteLine("Login attempt for user: " + request.Username);
            // Hardcoded user validation for now
            if (request.Username == "testuser" && request.Password == "Password123")
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes("d88e1595-d539-441f-994d-c238a24d1ee6"); // Move to appsettings.json in production

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, request.Username),
                        new Claim(ClaimTypes.Role, "User")
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = "SurgeryApp",
                    Audience = "SurgeryAppUsers",
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwt = tokenHandler.WriteToken(token);

                return Ok(new { token = jwt });
                
            }

            return Unauthorized("Invalid username or password");
        }

        // GET: api/users/profile
        // Protected endpoint: requires valid JWT
        /*
        [HttpGet("profile")]
        [Authorize]
        public IActionResult GetProfile()
        {
            return Ok(new
            {
                message = "This is a protected profile endpoint",
                user = User.Identity?.Name
            });
        }

        // Example GET api/users/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        */
    }
    
      // Model for login request
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

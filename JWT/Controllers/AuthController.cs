using JWT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static JWT.Data.JwtDb;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            var existingUser = _context.Users.SingleOrDefault(u => u.Email == user.Email);

            if (existingUser == null || existingUser.Password != user.Password)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            var token = JwtHelper.GenerateJwt(user.Email, _configuration["Jwt:Key"]);

            return Ok(new { token });
        }
    }
}

using JWT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static JWT.Data.JwtDb;

namespace JWT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _jwtService = new JwtService(config["Jwt:Key"]);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            if (dbUser == null)
                return Unauthorized("Invalid credentials");

            var token = _jwtService.GenerateToken(user.Email);
            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
                return BadRequest("Email already exists");

            _context.Users.Add(user);
           
            return Ok("User registered");
        }
    }
}

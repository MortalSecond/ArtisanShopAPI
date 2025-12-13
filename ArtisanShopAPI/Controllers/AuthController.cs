using ArtisanShopAPI.Data;
using ArtisanShopAPI.Models;
using ArtisanShopAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtisanShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAuthService _authService;

        public AuthController(AppDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto dto)
        {
            // Find User
            var user = await _context.AdminUsers.FirstOrDefaultAsync(u=>u.Username == dto.Username);

            if(user == null)
                return Unauthorized("Invalid credentials");

            // Verify Password
            if (!_authService.VerifyPassword(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials");

            // Update Last Login
            user.LastLogin = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            // Generate Token
            var token = _authService.GenerateJwtToken(user);

            return Ok(new
            {
                token = token,
                username = dto.Username,
                email = user.Email,
                expiresIn = 28800 // 8hrs
            });
        }

        // DTOs
        public class RegisterDto
        {
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class LoginDto
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}

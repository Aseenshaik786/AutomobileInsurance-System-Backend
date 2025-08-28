// Controllers/AuthController.cs
using AutomobileInsuranceSystem.Contexts;
using AutomobileInsuranceSystem.Models;
using AutomobileInsuranceSystem.Models.DTOs;
using AutomobileInsuranceSystem.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutomobileInsuranceSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ITokenService _tokenService;

        public AuthController(AppDbContext context, IPasswordHasher<User> passwordHasher, ITokenService tokenService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Email already registered.");

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Role = string.IsNullOrEmpty(dto.Role) ? "User" : dto.Role,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                AadhaarNumber = dto.AadhaarNumber,
                PANNumber = dto.PANNumber,
                StreetAddress = dto.StreetAddress,
                City = dto.City,
                ZipCode = dto.ZipCode,
                Country = dto.Country,
                Occupation = dto.Occupation,
                Website = dto.Website,
                PictureUrl = dto.PictureUrl,
                CreatedAt = DateTime.UtcNow
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully.", userId = user.UserId });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null) return Unauthorized("Invalid credentials.");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed) return Unauthorized("Invalid credentials.");

            var token = await _tokenService.GenerateToken(new TokenUser
            {
                UserId = user.UserId,
                Username = user.Email,
                Role = user.Role
            });

            return Ok(new { token, role = user.Role, email = user.Email });

        }
    }
}

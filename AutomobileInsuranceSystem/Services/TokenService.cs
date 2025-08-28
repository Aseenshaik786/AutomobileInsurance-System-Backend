// Services/TokenService.cs
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutomobileInsuranceSystem.Models.DTOs;
using AutomobileInsuranceSystem.Interfaces;

namespace AutomobileInsuranceSystem.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration)
        {
            string secret = configuration["Jwt:Key"];
            if (string.IsNullOrWhiteSpace(secret) || secret.Length < 32)
                throw new Exception("JWT secret is missing or too short in appsettings.json under 'Jwt:Key'.");

            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        }

        public Task<string> GenerateToken(TokenUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role ?? "User")
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = creds,
                Issuer = "AutoInsuranceAPI",
                Audience = "AutoInsuranceClient"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(descriptor);
            return Task.FromResult(tokenHandler.WriteToken(token));
        }
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TaskSync.Application.Interfaces;

namespace TaskSync.Infrastructure.Services;

public sealed class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<string> GenerateTokenAsync(Guid userId)
    {
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);

        var token = new JwtSecurityToken(
            claims:
            [
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            ],
            expires: DateTime.UtcNow.AddHours(12),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)
        );

        return Task.FromResult(
            new JwtSecurityTokenHandler().WriteToken(token));
    }
}
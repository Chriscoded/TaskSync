using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TaskSync.Application.Abstractions;
using TaskSync.Application.Interfaces;

namespace TaskSync.Infrastructure.Services;

public sealed class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly IApplicationDbContext _context;

    public JwtService(
        IConfiguration configuration,
        IApplicationDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public async Task<string> GenerateTokenAsync(Guid userId)
    {
        var user = await _context.Users
            .FirstAsync(x => x.Id == userId);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new("tenantId", user.TenantId.ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
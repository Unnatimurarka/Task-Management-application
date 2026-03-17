using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TaskManager.API.DTOs;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;
namespace TaskManager.API.Services;
public class AuthService
{
    private readonly IUserRepository _userRepo;
    private readonly IConfiguration _config;
    public AuthService(IUserRepository userRepo, IConfiguration config)
    {
        _userRepo = userRepo;
        _config = config;
    }

    public async Task<AuthResponseDto?> RegisterAsync(RegisterDto dto)
    {
        var existing = await _userRepo.GetByUsernameAsync(dto.Username);
        if (existing != null) return null;

        var user = new User { Username = dto.Username, PasswordHash = Hash(dto.Password) };
        await _userRepo.CreateAsync(user);
        return await LoginAsync(new LoginDto(dto.Username, dto.Password));
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
    {
        var user = await _userRepo.GetByUsernameAsync(dto.Username);
        if (user == null || user.PasswordHash != Hash(dto.Password)) return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Secret"]!);
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(descriptor);
        return new AuthResponseDto(tokenHandler.WriteToken(token), user.Username);
    }

    private static string Hash(string pwd) => 
        Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(pwd)));
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartHome.Core.Domain.Entities;
using SmartHome.Core.Domain.Enums;
using SmartHome.Core.DTO.Auth;
using SmartHome.Core.Persistence;
using SmartHome.Core.Services;
using SmartHome.Core.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartHome.Core.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly JwtOptions _jwt;
    private readonly PasswordHasher<User> _hasher = new();

    public AuthService(AppDbContext db, IOptions<JwtOptions> jwtOptions)
    {
        _db = db;
        _jwt = jwtOptions.Value;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        var email = dto.Email.Trim().ToLowerInvariant();
        var phone = dto.PhoneNumber.Trim();

        var exists = await _db.Users.AnyAsync(u =>
            u.Email.ToLower() == email || u.PhoneNumber == phone);

        if (exists)
            throw new InvalidOperationException("Користувач із таким email або телефоном вже існує.");

        var user = new User
        {
            FirstName = dto.FirstName.Trim(),
            LastName = dto.LastName.Trim(),
            Email = email,
            PhoneNumber = phone,
            Role = UserRole.User,
            Status = UserStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        user.PasswordHash = _hasher.HashPassword(user, dto.Password);

        var security = new SecurityState
        {
            User = user,
            Mode = SecurityMode.DISARMED,
            UpdatedAt = DateTime.UtcNow
        };

        _db.Users.Add(user);
        _db.SecurityStates.Add(security);

        await _db.SaveChangesAsync();

        return CreateToken(user);
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var login = dto.Login.Trim();
        var password = dto.Password;

        var isEmail = login.Contains("@");
        var errorText = isEmail
            ? "Неправильна пошта або пароль."
            : "Неправильний номер телефону або пароль.";

        User? user;

        if (isEmail)
        {
            var email = login.ToLowerInvariant();
            user = await _db.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email);
        }
        else
        {
            user = await _db.Users.FirstOrDefaultAsync(u => u.PhoneNumber == login);
        }

        if (user is null || user.Status == UserStatus.Blocked)
            throw new UnauthorizedAccessException(errorText);

        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);
        if (result == PasswordVerificationResult.Failed)
            throw new UnauthorizedAccessException(errorText);

        return CreateToken(user);
    }

    private AuthResponseDto CreateToken(User user)
    {
        var keyBytes = Encoding.UTF8.GetBytes(_jwt.Key);
        var key = new SymmetricSecurityKey(keyBytes);
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        var expires = DateTime.UtcNow.AddMinutes(_jwt.ExpiresMinutes);

        var token = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new AuthResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAt = expires
        };
    }
}
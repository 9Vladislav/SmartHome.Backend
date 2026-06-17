using Microsoft.EntityFrameworkCore;
using SmartHome.Core.DTO.Profile;
using SmartHome.Core.Persistence;
using SmartHome.Core.Services.Interfaces;

namespace SmartHome.Core.Services.Implementations;

public class ProfileService : IProfileService
{
    private readonly AppDbContext _db;

    public ProfileService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<ProfileDto> GetMyProfileAsync(int userId)
    {
        var user = await _db.Users
            .FirstOrDefaultAsync(x => x.UserId == userId);

        if (user is null)
            throw new KeyNotFoundException("Користувача не знайдено.");

        return new ProfileDto
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email
        };
    }

    public async Task<ProfileDto> UpdateMyProfileAsync(int userId, UpdateProfileDto dto)
    {
        var user = await _db.Users
            .FirstOrDefaultAsync(x => x.UserId == userId);

        if (user is null)
            throw new KeyNotFoundException("Користувача не знайдено.");

        var email = dto.Email.Trim().ToLowerInvariant();
        var phone = dto.PhoneNumber.Trim();

        var exists = await _db.Users.AnyAsync(x =>
            x.UserId != userId &&
            (x.Email.ToLower() == email || x.PhoneNumber == phone));

        if (exists)
            throw new InvalidOperationException(
                "Користувач із таким email або телефоном вже існує.");

        user.FirstName = dto.FirstName.Trim();
        user.LastName = dto.LastName.Trim();
        user.PhoneNumber = phone;
        user.Email = email;

        await _db.SaveChangesAsync();

        return new ProfileDto
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email
        };
    }
}
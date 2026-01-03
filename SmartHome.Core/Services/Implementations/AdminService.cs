using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartHome.Core.Domain.Entities;
using SmartHome.Core.Domain.Enums;
using SmartHome.Core.DTO.Admin;
using SmartHome.Core.DTO.Events;
using SmartHome.Core.DTO.Incidents;
using SmartHome.Core.DTO.Rooms;
using SmartHome.Core.DTO.Sensors;
using SmartHome.Core.Mapping;
using SmartHome.Core.Persistence;
using SmartHome.Core.Services.Interfaces;

namespace SmartHome.Core.Services.Implementations;

public class AdminService : IAdminService
{
    private readonly AppDbContext _db;
    private readonly PasswordHasher<User> _hasher = new();

    public AdminService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<AdminUserDto>> GetUsersAsync()
    {
        var users = await _db.Users
            .AsNoTracking()
            .OrderByDescending(u => u.CreatedAt)
            .ToListAsync();

        return users.Select(u => u.ToAdminDto()).ToList();
    }

    public async Task<AdminUserDto?> GetUserByIdAsync(int userId)
    {
        var user = await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserId == userId);

        return user?.ToAdminDto();
    }

    public async Task<AdminUserDto?> CreateUserAsync(CreateUserDto dto)
    {
        var email = dto.Email.Trim().ToLowerInvariant();
        var phone = dto.PhoneNumber.Trim();

        var exists = await _db.Users.AnyAsync(u =>
            u.Email.ToLower() == email || u.PhoneNumber == phone);

        if (exists)
            return null;

        var user = new User
        {
            FirstName = dto.FirstName.Trim(),
            LastName = dto.LastName.Trim(),
            Email = email,
            PhoneNumber = phone,
            Role = dto.Role,
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
        return user.ToAdminDto();
    }

    public async Task<bool> UpdateUserAsync(int userId, UpdateUserDto dto)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        if (user is null) return false;

        var email = dto.Email.Trim().ToLowerInvariant();
        var phone = dto.PhoneNumber.Trim();

        var exists = await _db.Users.AnyAsync(u =>
            u.UserId != userId &&
            (u.Email.ToLower() == email || u.PhoneNumber == phone));

        if (exists)
            return false;

        user.FirstName = dto.FirstName.Trim();
        user.LastName = dto.LastName.Trim();
        user.Email = email;
        user.PhoneNumber = phone;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> SetUserStatusAsync(int userId, UserStatus status)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        if (user is null) return false;

        user.Status = status;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        if (user is null) return false;

        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<List<RoomDto>> GetAllRoomsAsync()
    {
        var rooms = await _db.Rooms
            .AsNoTracking()
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        return rooms.Select(r => r.ToDto()).ToList();
    }

    public async Task<List<SensorDto>> GetAllSensorsAsync()
    {
        var sensors = await _db.Sensors
            .AsNoTracking()
            .Include(s => s.Room)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();

        return sensors.Select(s => s.ToDto()).ToList();
    }

    public async Task<List<EventDto>> GetAllEventsAsync()
    {
        var events = await _db.Events
            .AsNoTracking()
            .Include(e => e.Sensor)
                .ThenInclude(s => s.Room)
            .Include(e => e.Incident)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();

        return events.Select(e => e.ToDto()).ToList();
    }

    public async Task<List<IncidentDto>> GetAllIncidentsAsync()
    {
        var incidents = await _db.Incidents
            .AsNoTracking()
            .Include(i => i.Event)
                .ThenInclude(e => e.Sensor)
                    .ThenInclude(s => s.Room)
            .OrderByDescending(i => i.CreatedAt)
            .ToListAsync();

        return incidents.Select(i => i.ToDto()).ToList();
    }

    public async Task<AdminOverviewStatsDto> GetOverviewStatsAsync()
    {
        var now = DateTime.UtcNow;
        var since24h = now.AddHours(-24);

        return new AdminOverviewStatsDto
        {
            UsersTotal = await _db.Users.CountAsync(),
            UsersActive = await _db.Users.CountAsync(u => u.Status == UserStatus.Active),
            UsersBlocked = await _db.Users.CountAsync(u => u.Status == UserStatus.Blocked),

            SensorsTotal = await _db.Sensors.CountAsync(),
            SensorsEnabled = await _db.Sensors.CountAsync(s => s.IsEnabled),
            SensorsDisabled = await _db.Sensors.CountAsync(s => !s.IsEnabled),

            IncidentsOpen = await _db.Incidents.CountAsync(i => i.Status == IncidentStatus.OPEN),
            IncidentsLast24h = await _db.Incidents.CountAsync(i => i.CreatedAt >= since24h),
            EventsLast24h = await _db.Events.CountAsync(e => e.CreatedAt >= since24h)
        };
    }
}

using Microsoft.EntityFrameworkCore;
using SmartHome.Core.Domain.Entities;
using SmartHome.Core.Domain.Enums;
using SmartHome.Core.DTO.Security;
using SmartHome.Core.Mapping;
using SmartHome.Core.Persistence;
using SmartHome.Core.Services.Interfaces;

namespace SmartHome.Core.Services.Implementations;

public class SecurityService : ISecurityService
{
    private readonly AppDbContext _db;

    public SecurityService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<SecurityStateDto> GetMyStateAsync(int userId)
    {
        var state = await _db.SecurityStates
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.UserId == userId);

        if (state is null)
        {
            state = new SecurityState
            {
                UserId = userId,
                Mode = SecurityMode.DISARMED,
                UpdatedAt = DateTime.UtcNow
            };

            _db.SecurityStates.Add(state);
            await _db.SaveChangesAsync();
        }

        return state.ToDto();
    }

    public async Task<SecurityStateDto> SetMyModeAsync(int userId, SecurityMode mode)
    {
        var state = await _db.SecurityStates
            .FirstOrDefaultAsync(s => s.UserId == userId);

        if (state is null)
        {
            state = new SecurityState
            {
                UserId = userId,
                Mode = mode,
                UpdatedAt = DateTime.UtcNow
            };

            _db.SecurityStates.Add(state);
            await _db.SaveChangesAsync();
            return state.ToDto();
        }

        if (state.Mode == mode)
            return state.ToDto();

        state.Mode = mode;
        state.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return state.ToDto();
    }
}
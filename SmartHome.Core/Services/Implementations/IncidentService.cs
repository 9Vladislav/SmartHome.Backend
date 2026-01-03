using Microsoft.EntityFrameworkCore;
using SmartHome.Core.Domain.Enums;
using SmartHome.Core.DTO.Incidents;
using SmartHome.Core.Mapping;
using SmartHome.Core.Persistence;
using SmartHome.Core.Services.Interfaces;

namespace SmartHome.Core.Services.Implementations;

public class IncidentService : IIncidentService
{
    private readonly AppDbContext _db;

    public IncidentService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<IncidentDto>> GetMyIncidentsAsync(int userId)
    {
        var incidents = await _db.Incidents
            .AsNoTracking()
            .Include(i => i.Event)
                .ThenInclude(e => e.Sensor)
                    .ThenInclude(s => s.Room)
            .Where(i => i.Event.Sensor.Room.UserId == userId)
            .OrderByDescending(i => i.CreatedAt)
            .ToListAsync();

        return incidents.Select(i => i.ToDto()).ToList();
    }

    public async Task<IncidentDto?> GetMyIncidentByIdAsync(int userId, int incidentId)
    {
        var incident = await _db.Incidents
            .AsNoTracking()
            .Include(i => i.Event)
                .ThenInclude(e => e.Sensor)
                    .ThenInclude(s => s.Room)
            .FirstOrDefaultAsync(i =>
                i.IncidentId == incidentId &&
                i.Event.Sensor.Room.UserId == userId);

        return incident?.ToDto();
    }

    public async Task<bool> ResolveIncidentAsync(int userId, int incidentId)
    {
        var incident = await _db.Incidents
            .Include(i => i.Event)
                .ThenInclude(e => e.Sensor)
                    .ThenInclude(s => s.Room)
            .FirstOrDefaultAsync(i =>
                i.IncidentId == incidentId &&
                i.Event.Sensor.Room.UserId == userId);

        if (incident is null)
            return false;

        if (incident.Status == IncidentStatus.RESOLVED)
            return true;

        incident.Status = IncidentStatus.RESOLVED;
        incident.ResolvedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return true;
    }
}

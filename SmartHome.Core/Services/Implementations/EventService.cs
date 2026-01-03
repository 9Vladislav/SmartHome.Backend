using Microsoft.EntityFrameworkCore;
using SmartHome.Core.Domain.Entities;
using SmartHome.Core.Domain.Enums;
using SmartHome.Core.DTO.Events;
using SmartHome.Core.Persistence;
using SmartHome.Core.Services.Interfaces;

namespace SmartHome.Core.Services.Implementations;

public class EventService : IEventService
{
    private readonly AppDbContext _db;

    public EventService(AppDbContext db)
    {
        _db = db;
    }

    public async Task HandleDeviceEventAsync(CreateEventDto dto)
    {
        var sensor = await _db.Sensors
            .Include(s => s.Room)
            .ThenInclude(r => r.User)
            .FirstOrDefaultAsync(s => s.SensorId == dto.SensorId);

        if (sensor is null || !sensor.IsEnabled)
            return;

        var occurredAt = dto.OccurredAt ?? DateTime.UtcNow;

        var ev = new Event
        {
            SensorId = sensor.SensorId,
            Type = dto.Type,
            OccurredAt = occurredAt,
            CreatedAt = DateTime.UtcNow
        };

        _db.Events.Add(ev);
        await _db.SaveChangesAsync();

        var security = await _db.SecurityStates
            .FirstOrDefaultAsync(s => s.UserId == sensor.Room.UserId);

        if (security is null || security.Mode != SecurityMode.ARMED)
            return;

        if (ev.Type == EventType.CONTACT_CLOSED)
            return;

        var incident = new Incident
        {
            EventId = ev.EventId,
            Status = IncidentStatus.OPEN,
            CreatedAt = DateTime.UtcNow
        };

        _db.Incidents.Add(incident);
        await _db.SaveChangesAsync();

        var notification = new Notification
        {
            UserId = sensor.Room.UserId,
            IncidentId = incident.IncidentId,
            Type = NotificationType.INCIDENT,
            Message = $"Подія {ev.Type} від сенсора '{sensor.Name}'",
            IsRead = false,
            CreatedAt = DateTime.UtcNow
        };

        _db.Notifications.Add(notification);
        await _db.SaveChangesAsync();
    }
}

using Microsoft.EntityFrameworkCore;
using SmartHome.Core.DTO.Notifications;
using SmartHome.Core.Mapping;
using SmartHome.Core.Persistence;
using SmartHome.Core.Services.Interfaces;

namespace SmartHome.Core.Services.Implementations;

public class NotificationService : INotificationService
{
    private readonly AppDbContext _db;

    public NotificationService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<NotificationDto>> GetMyNotificationsAsync(int userId)
    {
        var list = await _db.Notifications
            .AsNoTracking()
            .Include(n => n.Incident)
                .ThenInclude(i => i.Event)
                    .ThenInclude(e => e.Sensor)
                        .ThenInclude(s => s.Room)
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();

        return list.Select(n => n.ToDto()).ToList();
    }

    public async Task<List<NotificationDto>> GetMyUnreadNotificationsAsync(int userId)
    {
        var list = await _db.Notifications
            .AsNoTracking()
            .Include(n => n.Incident)
                .ThenInclude(i => i.Event)
                    .ThenInclude(e => e.Sensor)
                        .ThenInclude(s => s.Room)
            .Where(n => n.UserId == userId && !n.IsRead)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();

        return list.Select(n => n.ToDto()).ToList();
    }

    public async Task<bool> MarkAsReadAsync(int userId, int notificationId)
    {
        var n = await _db.Notifications
            .FirstOrDefaultAsync(x => x.NotificationId == notificationId && x.UserId == userId);

        if (n is null)
            return false;

        if (n.IsRead)
            return true;

        n.IsRead = true;
        await _db.SaveChangesAsync();
        return true;
    }
}

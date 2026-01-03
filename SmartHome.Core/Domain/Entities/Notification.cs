using SmartHome.Core.Domain.Enums;

namespace SmartHome.Core.Domain.Entities;

public class Notification
{
    public int NotificationId { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int? IncidentId { get; set; }
    public Incident? Incident { get; set; }

    public NotificationType Type { get; set; } = NotificationType.INCIDENT;
    public string Message { get; set; } = null!;

    public bool IsRead { get; set; } = false;
    public DateTime CreatedAt { get; set; }
}

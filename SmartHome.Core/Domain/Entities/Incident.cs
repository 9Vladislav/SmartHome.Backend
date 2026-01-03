using SmartHome.Core.Domain.Enums;

namespace SmartHome.Core.Domain.Entities;

public class Incident
{
    public int IncidentId { get; set; }

    public int EventId { get; set; }
    public Event Event { get; set; } = null!;

    public IncidentStatus Status { get; set; } = IncidentStatus.OPEN;
    public DateTime CreatedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }

    public List<Notification> Notifications { get; set; } = new();
}

using SmartHome.Core.Domain.Enums;

namespace SmartHome.Core.Domain.Entities;

public class Event
{
    public int EventId { get; set; }

    public int SensorId { get; set; }
    public Sensor Sensor { get; set; } = null!;

    public EventType Type { get; set; }
    public DateTime OccurredAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public Incident? Incident { get; set; }
}

using SmartHome.Core.Domain.Enums;

namespace SmartHome.Core.Domain.Entities;

public class Sensor
{
    public int SensorId { get; set; }

    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;

    public SensorType Type { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public bool IsEnabled { get; set; } = true;
    public DateTime CreatedAt { get; set; }

    public List<Event> Events { get; set; } = new();
}

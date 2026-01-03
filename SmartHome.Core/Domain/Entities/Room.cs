namespace SmartHome.Core.Domain.Entities;

public class Room
{
    public int RoomId { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<Sensor> Sensors { get; set; } = new();
}

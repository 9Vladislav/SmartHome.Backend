using SmartHome.Core.Domain.Enums;

namespace SmartHome.Core.Domain.Entities;

public class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    public UserRole Role { get; set; }
    public UserStatus Status { get; set; } = UserStatus.Active;

    public DateTime CreatedAt { get; set; }

    public List<Room> Rooms { get; set; } = new();
    public SecurityState? SecurityState { get; set; }
    public List<Notification> Notifications { get; set; } = new();
}

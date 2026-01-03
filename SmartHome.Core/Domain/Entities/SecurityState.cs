using SmartHome.Core.Domain.Enums;

namespace SmartHome.Core.Domain.Entities;

public class SecurityState
{
    public int SecurityStateId { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public SecurityMode Mode { get; set; } = SecurityMode.DISARMED;
    public DateTime UpdatedAt { get; set; }
}

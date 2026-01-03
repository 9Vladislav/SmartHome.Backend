using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.Domain.Enums;

namespace SmartHome.Core.DTO.Notifications;

public class NotificationDto
{
    public int NotificationId { get; set; }
    public NotificationType Type { get; set; }
    public string Message { get; set; } = null!;
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }

    public int? IncidentId { get; set; }

    public string? RoomName { get; set; }
    public string? SensorName { get; set; }
}

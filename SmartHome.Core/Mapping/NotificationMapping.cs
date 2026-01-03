using SmartHome.Core.Domain.Entities;
using SmartHome.Core.DTO.Notifications;

namespace SmartHome.Core.Mapping;

public static class NotificationMapping
{
    public static NotificationDto ToDto(this Notification n)
    {
        return new NotificationDto
        {
            NotificationId = n.NotificationId,
            Type = n.Type,
            Message = n.Message,
            IsRead = n.IsRead,
            CreatedAt = n.CreatedAt,
            IncidentId = n.IncidentId,
            RoomName = n.Incident?.Event.Sensor.Room.Name,
            SensorName = n.Incident?.Event.Sensor.Name
        };
    }
}

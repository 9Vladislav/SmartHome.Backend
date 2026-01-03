using SmartHome.Core.Domain.Entities;
using SmartHome.Core.DTO.Incidents;

namespace SmartHome.Core.Mapping;

public static class IncidentMapping
{
    public static IncidentDto ToDto(this Incident incident)
    {
        return new IncidentDto
        {
            IncidentId = incident.IncidentId,
            EventId = incident.EventId,
            Status = incident.Status,
            CreatedAt = incident.CreatedAt,
            ResolvedAt = incident.ResolvedAt,
            SensorName = incident.Event.Sensor.Name,
            RoomName = incident.Event.Sensor.Room.Name
        };
    }
}

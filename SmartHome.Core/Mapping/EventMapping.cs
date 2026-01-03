using SmartHome.Core.Domain.Entities;
using SmartHome.Core.DTO.Events;

namespace SmartHome.Core.Mapping;

public static class EventMapping
{
    public static EventDto ToDto(this Event ev)
    {
        return new EventDto
        {
            EventId = ev.EventId,
            SensorId = ev.SensorId,
            Type = ev.Type,
            OccurredAt = ev.OccurredAt,
            CreatedAt = ev.CreatedAt,
            SensorName = ev.Sensor.Name,
            RoomName = ev.Sensor.Room.Name,
            IncidentId = ev.Incident?.IncidentId
        };
    }
}

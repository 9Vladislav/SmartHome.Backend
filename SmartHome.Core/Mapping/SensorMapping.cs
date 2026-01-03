using SmartHome.Core.Domain.Entities;
using SmartHome.Core.DTO.Sensors;

namespace SmartHome.Core.Mapping;

public static class SensorMapping
{
    public static SensorDto ToDto(this Sensor sensor)
    {
        return new SensorDto
        {
            SensorId = sensor.SensorId,
            RoomId = sensor.RoomId,
            RoomName = sensor.Room.Name,
            Type = sensor.Type,
            Name = sensor.Name,
            Description = sensor.Description,
            IsEnabled = sensor.IsEnabled,
            CreatedAt = sensor.CreatedAt
        };
    }
}

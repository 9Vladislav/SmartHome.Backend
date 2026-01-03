using SmartHome.Core.Domain.Entities;
using SmartHome.Core.DTO.Rooms;

namespace SmartHome.Core.Mapping;

public static class RoomMapping
{
    public static RoomDto ToDto(this Room room)
    {
        return new RoomDto
        {
            RoomId = room.RoomId,
            Name = room.Name,
            Description = room.Description,
            CreatedAt = room.CreatedAt
        };
    }
}

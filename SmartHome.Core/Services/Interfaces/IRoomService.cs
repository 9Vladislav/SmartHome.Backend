using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.DTO.Rooms;

namespace SmartHome.Core.Services.Interfaces;

public interface IRoomService
{
    Task<List<RoomDto>> GetMyRoomsAsync(int userId);
    Task<RoomDto?> GetMyRoomByIdAsync(int userId, int roomId);
    Task<RoomDto> CreateRoomAsync(int userId, CreateRoomDto dto);
    Task<bool> UpdateRoomAsync(int userId, int roomId, UpdateRoomDto dto);
    Task<bool> DeleteRoomAsync(int userId, int roomId);
}

using Microsoft.EntityFrameworkCore;
using SmartHome.Core.Domain.Entities;
using SmartHome.Core.DTO.Rooms;
using SmartHome.Core.Mapping;
using SmartHome.Core.Persistence;
using SmartHome.Core.Services.Interfaces;

namespace SmartHome.Core.Services.Implementations;

public class RoomService : IRoomService
{
    private readonly AppDbContext _db;

    public RoomService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<RoomDto>> GetMyRoomsAsync(int userId)
    {
        var rooms = await _db.Rooms
            .AsNoTracking()
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        return rooms.Select(r => r.ToDto()).ToList();
    }

    public async Task<RoomDto?> GetMyRoomByIdAsync(int userId, int roomId)
    {
        var room = await _db.Rooms
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.UserId == userId && r.RoomId == roomId);

        return room?.ToDto();
    }

    public async Task<RoomDto> CreateRoomAsync(int userId, CreateRoomDto dto)
    {
        var room = new Room
        {
            UserId = userId,
            Name = dto.Name.Trim(),
            Description = dto.Description?.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        _db.Rooms.Add(room);
        await _db.SaveChangesAsync();

        return room.ToDto();
    }

    public async Task<bool> UpdateRoomAsync(int userId, int roomId, UpdateRoomDto dto)
    {
        var room = await _db.Rooms
            .FirstOrDefaultAsync(r => r.UserId == userId && r.RoomId == roomId);

        if (room is null)
            return false;

        room.Name = dto.Name.Trim();
        room.Description = dto.Description?.Trim();

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteRoomAsync(int userId, int roomId)
    {
        var room = await _db.Rooms
            .FirstOrDefaultAsync(r => r.UserId == userId && r.RoomId == roomId);

        if (room is null)
            return false;

        _db.Rooms.Remove(room);
        await _db.SaveChangesAsync();
        return true;
    }
}

using Microsoft.EntityFrameworkCore;
using SmartHome.Core.Domain.Entities;
using SmartHome.Core.DTO.Sensors;
using SmartHome.Core.Mapping;
using SmartHome.Core.Persistence;
using SmartHome.Core.Services.Interfaces;

namespace SmartHome.Core.Services.Implementations;

public class SensorService : ISensorService
{
    private readonly AppDbContext _db;

    public SensorService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<SensorDto>> GetMySensorsAsync(int userId)
    {
        var sensors = await _db.Sensors
            .AsNoTracking()
            .Include(s => s.Room)
            .Where(s => s.Room.UserId == userId)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();

        return sensors.Select(s => s.ToDto()).ToList();
    }

    public async Task<List<SensorDto>> GetMyRoomSensorsAsync(int userId, int roomId)
    {
        var sensors = await _db.Sensors
            .AsNoTracking()
            .Include(s => s.Room)
            .Where(s => s.RoomId == roomId && s.Room.UserId == userId)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();

        return sensors.Select(s => s.ToDto()).ToList();
    }

    public async Task<SensorDto?> GetMySensorByIdAsync(int userId, int sensorId)
    {
        var sensor = await _db.Sensors
            .AsNoTracking()
            .Include(s => s.Room)
            .FirstOrDefaultAsync(s => s.SensorId == sensorId && s.Room.UserId == userId);

        return sensor?.ToDto();
    }

    public async Task<SensorDto?> CreateSensorAsync(int userId, int roomId, CreateSensorDto dto)
    {
        var room = await _db.Rooms
            .FirstOrDefaultAsync(r => r.RoomId == roomId && r.UserId == userId);

        if (room is null)
            return null;

        var sensor = new Sensor
        {
            RoomId = roomId,
            Type = dto.Type,
            Name = dto.Name.Trim(),
            Description = dto.Description?.Trim(),
            IsEnabled = true,
            CreatedAt = DateTime.UtcNow
        };

        _db.Sensors.Add(sensor);
        await _db.SaveChangesAsync();

        sensor.Room = room;
        return sensor.ToDto();
    }

    public async Task<bool> UpdateSensorAsync(int userId, int sensorId, UpdateSensorDto dto)
    {
        var sensor = await _db.Sensors
            .Include(s => s.Room)
            .FirstOrDefaultAsync(s => s.SensorId == sensorId && s.Room.UserId == userId);

        if (sensor is null)
            return false;

        sensor.Name = dto.Name.Trim();
        sensor.Description = dto.Description?.Trim();

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> SetSensorEnabledAsync(int userId, int sensorId, bool isEnabled)
    {
        var sensor = await _db.Sensors
            .Include(s => s.Room)
            .FirstOrDefaultAsync(s => s.SensorId == sensorId && s.Room.UserId == userId);

        if (sensor is null)
            return false;

        sensor.IsEnabled = isEnabled;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteSensorAsync(int userId, int sensorId)
    {
        var sensor = await _db.Sensors
            .Include(s => s.Room)
            .FirstOrDefaultAsync(s => s.SensorId == sensorId && s.Room.UserId == userId);

        if (sensor is null)
            return false;

        _db.Sensors.Remove(sensor);
        await _db.SaveChangesAsync();
        return true;
    }
}

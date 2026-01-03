using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.DTO.Sensors;

namespace SmartHome.Core.Services.Interfaces;

public interface ISensorService
{
    Task<List<SensorDto>> GetMySensorsAsync(int userId);
    Task<List<SensorDto>> GetMyRoomSensorsAsync(int userId, int roomId);
    Task<SensorDto?> GetMySensorByIdAsync(int userId, int sensorId);

    Task<SensorDto?> CreateSensorAsync(int userId, int roomId, CreateSensorDto dto);
    Task<bool> UpdateSensorAsync(int userId, int sensorId, UpdateSensorDto dto);
    Task<bool> SetSensorEnabledAsync(int userId, int sensorId, bool isEnabled);
    Task<bool> DeleteSensorAsync(int userId, int sensorId);
}

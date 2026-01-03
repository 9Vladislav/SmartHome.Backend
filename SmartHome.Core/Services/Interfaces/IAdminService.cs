using SmartHome.Core.DTO.Admin;
using SmartHome.Core.DTO.Events;
using SmartHome.Core.DTO.Incidents;
using SmartHome.Core.DTO.Rooms;
using SmartHome.Core.DTO.Sensors;
using SmartHome.Core.Domain.Enums;

namespace SmartHome.Core.Services.Interfaces;

public interface IAdminService
{
    Task<List<AdminUserDto>> GetUsersAsync();
    Task<AdminUserDto?> GetUserByIdAsync(int userId);
    Task<AdminUserDto?> CreateUserAsync(CreateUserDto dto);
    Task<bool> UpdateUserAsync(int userId, UpdateUserDto dto);
    Task<bool> SetUserStatusAsync(int userId, UserStatus status);
    Task<bool> DeleteUserAsync(int userId);

    Task<List<RoomDto>> GetAllRoomsAsync();
    Task<List<SensorDto>> GetAllSensorsAsync();
    Task<List<EventDto>> GetAllEventsAsync();
    Task<List<IncidentDto>> GetAllIncidentsAsync();

    Task<AdminOverviewStatsDto> GetOverviewStatsAsync();
}

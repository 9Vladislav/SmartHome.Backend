using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SmartHome.Core.DTO.Sensors;
using SmartHome.Core.Services.Interfaces;

namespace SmartHome.Api.Controllers;

[ApiController]
[Authorize]
public class SensorsController : ControllerBase
{
    private readonly ISensorService _sensors;
    private readonly IRoomService _rooms;

    public SensorsController(ISensorService sensors, IRoomService rooms)
    {
        _sensors = sensors;
        _rooms = rooms;
    }

    private int CurrentUserId =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet("api/sensors")]
    public async Task<IActionResult> GetMySensors()
    {
        var sensors = await _sensors.GetMySensorsAsync(CurrentUserId);
        return Ok(sensors);
    }

    [HttpGet("api/rooms/{roomId:int}/sensors")]
    public async Task<IActionResult> GetRoomSensors([FromRoute] int roomId)
    {
        var room = await _rooms.GetMyRoomByIdAsync(CurrentUserId, roomId);
        if (room is null) return NotFound();

        var sensors = await _sensors.GetMyRoomSensorsAsync(CurrentUserId, roomId);
        return Ok(sensors);
    }

    [HttpGet("api/sensors/{id:int}")]
    public async Task<IActionResult> GetSensorById([FromRoute] int id)
    {
        var sensor = await _sensors.GetMySensorByIdAsync(CurrentUserId, id);
        if (sensor is null) return NotFound();
        return Ok(sensor);
    }

    [HttpPost("api/rooms/{roomId:int}/sensors")]
    public async Task<IActionResult> Create([FromRoute] int roomId, [FromBody] CreateSensorDto dto)
    {
        var created = await _sensors.CreateSensorAsync(CurrentUserId, roomId, dto);
        if (created is null) return NotFound();

        return CreatedAtAction(nameof(GetSensorById), new { id = created.SensorId }, created);
    }

    [HttpPut("api/sensors/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSensorDto dto)
    {
        var ok = await _sensors.UpdateSensorAsync(CurrentUserId, id, dto);
        if (!ok) return NotFound();
        return NoContent();
    }

    [HttpPatch("api/sensors/{id:int}/enabled")]
    public async Task<IActionResult> SetEnabled([FromRoute] int id, [FromBody] SetSensorEnabledDto dto)
    {
        var ok = await _sensors.SetSensorEnabledAsync(CurrentUserId, id, dto.IsEnabled);
        if (!ok) return NotFound();
        return NoContent();
    }

    [HttpDelete("api/sensors/{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var ok = await _sensors.DeleteSensorAsync(CurrentUserId, id);
        if (!ok) return NotFound();
        return NoContent();
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHome.Core.DTO.Events;
using SmartHome.Core.Services.Interfaces;

namespace SmartHome.Api.Controllers;

[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly IEventService _events;
    private readonly IConfiguration _config;

    public EventsController(IEventService events, IConfiguration config)
    {
        _events = events;
        _config = config;
    }

    [HttpPost("device")]
    [AllowAnonymous]
    public async Task<IActionResult> PostFromDevice(
        [FromBody] CreateEventDto dto,
        [FromHeader(Name = "X-Device-Key")] string? deviceKey)
    {
        var expectedKey = _config["DeviceIngest:Key"];

        if (string.IsNullOrWhiteSpace(deviceKey) || deviceKey != expectedKey)
            return Unauthorized();

        await _events.HandleDeviceEventAsync(dto);
        return Ok();
    }
}

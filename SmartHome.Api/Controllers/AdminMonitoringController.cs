using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHome.Core.Services.Interfaces;

namespace SmartHome.Api.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")]
public class AdminMonitoringController : ControllerBase
{
    private readonly IAdminService _admin;

    public AdminMonitoringController(IAdminService admin)
    {
        _admin = admin;
    }

    [HttpGet("rooms")]
    public async Task<IActionResult> GetAllRooms()
        => Ok(await _admin.GetAllRoomsAsync());

    [HttpGet("sensors")]
    public async Task<IActionResult> GetAllSensors()
        => Ok(await _admin.GetAllSensorsAsync());

    [HttpGet("events")]
    public async Task<IActionResult> GetAllEvents()
        => Ok(await _admin.GetAllEventsAsync());

    [HttpGet("incidents")]
    public async Task<IActionResult> GetAllIncidents()
        => Ok(await _admin.GetAllIncidentsAsync());

    [HttpGet("stats/overview")]
    public async Task<IActionResult> OverviewStats()
        => Ok(await _admin.GetOverviewStatsAsync());
}

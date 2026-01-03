using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SmartHome.Core.DTO.Incidents;
using SmartHome.Core.Services.Interfaces;

namespace SmartHome.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/incidents")]
public class IncidentsController : ControllerBase
{
    private readonly IIncidentService _incidents;

    public IncidentsController(IIncidentService incidents)
    {
        _incidents = incidents;
    }

    private int CurrentUserId =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> GetMyIncidents()
    {
        var list = await _incidents.GetMyIncidentsAsync(CurrentUserId);
        return Ok(list);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var incident = await _incidents.GetMyIncidentByIdAsync(CurrentUserId, id);
        if (incident is null) return NotFound();
        return Ok(incident);
    }

    [HttpPut("{id:int}/resolve")]
    public async Task<IActionResult> Resolve([FromRoute] int id)
    {
        var ok = await _incidents.ResolveIncidentAsync(CurrentUserId, id);
        if (!ok) return NotFound();
        return NoContent();
    }
}
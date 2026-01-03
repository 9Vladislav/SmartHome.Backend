using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SmartHome.Core.DTO.Security;
using SmartHome.Core.Services.Interfaces;

namespace SmartHome.Api.Controllers;

[ApiController]
[Route("api/security")]
[Authorize]
public class SecurityController : ControllerBase
{
    private readonly ISecurityService _security;

    public SecurityController(ISecurityService security)
    {
        _security = security;
    }

    private int CurrentUserId =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet("state")]
    public async Task<IActionResult> GetState()
    {
        var state = await _security.GetMyStateAsync(CurrentUserId);
        return Ok(state);
    }

    [HttpPut("mode")]
    public async Task<IActionResult> SetMode([FromBody] SetSecurityModeDto dto)
    {
        var result = await _security.SetMyModeAsync(CurrentUserId, dto.Mode);
        return Ok(result);
    }
}

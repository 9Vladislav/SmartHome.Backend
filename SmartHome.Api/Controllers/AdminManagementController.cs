using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHome.Core.DTO.Admin;
using SmartHome.Core.Services.Interfaces;

namespace SmartHome.Api.Controllers;

[ApiController]
[Route("api/admin/users")]
[Authorize(Roles = "Admin")]
public class AdminManagementController : ControllerBase
{
    private readonly IAdminService _admin;

    public AdminManagementController(IAdminService admin)
    {
        _admin = admin;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _admin.GetUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserById([FromRoute] int id)
    {
        var user = await _admin.GetUserByIdAsync(id);
        if (user is null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
    {
        var created = await _admin.CreateUserAsync(dto);
        if (created is null)
            return BadRequest("User with same email or phone already exists.");

        return CreatedAtAction(nameof(GetUserById), new { id = created.UserId }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUser(
        [FromRoute] int id,
        [FromBody] UpdateUserDto dto)
    {
        var ok = await _admin.UpdateUserAsync(id, dto);
        if (!ok)
            return BadRequest("User not found or email/phone already used.");

        return NoContent();
    }

    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> SetUserStatus(
        [FromRoute] int id,
        [FromBody] SetUserStatusDto dto)
    {
        var ok = await _admin.SetUserStatusAsync(id, dto.Status);
        if (!ok) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id)
    {
        var ok = await _admin.DeleteUserAsync(id);
        if (!ok) return NotFound();
        return NoContent();
    }
}

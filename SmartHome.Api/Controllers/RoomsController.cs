using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SmartHome.Core.DTO.Rooms;
using SmartHome.Core.Services.Interfaces;

namespace SmartHome.Api.Controllers;

[ApiController]
[Route("api/rooms")]
[Authorize]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _rooms;

    public RoomsController(IRoomService rooms)
    {
        _rooms = rooms;
    }

    private int CurrentUserId =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> GetMyRooms()
    {
        var rooms = await _rooms.GetMyRoomsAsync(CurrentUserId);
        return Ok(rooms);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMyRoomById([FromRoute] int id)
    {
        var room = await _rooms.GetMyRoomByIdAsync(CurrentUserId, id);
        if (room is null) return NotFound();
        return Ok(room);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoomDto dto)
    {
        var created = await _rooms.CreateRoomAsync(CurrentUserId, dto);
        return CreatedAtAction(nameof(GetMyRoomById), new { id = created.RoomId }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRoomDto dto)
    {
        var ok = await _rooms.UpdateRoomAsync(CurrentUserId, id, dto);
        if (!ok) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var ok = await _rooms.DeleteRoomAsync(CurrentUserId, id);
        if (!ok) return NotFound();
        return NoContent();
    }
}

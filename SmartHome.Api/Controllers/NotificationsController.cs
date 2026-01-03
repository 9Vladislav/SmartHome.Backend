using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SmartHome.Core.Services.Interfaces;

namespace SmartHome.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/notifications")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notifications;

    public NotificationsController(INotificationService notifications)
    {
        _notifications = notifications;
    }

    private int CurrentUserId =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _notifications.GetMyNotificationsAsync(CurrentUserId);
        return Ok(list);
    }

    [HttpGet("unread")]
    public async Task<IActionResult> GetUnread()
    {
        var list = await _notifications.GetMyUnreadNotificationsAsync(CurrentUserId);
        return Ok(list);
    }

    [HttpPatch("{id:int}/read")]
    public async Task<IActionResult> MarkAsRead([FromRoute] int id)
    {
        var ok = await _notifications.MarkAsReadAsync(CurrentUserId, id);
        if (!ok) return NotFound();
        return NoContent();
    }
}

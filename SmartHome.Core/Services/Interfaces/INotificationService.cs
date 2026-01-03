using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.DTO.Notifications;

namespace SmartHome.Core.Services.Interfaces;

public interface INotificationService
{
    Task<List<NotificationDto>> GetMyNotificationsAsync(int userId);
    Task<List<NotificationDto>> GetMyUnreadNotificationsAsync(int userId);
    Task<bool> MarkAsReadAsync(int userId, int notificationId);
}

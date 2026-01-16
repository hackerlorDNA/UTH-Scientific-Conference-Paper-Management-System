using Notification.Service.DTOs;

namespace Notification.Service.Interfaces;

public interface INotificationService
{
    Task<bool> SendNotificationAsync(NotificationDto notification);
    Task<bool> SendBulkNotificationsAsync(List<NotificationDto> notifications);
    Task<List<NotificationDto>> GetUserNotificationsAsync(Guid userId, int page, int pageSize);
    Task<bool> MarkAsReadAsync(Guid notificationId);
    Task<int> GetUnreadCountAsync(Guid userId);
}

using Notification.Service.DTOs;

namespace Notification.Service.Interfaces;

public interface IEmailService
{
    Task<bool> SendEmailAsync(EmailRequest request);
    Task<bool> SendBulkEmailAsync(List<EmailRequest> requests);
    Task<bool> SendTemplateEmailAsync(string templateName, Dictionary<string, string> data, string toEmail);
}

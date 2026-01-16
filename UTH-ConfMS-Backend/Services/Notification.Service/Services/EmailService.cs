using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Notification.Service.DTOs;
using Notification.Service.Interfaces;
using Notification.Service.Configuration;

namespace Notification.Service.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<EmailService> _logger;
    private readonly SmtpClient _smtpClient;

    public EmailService(
        IOptions<EmailSettings> emailSettings,
        ILogger<EmailService> logger)
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;

        // Configure SMTP client
        _smtpClient = new SmtpClient(_emailSettings.SmtpHost, _emailSettings.SmtpPort)
        {
            Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword),
            EnableSsl = _emailSettings.EnableSsl,
            Timeout = 30000
        };
    }

    public async Task<bool> SendEmailAsync(EmailRequest request)
    {
        try
        {
            _logger.LogInformation("Sending email to {ToEmail} with subject: {Subject}", 
                request.ToEmail, request.Subject);

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.FromEmail, _emailSettings.FromName),
                Subject = request.Subject,
                Body = request.Body,
                IsBodyHtml = request.IsHtml
            };

            mailMessage.To.Add(request.ToEmail);

            // Add CC recipients
            if (request.CcEmails != null)
            {
                foreach (var cc in request.CcEmails)
                {
                    mailMessage.CC.Add(cc);
                }
            }

            // Add attachments
            if (request.Attachments != null)
            {
                foreach (var attachment in request.Attachments)
                {
                    var att = new Attachment(new MemoryStream(attachment.Content), attachment.FileName);
                    mailMessage.Attachments.Add(att);
                }
            }

            await _smtpClient.SendMailAsync(mailMessage);

            _logger.LogInformation("Email sent successfully to {ToEmail}", request.ToEmail);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {ToEmail}: {Message}", 
                request.ToEmail, ex.Message);
            return false;
        }
    }

    public async Task<bool> SendBulkEmailAsync(List<EmailRequest> requests)
    {
        _logger.LogInformation("Sending bulk emails. Count: {Count}", requests.Count);

        var tasks = requests.Select(SendEmailAsync);
        var results = await Task.WhenAll(tasks);

        var successCount = results.Count(r => r);
        _logger.LogInformation("Bulk email completed. Success: {Success}/{Total}", 
            successCount, requests.Count);

        return successCount == requests.Count;
    }

    public async Task<bool> SendTemplateEmailAsync(
        string templateName, 
        Dictionary<string, string> data, 
        string toEmail)
    {
        try
        {
            _logger.LogInformation("Sending template email '{Template}' to {ToEmail}", 
                templateName, toEmail);

            var template = GetEmailTemplate(templateName);
            if (template == null)
            {
                _logger.LogError("Email template '{Template}' not found", templateName);
                return false;
            }

            // Replace placeholders with actual data
            var subject = ReplacePlaceholders(template.Subject, data);
            var body = ReplacePlaceholders(template.Body, data);

            var request = new EmailRequest
            {
                ToEmail = toEmail,
                Subject = subject,
                Body = body,
                IsHtml = template.IsHtml
            };

            return await SendEmailAsync(request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send template email '{Template}' to {ToEmail}", 
                templateName, toEmail);
            return false;
        }
    }

    private EmailTemplate? GetEmailTemplate(string templateName)
    {
        // In production, load templates from database or file system
        // For now, return hardcoded templates
        return templateName.ToLower() switch
        {
            "welcome" => new EmailTemplate
            {
                Name = "welcome",
                Subject = "Welcome to UTH-ConfMS - {{UserName}}",
                Body = @"
                    <html>
                        <body>
                            <h2>Welcome to UTH Conference Management System!</h2>
                            <p>Dear {{UserName}},</p>
                            <p>Thank you for registering with UTH-ConfMS.</p>
                            <p>Your account has been created successfully.</p>
                            <p>Best regards,<br/>UTH-ConfMS Team</p>
                        </body>
                    </html>",
                IsHtml = true
            },
            "submission_received" => new EmailTemplate
            {
                Name = "submission_received",
                Subject = "Submission Received - {{SubmissionTitle}}",
                Body = @"
                    <html>
                        <body>
                            <h2>Submission Received</h2>
                            <p>Dear {{AuthorName}},</p>
                            <p>Your submission <strong>{{SubmissionTitle}}</strong> has been received.</p>
                            <p>Submission ID: {{SubmissionId}}</p>
                            <p>Conference: {{ConferenceName}}</p>
                            <p>You will be notified once the review process is complete.</p>
                            <p>Best regards,<br/>Conference Committee</p>
                        </body>
                    </html>",
                IsHtml = true
            },
            "review_assignment" => new EmailTemplate
            {
                Name = "review_assignment",
                Subject = "Review Assignment - {{SubmissionTitle}}",
                Body = @"
                    <html>
                        <body>
                            <h2>New Review Assignment</h2>
                            <p>Dear {{ReviewerName}},</p>
                            <p>You have been assigned to review the following submission:</p>
                            <p><strong>{{SubmissionTitle}}</strong></p>
                            <p>Conference: {{ConferenceName}}</p>
                            <p>Review Deadline: {{ReviewDeadline}}</p>
                            <p>Please accept or decline this assignment in the system.</p>
                            <p>Best regards,<br/>Conference Committee</p>
                        </body>
                    </html>",
                IsHtml = true
            },
            "decision_notification" => new EmailTemplate
            {
                Name = "decision_notification",
                Subject = "Decision for Your Submission - {{SubmissionTitle}}",
                Body = @"
                    <html>
                        <body>
                            <h2>Submission Decision</h2>
                            <p>Dear {{AuthorName}},</p>
                            <p>The review process for your submission <strong>{{SubmissionTitle}}</strong> is complete.</p>
                            <p>Decision: <strong>{{Decision}}</strong></p>
                            <p>{{Comments}}</p>
                            <p>Best regards,<br/>Conference Committee</p>
                        </body>
                    </html>",
                IsHtml = true
            },
            _ => null
        };
    }

    private string ReplacePlaceholders(string text, Dictionary<string, string> data)
    {
        foreach (var (key, value) in data)
        {
            text = text.Replace($"{{{{{key}}}}}", value);
        }
        return text;
    }
}

public class EmailTemplate
{
    public string Name { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool IsHtml { get; set; }
}

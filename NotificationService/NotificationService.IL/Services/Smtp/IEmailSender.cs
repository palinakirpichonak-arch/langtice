using Shared.Data;

namespace NotificationService.IL.Services.Smtp;

public interface IEmailSender
{
    Task SendEmail(string email, MessageNotificationType notificationType, string body,  params object[] parameters);
}
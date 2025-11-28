using Shared.Data;

namespace NotificationService.IL.Services.Smtp;

public interface IEmailSender
{
    Task SendEmail(string email, MessageType type, string body,  params object[] parameters);
}
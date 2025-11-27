namespace NotificationService.IL.Services.Smtp;

public interface IEmailSender
{
    Task SendEmail(string email, string subject, string body);
}
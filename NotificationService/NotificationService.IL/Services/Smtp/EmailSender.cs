using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NotificationService.IL.Options;
using Shared.Data;

namespace NotificationService.IL.Services.Smtp;

public class EmailSender : IEmailSender
{
    private readonly ILogger<EmailSender> _logger;
    private readonly EmailOptions _emailOptions;
    private readonly SmtpClient _smtpClient;

    public EmailSender(ILogger<EmailSender> logger, IOptions<EmailOptions> options)
    {
        _logger = logger;
        _emailOptions = options.Value;

        _smtpClient = new SmtpClient(_emailOptions.Server)
        {
            Port = _emailOptions.Port,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
        };

        _smtpClient.Credentials = new System.Net.NetworkCredential(_emailOptions.Email, _emailOptions.Password);
    }

    private MailMessage MailMessageServer(string from, string displayName, string to, string subject, string body)
    {
        var message = new MailMessage()
        {
            From = new MailAddress(from, displayName,Encoding.UTF8),
        };
        
        message.To.Add(new MailAddress(to));
        message.Subject = subject;
        message.Body = body;
        message.BodyEncoding = Encoding.UTF8;
        message.IsBodyHtml = true;
        message.Headers.Add("Mail", "Langtice App Mail");
        message.Priority = MailPriority.High;
        message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        return message;
    }

    public async Task SendEmail(string email, MessageType type, string body,  params object[] parameters)
    {
        var subjectText = string.Format(EmailSubjectTemplates.Templates[type].MessageSubject, parameters);
        
       var msg = MailMessageServer(_emailOptions.Email, _emailOptions.SenderName, email, subjectText, body );
       
       await _smtpClient.SendMailAsync(msg);
    }
}
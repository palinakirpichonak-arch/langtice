using System.Text.Json;
using MainService.DAL.Models.UserFlashCardModel;
using MainService.PL.Services.gRPC;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Shared.Data;
using Shared.Dto;
using Shared.Options;

namespace MainService.BLL.Services.RabbitMq;

public class MessageSender : IMessageSender
{
    private readonly ILogger<MessageSender> _logger;
    private readonly RabbitMqOptions _rabbitMqOptions;
    private readonly IGrpcClient _grpcClient;
    
    public MessageSender(
        ILogger<MessageSender> logger,
        IOptions<RabbitMqOptions> rabbitMqOptions,
        IGrpcClient grpcClient)
    {
        _logger = logger;
        _rabbitMqOptions = rabbitMqOptions.Value;
        _grpcClient = grpcClient;
    }

    public async Task SendExpirationNotificationAsync(
        UserFlashCard card,
        TimeSpan remaining,
        IChannel channel,
        CancellationToken cancellationToken)
    {
        if (channel == null)
        {
            _logger.LogError("RabbitMQ channel is null, cannot send notification.");
            return;
        }
        try
        {
            var email = await _grpcClient.SendMessage(card.UserId.ToString(), cancellationToken);

            if (string.IsNullOrWhiteSpace(email))
            {
                _logger.LogWarning(
                    "Got empty email for user {UserId}. Skipping notification for flashcard {CardId}",
                    card.UserId, card.Id);
                return;
            }

            var msg = new Message
            {
                UserId = card.UserId.ToString(),
                Email = email,
                MessageBody = EmailSubjectTemplates.Templates[MessageType.Expiration].MessageBody
            };

            var body = JsonSerializer.SerializeToUtf8Bytes(msg);

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: _rabbitMqOptions.Queue,
                body: body,
                cancellationToken: cancellationToken);

            _logger.LogInformation(
                "Sent remaining-time notification for flashcards {Id} to user {UserId}-{UserEmail}. Left: {Remaining}s",
                card.Id, card.UserId, msg.Email, (long)remaining.TotalSeconds);
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("RabbitMQ channel was canceled.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error while sending notification for flashcard {CardId} (UserId={UserId})",
                card.Id, card.UserId);
        }
    }
}
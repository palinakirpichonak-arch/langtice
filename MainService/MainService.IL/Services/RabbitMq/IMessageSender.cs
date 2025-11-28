using MainService.DAL.Models.UserFlashCardModel;
using RabbitMQ.Client;

namespace MainService.BLL.Services.RabbitMq;

public interface IMessageSender
{
        Task SendExpirationNotificationAsync(
            UserFlashCard card,
            TimeSpan remaining,
            IChannel channel,
            CancellationToken cancellationToken);
}
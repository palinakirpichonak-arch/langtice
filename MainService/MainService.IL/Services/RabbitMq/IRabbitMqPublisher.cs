using Shared.Dto;

namespace MainService.BLL.Services.RabbitMq;

public interface IRabbitMqPublisher
{
    Task PublishAsync(Message message, CancellationToken cancellationToken = default);
}
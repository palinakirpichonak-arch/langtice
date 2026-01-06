using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NotificationService.IL.Services.Smtp;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Data;
using Shared.Dto;
using Shared.Options;

namespace NotificationService.IL.Services.RabbitMq
{
    public class RabbitConsumerService : BackgroundService
    {
        private readonly ILogger<RabbitConsumerService> _logger;
        private readonly RabbitMqOptions _rabbitMqOptions;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private IConnection? _connection;
        private IChannel? _channel;

        public RabbitConsumerService(
            ILogger<RabbitConsumerService> logger,
            IOptions<RabbitMqOptions> rabbitMqOptions,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _rabbitMqOptions = rabbitMqOptions.Value;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _rabbitMqOptions.HostName,
                    Port = _rabbitMqOptions.Port,
                    UserName = _rabbitMqOptions.UserName,
                    Password = _rabbitMqOptions.Password,
                };

                _connection = await factory.CreateConnectionAsync(cancellationToken);
                _channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

                await _channel.QueueDeclareAsync(
                    queue: _rabbitMqOptions.Queue,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null,
                    cancellationToken: cancellationToken);

                await base.StartAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize RabbitMQ consumer");
                throw;
            }
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            if (_channel == null)
            {
                _logger.LogError("RabbitMQ channel is not initialized.");
                return;
            }

            _logger.LogInformation(" [*] Waiting for messages.");

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (_, ea) =>
            {
                try
                {
                    var bodyBytes = ea.Body.ToArray();
                    if (bodyBytes.Length == 0)
                    {
                        _logger.LogWarning("Received empty message body.");
                        return;
                    }

                    var messageString = Encoding.UTF8.GetString(bodyBytes);
                    _logger.LogInformation(" [x] Received raw message: {Message}", messageString);
                    
                    Message? fullEmail;
                    try
                    {
                        fullEmail = JsonSerializer.Deserialize<Message>(messageString);
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError(jsonEx, "Failed to deserialize message JSON: {Json}", messageString);
                        return;
                    }

                    if (fullEmail == null)
                    {
                        _logger.LogWarning("Deserialized message is null. Raw: {Json}", messageString);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(fullEmail.Email))
                    {
                        _logger.LogWarning("Message is missing email. Message: {Json}", messageString);
                        return;
                    }

                    using var scope = _serviceScopeFactory.CreateScope();

                    var emailSender = scope.ServiceProvider.GetService<IEmailSender>();
                    if (emailSender == null)
                    {
                        _logger.LogError("IEmailSender dependency is missing from DI container.");
                        return;
                    }

                    try
                    {
                        await emailSender.SendEmail(
                            fullEmail.Email,
                            MessageNotificationType.ExpirationNotification,
                            fullEmail.MessageBody ?? string.Empty
                        );
                    }
                    catch (Exception emailEx)
                    {
                        _logger.LogError(emailEx,
                            "Failed to send email to {Email}. Message: {Message}",
                            fullEmail.Email,
                            fullEmail.MessageBody);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unknown error while processing message");
                }
            };

            try
            {
                await _channel.BasicConsumeAsync(
                    _rabbitMqOptions.Queue,
                    autoAck: true,
                    consumer,
                    cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to start RabbitMQ consumer");
            }
            
            try
            {
                await Task.Delay(Timeout.Infinite, cancellationToken);
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogWarning(ex, "Task canceled");
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("RabbitMQ consumer stopping...");

            try
            {
                if (_channel != null)
                    await _channel.CloseAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error closing RabbitMQ channel");
            }

            try
            {
                if (_connection != null)
                    await _connection.CloseAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error closing RabbitMQ connection");
            }

            await base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            try
            {
                _channel?.Dispose();
                _connection?.Dispose();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error disposing RabbitMQ resources");
            }

            base.Dispose();
        }
    }
}

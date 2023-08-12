using Azure.Messaging.ServiceBus;
using EDG.LoyaltyGames.Core.Entites.ServiceBus;
using EDG.LoyaltyGames.Core.Interfaces.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace EDG.LoyaltyGames.Infrastructure.ServiceBus
{
    public class GameServiceBusClient : IGameServiceBusClient
    {
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ILogger<GameServiceBusClient> _logger;       
        private readonly int MessageTTLDays;
        public GameServiceBusClient(ILogger<GameServiceBusClient> logger, ServiceBusClient serviceBusClient, IOptions<ServiceBusSetting> options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceBusClient = serviceBusClient ?? throw new ArgumentNullException(nameof(_serviceBusClient));            
            MessageTTLDays = options.Value.MessageTTLDays;

        }
        public async Task SendAsync<T>(T queueMessage, string queueName)
        {
            try
            {
                var messageSender = _serviceBusClient.CreateSender(queueName);
                var messageBody = JsonSerializer.Serialize(queueMessage);
                var sbMessage = new ServiceBusMessage(messageBody);

                sbMessage.TimeToLive = TimeSpan.FromDays(MessageTTLDays);
                sbMessage.MessageId = Guid.NewGuid().ToString();

                if (messageSender != null)
                {                    
                    await messageSender.SendMessageAsync(sbMessage);
                }
                else
                {
                    _logger.LogError("Servicebus Sender is not able to create.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            
        }
    }
}

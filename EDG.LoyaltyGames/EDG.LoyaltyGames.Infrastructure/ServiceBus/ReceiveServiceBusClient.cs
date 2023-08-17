using Azure.Messaging.ServiceBus;
using EDG.LoyaltyGames.Core.Entites.ServiceBus;
using EDG.LoyaltyGames.Core.Interfaces.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDG.LoyaltyGames.Infrastructure.ServiceBus
{
    public class ReceiveServiceBusClient : IReceiveServiceBusClient
    {
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ILogger<ReceiveServiceBusClient> _logger;        
        
        public ReceiveServiceBusClient(ILogger<ReceiveServiceBusClient> logger, ServiceBusClient serviceBusClient)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceBusClient = serviceBusClient ?? throw new ArgumentNullException(nameof(_serviceBusClient));                       
        }
        public async Task<T> ReceiveAsync<T>(string queueName)
        {
            try
            {
                _logger.LogInformation("Receiving messages...");

                // Receive and process messages in a loop
                while (true)
                {
                    var receiverClient = _serviceBusClient.CreateReceiver(queueName);
                    var message = await receiverClient.ReceiveMessageAsync();

                    if (message != null)
                    {
                        string messageBody = Encoding.UTF8.GetString(message.Body);
                        Console.WriteLine($"Received message: SequenceNumber={message.SequenceNumber} Body={messageBody}");

                        // Process the message here

                        await receiverClient.CompleteMessageAsync(message);
                        return JsonConvert.DeserializeObject<T>(messageBody);
                    }
                    else
                    {
                        // No more messages in the queue, break the loop
                        return default;
                    }
                }

                Console.WriteLine("No more messages to receive.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return default;
            }
        }
        
    }
}

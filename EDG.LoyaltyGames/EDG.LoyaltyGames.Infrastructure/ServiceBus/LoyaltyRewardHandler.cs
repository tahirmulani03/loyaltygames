﻿using Azure.Messaging.ServiceBus;
using EDG.LoyaltyGames.Core.Entites.ServiceBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace EDG.LoyaltyGames.Infrastructure.ServiceBus
{
    public class LoyaltyRewardHandler : BackgroundService
    {
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ILogger<LoyaltyRewardHandler> _logger;
        private readonly ServiceBusSetting _serviceBusSetting;

        public LoyaltyRewardHandler(ILogger<LoyaltyRewardHandler> logger, ServiceBusClient serviceBusClient, IOptions<ServiceBusSetting> options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceBusClient = serviceBusClient ?? throw new ArgumentNullException(nameof(_serviceBusClient));
            _serviceBusSetting = options.Value;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var options = new ServiceBusProcessorOptions
            {
                AutoCompleteMessages = false,
                MaxConcurrentCalls = _serviceBusSetting.MaxConcurrentCalls
            };
            var processor = _serviceBusClient.CreateProcessor(_serviceBusSetting.RewardTopicName, _serviceBusSetting.RewardSubscription, options);
            try
            {
                processor.ProcessMessageAsync += MessageHandler;
                processor.ProcessErrorAsync += ErrorHandler;
                await processor.StartProcessingAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            _logger.LogError(args.Exception.Message);
            return Task.CompletedTask;
        }

        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            _logger.LogInformation($"{nameof(LoyaltyRewardHandler)} : Getting Messages from Topic: {_serviceBusSetting.RewardTopicName} and Subscription:{_serviceBusSetting.RewardSubscription}.");
            var messageBody = Encoding.UTF8.GetString(args.Message.Body);
            var loyaltyReward = JsonConvert.DeserializeObject<LoyaltyRewardsModelV1>(messageBody);

            // We will remove message from service bus once sucessfully received and to avoid duplicate porcessing of same data.
            await args.CompleteMessageAsync(args.Message);
        }
    }
}

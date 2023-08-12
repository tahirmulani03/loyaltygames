using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDG.LoyaltyGames.Core.Interfaces.ServiceBus
{
    public interface IGameServiceBusClient
    {
        Task SendAsync<T>(T queueMessage, string queueName);        

    }
}

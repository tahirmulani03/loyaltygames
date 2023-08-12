﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDG.LoyaltyGames.Core.Interfaces.ServiceBus
{
    public interface IReceiveServiceBusClient
    {
        Task<T> ReceiveAsync<T>(string queueName);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDG.LoyaltyGames.Core.Interfaces
{
    public interface IKeyVaultManager
    {
        public Task<string> GetSecret(string secretName);
    }
}

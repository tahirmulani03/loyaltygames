﻿using EDG.LoyaltyGames.Core.Entites.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDG.LoyaltyGames.Core.Interfaces.Games
{
    public interface IGameRepository
    {
        Task<IReadOnlyList<GameEntity>> GetAllAsync();
        Task CreateAsync(GameRequest gameEntity);
        Task<GameEntity> GetByIdAsync(int gameId);
        Task<bool> UpdateAsync(GameEntity gameEntity);
        Task<bool> DeleteAsync(GameEntity gameEntity);
        Task UpdateScoreAsync(GameScoreRequest gameScoreRequest);

    }
}

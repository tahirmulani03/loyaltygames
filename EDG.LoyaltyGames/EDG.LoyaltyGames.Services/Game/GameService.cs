using AutoMapper;
using EDG.LoyaltyGames.Core.Entites;
using EDG.LoyaltyGames.Core.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDG.LoyaltyGames.Services.Game
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        public GameService(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        public async Task CreateAsync(GameRequest gameRequest)
        {
            try
            {
                //var gameEntity = _mapper.Map<GameEntity>(gameRequest);

                await _gameRepository.CreateAsync(gameRequest);               
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<bool> DeleteAsync(ObjectId gameId)
        {
            throw new NotImplementedException();
        }

        public Task<GameEntity> GetGameAsync(ObjectId gameId)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<GameEntity>> GetGamesAsync()
        {
            try
            {
                var games= await _gameRepository.GetAllAsync();
                return games;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<bool> UpdateAsync(GameEntity gameEntity)
        {
            throw new NotImplementedException();
        }

        public Task<GameEntity> UpdateScoreAsync(GameEntity gameEntity)
        {
            throw new NotImplementedException();
        }
    }
}

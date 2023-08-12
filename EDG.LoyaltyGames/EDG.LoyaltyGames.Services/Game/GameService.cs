using AutoMapper;
using EDG.LoyaltyGames.Core.Entites.Games;
using EDG.LoyaltyGames.Core.Interfaces.Games;
using EDG.LoyaltyGames.Infrastructure.SignalR;
using EDG.LoyaltyGames.Infrastructure.TimerManager;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace EDG.LoyaltyGames.Services.Game
{
    public class GameService : IGameService
    {
        private readonly ILogger<GameService> _logger;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<GameHub> _hubContext;
        private readonly TelemetryClient _telemetryClient;
        public GameService(IGameRepository gameRepository, IMapper mapper, ILogger<GameService> logger, IHubContext<GameHub> hubContext, TelemetryClient telemetryClient)
        {
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
            _telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));
        }

        public async Task CreateAsync(GameRequest gameRequest)
        {
            var gameProperties = new Dictionary<string, string>
                {
                    { "GameName", gameRequest.GameName },
                    { "BrandName", gameRequest.BrandName }
                };
            try
            {
                _logger.LogInformation($"{nameof(CreateAsync)}");
                _telemetryClient.TrackEvent($"{nameof(GameService)} : CreateAsync", gameProperties);

                await _gameRepository.CreateAsync(gameRequest);               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _telemetryClient.TrackException(ex, gameProperties);
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

        public async Task UpdateScoreAsync(GameScoreRequest gameScoreRequest)
        {
            try
            {
                //var timerManager = new TimerManager(() =>_hubContext.Clients.All.SendAsync("ScoreUpdate", "Your are Top scorer in this game."));
                if (gameScoreRequest.GameScore > 100 && gameScoreRequest.GameLevel > 1)
                {
                    await _hubContext.Clients.All.SendAsync("ScoreUpdate", "Your are Top scorer in this game.");
                }

                _logger.LogInformation($"{nameof(UpdateScoreAsync)}");
                await _gameRepository.UpdateScoreAsync(gameScoreRequest);

                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}

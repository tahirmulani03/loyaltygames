using EDG.LoyaltyGames.Core.Entites;
using AutoMapper;

namespace EDG.LoyaltyGames.Core.Mapper
{
    public class DTOMapper : Profile
    {
        public DTOMapper() {
            var mappingConfig = new MapperConfiguration(cfg=>
            {
                cfg.CreateMap<GameRequest, GameEntity>()
                .ForMember(des => des.GameName, opt =>
                opt.MapFrom(src => src.GameName));
            });
            CreateMap<GameRequest,GameEntity>();

        }
    }
}

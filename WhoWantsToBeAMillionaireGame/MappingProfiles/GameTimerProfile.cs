using AutoMapper;
using WhoWantsToBeAMillionaireGame.Core.DataTransferObjects;
using WhoWantsToBeAMillionaireGame.DataBase.Entities;

namespace WhoWantsToBeAMillionaireGame.MappingProfiles
{
    public class GameTimerProfile:Profile
    {
        public GameTimerProfile()
        {
            CreateMap<GameTimer, GameTimerDto>();
            CreateMap<GameTimerDto, GameTimer>();
        }
    }
}

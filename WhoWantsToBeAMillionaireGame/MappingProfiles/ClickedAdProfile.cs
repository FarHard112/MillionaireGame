using AutoMapper;
using WhoWantsToBeAMillionaireGame.Core.DataTransferObjects;
using WhoWantsToBeAMillionaireGame.DataBase.Entities;

namespace WhoWantsToBeAMillionaireGame.MappingProfiles
{
    public class ClickedAdProfile : Profile
    {
        public ClickedAdProfile()
        {

            CreateMap<ClickedAd, ClickedAdDto>();
            CreateMap<ClickedAdDto, ClickedAd>();
        }
    }
}

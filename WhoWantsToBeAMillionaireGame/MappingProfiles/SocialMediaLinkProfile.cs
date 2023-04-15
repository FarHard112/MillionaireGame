using AutoMapper;
using WhoWantsToBeAMillionaireGame.Core.DataTransferObjects;
using WhoWantsToBeAMillionaireGame.DataBase.Entities;

namespace WhoWantsToBeAMillionaireGame.MappingProfiles
{
    public class SocialMediaLinkProfile:Profile
    {
        public SocialMediaLinkProfile()
        {
            CreateMap<SocialMediaLink, SocialMediaLinkDto>();
            CreateMap<SocialMediaLinkDto, SocialMediaLink>();
        }
    }
}

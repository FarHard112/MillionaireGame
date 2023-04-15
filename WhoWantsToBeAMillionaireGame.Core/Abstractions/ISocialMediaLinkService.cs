using WhoWantsToBeAMillionaireGame.Core.DataTransferObjects;

namespace WhoWantsToBeAMillionaireGame.Core.Abstractions
{
    public interface ISocialMediaLinkService
    {
        Task<SocialMediaLinkDto> GetSocialMediaLink();
        Task<int> UpdateSocialMediaLink(SocialMediaLinkDto socaiMediaLinkDto);
    }
}

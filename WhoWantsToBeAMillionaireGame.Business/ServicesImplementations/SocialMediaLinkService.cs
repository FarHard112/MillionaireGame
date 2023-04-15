using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WhoWantsToBeAMillionaireGame.Core.Abstractions;
using WhoWantsToBeAMillionaireGame.Core.DataTransferObjects;
using WhoWantsToBeAMillionaireGame.Data.Abstractions;
using WhoWantsToBeAMillionaireGame.DataBase.Entities;

namespace WhoWantsToBeAMillionaireGame.Business.ServicesImplementations
{
    public class SocialMediaLinkService : ISocialMediaLinkService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SocialMediaLinkService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<SocialMediaLinkDto> GetSocialMediaLink()
        {
            var media = await _unitOfWork
                .SocialMediaLink
                .Get()
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (media == null)
            {
                return new SocialMediaLinkDto();
            }

            return _mapper.Map<SocialMediaLinkDto>(media);

        }

        public async Task<int> UpdateSocialMediaLink(SocialMediaLinkDto socaiMediaLinkDto)
        {
            if (socaiMediaLinkDto is null) throw new ArgumentNullException(nameof(SocialMediaLinkDto));
            await _unitOfWork.SocialMediaLink.UpdateAsync(_mapper.Map<SocialMediaLink>(socaiMediaLinkDto));
            var result = await _unitOfWork.Commit();
            return result;
        }
    }
}

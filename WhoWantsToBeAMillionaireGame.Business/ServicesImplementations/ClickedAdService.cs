using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using WhoWantsToBeAMillionaireGame.Core.Abstractions;
using WhoWantsToBeAMillionaireGame.Core.DataTransferObjects;
using WhoWantsToBeAMillionaireGame.Data.Abstractions;
using WhoWantsToBeAMillionaireGame.DataBase;
using WhoWantsToBeAMillionaireGame.DataBase.Entities;

namespace WhoWantsToBeAMillionaireGame.Business.ServicesImplementations
{
    public class ClickedAdService : IClickedAdService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAdvertiseService _advertiseService;
        private readonly WhoWantsToBeAMillionaireGameDbContext _dbContext;

        public ClickedAdService(IUnitOfWork unitOfWork, IMapper mapper, IAdvertiseService advertiseService, WhoWantsToBeAMillionaireGameDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _advertiseService = advertiseService;
            _dbContext = dbContext;
        }

        public async Task<ClickedAdDto> GetClickedAdByIdAsync(Guid Id)
        {
            var entity = await _unitOfWork.ClickedAd.GetByIdAsync(Id);

            return _mapper.Map<ClickedAdDto>(entity);
        }

        public async Task<int> RegisterAdClick(Guid Id, string ipAdress)
        {
            var advertisement = await _advertiseService.GetAdvertiseByIdAsync(Id);


            if (advertisement == null) throw new ArgumentException("Advertisement not found !", nameof(advertisement));

            if (advertisement.Clicks == null) advertisement.Clicks = 0;
            var currentDate = DateTime.UtcNow.Date;

            var existingClick = await _dbContext.ClickedAds
                .FirstOrDefaultAsync(x => x.AdvertisementId == Id && x.IPAddress == ipAdress && x.Date == currentDate);

            if (existingClick == null)
            {
                advertisement.Clicks += 1;
                await _advertiseService.UpdateAdvertiseAsync(advertisement);

                // Add a new click record
                var newClick = new ClickedAd()
                {
                    AdvertisementId = Id,
                    IPAddress = ipAdress,
                    Date = currentDate,
                    Id = Guid.NewGuid()
                };
                await _unitOfWork.ClickedAd.AddAsync(newClick);

            }

            var result = await _unitOfWork.Commit();
            return result;

        }
    }
}

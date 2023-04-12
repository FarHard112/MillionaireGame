using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WhoWantsToBeAMillionaireGame.Core.Abstractions;
using WhoWantsToBeAMillionaireGame.Core.DataTransferObjects;
using WhoWantsToBeAMillionaireGame.Data.Abstractions;
using WhoWantsToBeAMillionaireGame.DataBase.Entities;

namespace WhoWantsToBeAMillionaireGame.Business.ServicesImplementations
{
    public class AdvertiseService : IAdvertiseService
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        public AdvertiseService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateAdvertiseAsync(AdvertiseDto adDto)
        {
            var entity = _mapper.Map<Advertise>(adDto);
            if (entity == null) { throw new ArgumentNullException(nameof(adDto)); }

            await _unitOfWork.Advertise.AddAsync(entity);
            return await _unitOfWork.Commit();

        }

        public async Task<int> DeleteAdvertiseAsync(Guid id)
        {

            var entity = await _unitOfWork.Advertise
                .Get()
                .FirstOrDefaultAsync(ad => ad.Id.Equals(id));
            if (entity != null)
            {
                _unitOfWork.Advertise.Remove(entity);
                return await _unitOfWork.Commit();
            }
            throw new ArgumentException(nameof(id));
        }

        public async Task<AdvertiseDto> GetAdvertiseByIdAsync(Guid Id)
        {
            var entity = await _unitOfWork.Advertise.GetByIdAsync(Id);
            var result = _mapper.Map<AdvertiseDto>(entity);
            return result;
        }

        public async Task<List<AdvertiseDto>> GetAllAdvertisesAsync()
        {
            return await _unitOfWork
             .Advertise
             .Get()
             .AsNoTracking()
             .Select(ad => _mapper.Map<AdvertiseDto>(ad))
             .ToListAsync();
        }

        public async Task<int> IncrementImpressionsAsync(Guid Id)
        {
            var advertisement = await _unitOfWork.Advertise.GetByIdAsync(Id);
            if (advertisement != null)
            {
                if (advertisement.Impressions == null) advertisement.Impressions = 0;
                advertisement.Impressions += 1;
                await _unitOfWork.Advertise.UpdateAsync(advertisement);
                var result = await _unitOfWork.Commit();
                return result;
            }
            throw new ArgumentNullException(nameof(Id));
        }

        public async Task<int> UpdateAdvertiseAsync(AdvertiseDto adDto)
        {
            var entity = _mapper.Map<Advertise>(adDto);
            if (entity == null) { throw new ArgumentNullException(nameof(adDto)); }

            await _unitOfWork.Advertise.UpdateAsync(entity);
            var result = await _unitOfWork.Commit();
            return result;
        }


    }
}

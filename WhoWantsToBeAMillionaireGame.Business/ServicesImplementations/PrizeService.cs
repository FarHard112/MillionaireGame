using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WhoWantsToBeAMillionaireGame.Core.Abstractions;
using WhoWantsToBeAMillionaireGame.Core.DataTransferObjects;
using WhoWantsToBeAMillionaireGame.Data.Abstractions;
using WhoWantsToBeAMillionaireGame.DataBase.Entities;

namespace WhoWantsToBeAMillionaireGame.Business.ServicesImplementations;

public class PrizeService : IPrizeService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public PrizeService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<List<PrizeComboBoxDto>> GetAllPrizesAsync()
    {
        return await _unitOfWork
            .GamePrize
            .Get()
            .AsNoTracking()
            .Select(prize => _mapper.Map<PrizeComboBoxDto>(prize))
            .ToListAsync();
    }

    public async Task<int> CreatePrizeAsync(PrizeDto prizeDto)
    {
        var entity = _mapper.Map<Prize>(prizeDto);
        if (entity == null)
            throw new ArgumentException("Prize cannot be null ", nameof(prizeDto));

        await _unitOfWork.GamePrize.AddAsync(entity);
        var result = await _unitOfWork.Commit();
        return result;

    }

    public async Task<int> ChangeAvailabilityAsync(Guid id, bool newValue)
    {
        throw new NotImplementedException();
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        var entity = await _unitOfWork.GamePrize
            .Get()
            .FirstOrDefaultAsync(prize => prize.Id.Equals(id));
        if (entity != null)
        {
            _unitOfWork.GamePrize.Remove(entity);
            return await _unitOfWork.Commit();
        }
        throw  new ArgumentException(nameof(id));
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WhoWantsToBeAMillionaireGame.Core.Abstractions;
using WhoWantsToBeAMillionaireGame.Core.DataTransferObjects;
using WhoWantsToBeAMillionaireGame.Data.Abstractions;
using WhoWantsToBeAMillionaireGame.DataBase.Entities;

namespace WhoWantsToBeAMillionaireGame.Business.ServicesImplementations;

public class PrizeColorService : IColorPrizeService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public PrizeColorService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<PrizeColorDto>> GetAllColorsAsync()
    {
        return await _unitOfWork
            .ColorPrize
            .Get()
            .AsNoTracking()
            .Select(color => _mapper.Map<PrizeColorDto>(color))
            .ToListAsync();

    }

    public async Task<int> CreateColorAsync(PrizeColorDto colorDto)
    {
        var entity = _mapper.Map<ColorPrize>(colorDto);
        if (entity == null) { throw new ArgumentNullException(nameof(colorDto)); }

        await _unitOfWork.ColorPrize.AddAsync(entity);
        var result = await _unitOfWork.Commit();
        return result;
    }

    public async Task<PrizeColorDto> GetColorByIdAsync(Guid colorId)
    {
        var entity = await _unitOfWork.ColorPrize
            .Get()
            .FirstOrDefaultAsync(entity => entity.Id.Equals(colorId));
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        var dto = _mapper.Map<PrizeColorDto>(entity);

        return dto;
    }
}
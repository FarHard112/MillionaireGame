using WhoWantsToBeAMillionaireGame.Core.DataTransferObjects;

namespace WhoWantsToBeAMillionaireGame.Core.Abstractions;

public interface IColorPrizeService
{
    Task<List<PrizeColorDto>> GetAllColorsAsync();
    Task<int> CreateColorAsync(PrizeColorDto colorDto);
    Task<PrizeColorDto> GetColorByIdAsync(Guid colorId);

}
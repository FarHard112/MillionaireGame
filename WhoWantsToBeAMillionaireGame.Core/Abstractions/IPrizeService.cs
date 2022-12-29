using WhoWantsToBeAMillionaireGame.Core.DataTransferObjects;

namespace WhoWantsToBeAMillionaireGame.Core.Abstractions;

public interface IPrizeService
{
    Task<List<PrizeComboBoxDto>> GetAllPrizesAsync();
    Task<int> CreatePrizeAsync(PrizeDto prizeDto);
    Task<int> ChangeAvailabilityAsync(Guid id, bool newValue);
    Task<int> DeleteAsync(Guid id);
}
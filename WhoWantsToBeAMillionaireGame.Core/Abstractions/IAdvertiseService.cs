using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoWantsToBeAMillionaireGame.Core.DataTransferObjects;

namespace WhoWantsToBeAMillionaireGame.Core.Abstractions
{
    public interface IAdvertiseService
    {
        Task<List<AdvertiseDto>> GetAllAdvertisesAsync();
        Task<int> CreateAdvertiseAsync(AdvertiseDto adDto);
        Task<int> DeleteAdvertiseAsync(Guid id);
        Task<int> UpdateAdvertiseAsync(AdvertiseDto adDto);
        Task<AdvertiseDto> GetAdvertiseByIdAsync(Guid Id);
        Task<int> IncrementImpressionsAsync(Guid Id);
    }
}

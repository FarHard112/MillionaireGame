using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoWantsToBeAMillionaireGame.Core.DataTransferObjects;

namespace WhoWantsToBeAMillionaireGame.Core.Abstractions
{
    public interface IClickedAdService
    {
        Task<int> RegisterAdClick(Guid Id, string ipAdress);
        Task<ClickedAdDto> GetClickedAdByIdAsync(Guid Id);
    }
}

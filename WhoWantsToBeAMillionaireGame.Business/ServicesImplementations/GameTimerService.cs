using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WhoWantsToBeAMillionaireGame.Core.Abstractions;
using WhoWantsToBeAMillionaireGame.Core.DataTransferObjects;
using WhoWantsToBeAMillionaireGame.Data.Abstractions;
using WhoWantsToBeAMillionaireGame.DataBase.Entities;

namespace WhoWantsToBeAMillionaireGame.Business.ServicesImplementations
{
    public class GameTimerService : IGameTimer
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GameTimerService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<int> UpdateTimer(GameTimerDto timerDto)
        {
            var entity = _mapper.Map<GameTimer>(timerDto);
            if (entity == null) { throw new ArgumentNullException(nameof(timerDto)); }

            await _unitOfWork.GameTimer.UpdateAsync(entity);
            var result = await _unitOfWork.Commit();
            return result;
        }

        public async Task<GameTimerDto> GetGameTimer()
        {
            var timer = await _unitOfWork
                .GameTimer
                .Get()
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (timer == null)
            {
                return new GameTimerDto();
            }

            return _mapper.Map<GameTimerDto>(timer);
        }
    }
}

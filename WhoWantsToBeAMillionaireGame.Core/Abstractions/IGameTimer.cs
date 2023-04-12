using WhoWantsToBeAMillionaireGame.Core.DataTransferObjects;

namespace WhoWantsToBeAMillionaireGame.Core.Abstractions
{
    public interface IGameTimer
    {
        public Task<int> UpdateTimer(GameTimerDto timerDto);
        public Task<GameTimerDto> GetGameTimer();

    }
}

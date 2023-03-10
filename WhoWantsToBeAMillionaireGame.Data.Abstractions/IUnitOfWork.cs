using WhoWantsToBeAMillionaireGame.Data.Abstractions.Repositories;
using WhoWantsToBeAMillionaireGame.DataBase.Entities;

namespace WhoWantsToBeAMillionaireGame.Data.Abstractions;

public interface IUnitOfWork
{
    IRepository<Question> Question { get; }
    IRepository<Answer> Answer { get; }
    IRepository<Game> Game { get; }
    IRepository<GameQuestion> GameQuestion { get; }
    IRepository<Prize> GamePrize { get; }
    IRepository<ColorPrize> ColorPrize { get; }

    Task<int> Commit();
}
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
    IRepository<LoginUser> LoginUser { get; }
    IRepository<Advertise> Advertise { get; }
    IRepository<ClickedAd> ClickedAd { get; }
    IRepository<GameTimer> GameTimer { get; }
    IRepository<SocialMediaLink> SocialMediaLink { get; }
    Task<int> Commit();
}
using Microsoft.EntityFrameworkCore;
using WhoWantsToBeAMillionaireGame.Data.Abstractions;
using WhoWantsToBeAMillionaireGame.Data.Abstractions.Repositories;
using WhoWantsToBeAMillionaireGame.DataBase;
using WhoWantsToBeAMillionaireGame.DataBase.Entities;

namespace WhoWantsToBeAMillionaireGame.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly WhoWantsToBeAMillionaireGameDbContext _dbContext;
    public IRepository<Question> Question { get; }
    public IRepository<Answer> Answer { get; }
    public IRepository<Game> Game { get; }
    public IRepository<GameQuestion> GameQuestion { get; }
    public IRepository<Prize> GamePrize { get; }
    public IRepository<ColorPrize> ColorPrize { get; }
    public IRepository<LoginUser> LoginUser { get; }
    public IRepository<Advertise> Advertise { get; }
    public IRepository<ClickedAd> ClickedAd { get; }
    public IRepository<GameTimer> GameTimer { get; }


    public UnitOfWork(WhoWantsToBeAMillionaireGameDbContext dbContext,
        IRepository<Question> question,
        IRepository<Answer> answer,
        IRepository<Game> game,
        IRepository<GameQuestion> gameQuestion,
        IRepository<Prize> gamePrize, IRepository<ColorPrize> colorPrize, IRepository<LoginUser> loginUser, IRepository<Advertise> advertise, IRepository<ClickedAd> clickedAd,IRepository<GameTimer>gameTimer)
    {
        _dbContext = dbContext;

        Question = question;
        Answer = answer;
        Game = game;
        GameQuestion = gameQuestion;
        GamePrize = gamePrize;
        ColorPrize = colorPrize;
        LoginUser = loginUser;
        Advertise = advertise;
        ClickedAd= clickedAd;
        GameTimer = gameTimer;
    }

    public async Task<int> Commit()
    {
        try
        {
            return await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            var innerException = ex.InnerException;
            throw new Exception($"An error occurred while saving the entity changes: {innerException.Message}", ex);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Numerics;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using WhoWantsToBeAMillionaireGame.DataBase.Entities;
using Timer = System.Threading.Timer;

namespace WhoWantsToBeAMillionaireGame.DataBase;

public class WhoWantsToBeAMillionaireGameDbContext : DbContext
{
    public DbSet<Question> Question { get; set; }
    public DbSet<Prize> Prize { get; set; }
    public DbSet<Answer> Answer { get; set; }
    public DbSet<Game> Game { get; set; }
    public DbSet<GameQuestion> GameQuestions { get; set; }
    public DbSet<ColorPrize> PrizeColors { get; set; }
    public DbSet<LoginUser> LoginUsers { get; set; }
    public DbSet<Advertise> Advertises { get; set; }
    public DbSet<ClickedAd> ClickedAds { get; set; }
    public DbSet<GameTimer> GameTimer { get; set; }
    public DbSet<SocialMediaLink> SocialMediaLinks { get; set; }
    public DbSet<Logs> Logs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Question>()
            .HasIndex(question => question.Text)
            .IsUnique();

        builder.Entity<Answer>()
            .HasIndex(answer => new
            {
                answer.Text,
                answer.QuestionId
            })
        .IsUnique();
        builder.Entity<Prize>()
            .HasIndex(prize => new { prize.Text }).IsUnique();
        builder.Entity<ColorPrize>()
            .HasIndex(color => new { color.Text }).IsUnique();

        builder.Entity<GameQuestion>()
            .HasIndex(gameQuestion => new
            {
                gameQuestion.GameId,
                gameQuestion.QuestionId
            });


        base.OnModelCreating(builder);
    }

    public WhoWantsToBeAMillionaireGameDbContext(DbContextOptions<WhoWantsToBeAMillionaireGameDbContext> options)
        : base(options)
    {
    }
}
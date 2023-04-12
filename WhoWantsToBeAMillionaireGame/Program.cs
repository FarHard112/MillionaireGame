using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Serilog;
using Serilog.Events;
using WhoWantsToBeAMillionaireGame.Business.ServicesImplementations;
using WhoWantsToBeAMillionaireGame.Core.Abstractions;
using WhoWantsToBeAMillionaireGame.Data.Abstractions;
using WhoWantsToBeAMillionaireGame.Data.Abstractions.Repositories;
using WhoWantsToBeAMillionaireGame.Data.Repositories;
using WhoWantsToBeAMillionaireGame.DataBase;
using WhoWantsToBeAMillionaireGame.DataBase.Entities;

namespace WhoWantsToBeAMillionaireGame;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((ctx, lc) => lc
            .WriteTo.Console()
            .WriteTo.File(GetPathToLogFile(),
                LogEventLevel.Information));

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("Default");
        builder.Services.AddDbContext<WhoWantsToBeAMillionaireGameDbContext>(
            optionBuilder => optionBuilder.UseSqlServer(connectionString));

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddDistributedMemoryCache();

        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(10);
            options.Cookie.Name = ".Game.Session";
            options.Cookie.IsEssential = true;
        });

        builder.Services.AddControllersWithViews();
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(x => x.LoginPath = "/admin/Auth/Login");

        // Add business services
        builder.Services.AddScoped<IQuestionService, QuestionService>();
        builder.Services.AddScoped<IAnswerService, AnswerService>();
        builder.Services.AddScoped<ISourceService, JsonFileSourceService>();
        builder.Services.AddScoped<IGameService, GameService>();
        builder.Services.AddScoped<IPrizeService, PrizeService>();
        builder.Services.AddScoped<IColorPrizeService, PrizeColorService>();
        builder.Services.AddScoped<ILoginUserService, LoginUserService>();
        builder.Services.AddScoped<IAdvertiseService, AdvertiseService>();
        builder.Services.AddScoped<IClickedAdService, ClickedAdService>();
        builder.Services.AddScoped<IGameTimer, GameTimerService>();


        // Add repositories
        builder.Services.AddScoped<IRepository<Question>, Repository<Question>>();
        builder.Services.AddScoped<IRepository<Answer>, Repository<Answer>>();
        builder.Services.AddScoped<IRepository<Game>, Repository<Game>>();
        builder.Services.AddScoped<IRepository<GameQuestion>, Repository<GameQuestion>>();
        builder.Services.AddScoped<IRepository<Prize>, Repository<Prize>>();
        builder.Services.AddScoped<IRepository<ColorPrize>, Repository<ColorPrize>>();
        builder.Services.AddScoped<IRepository<LoginUser>, Repository<LoginUser>>();
        builder.Services.AddScoped<IRepository<Advertise>, Repository<Advertise>>();
        builder.Services.AddScoped<IRepository<ClickedAd>, Repository<ClickedAd>>();
        builder.Services.AddScoped<IRepository<GameTimer>, Repository<GameTimer>>();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }



        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
                Path.Combine(app.Environment.ContentRootPath, "uploads")),
            RequestPath = "/uploads"
        });
        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();
        app.UseSession();
        app.UseMiddleware<PageLoadMiddleware>();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapAreaControllerRoute(
                    name: "AdminGame",
                    areaName: "AdminGame",
                    pattern: "admin/{controller=Auth}/{action=Login}/{id?}"
                );

            endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

        });


        app.Run();
    }

    private static string GetPathToLogFile()
    {
        var sb = new StringBuilder();
        sb.Append(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        sb.Append(@"\logs\");
        sb.Append($"{DateTime.Now:yyyyMMddhhmmss}");
        sb.Append("data.log");
        return sb.ToString();
    }
}
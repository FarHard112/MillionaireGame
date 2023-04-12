using WhoWantsToBeAMillionaireGame.Core.Abstractions;

public class PageLoadMiddleware
{
    private readonly RequestDelegate _next;

    public PageLoadMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
    {
        if (context.Request.Path.StartsWithSegments("/PageLoadEvent"))
        {
            // Create a scope to resolve the scoped service
            using var scope = serviceProvider.CreateScope();
            var advertiseService = scope.ServiceProvider.GetRequiredService<IAdvertiseService>();

            var adId = context.Request.Query["adId"].ToString();
            if (!string.IsNullOrEmpty(adId) && Guid.TryParse(adId, out var parsedAdId))
            {
                await advertiseService.IncrementImpressionsAsync(parsedAdId);
            }

            Console.WriteLine($"Ad ID: {adId}");
        }

        await _next(context);
    }
}

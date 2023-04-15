using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WhoWantsToBeAMillionaireGame.Core.Abstractions;
using WhoWantsToBeAMillionaireGame.Models;

namespace WhoWantsToBeAMillionaireGame.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISocialMediaLinkService _socialMediaLinkService;

        public HomeController(ILogger<HomeController> logger, ISocialMediaLinkService socialMediaLinkService)
        {
            _logger = logger;
            _socialMediaLinkService = socialMediaLinkService;
        }

        public async Task<IActionResult> Index()
        {
            var entity = await _socialMediaLinkService.GetSocialMediaLink();
            ViewBag.TikTokUrl = entity.TikTokUrl;
            ViewBag.InstagramUrl = entity.InstagramUrl;
            ViewBag.FacebookUrl = entity.FacebookUrl;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
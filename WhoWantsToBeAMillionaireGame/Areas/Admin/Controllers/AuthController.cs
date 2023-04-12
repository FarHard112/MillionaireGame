using Microsoft.AspNetCore.Mvc;

namespace WhoWantsToBeAMillionaireGame.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

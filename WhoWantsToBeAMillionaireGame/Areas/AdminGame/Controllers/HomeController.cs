using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WhoWantsToBeAMillionaireGame.Areas.AdminGame.Controllers
{
    [Authorize]
    [Area("AdminGame")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
       
    }
}

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WhoWantsToBeAMillionaireGame.Core.Abstractions;
using WhoWantsToBeAMillionaireGame.EmailService;

namespace WhoWantsToBeAMillionaireGame.Areas.AdminGame.Controllers
{
    [Area("AdminGame")]
    public class AuthController : Controller
    {
        private readonly ILoginUserService _userService;

        public AuthController(ILoginUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Email)
        {
            if (Email is null) return Json("Email unvani bosh ola bilmez");
            var users = await _userService.GetAllUsersAsync();
            if (users.Select(x => x.Email).Contains(Email))
            {
                TempData["email"] = Email;
                return RedirectToAction("OtpCheck", "Auth");
            }

            return Json("Email unvani bazada tapilmadi !!!");
        }

        [HttpGet]
        public IActionResult OtpCheck()
        {
            EmailService.EmailService emailService = new();
            try
            {

                string email = TempData["email"].ToString();
                if (email is null) return Redirect("Login");
                Random rnd = new Random();
                string randomNum = (rnd.Next(9999, 99999)).ToString();
                emailService.SendEmailAsync(email, "OTP Kod", "Sizin OTP kodunuz : " + randomNum);
                TempData["code"] = randomNum;
                TempData["email"] = email;
                return View();
            }
            catch (Exception e)
            {
                return Json("Sistem xetasi" + e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> OtpCheck(string randomNumber)
        {
            string Email = TempData["email"].ToString();
            if (Email is null) return Redirect("Login");
            try
            {
                if (randomNumber == TempData["code"].ToString())
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Email, Email)
                   
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, "Login");

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));
                    return RedirectToActionPermanent("Index", "Home");

                }
                return Json("Kod sehvdir !");
            }
            catch (Exception e)
            {
                return Json("Sistem xetasi" + e.Message);
            }
        }
    }
}


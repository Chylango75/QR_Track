using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QR_Track.Models;
using QRCoder;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Security.Claims;

namespace QR_Track.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly QrTrackDbContext context;

        public HomeController(ILogger<HomeController> logger, QrTrackDbContext dBContext)
        {
            _logger = logger;
            context = dBContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string pwd)
        {
            if (username == "Admin" && pwd == "123")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "usuarioDemo"),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var claimsIdentity = new ClaimsIdentity(claims, "MiCookieAuth");
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                HttpContext.SignInAsync("MiCookieAuth", new ClaimsPrincipal(claimsIdentity), authProperties).Wait();

                return RedirectToAction("Index", "QR");
            }

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("MiCookieAuth").Wait();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

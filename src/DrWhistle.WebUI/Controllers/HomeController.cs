using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using DrWhistle.Infrastructure.Identity;
using DrWhistle.WebUI.Models;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DrWhistle.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }

        public IActionResult Index()
        {
            var ti = HttpContext.GetMultiTenantContext<Tenant>()?.TenantInfo;
            var title = (ti?.Name ?? "No tenant") + " - ";

            ViewData["style"] = "navbar-light bg-light";

            if (!User.Identity.IsAuthenticated)
            {
                title += "Not Authenticated";
            }
            else
            {
                title += "Authenticated";
                ViewData["style"] = "navbar-dark bg-dark";
            }

            ViewData["Title"] = title;

            if (ti != null)
            {
                var cookieOptionsMonitor = HttpContext.RequestServices.GetService<IOptionsMonitor<CookieAuthenticationOptions>>();
                var cookieName = cookieOptionsMonitor.Get(CookieAuthenticationDefaults.AuthenticationScheme).Cookie.Name;
                ViewData["CookieName"] = cookieName;

                var schemes = HttpContext.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>();
                ViewData["ChallengeScheme"] = schemes.GetDefaultChallengeSchemeAsync().Result.Name;
            }

            return View(ti);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
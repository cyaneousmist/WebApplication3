/* using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml.Linq;
using WebApplication3.Model;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor contxt;
        private readonly IUserService _userService;
        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            contxt = httpContextAccessor;

        }

        public IActionResult Index()
        {
            contxt.HttpContext.Session.SetString("Name", Name);
            contxt.HttpContext.Session.SetString("Email", Email);
            contxt.HttpContext.Session.SetString("PhoneNumber", PhoneNo);
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

    }
}
*/
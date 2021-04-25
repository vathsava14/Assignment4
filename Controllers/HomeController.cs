using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Assignment4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> dblogger;

        public HomeController(ILogger<HomeController> logger)
        {
            dblogger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

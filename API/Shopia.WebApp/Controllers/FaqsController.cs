using Microsoft.AspNetCore.Mvc;

namespace Shopia.WebApp.Controllers
{
    public class FaqsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

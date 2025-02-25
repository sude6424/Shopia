using Microsoft.AspNetCore.Mvc;

namespace Shopia.WebApp.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

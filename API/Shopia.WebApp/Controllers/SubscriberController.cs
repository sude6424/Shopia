using Microsoft.AspNetCore.Mvc;
using Shopia.Application.DTO_s.SubscriberDTO_s;
using Shopia.Application.Services.SubscriberServices;

namespace Shopia.WebApp.Controllers
{
    public class SubscriberController : Controller
    {
        private readonly ISubscriberServices _Services;

        public SubscriberController(ISubscriberServices services)
        {
            _Services = services;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSubscriberDTO subscriber)
        {
            subscriber.SubscriberDate = DateTime.Now;
            await _Services.CreateSubsricer(subscriber);
            return RedirectToAction("Index", "Home");
        }

    }
}

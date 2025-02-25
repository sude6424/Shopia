using Microsoft.AspNetCore.Mvc;
using Shopia.Application.DTO_s.HelpDTO_s;
using Shopia.Application.Services.HelpServices;

namespace Shopia.WebApp.Controllers
{
    public class HelpController : Controller
    {
        private readonly IHelpServices _services;

        public HelpController(IHelpServices services)
        {
            _services = services;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateHelp(CreateHelpDTO dto)
        {
            dto.CreatedDate = DateTime.Now;
            dto.Status = 1;
            await _services.CreateHelpAsync(dto);
            return RedirectToAction("Index");
        }
    }
}

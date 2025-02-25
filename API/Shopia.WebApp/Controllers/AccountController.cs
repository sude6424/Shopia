using Microsoft.AspNetCore.Mvc;
using Shopia.Application.DTO_s.AccountDTO_s;
using Shopia.Application.DTO_s.CustomerDTO_s;
using Shopia.Application.Services.AccountServices;
using Shopia.Application.Services.CustomerServices;
using Shopia.Domain.Entities;

namespace Shopia.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountServices _accountServices;
        private readonly ICustomerServices _customerServices;

        public AccountController(IAccountServices accountServices, ICustomerServices customerServices)
        {
            _accountServices = accountServices;
            _customerServices = customerServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var values = await _accountServices.Login(dto);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var values = await _accountServices.Register(dto);
            var customer = new CreateCustomerDTO
            {
                Email = dto.Email,
                Firstname = dto.Name,
                LastName = dto.Lastname,
                UserId = values,
                PhoneNumber=dto.PhoneNumber,
            };
            await _customerServices.CreateCustomerAsync(customer);
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _accountServices.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}

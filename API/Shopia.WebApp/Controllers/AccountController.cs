using Microsoft.AspNetCore.Mvc;
using Shopia.Application.DTO_s.AccountDTO_s;
using Shopia.Application.DTO_s.CustomerDTO_s;
using Shopia.Application.Interface;
using Shopia.Application.Services.AccountServices;
using Shopia.Application.Services.CustomerServices;
using Shopia.Application.Services.OrderItemServices;
using Shopia.Application.Services.OrderServices;
using Shopia.Domain.Entities;

namespace Shopia.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountServices _accountServices;
        private readonly ICustomerServices _customerServices;
        private readonly IUserIdentityRepository _userIdentityRepository;
        private readonly IOrderServices _orderServices;
        private readonly IOrderItemServices _orderItemServices;

        public AccountController(IAccountServices accountServices, ICustomerServices customerServices, IUserIdentityRepository userIdentityRepository, IOrderServices orderServices, IOrderItemServices orderItemServices)
        {
            _accountServices = accountServices;
            _customerServices = customerServices;
            _userIdentityRepository = userIdentityRepository;
            _orderServices = orderServices;
            _orderItemServices = orderItemServices;
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
        public async Task<IActionResult> Profile()
        {
            var userid = await _userIdentityRepository.GetUserIdOnAuth(User);
            var user= await _customerServices.GetCustomerByUserId(userid);
            var order= await _orderServices.GetOrderByUserId(userid);
            var result = new ResultProfileDTO
            {
                UserId = userid,
                Name=user.Firstname,
                Surname=user.LastName,
                PhoneNumber=user.PhoneNumber,
                Email=user.Email,
                Orders= order,
            };
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(string name, string surname)
        {
            var userId= await _accountServices.GetUserIdAsync(User);
            var result= await _accountServices.UpdateUser(userId, name, surname);
            await _customerServices.UpdateNameAndSurname(userId,name, surname);
            return RedirectToAction("Profile", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO model)
        {
            var userid = await _accountServices.GetUserIdAsync(User);
            model.UserId = userid;
            var result = await _accountServices.ChangePassword(model);
            return RedirectToAction("Profile", "Account");
        }
    }
}

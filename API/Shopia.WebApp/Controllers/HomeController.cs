using Microsoft.AspNetCore.Mvc;
using Shopia.Application.DTO_s.CartDTO_s;
using Shopia.Application.DTO_s.CartItemDTO_s;
using Shopia.Application.Interface;
using Shopia.Application.Services.CartServices;
using Shopia.Application.Services.ProductServices;
using Shopia.WebApp.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Shopia.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductServices _productServices;
        private readonly ICartServices _cartServices;
        private readonly IUserIdentityRepository _userIdentityRepository;

        public HomeController(ILogger<HomeController> logger, IProductServices productServices, ICartServices cartServices, IUserIdentityRepository userIdentityRepository)
        {
            _logger = logger;
            _productServices = productServices;
            _cartServices = cartServices;
            _userIdentityRepository = userIdentityRepository;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Request.Cookies.TryGetValue("cart", out string cartData))
                {
                    string cookieName = "cart";
                    CreateCartDTO cartItems = new CreateCartDTO();

                    if (Request.Cookies.TryGetValue(cookieName, out string cartData1))
                    {
                        cartItems = JsonSerializer.Deserialize<CreateCartDTO>(cartData1) ?? new CreateCartDTO();
                    }
                    cartItems.CreatedDate = DateTime.Now;
                    var userId = await _userIdentityRepository.GetUserIdOnAuth(User);
                    cartItems.UserId = userId;
                    var result = await _cartServices.CheckCartAsync(userId);
                    if (!result)
                    {
                        await _cartServices.CreateCartAsync(cartItems);

                    }
                }

                var values1 = await _productServices.GetProductTake(8);
                return View(values1);
            }
            else
            {
                string cookieName = "cart";
                if (!Request.Cookies.ContainsKey(cookieName))
                {
                    var cartItem = new CreateCartDTO();
                    cartItem.CartItems = new List<CreateCartItemDTO>();

                    var cartData = JsonSerializer.Serialize(cartItem);
                    Response.Cookies.Append(cookieName, cartData, new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(7),
                        HttpOnly = true,
                        Secure = true
                    });
                }
            }
            var values = await _productServices.GetProductTake(8);
            return View(values);
        }

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

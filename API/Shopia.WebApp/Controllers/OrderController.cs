using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shopia.Application.DTO_s.CartDTO_s;
using Shopia.Application.DTO_s.CartItemDTO_s;
using Shopia.Application.DTO_s.OrderDTO_s;
using Shopia.Application.DTO_s.OrderItemDTO_s;
using Shopia.Application.Interface;
using Shopia.Application.Services.CartItemServices;
using Shopia.Application.Services.CartServices;
using Shopia.Application.Services.CustomerServices;
using Shopia.Application.Services.OrderServices;
using Shopia.Application.Services.ProductServices;
using Shopia.Domain.Entities;
using System.Diagnostics.Eventing.Reader;
using System.Text.Json;

namespace Shopia.WebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderServices _orderservices;
        private readonly ICartServices _cartservices;
        private readonly IProductServices _productServices;
        private readonly ICartItemServices _cartitemservices;
        private readonly ICustomerServices _customerservices;
        private readonly IUserIdentityRepository _userIdentityRepository;


        public OrderController(IOrderServices services, ICartServices cartservices, IProductServices productServices, ICartItemServices cartitemservices, ICustomerServices customerservices, IUserIdentityRepository userIdentityRepository)
        {
            _orderservices = services;
            _cartservices = cartservices;
            _productServices = productServices;
            _cartitemservices = cartitemservices;
            _customerservices = customerservices;
            _userIdentityRepository = userIdentityRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Checkout(int cartId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                string cookieName = "cart";
                CreateCartDTO cartItems = new CreateCartDTO();



                if (Request.Cookies.TryGetValue(cookieName, out string cartData1))
                {
                    cartItems = JsonSerializer.Deserialize<CreateCartDTO>(cartData1) ?? new CreateCartDTO();
                }
                GetByIdCartDTO detailedCartItems = new GetByIdCartDTO();
                detailedCartItems.CartItems = new List<ResultCartItemDTO>();
                var totalsum = 0;

                foreach (var item in cartItems.CartItems)
                {
                    var detailedItem = await _productServices.GetByIdProductAsync(item.ProductId);

                    var newproduct = new Product();

                    newproduct.ProductId = detailedItem.ProductId;
                    newproduct.ProductName = detailedItem.ProductName;
                    newproduct.Price = detailedItem.Price;
                    newproduct.ImageUrl = detailedItem.ImageUrl;
                    newproduct.Description = detailedItem.Description;
                    newproduct.CategoryId = detailedItem.CategoryId;
                    newproduct.Stock = detailedItem.Stock;


                    totalsum += item.TotalPrice;

                    var newcartItem = new ResultCartItemDTO
                    {
                        Quantity = item.Quantity,
                        TotalPrice = item.TotalPrice,
                        ProductId = item.ProductId,
                        Product = newproduct,
                    };

                    detailedCartItems.CartItems.Add(newcartItem);
                }
                detailedCartItems.TotalAmount = totalsum;
                return View(detailedCartItems);
            }
            else
            {
                var value = await _cartservices.GetByIdCartAsync(cartId);
                var userid = await _userIdentityRepository.GetUserIdOnAuth(User);
                var customer = await _customerservices.GetCustomerByUserId(userid);

                value.Customer = new Customer
                {
                    CustomerId = customer.CustomerId,
                    Email = customer.Email,
                    Firstname = customer.Firstname,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                    UserId = customer.UserId,
                };

                if (value == null)
                {
                    return View();
                }
                return View(value);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDTO dto, int cartId)
        {

            try
            {
                var cart = new GetByIdCartDTO();
                if (!User.Identity.IsAuthenticated)
                {
                    string cookieName = "cart";
                    CreateCartDTO cartItems = new CreateCartDTO();

                    if (Request.Cookies.TryGetValue(cookieName, out string cartData1))
                    {
                        cartItems = JsonSerializer.Deserialize<CreateCartDTO>(cartData1) ?? new CreateCartDTO();
                    }
                    GetByIdCartDTO detailedCartItems = new GetByIdCartDTO();
                    detailedCartItems.CartItems = new List<ResultCartItemDTO>();
                    var totalsum = 0;

                    foreach (var item in cartItems.CartItems)
                    {
                        var detailedItem = await _productServices.GetByIdProductAsync(item.ProductId);

                        var newproduct = new Product();

                        newproduct.ProductId = detailedItem.ProductId;
                        newproduct.ProductName = detailedItem.ProductName;
                        newproduct.Price = detailedItem.Price;
                        newproduct.ImageUrl = detailedItem.ImageUrl;
                        newproduct.Description = detailedItem.Description;
                        newproduct.CategoryId = detailedItem.CategoryId;
                        newproduct.Stock = detailedItem.Stock;


                        totalsum += item.TotalPrice;

                        var newcartItem = new ResultCartItemDTO
                        {
                            Quantity = item.Quantity,
                            TotalPrice = item.TotalPrice,
                            ProductId = item.ProductId,
                            Product = newproduct
                        };

                        detailedCartItems.CartItems.Add(newcartItem);
                    }
                    detailedCartItems.TotalAmount = totalsum;
                    detailedCartItems.UserId = "1111111111111";
                    cart = detailedCartItems;
                }
                else
                {
                    cart = await _cartservices.GetByIdCartAsync(cartId);
                }

                List<CreateOrderItemDTO> result = new List<CreateOrderItemDTO>();
                foreach (var item in cart.CartItems)
                {
                    var newOrderItem = new CreateOrderItemDTO
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        TotalPrice = item.TotalPrice,
                    };
                    result.Add(newOrderItem);
                }

                dto.CustomerId = 14; // Örnek, gerçek değeri burada kullan
                dto.OrderItems = result;
                dto.OrderStatus = "Siparişiniz Alındı!";
                await _orderservices.CreateOrderAsync(dto);
                if (cart.CartItems != null)
                {
                    foreach (var item in cart.CartItems)
                    {
                        if (item.CartItemId != 0)
                        {
                            await _cartitemservices.DeleteCartItemAsync(item.CartItemId);

                        }
                    }
                    if (cartId != 0)
                    {
                        await _cartservices.DeleteCartAsync(cartId);
                    }
                }

                string cookieName1 = "cart";
                Response.Cookies.Delete(cookieName1);

                var emptyCart = new CreateCartDTO
                {
                    CartItems = new List<CreateCartItemDTO>()
                };

                var cartData2 = JsonSerializer.Serialize(emptyCart);
                Response.Cookies.Append(cookieName1, cartData2, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(7),
                    HttpOnly = true,
                    Secure = true
                });

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.Message);
            }
        }

        public async Task<ActionResult> GetCity()
        {
            var values = await _orderservices.GetAllCity();
            return Json(new { success = true, data = values });
        }
        public async Task<ActionResult> GetTownByCityId(int cityId)
        {
            var values = await _orderservices.GetTownByCityId(cityId);
            return Json(new { success = true, data = values });
        }
    }
}





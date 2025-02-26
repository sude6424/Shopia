using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shopia.Application.DTO_s.CartDTO_s;
using Shopia.Application.DTO_s.CartItemDTO_s;
using Shopia.Application.Interface;
using Shopia.Application.Services.CartItemServices;
using Shopia.Application.Services.CartServices;
using Shopia.Application.Services.ProductServices;
using Shopia.Domain.Entities;
using System.Text.Json;

namespace Shopia.WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartServices _cartServices;
        private readonly ICartItemServices _cartItemServices;
        private readonly IProductServices _productServices;
        private readonly IUserIdentityRepository _userIdentityRepository;



        public CartController(ICartServices cartServices, ICartItemServices cartItemServices, IProductServices productServices, IUserIdentityRepository userIdentityRepository)
        {
            _cartServices = cartServices;
            _cartItemServices = cartItemServices;
            _productServices = productServices;
            _userIdentityRepository = userIdentityRepository;
        }




        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {

                if (Request.Cookies.TryGetValue("cart", out string cartData))
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
                return View();
            }
            else
            {
                var userId = await _userIdentityRepository.GetUserIdOnAuth(User);
                var value = await _cartServices.GetByUserIdCartAsync(userId);
                return View(value);
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddToCartItem([FromBody] CreateCartItemDTO model)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    //model.CartId = 1;
                    var userid = await _userIdentityRepository.GetUserIdOnAuth(User);
                    var checkcart = await _cartServices.CheckCartAsync(userid);
                    if (checkcart)
                    {
                        var cart = await _cartServices.GetByUserIdCartAsync(userid);
                        var check = await _cartItemServices.CheckCartItem(cart.CartId, model.ProductId);

                        if (check)
                        {
                            await _cartItemServices.UpdateQuantity(cart.CartId, model.ProductId, model.Quantity);
                        }
                        else
                        {
                            model.CartId = cart.CartId;
                            await _cartItemServices.CreateCartItemAsync(model);
                        }
                        var sumprice = cart.TotalAmount + model.TotalPrice;
                        await _cartServices.UpdateTotalAmount(cart.CartId, sumprice);
                    }
                    else
                    {
                        var newcart = new CreateCartDTO { CreatedDate = DateTime.Now, UserId = userid, CartItems = new List<CreateCartItemDTO>() };
                        await _cartServices.CreateCartAsync(newcart);


                        var cart = await _cartServices.GetByUserIdCartAsync(userid);
                        var check = await _cartItemServices.CheckCartItem(model.CartId, model.ProductId);

                        if (check)
                        {
                            await _cartItemServices.UpdateQuantity(model.CartId, model.ProductId, model.Quantity);
                        }
                        else
                        {
                            model.CartId = cart.CartId;
                            await _cartItemServices.CreateCartItemAsync(model);
                        }
                        var sumprice = cart.TotalAmount + model.TotalPrice;
                        await _cartServices.UpdateTotalAmount(model.CartId, sumprice);
                    }




                }
                else
                {
                    string cookieName = "cart";
                    CreateCartDTO cartItems;

                    if (Request.Cookies.TryGetValue(cookieName, out string cartData))
                    {
                        cartItems = JsonSerializer.Deserialize<CreateCartDTO>(cartData) ?? new CreateCartDTO();
                    }
                    else
                    {
                        cartItems = new CreateCartDTO
                        {
                            CartItems = new List<CreateCartItemDTO>()
                        };
                    }

                    // Eğer `CartItems` null ise yeni bir liste ata

                    var existingItem = cartItems.CartItems.FirstOrDefault(item => item.ProductId == model.ProductId);
                    if (existingItem != null)
                    {

                        existingItem.Quantity += model.Quantity;
                        existingItem.TotalPrice += model.TotalPrice;
                    }
                    else
                    {

                        cartItems.CartItems.Add(model);
                    }


                    var updatedCartData = JsonSerializer.Serialize(cartItems);
                    Response.Cookies.Append(cookieName, updatedCartData, new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(7),
                        HttpOnly = true,
                        Secure = true
                    });
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex });
            }

        }

        [HttpGet]
        public async Task<JsonResult> DeleteCartItem(int id, int productId)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (id == 0) return Json(new { error = "Ürün bulunamadı." });

                    var cartItem = await _cartItemServices.GetByIdCartItemAsync(id);
                    if (cartItem == null) return Json(new { error = "Ürün bulunamadı." });

                    await _cartItemServices.DeleteCartItemAsync(id);
                    var cart = await _cartServices.GetByIdCartAsync(cartItem.CartId);
                    var tempCartTotal = cart.TotalAmount - cartItem.TotalPrice;
                    await _cartServices.UpdateTotalAmount(cart.CartId, tempCartTotal);
                }
                else
                {
                    string cookieName = "cart";


                    CreateCartDTO cartItems;



                    if (Request.Cookies.TryGetValue(cookieName, out string cartData))
                    {
                        // Yeni ürün ekle
                        cartItems = JsonSerializer.Deserialize<CreateCartDTO>(cartData) ?? new CreateCartDTO();

                    }
                    else
                    {
                        cartItems = new CreateCartDTO
                        {
                            CartItems = new List<CreateCartItemDTO>()
                        };
                    }

                    var existingItem = cartItems.CartItems.FirstOrDefault(item => item.ProductId == productId);
                    if (existingItem != null)
                    {
                        cartItems.CartItems.Remove(existingItem);
                    }
                    else
                    {

                        cartItems.CartItems.Add(new CreateCartItemDTO());
                    }
                    var updatedCartData = JsonSerializer.Serialize(cartItems);

                    Response.Cookies.Append(cookieName, updatedCartData, new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(7),
                        HttpOnly = true,
                        Secure = true
                    });
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex });
            }
        }
        [HttpPost]
        public async Task<ActionResult> UpdateQuantityOnCart(UpdateCartItemDTO dto)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var cart = await _cartServices.GetByIdCartAsync(dto.CartId);
                    await _cartItemServices.UpdateQuantity(dto.CartId, dto.ProductId, dto.Quantity);
                    var cartItem = await _cartItemServices.GetByIdCartItemAsync(dto.CartItemId);
                    var product = await _productServices.GetByIdProductAsync(dto.ProductId);
                    decimal sumprice = cart.TotalAmount;

                    // Ürün fiyatını ve miktarını kontrol et
                    if (cartItem.Quantity == 0)
                    {
                        await _cartItemServices.DeleteCartItemAsync(cartItem.CartItemId);
                    }




                    // Miktar geçerli mi kontrol et
                    if (dto.Quantity > 0)
                    {
                        sumprice = cart.TotalAmount - product.Price;
                    }
                    await _cartServices.UpdateTotalAmount(cart.CartId, sumprice);



                }
                else
                {
                    string cookieName = "cart";

                    CreateCartDTO cartItems;



                    if (Request.Cookies.TryGetValue(cookieName, out string cartData))
                    {
                        // Yeni ürün ekle
                        cartItems = JsonSerializer.Deserialize<CreateCartDTO>(cartData) ?? new CreateCartDTO();

                    }
                    else
                    {
                        cartItems = new CreateCartDTO
                        {
                            CartItems = new List<CreateCartItemDTO>()
                        };
                    }

                    var existingItem = cartItems.CartItems.FirstOrDefault(item => item.ProductId == dto.ProductId);
                    var cookieproduct = await _productServices.GetByIdProductAsync(dto.ProductId);
                    if (existingItem != null)
                    {
                        if (dto.Quantity > 0)
                        {
                            existingItem.Quantity += dto.Quantity;
                            existingItem.TotalPrice = Convert.ToInt32(existingItem.Quantity * cookieproduct.Price);
                        }
                        else
                        {


                            existingItem.Quantity += dto.Quantity;
                            existingItem.TotalPrice = Convert.ToInt32(existingItem.Quantity * cookieproduct.Price);
                            if (existingItem.TotalPrice == 0)
                            {
                                cartItems.CartItems.Remove(existingItem);
                            }

                        }

                    }
                    else
                    {

                        cartItems.CartItems.Add(new CreateCartItemDTO());
                    }


                    // Sepet verisini güncelle
                    var updatedCartData = JsonSerializer.Serialize(cartItems);

                    Response.Cookies.Append(cookieName, updatedCartData, new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(7),
                        HttpOnly = true,
                        Secure = true
                    });

                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex });


            }

        }
    }
}


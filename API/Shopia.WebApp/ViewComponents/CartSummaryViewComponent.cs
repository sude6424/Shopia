using Microsoft.AspNetCore.Mvc;
using Shopia.Application.DTO_s.CartDTO_s;
using Shopia.Application.Interface;
using Shopia.Application.Services.CartItemServices;
using System.Text.Json;

namespace Shopia.WebApp.ViewComponents
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly IUserIdentityRepository _userIdentityRepository;
        private readonly ICartItemServices _cartItemServices;

        public CartSummaryViewComponent(IUserIdentityRepository userIdentityRepository, ICartItemServices cartItemServices)
        {
            _userIdentityRepository = userIdentityRepository;
            _cartItemServices = cartItemServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int cartItemCount = 0;

            if (User.Identity.IsAuthenticated)
            {
                var userId = await _userIdentityRepository.GetUserIdOnAuth(UserClaimsPrincipal);
                var cartItems = await _cartItemServices.GetCountCartItemByCartId(userId);
                if (cartItems == 0)
                {
                    cartItemCount = 0;

                }
                else
                {
                    cartItemCount = cartItems;

                }
            }
            else
            {
                string cookieName = "cart";
                if (Request.Cookies.TryGetValue(cookieName, out string cartData))
                {

                    var cart = JsonSerializer.Deserialize<CreateCartDTO>(cartData);
                    if (cart != null && cart.CartItems != null)
                    {
                        cartItemCount = cart.CartItems.Sum(item => item.Quantity);
                    }
                }
            }


            return View(cartItemCount);
        }
    }
}

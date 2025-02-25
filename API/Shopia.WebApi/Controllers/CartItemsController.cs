using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopia.Application.DTO_s.CartItemDTO_s;
using Shopia.Application.Services.CartItemServices;

namespace Shopia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly ICartItemServices _cartItemServices;

        public CartItemsController(ICartItemServices cartItemServices)
        {
            _cartItemServices = cartItemServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCartItem()
        {
            var values = await _cartItemServices.GetAllCartItemAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartItemById(int id)
        {
            var values = await _cartItemServices.GetByIdCartItemAsync(id);
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCartItem(CreateCartItemDTO dto)
        {
            await _cartItemServices.CreateCartItemAsync(dto);
            return Ok("Başarılı bir şekilde sepet oluşturuldu");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCartItem(UpdateCartItemDTO dto)
        {
            await _cartItemServices.UpdateCartItemAsync(dto);
            return Ok("Başarılı bir şekilde sepet güncellendi");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            await _cartItemServices.DeleteCartItemAsync(id);
            return Ok("Başarılı bir şekilde sepet silindi");
        }
    }
}

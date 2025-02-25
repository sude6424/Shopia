using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopia.Application.DTO_s.CartDTO_s;
using Shopia.Application.Services.CartServices;

namespace Shopia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartServices _cartServices;

        public CartsController(ICartServices cartServices)
        {
            _cartServices = cartServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCart()
        {
            var values = await _cartServices.GetAllCartAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartById(int id)
        {
            var values = await _cartServices.GetByIdCartAsync(id);
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCart(CreateCartDTO dto)
        {
            await _cartServices.CreateCartAsync(dto);
            return Ok("Başarılı bir şekilde sepet oluşturuldu");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCart(UpdateCartDTO dto)
        {
            await _cartServices.UpdateCartAsync(dto);
            return Ok("Başarılı bir şekilde sepet güncellendi");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCart(int id)
        {
            await _cartServices.DeleteCartAsync(id);
            return Ok("Başarılı bir şekilde sepet silindi");
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopia.Application.DTO_s.OrderDTO_s;
using Shopia.Application.Services.OrderServices;

namespace Shopia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderServices _services;

        public OrdersController(IOrderServices services)
        {
            _services = services;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            var values = await _services.GetAllOrderAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var values = await _services.GetByIdOrderAsync(id);
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDTO dto)
        {
            await _services.CreateOrderAsync(dto);
            return Ok("Siparişiniz başarılı bir şekilde oluşturuldu");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDTO dto)
        {
            await _services.UpdateOrderAsync(dto);
            return Ok("Siparişiniz başarılı bir şekilde güncellendi");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _services.DeleteOrderAsync(id);
            return Ok("Siparişiniz başarılı bir şekilde silindi");
        }
    }
}

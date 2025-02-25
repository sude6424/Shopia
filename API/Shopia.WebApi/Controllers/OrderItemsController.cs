using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopia.Application.DTO_s.OrderItemDTO_s;
using Shopia.Application.Services.OrderItemServices;

namespace Shopia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemServices _orderItemServices; 

        public OrderItemsController(IOrderItemServices orderItemServices)
        {
            _orderItemServices = orderItemServices; 
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrderItem()
        {
            var values = await _orderItemServices.GetAllOrderItemAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdOrderItem(int id)
        {
            var values = await _orderItemServices.GetByIdOrderItemAsync(id);
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrderItem(CreateOrderItemDTO dto)
        {
            await _orderItemServices.CreateOrderItemAsync(dto);
            return Ok("Sipariş ürünü başarılı bir şekilde oluşturuldu");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrderItem(UpdateOrderItemDTO dto)
        {
            await _orderItemServices.UpdateOrderItemAsync(dto);
            return Ok("Sipariş ürünü başarılı bir şekilde güncellendi");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            await _orderItemServices.DeleteOrderItemAsync(id);
            return Ok("Sipariş ürünü başarılı bir şekilde silindi");
        }
    }
}

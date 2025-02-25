using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopia.Application.DTO_s.CustomerDTO_s;
using Shopia.Application.Services.CustomerServices;

namespace Shopia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerServices _services;

        public CustomersController(ICustomerServices services)
        {
            _services = services;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCustomer()
        {
            var values= await _services.GetAllCustomerAsync();
            return Ok(values);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCustomer(int id)
        {
            var values = await _services.GetByIdCustomerAsync(id);
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDTO dto)
        {
            await _services.CreateCustomerAsync(dto);
            return Ok("Müşteri başarılı bir şekilde oluşturuldu.");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerDTO dto)
        {
            await _services.UpdateCustomerAsync(dto);
            return Ok("Müşteri bilgileri başarılı bir şekilde güncellendi.");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _services.DeleteCustomerAsync(id);
            return Ok("Müşteri başarılı bir şekilde silindi.");
        }   
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopia.Application.DTO_s.ProductDTO_s;
using Shopia.Application.Services.ProductServices;

namespace Shopia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductsController(IProductServices productServices)
        {
            _productServices = productServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var values = await _productServices.GetAllProductAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdProduct(int id)
        {
            var values = await _productServices.GetByIdProductAsync(id);
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDTO dto)
        {
            await _productServices.CreateProductAsync(dto);
            return Ok("Ürün başarılı bir şekilde oluşturuldu");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO dto)
        {
            await _productServices.UpdateProductAsync(dto);
            return Ok("Ürün başarılı bir şekilde güncellendi");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productServices.DeleteProductAsync(id);
            return Ok("Ürün başarılı bir şekilde silindi");
        }
    }
}

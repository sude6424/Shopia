using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopia.Application.DTO_s.CategoryDTO_s;
using Shopia.Application.Services.CategoryServices;

namespace Shopia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;

        public CategoriesController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }
        [HttpGet]
        public async Task< IActionResult> GetAllCategories()
        {
            var categories = await _categoryServices.GetAllCategoryAsync();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryServices.GetByIdCategoryAsync(id);
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync(CreateCategoryDTO dto)
        {
            await _categoryServices.CreateCategoryAsync(dto);
            return Ok("Başarılı bir şekilde kategori oluşturuldu.");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO dto)
        {
            await _categoryServices.UpdateCategoryAsync(dto);
            return Ok("Başarılı bir şekilde kategori güncellendi.");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryServices.DeleteCategoryAsync(id);
            return Ok("Başarılı bir şekilde kategori silindi.");
        }
        
    }
}

using Shopia.Application.DTO_s.CategoryDTO_s;
using Shopia.Application.Interface;
using Shopia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.Services.CategoryServices
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IRepository<Category> _repository;

        public CategoryServices(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task CreateCategoryAsync(CreateCategoryDTO model)
        {
            await _repository.CreateAsync(new Category
            {
                CategoryName = model.CategoryName
            });
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(category);
        }

        public async Task<List<ResultCategoryDTO>> GetAllCategoryAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories.Select(x => new ResultCategoryDTO
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName
            }).ToList();
        }

        public async Task<GetByIdCategoryDTO> GetByIdCategoryAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            var newCategory = new GetByIdCategoryDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };
            return newCategory;
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDTO model)
        {
            var category = await _repository.GetByIdAsync(model.CategoryId);
            category.CategoryName = model.CategoryName;
            await _repository.UpdateAsync(category);
        }
    }

}

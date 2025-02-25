using Shopia.Application.DTO_s.CategoryDTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.Services.CategoryServices
{
    public interface ICategoryServices
    {
        Task<List<ResultCategoryDTO>> GetAllCategoryAsync();
        Task<GetByIdCategoryDTO> GetByIdCategoryAsync(int id);
        Task CreateCategoryAsync(CreateCategoryDTO model);
        Task UpdateCategoryAsync(UpdateCategoryDTO model);
        Task DeleteCategoryAsync(int id);
    }
}

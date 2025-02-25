using Shopia.Application.DTO_s.ProductDTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.Services.ProductServices
{
    public interface IProductServices
    {
        Task<List<ResultProductDTO>> GetAllProductAsync();
        Task<GetByIdProductDTO> GetByIdProductAsync(int id);
        Task CreateProductAsync(CreateProductDTO model);
        Task UpdateProductAsync(UpdateProductDTO model);
        Task DeleteProductAsync(int id);
        Task<List<ResultProductDTO>> GetProductTake(int sayi);
        Task<List<ResultProductDTO>> GetProductByCategory(int categoryId);
        Task<List<ResultProductDTO>> GetProductByPrice(decimal minprice, decimal maxprice);
        Task<List<ResultProductDTO>> GetProductBySearch(string search);
    }
}

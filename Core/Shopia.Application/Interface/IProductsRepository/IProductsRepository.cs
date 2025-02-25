using Shopia.Application.DTO_s.ProductDTO_s;
using Shopia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.Interface.IProductsRepository
{
    public interface IProductsRepository 
    {
        Task<List<Product>> GetProductByCategory(int categoryId);
        Task<List<Product>> GetProductByPriceFilter(decimal minprice, decimal maxprice);
        Task<List<Product>> GetProductBySearch(string search);

    }
}

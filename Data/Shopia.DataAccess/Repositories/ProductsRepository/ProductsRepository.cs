using Microsoft.EntityFrameworkCore;
using Shopia.Application.DTO_s.ProductDTO_s;
using Shopia.Application.Interface.IProductsRepository;
using Shopia.DataAccess.Context;
using Shopia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.DataAccess.Repositories.ProductsRepository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly AppDbContext _context;

        public ProductsRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<Product>> GetProductByCategory(int categoryId)
        {
            return await _context.Products.Where(x => x.CategoryId == categoryId).ToListAsync();

        }

        public async Task<List<Product>> GetProductByPriceFilter(decimal minprice, decimal maxprice)
        {
            return await _context.Products.Where(x => x.Price >= minprice && x.Price <= maxprice).ToListAsync();
        }

        public async Task<List<Product>> GetProductBySearch(string search)
        {
            return await _context.Products.Where(x => x.ProductName.Contains(search) || x.Description.Contains(search)).ToListAsync();
        }
    }
}

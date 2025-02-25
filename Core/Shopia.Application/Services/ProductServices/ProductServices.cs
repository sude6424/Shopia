using Shopia.Application.DTO_s.ProductDTO_s;
using Shopia.Application.Interface;
using Shopia.Application.Interface.IProductsRepository;
using Shopia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopia.Application.Services.ProductServices
{
    public class ProductServices : IProductServices
    {
        private readonly IRepository<Product> _repository;
        private readonly IProductsRepository _productsRepository;

        public ProductServices(IRepository<Product> repository, IProductsRepository productsRepository)
        {
            _repository = repository;
            _productsRepository = productsRepository;
        }

        public async Task CreateProductAsync(CreateProductDTO model)
        {
            await _repository.CreateAsync(new Product
            {
                ProductName = model.ProductName,
                Description = model.Description,
                Price = model.Price,
                Stock = model.Stock,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId
            });
        }

        public async Task DeleteProductAsync(int id)
        {
            var values = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(values);
        }

        public async Task<List<ResultProductDTO>> GetAllProductAsync()
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new ResultProductDTO
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Description = x.Description,
                Price = x.Price,
                Stock = x.Stock,
                ImageUrl = x.ImageUrl,
                CategoryId = x.CategoryId
            }).ToList();
        }

        public async Task<GetByIdProductDTO> GetByIdProductAsync(int id)
        {
            var values = await _repository.GetByIdAsync(id);
            var result = new GetByIdProductDTO
            {
                ProductId = values.ProductId,
                ProductName = values.ProductName,
                Description = values.Description,
                Price = values.Price,
                Stock = values.Stock,
                ImageUrl = values.ImageUrl,
                CategoryId = values.CategoryId
            };
            return result;
        }

        public async Task<List<ResultProductDTO>> GetProductByCategory(int categoryId)
        {
            var values = await _productsRepository.GetProductByCategory(categoryId);
            return values.Select(x => new ResultProductDTO
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Description = x.Description,
                Price = x.Price,
                Stock = x.Stock,
                ImageUrl = x.ImageUrl,
                CategoryId = x.CategoryId
            }).ToList();
        }

        public async Task<List<ResultProductDTO>> GetProductByPrice(decimal minprice, decimal maxprice)
        {
            var values = await _productsRepository.GetProductByPriceFilter(minprice, maxprice);
            return values.Select(x => new ResultProductDTO
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Description = x.Description,
                Price = x.Price,
                Stock = x.Stock,
                ImageUrl = x.ImageUrl,
                CategoryId = x.CategoryId
            }).ToList();
        }

        public async Task<List<ResultProductDTO>> GetProductBySearch(string search)
        {
            var values = await _productsRepository.GetProductBySearch(search);
            return values.Select(x => new ResultProductDTO
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Description = x.Description,
                Price = x.Price,
                Stock = x.Stock,
                ImageUrl = x.ImageUrl,
                CategoryId = x.CategoryId
            }).ToList();
        }

        public async Task<List<ResultProductDTO>> GetProductTake(int sayi)
        {
            var values = await _repository.GetTakeAsync(sayi);
            return values.Select(x => new ResultProductDTO
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Description = x.Description,
                Price = x.Price,
                Stock = x.Stock,
                ImageUrl = x.ImageUrl,
                CategoryId = x.CategoryId
            }).ToList();
        }

        public async Task UpdateProductAsync(UpdateProductDTO model)
        {
            var values = await _repository.GetByIdAsync(model.ProductId);
            values.ProductName = model.ProductName;
            values.Description = model.Description;
            values.Price = model.Price;
            values.Stock = model.Stock;
            values.ImageUrl = model.ImageUrl;
            values.CategoryId = model.CategoryId;
            await _repository.UpdateAsync(values);
        }
    }
}

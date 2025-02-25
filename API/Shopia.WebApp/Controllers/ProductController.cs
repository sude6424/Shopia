using Microsoft.AspNetCore.Mvc;
using Shopia.Application.Services.ProductServices;

namespace Shopia.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        public async Task<IActionResult> Index(int categoryId, decimal minprice, decimal maxprice, string search)
        {
            if (categoryId !=0)
            {
                var values = await _productServices.GetProductByCategory(categoryId);
                return View(values);
            }
            if (maxprice!=0)
            {
                var values = await _productServices.GetProductByPrice(minprice, maxprice);
                return View(values);
            }
           

            var value = await _productServices.GetAllProductAsync();
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> Index(string search)
        {
            if (search == null)
            {
                return View();
            }
            var value = await _productServices.GetProductBySearch(search);
            return View(value);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var value = await _productServices.GetByIdProductAsync(id);
            return View(value);
        }

    }
}

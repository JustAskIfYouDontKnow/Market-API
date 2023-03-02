using Market.API.Database;
using Microsoft.AspNetCore.Mvc;

namespace Market.API.Controllers.Client
{

    public class ProductController : AbstractClientController
    {
        
        public ProductController(IDatabaseContainer databaseContainer) : base(databaseContainer)
        { }


        [HttpPost]
        public async Task<IActionResult> CreateProduct(string title, string description, decimal price, int userId)
        {
            var createdProduct = await _databaseContainer.Product.Create(title, description, price, userId);
            return Ok(createdProduct);
        }
        
        
        [HttpGet]
        public async Task<IActionResult> FindProduct(int id)
        {
            var foundedProduct = await _databaseContainer.Product.FindOneById(id);
            return Ok(foundedProduct);
        }

        
        [HttpGet]
        public async Task<IActionResult> GetPaginatedProducts(int skip, int take)
        {
            var paginationProduct = await _databaseContainer.Product.GetProductsRange(skip, take);
            return Ok(paginationProduct);
        }
    }
}
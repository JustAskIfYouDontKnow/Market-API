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
            var user = await _databaseContainer.User.GetOneById(userId);
            var createdProduct = await _databaseContainer.Product.Create(title, description, price, user.Id);
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
        
        
        [HttpGet]
        public async Task<IActionResult> FindProductsByUserId(int userId)
        {
            var user = await _databaseContainer.User.GetOneById(userId);
            var products = await _databaseContainer.Product.FindListByUserId(userId);
            return Ok(products);
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteProductById(int userId, int productId)
        {
            var user = await _databaseContainer.User.GetOneById(userId);
            var product = await _databaseContainer.Product.FindOneById(productId);

            if (user.Id == product.CreatedByUserId)
            {
                await _databaseContainer.Product.Delete(product);
                return Ok();
            }

            throw new Exception("Can't delete product");

        }
    }
}
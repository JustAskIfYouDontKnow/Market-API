using Market.API.Database;
using Microsoft.AspNetCore.Mvc;

namespace Market.API.Controllers.Client
{

    public class ProductController : AbstractClientController
    {

        public ProductController(IDatabaseContainer databaseContainer) : base(databaseContainer) { }


        [HttpPost]
        public async Task<IActionResult> CreateProduct(string title, string description, decimal price, int userId)
        {
            var user = await DatabaseContainer.User.GetOneById(userId);
            var createdProduct = await DatabaseContainer.Product.Create(title, description, price, user.Id);
            return Ok(createdProduct);
        }


        [HttpGet]
        public async Task<IActionResult> GetProduct(int id)
        {
            var foundedProduct = await DatabaseContainer.Product.GetOneById(id);
            return Ok(foundedProduct);
        }


        [HttpGet]
        public async Task<IActionResult> GetProductsList(int skip, int take)
        {
            var paginationProduct = await DatabaseContainer.Product.GetProductsRange(skip, take);
            return Ok(paginationProduct);
        }


        [HttpGet]
        public async Task<IActionResult> FindProductsByUserId(int userId)
        {
            var user = await DatabaseContainer.User.GetOneById(userId);
            var products = await DatabaseContainer.Product.FindListByUserId(userId);
            return Ok(products);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteProductById(int userId, int productId)
        {
            var user = await DatabaseContainer.User.GetOneById(userId);
            var product = await DatabaseContainer.Product.GetOneById(productId);

            if (user.Id != product.CreatedByUserId)
            {
                throw new Exception("Can't delete product");
            }

            await DatabaseContainer.Product.Delete(product);
            return Ok();

        }
    }
}
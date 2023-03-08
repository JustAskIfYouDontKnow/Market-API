using Market.API.Client.Payload;
using Market.API.Client.Product;
using Market.API.Database;
using Market.API.Database.OrderProduct;
using Market.API.Database.User;
using Microsoft.AspNetCore.Mvc;

namespace Market.API.Controllers.Client
{

    public class ProductController : AbstractClientController
    {

        public ProductController(IDatabaseContainer databaseContainer) : base(databaseContainer) { }

      
        [HttpPost]
        public async Task<IActionResult> CreateProduct(int userId, string title, string description, decimal price)
        {
            var user = await DatabaseContainer.User.GetOneById(userId);
            var createdProduct = await DatabaseContainer.Product.Create(user.Id, title, description, price);
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
            var products = await DatabaseContainer.Product.GetProductsRange(skip, take);
            
            var response = products.Select(p => new 
            {
                ProductId = p.Id,
                UserId = p.UserId,
                Description = p.Description,
                Price = p.Price,
                FirstName = p.UserModel.FirstName,
                LastName = p.UserModel.LastName,
                
            }).ToList();
            
            return Ok(response);
        }


        [HttpPost]
        [ProducesResponseType(typeof(GetUserProducts.ResponseListByUserId), 200)]
        public async Task<GetUserProducts.ResponseListByUserId> ListProductsByUserId([FromBody] GetUserProducts request)
        {
            var user = await DatabaseContainer.User.GetOneById(request.UserId);
            var products = await DatabaseContainer.Product.FindListByUserId(user.Id);

            return new GetUserProducts.ResponseListByUserId()
            {
                Products = products.Select(
                        x => new Product()
                        {
                            Id = x.Id,
                            UserId = x.UserId,
                            Description = x.Description,
                            Price = x.Price,
                        }
                    )
                    .ToList()
            };
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteProductById(int userId, int productId)
        {
            var user = await DatabaseContainer.User.GetOneById(userId);
            var product = await DatabaseContainer.Product.GetOneById(productId);

            if (user.Id != product.UserId)
            {
               return BadRequest($"User {user.Id} tried to delete product: {product.Id}. Cannot complete this operation because it is not a user's product.");
            }

            await DatabaseContainer.Product.Delete(product);
            return Ok();

        }
    }
}
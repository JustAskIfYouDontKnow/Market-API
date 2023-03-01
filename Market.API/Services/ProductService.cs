using Market.API.Models;

namespace Market.API.Services
{
    public class ProductService : IProductService
    {

        public Task<List<Product>> GetProducts()
        {
            throw new NotImplementedException();
        }
        

        public Task<Product> GetProduct(int id)
        {
            throw new NotImplementedException();
        }


        public Task<Product> CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }


        public Task<Product> UpdateProduct(int id, Product product)
        {
            throw new NotImplementedException();
        }


        public Task DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }
    }
}
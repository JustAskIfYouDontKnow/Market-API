using System;
using System.Threading.Tasks;

namespace Market.API.Database.Product;

public class ProductRepository : AbstractRepository<ProductModel>, IProductRepository
{
    public ProductRepository(PostgresContext context) : base(context)
    {
    }

    public async Task<ProductModel> Create(int orderId, string title, string description, decimal price)
    {
        var model = ProductModel.CreateModel(orderId, title, description, price);
        
        var result = await CreateModelAsync(model);
        if (result == null)
        {
            throw new Exception("Product model is not created.");
        }

        return result;
    }
}
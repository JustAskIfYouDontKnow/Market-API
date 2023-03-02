using System.Threading.Tasks;

namespace Market.API.Database.Product;

public interface IProductRepository
{
    Task<ProductModel> Create(int orderId, string title, string description, decimal price);
}
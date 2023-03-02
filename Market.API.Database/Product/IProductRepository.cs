using System.Collections.Generic;
using System.Threading.Tasks;

namespace Market.API.Database.Product;

public interface IProductRepository
{
    Task<ProductModel> Create(string title, string description, decimal price, int userId);

    Task<List<ProductModel>> FindList(IEnumerable<int> ids);
    
    Task<ProductModel> FindOneById(int id);
    
    Task<List<ProductModel>> GetProductsRange(int skip, int take);
}
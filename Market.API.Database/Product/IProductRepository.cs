using System.Collections.Generic;
using System.Threading.Tasks;

namespace Market.API.Database.Product;

public interface IProductRepository
{
    Task<ProductModel> Create(string title, string description, decimal price, int userId);

    Task<List<ProductModel>> FindList(IEnumerable<int> ids);
    
    Task<ProductModel> GetOneById(int id);
    
    Task<List<ProductModel>> FindListByUserId(int userId);
    
    Task<List<ProductModel>> GetProductsRange(int skip, int take);
    
    Task<bool> Delete(ProductModel product);
}
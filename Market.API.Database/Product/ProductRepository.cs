using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Market.API.Database.Product;

public class ProductRepository : AbstractRepository<ProductModel>, IProductRepository
{
    public ProductRepository(PostgresContext context) : base(context) { }


    public async Task<ProductModel> Create(int userId, string title, string description, decimal price)
    {
        var model = ProductModel.CreateModel(userId, title, description, price);

        var result = await CreateModelAsync(model);

        if (result is null)
        {
            throw new Exception("Product model is not created.");
        }

        return result;
    }


    public async Task<List<ProductModel>> FindList(IEnumerable<int> ids)
    {
        return await DbModel.Where(x => ids.Contains(x.Id)).ToListAsync();
    }


    public async Task<ProductModel> GetOneById(int id)
    {
        var model = await DbModel.FindAsync(id);

        if (model is null)
        {
            throw new Exception($"Product by id {id} not found");
        }

        return model;
    }


    public async Task<List<ProductModel>> FindListByUserId(int userId)
    {
        return await DbModel.Where(x => x.UserId == userId).ToListAsync();
    }


    public async Task<List<ProductModel>> GetProductsRange(int skip, int take)
    {
        return await DbModel.Include(x =>x.UserModel).Include(x=>x.OrderProducts).Skip(skip).Take(take).ToListAsync();
    }


    public async Task<bool> Delete(ProductModel product)
    {
        await DeleteModel(product);
        return true;
    }
}
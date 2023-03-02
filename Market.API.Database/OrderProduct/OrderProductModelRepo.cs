using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Market.API.Database.Product;
using Market.API.Database.User;
using Microsoft.EntityFrameworkCore;

namespace Market.API.Database.OrderProduct;

public class OrderProductModelRepo : AbstractRepository<OrderProductModel>, IOrderProductModelRepo
{
    public OrderProductModelRepo(PostgresContext context) : base(context)
    {
    }


    public async Task<OrderProductModel> Create(UserModel user, ProductModel product, string deliveryAddress)
    {
        var model = OrderProductModel.CreateModel(user, product, deliveryAddress);

        var result = await CreateModelAsync(model);
        if (result is null)
        {
            throw new Exception("Order product is not created");
        }

        return result;
    }
    
    public async Task<List<OrderProductModel>> FindOrdersByUserId(UserModel user)
    {
        var result = await DbModel.Where(x => x.UserId == user.Id).ToListAsync();
        if (result == null)
        {
            throw new Exception($"Order for user id {user.Id} is not found");
        }
        return result;
    }


    public async Task<OrderProductModel> FindOneById(int id)
    {
        var model = await DbModel.FindAsync(id);

        if (model is null)
        {
            throw new Exception($"Order by id {id} not found");
        }

        return model;
    }


    public async Task<List<OrderProductModel>> FindListByUserId(int userId)
    {
        return await DbModel.Where(x => x.UserId == userId).ToListAsync();
    }


    public async Task<List<OrderProductModel>> GetProductsRange(int skip, int take)
    {
        return await DbModel.Skip(skip).Take(take).ToListAsync();
    }


    public async Task<bool> Delete(OrderProductModel product)
    {
        await DeleteModel(product);
        return true;
    }
}
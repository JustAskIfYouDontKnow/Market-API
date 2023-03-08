using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Market.API.Database.OrderProduct;
using Market.API.Database.Product;
using Market.API.Database.User;
using Microsoft.EntityFrameworkCore;

namespace Market.API.Database.Order;

public class OrderModelRepository : AbstractRepository<OrderModel>, IOrderModelRepository
{
    public OrderModelRepository(PostgresContext context) : base(context) { }


    public async Task<OrderModel> Create(UserModel user, List<OrderProductModel> products, string deliveryAddress)
    {
        var model = OrderModel.CreateModel(user, products, deliveryAddress);
        var result = await CreateModelAsync(model);

        if (result is null)
        {
            throw new Exception("Order product is not created");
        }

        return result;
    }


    public async Task<List<OrderModel>> FindOrdersByUserId(UserModel user)
    {
        var result = await DbModel.Include(o => o.OrderProducts)
            .ThenInclude(op => op.ProductModel)
            .Where(x => x.UserId == user.Id)
            .ToListAsync();
        
        if (result == null)
        {
            throw new Exception($"Order for user id {user.Id} is not found");
        }

        return result;
    }


    public async Task<OrderModel> GetOneById(int id)
    {
        var model = await DbModel.FindAsync(id);

        if (model is null)
        {
            throw new Exception($"Order by id {id} not found");
        }

        return model;
    }
    
    public async Task<List<OrderModel>> GetProductsRange(int skip, int take)
    {
        return await DbModel.Skip(skip).Take(take).ToListAsync();
    }


    public async Task<bool> Delete(OrderModel product)
    {
        await DeleteModel(product);
        return true;
    }
}
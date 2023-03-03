using System.Collections.Generic;
using System.Threading.Tasks;
using Market.API.Database.OrderProduct;
using Market.API.Database.Product;
using Market.API.Database.User;

namespace Market.API.Database.Services;

public class OrderService
{
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderProductModelRepo _orderProduct;


    public OrderService(IUserRepository userRepository, IProductRepository productRepository, IOrderProductModelRepo orderProduct)
    {
        _userRepository = userRepository;
        _productRepository = productRepository;
        _orderProduct = orderProduct;
    }


    public async Task<bool> Create(UserModel user, IEnumerable<int> productIds, string deliveryAddress)
    {
        var products = await _productRepository.FindList(productIds);

        if (products.Count == 0)
        {
            return false;
        }

        foreach (var product in products)
        {
            await _orderProduct.Create(user, product, deliveryAddress);
        }

        return true;
    }
}
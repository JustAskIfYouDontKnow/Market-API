using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Market.API.Database.Order;
using Market.API.Database.OrderProduct;
using Market.API.Database.Product;
using Market.API.Database.User;

namespace Market.API.Database.Services;

public class OrderService
{
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderModelRepository _orderRepository;


    public OrderService(IUserRepository userRepository, IProductRepository productRepository, IOrderModelRepository orderRepository)
    {
        _userRepository = userRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }


    public async Task<bool> Create(UserModel user, IEnumerable<int> productIds, string deliveryAddress)
    {
        var products = await _productRepository.FindList(productIds);

        if (products.Count == 0)
        {
            return false;
        }
        var orderProducts = products.Select(p => new OrderProductModel { ProductId = p.Id }).ToList(); 
        await _orderRepository.Create(user, orderProducts, deliveryAddress);
        return true;
    }
}
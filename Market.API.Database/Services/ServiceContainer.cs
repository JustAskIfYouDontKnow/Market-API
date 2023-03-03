namespace Market.API.Database.Services;

public class ServiceContainer
{
    public readonly OrderService OrderService;


    public ServiceContainer(OrderService orderService)
    {
        OrderService = orderService;
    }
}
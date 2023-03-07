using Market.API.Client.Payload;
using Market.API.Database.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Market.API.Pages;

public class Products : PageModel
{
    private readonly ProductRepository _productRepository;

    public Products(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public ProductModel Product { get; set; }

    public IEnumerable<ProductModel> ProductsList { get; set; } = new List<ProductModel>();


    public async Task OnGet(int? id, int? skip, int? take)
    {
        if (id.HasValue)
        {
            Product = await _productRepository.GetOneById(id.Value);
        }

        if (skip.HasValue && take.HasValue)
        { ProductsList = await _productRepository.GetProductsRange(skip.Value, take.Value);
        }
    }
    
}
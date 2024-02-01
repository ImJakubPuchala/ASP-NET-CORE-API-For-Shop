using ASP_NET_CORE_API_For_Shop.Model;
using ASP_NET_CORE_API_For_Shop.ModelDTO;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_CORE_API_For_Shop.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductStatisticsController : ControllerBase
{
    private readonly ShopContext _context;

    public ProductStatisticsController(ShopContext context)
    {
        _context = context;
    }

    [HttpGet("{ean}")]
    public ActionResult<ProductStatistics> GetProductStatistics(string ean)
    {
        var product = _context.Products
            .Where(p => p.Eancode == ean)
            .Select(p => new ProductStatistics
            {
                EAN = p.Eancode,
                Name = p.Name,
                TotalSales = p.Sales.Sum(s => s.QuantitySold),
                AverageRating = p.Reviews.Any() ? p.Reviews.Average(r => r.Rating) : 0
            })
            .FirstOrDefault();

        if (product == null)
        {
            return NotFound("Product not found for the given EAN code.");
        }

        return product;
    }
}

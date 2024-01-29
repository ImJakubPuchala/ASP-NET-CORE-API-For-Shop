using ASP_NET_CORE_API_For_Shop.Model;
using ASP_NET_CORE_API_For_Shop.ModelDTO;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_CORE_API_For_Shop.Controllers;

[ApiController]
[Route("[controller]")]
public class PriceController : ControllerBase
{
    private readonly ShopContext _context;

    public PriceController(ShopContext context)
    {
        _context = context;
    }

    [HttpGet("GetPrice")]
    public ActionResult<ProductInformation> GetPrice(string EAN)
    {
        try
        {
            var productInfo = _context.Products
                .Where(p => p.Eancode == EAN)
                .Select(p => new ProductInformation
                {
                    Id = p.ProductId,
                    Name = p.Name,
                    Price = p.Prices
                        .Where(price => price.EndDate == null)
                        .OrderByDescending(price => price.StartDate)
                        .Select(price => (double)price.Price1)
                        .FirstOrDefault(),
                    EAN = p.Eancode
                })
                .FirstOrDefault();

            if (productInfo == null)
            {
                return NotFound("Product not found for the given EAN code.");
            }

            return Ok(productInfo);
        }
        catch
        {
            return NotFound("An error occurred while fetching the product.");
        }
    }

    [HttpGet("GetAllProducts")]
    public ActionResult<IEnumerable<ProductInformation>> GetAllProducts()
    {
        try
        {
            var allProducts = _context.Products
                .Select(p => new ProductInformation
                {
                    Id = p.ProductId,
                    Name = p.Name,
                    Price = p.Prices
                        .Where(price => price.EndDate == null)
                        .OrderByDescending(price => price.StartDate)
                        .Select(price => (double)price.Price1)
                        .FirstOrDefault(),
                    EAN = p.Eancode
                })
                .ToList();

            return Ok(allProducts);
        }
        catch
        {
            return NotFound("An error occurred while fetching products.");
        }
    }

    [HttpPost("AddProduct")]
    public async Task<ActionResult<ProductInformation>> AddProduct(ProductInformation productInfo)
    {
        if (productInfo == null)
        {
            return BadRequest("Invalid product data.");
        }

        var newProduct = new Product
        {
            Name = productInfo.Name,
            Eancode = productInfo.EAN
        };

        var newPrice = new Price
        {
            Price1 = (decimal)productInfo.Price,
            StartDate = DateOnly.FromDateTime(DateTime.Now),
        };

        newProduct.Prices.Add(newPrice);
        _context.Products.Add(newProduct);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPrice), new { EAN = newProduct.Eancode }, productInfo);
    }

    [HttpPost("ChangeProductPrice")]
    public async Task<IActionResult> ChangeProductPrice(ProductNewPrice productNewPrice)
    {
        if (productNewPrice == null || string.IsNullOrWhiteSpace(productNewPrice.EAN))
        {
            return BadRequest("Invalid product data.");
        }

        var product = _context.Products.FirstOrDefault(p => p.Eancode == productNewPrice.EAN);

        if (product == null)
        {
            return NotFound("Product not found.");
        }

        var currentPrice = _context.Prices
            .Where(p => p.ProductId == product.ProductId && p.EndDate == null)
            .SingleOrDefault();

        if (currentPrice != null)
        {
            currentPrice.EndDate = DateOnly.FromDateTime(DateTime.Now);

            var newPriceEntry = new Price
            {
                ProductId = product.ProductId,
                Price1 = (decimal)productNewPrice.NewPrice,
                StartDate = DateOnly.FromDateTime(DateTime.Now)
            };

            _context.Prices.Add(newPriceEntry);
            await _context.SaveChangesAsync();

            return Ok("Price updated successfully.");
        }

        return NotFound("Current price not found.");
    }

}
using ASP_NET_CORE_API_For_Shop.Model;
using ASP_NET_CORE_API_For_Shop.ModelDTO;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_CORE_API_For_Shop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriceController : ControllerBase
    {
        private readonly ShopContext _context;

        public PriceController(ShopContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetPrice")]
        public ActionResult<ProductInformation> GetPrice(string EAN)
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
                        .FirstOrDefault()
                })
                .FirstOrDefault();

            if (productInfo == null)
            {
                return NotFound("Product not found for the given EAN code.");
            }

            return Ok(productInfo);
        }
    }
}

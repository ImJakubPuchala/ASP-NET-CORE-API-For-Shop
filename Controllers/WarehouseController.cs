using Microsoft.AspNetCore.Mvc;
using ASP_NET_CORE_API_For_Shop.Model;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;


namespace ASP_NET_CORE_API_For_Shop.Controllers;

[ApiController]
[Route("[controller]")]
public class WarehouseController : ControllerBase
{
    private readonly ShopContext _context;

    public WarehouseController(ShopContext context)
    {
        _context = context;
    }

    [HttpGet("{eanCode}")]
    public ActionResult<Warehouse> GetWarehouseItemByEAN(string eanCode)
    {
        var warehouseItem = _context.Warehouse
                                    .Include(w => w.Product)
                                    .FirstOrDefault(w => w.Product.Eancode == eanCode);
        if (warehouseItem == null)
        {
            return NotFound("Product not found for the given EAN code.");
        }
        return warehouseItem;
    }

    [HttpGet("GetAllWarehouseItems")]
    public ActionResult<IEnumerable<Warehouse>> GetAllWarehouseItems()
    {
        var allWarehouseItems = _context.Warehouse;
        return allWarehouseItems.ToList();
    }
}

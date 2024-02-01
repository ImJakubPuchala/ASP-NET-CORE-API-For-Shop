using Microsoft.AspNetCore.Mvc;
using ASP_NET_CORE_API_For_Shop.Model;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ASP_NET_CORE_API_For_Shop.ModelDTO;


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

    [HttpGet("GetAllWarehouseItems")]
    public ActionResult<IEnumerable<Warehouse>> GetAllWarehouseItems()
    {
        var allWarehouseItems = _context.Warehouse;
        return allWarehouseItems.ToList();
    }

    [HttpPost("AddWarehouseItem")]
    public async Task<IActionResult> AddWarehouseItem(WarehouseDto warehouseDto)
    {
        var product = _context.Products.FirstOrDefault(p => p.Eancode == warehouseDto.EANCode);
        if (product == null)
        {
            return NotFound("Product not found for the given EAN code.");
        }

        var existingWarehouseItem = _context.Warehouse
            .FirstOrDefault(w => w.ProductId == product.ProductId && w.WarehouseNumber == warehouseDto.WarehouseNumber);

        if (existingWarehouseItem != null)
        {
            existingWarehouseItem.Quantity += warehouseDto.Quantity;
        }
        else
        {
            var newWarehouseItem = new Warehouse
            {
                ProductId = product.ProductId,
                Quantity = warehouseDto.Quantity,
                WarehouseNumber = warehouseDto.WarehouseNumber
            };
            _context.Warehouse.Add(newWarehouseItem);
        }

        await _context.SaveChangesAsync();

        return Ok();
    }
}

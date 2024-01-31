namespace ASP_NET_CORE_API_For_Shop.Model;

public class Warehouse
{
    public int WarehouseId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string WarehouseNumber { get; set; }

    public virtual Product Product { get; set; }
}
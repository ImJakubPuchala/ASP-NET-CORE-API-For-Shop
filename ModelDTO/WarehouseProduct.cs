namespace ASP_NET_CORE_API_For_Shop.ModelDTO;

public class WarehouseProduct
{
    public int WarehouseItemId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string EANCode { get; set; }
    public int Quantity { get; set; }
    public string WarehouseNumber { get; set; }
}

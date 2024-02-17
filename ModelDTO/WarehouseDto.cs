namespace ASP_NET_CORE_API_For_Shop.ModelDTO;


public class WarehouseDto
{
    public string EANCode { get; set; }
    public int Quantity { get; set; }
    public string WarehouseNumber { get; set; }
}

public class UpdateWarehouseNumberDto
{
    public string NewWarehouseNumber { get; set; }
}
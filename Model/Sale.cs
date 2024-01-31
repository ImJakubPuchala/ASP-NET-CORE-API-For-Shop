namespace ASP_NET_CORE_API_For_Shop.Model;

public class Sale
{
    public int SaleId { get; set; }
    public int ProductId { get; set; }
    public int QuantitySold { get; set; }

    public virtual Product Product { get; set; }
}

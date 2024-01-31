namespace ASP_NET_CORE_API_For_Shop.Model;

public class Review
{
    public int ReviewId { get; set; }
    public int ProductId { get; set; }
    public float Rating { get; set; }

    public virtual Product Product { get; set; }
}
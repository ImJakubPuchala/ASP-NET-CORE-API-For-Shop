using System;
using System.Collections.Generic;

namespace ASP_NET_CORE_API_For_Shop.Model;

public partial class Product
{
    public Product()
    {
        Prices = new HashSet<Price>();
        Sales = new HashSet<Sale>();
        Reviews = new HashSet<Review>();
        WarehouseEntries = new HashSet<Warehouse>();
    }

    public int ProductId { get; set; }
    public string? Name { get; set; }
    public string? Eancode { get; set; }

    public virtual ICollection<Price> Prices { get; set; }
    public virtual ICollection<Sale> Sales { get; set; }
    public virtual ICollection<Review> Reviews { get; set; }
    public virtual ICollection<Warehouse> WarehouseEntries { get; set; }
}

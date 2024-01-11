using System;
using System.Collections.Generic;

namespace ASP_NET_CORE_API_For_Shop.Model;

public partial class Product
{
    public int ProductId { get; set; }

    public string? Name { get; set; }

    public string? Eancode { get; set; }

    public virtual ICollection<Price> Prices { get; set; } = new List<Price>();
}

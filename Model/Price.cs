using System;
using System.Collections.Generic;

namespace ASP_NET_CORE_API_For_Shop.Model;

public partial class Price
{
    public int PriceId { get; set; }

    public int? ProductId { get; set; }

    public decimal? Price1 { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual Product? Product { get; set; }
}

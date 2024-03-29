﻿namespace ASP_NET_CORE_API_For_Shop.ModelDTO;
public class ProductInformation
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string EAN { get; set; }
    public double NewPrice { get; set; }
}

public class ProductNewPrice
{     
    public string EAN { get; set; }
    public double NewPrice { get; set; }
}

public class ProductStatistics
{
    public string EAN { get; set; }
    public string Name { get; set; }
    public int TotalSales { get; set; }
    public double AverageRating { get; set; }
}



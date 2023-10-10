using System;
using System.Collections.Generic;

namespace Dashboard.data;

public class Home
{
    public int Id { get; set; }
    public string Address { get; set; } = string.Empty;
    public int Price { get; set; }
    public int? YearBuilt { get; set; }
    public int? SquareFeet { get; set; }
    public DateTime PriceDate { get; set; }    
    public int RegionId { get; set; }

    public List<Region> RegionList { get; set; } = new();

}


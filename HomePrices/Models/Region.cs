using System.Collections.Generic;

namespace HomePrices.data;

public class Region
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;

    public List<Home> HomeList { get; set; } = new();
}

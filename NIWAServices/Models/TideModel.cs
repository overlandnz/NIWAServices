namespace NIWAServices.Models;

public class TidesResponse
{
    public TidesMetadata Metadata { get; set; }
    public List<TideValue> Values { get; set; }
}
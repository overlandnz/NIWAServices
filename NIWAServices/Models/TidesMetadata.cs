namespace NIWAServices.Models;

public class TidesMetadata
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Datum { get; set; }
    public DateTimeOffset Start { get; set; }
    public long Days { get; set; }
    public long Interval { get; set; }
    public string Height { get; set; }
}
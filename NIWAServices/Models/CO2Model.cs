using System.Text.Json.Serialization;

namespace NIWAServices.Models;

public class CO2Model
{
    public DateTime Date { get; set; }
    public double LatestDailyAverage { get; set; }
    public double OneYearAgo { get; set; }
    [JsonPropertyName("OneDecageAgo")] public double OneDecadeAgo { get; set; }
}
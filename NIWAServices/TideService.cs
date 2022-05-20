using NIWAServices.Enums;
using NIWAServices.Models;
using RestSharp;

namespace NIWAServices;

public class TideService : BaseService
{
    public TideService(string apiKey)
        : base(apiKey)
    {
    }

    /// <summary>
    /// Fetches tide information for given location
    /// </summary>
    /// <param name="latitude">Required. Latitude of location</param>
    /// <param name="longitude">Required. Longitude of location</param>
    /// <param name="days">Number of days, range (1-31), default: 7</param>
    /// <param name="startDate">Start date. Default: UTCNow</param>
    /// <param name="tidesDatum">LAT: Lowest astronomical tide; MSL: Mean sea level</param>
    /// <returns><see cref="TidesResponse"/></returns>
    public async Task<TidesResponse> GetTides(
        double latitude,
        double longitude,
        int days = 7,
        DateTime? startDate = null,
        NIWATidesDatum tidesDatum = NIWATidesDatum.LowestAstronomicalTide
    )
    {
        RestRequest restRequest = new RestRequest("tides/data");
        restRequest.AddQueryParameter("lat", latitude);
        restRequest.AddQueryParameter("long", longitude);
        restRequest.AddQueryParameter("numberOfDays", days);
        restRequest.AddQueryParameter("datum", tidesDatum.ToNIWAString());

        if (startDate.HasValue)
        {
            restRequest.AddQueryParameter("startDate", startDate.Value.ToString("yyyy-MM-dd"));
        }
        
        return ProcessResponse(await _restClient.ExecuteAsync<TidesResponse>(restRequest));
    }
}
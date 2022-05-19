using System.Net;
using NIWAServices.Enums;
using NIWAServices.Models;
using RestSharp;

namespace NIWAServices;

public class TideService
{
    private readonly RestClient _restClient;

    public TideService(string apiKey)
    {
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new ArgumentException("API Key is required");
        }

        _restClient = new RestClient("https://api.niwa.co.nz/tides");
        _restClient.AddDefaultHeader("x-apikey", apiKey);
    }

    /// <summary>
    /// Fetches tide information for given location
    /// </summary>
    /// <param name="latitude">Required. Latitude of location</param>
    /// <param name="longitude">Required. Longitude of location</param>
    /// <param name="days">Number of days, range (1-31), default: 7</param>
    /// <param name="startDate">Start date. Default: UTCNow</param>
    /// <param name="tidesDatum">LAT: Lowest astronomical tide; MSL: Mean sea level</param>
    /// <returns></returns>
    public async Task<TidesResponse> GetTides(
        double latitude,
        double longitude,
        int days = 7,
        DateTime? startDate = null,
        NIWATidesDatum tidesDatum = NIWATidesDatum.LowestAstronomicalTide
    )
    {
        RestRequest restRequest = new RestRequest("data");
        restRequest.AddQueryParameter("lat", latitude);
        restRequest.AddQueryParameter("long", longitude);
        restRequest.AddQueryParameter("numberOfDays", days);
        restRequest.AddQueryParameter("datum", tidesDatum.ToNIWAString());

        if (startDate.HasValue)
        {
            restRequest.AddQueryParameter("startDate", startDate.Value.ToString("yyyy-MM-dd"));
        }

        RestResponse<TidesResponse> response = await _restClient.ExecuteAsync<TidesResponse>(restRequest);

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                return response.Data;

            case HttpStatusCode.Unauthorized:
                throw new UnauthorizedAccessException(response.ErrorMessage);

            case HttpStatusCode.BadRequest:
                throw new Exception("Bad request");

            default:
                throw new Exception(response.ErrorMessage);
        }
    }
}
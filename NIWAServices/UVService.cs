using NIWAServices.Models;
using RestSharp;

namespace NIWAServices;

public class UVService : BaseService
{
    public UVService(string apiKey)
        : base(apiKey)
    {
    }

    /// <summary>
    /// API for UVI (ultraviolet index) forecasts. Endpoints provide a time series of UVI forecasts as well as various images, for any given location in New Zealand.
    /// </summary>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    /// <returns></returns>
    public async Task<UVResponseModel> Get(double latitude, double longitude)
    {
        RestRequest request = new RestRequest("/uv/data");
        request.AddParameter("lat", latitude);
        request.AddParameter("long", longitude);

        return ProcessResponse(await _restClient.ExecuteAsync<UVResponseModel>(request));
    }
}
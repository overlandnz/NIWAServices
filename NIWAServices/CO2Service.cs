using NIWAServices.Models;
using RestSharp;

namespace NIWAServices;

public class CO2Service : BaseService
{
    public CO2Service(string apiKey)
        : base(apiKey)
    {
    }

    /// <summary>
    /// Gets the most recent CO2 information from Baring Head
    /// </summary>
    /// <returns></returns>
    public async Task<CO2Model> Get()
    {
        RestRequest restRequest = new RestRequest("/co2/info/baringhead.json");
        return ProcessResponse(await _restClient.ExecuteAsync<CO2Model>(restRequest));
    }
}
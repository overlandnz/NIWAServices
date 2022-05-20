using System.Net;
using RestSharp;

namespace NIWAServices;

public partial class BaseService
{
    protected readonly RestClient _restClient;

    protected BaseService(string apiKey)
    {
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new ArgumentException("API Key is required");
        }

        _restClient = new RestClient("https://api.niwa.co.nz/");
        _restClient.AddDefaultHeader("x-apikey", apiKey);
    }

    protected T ProcessResponse<T>(RestResponse<T> response)
    {
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
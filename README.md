# NIWA Services

C# API wrapper for NIWA's services

## Supported APIs

* Tides

## Using

`
	var service = new TideService("YourAPIKey")
	var response = service.GetTides(-41.340963931789474, 174.74689594599624);
`

Supports most of the parameters listed here https://developer.niwa.co.nz/docs/tide-api/1/routes/data/get

## Tests

Has some unit tests - update with your API key before use

# NIWA Services

C# API wrapper for NIWA's services

## Supported APIs

* Tides
* UV Index
* CO2 Reading

## Install

`Install-Package NIWAServices -Version 1.0.2`

or

`dotnet add package NIWAServices --version 1.0.2`

## Tides

```
var service = new TideService("YourAPIKey")
var response = service.GetTides(-41.340963931789474, 174.74689594599624);
```

## UV Index

```
var service = new UVService("YourAPIKey");
var value = await service.Get(-41.340963931789474, 174.74689594599624);
```

## CO2 Reading

```
var service = new CO2Service("YOURKEY");
var value = await service.Get();
```

Supports most of the parameters listed here https://developer.niwa.co.nz/docs/tide-api/1/routes/data/get

## Tests

Has some unit tests - update with your API key before use

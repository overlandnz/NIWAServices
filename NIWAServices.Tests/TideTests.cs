using NIWAServices.Enums;
using NIWAServices.Models;
using NUnit.Framework;

namespace NIWAServices.Tests;

[TestFixture]
public class TideTests
{
    private const double RedRocksLat = -41.340963931789474;
    private const double RedRocksLong = 174.74689594599624;
    private const string TestApiKey = "YOURKEY"; // Replace with your API key for testing

    [Test]
    public void TestThatBlankKeyThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new TideService(string.Empty));
    }

    [Test]
    public void TestThatNullKeyThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new TideService(null));
    }

    [Test]
    public async Task TestThatInvalidKeyThrowUnauthorizedAccessException()
    {
        TideService service = new TideService("123");
        Assert.ThrowsAsync<UnauthorizedAccessException>(async () => { await service.GetTides(RedRocksLat, RedRocksLong); });
    }

    [Test]
    public async Task TestThatValidKeyReturnsData()
    {
        TideService service = new TideService(TestApiKey);
        TidesResponse value = await service.GetTides(RedRocksLat, RedRocksLong);

        Assert.NotNull(value);
        Assert.NotNull(value.Metadata);
        Assert.NotNull(value.Values);
        Assert.IsNotEmpty(value.Values);
    }

    [Test]
    public async Task TestThatCustomDaysParameterWorks()
    {
        TideService service = new TideService(TestApiKey);
        TidesResponse value = await service.GetTides(RedRocksLat, RedRocksLong, days: 3);

        Assert.NotNull(value);
        Assert.NotNull(value.Metadata);
        Assert.That(value.Metadata.Days, Is.EqualTo(3));
    }

    [Test]
    public async Task TestThatCustomStartDateWorks()
    {
        TideService service = new TideService(TestApiKey);
        var startDate = DateTime.UtcNow.Date;
        TidesResponse value = await service.GetTides(RedRocksLat, RedRocksLong, startDate: startDate);

        Assert.NotNull(value);
        Assert.NotNull(value.Metadata);
        Assert.That(value.Metadata.Start.Date, Is.EqualTo(startDate));
    }

    [Test]
    public async Task TestThatMeanSeaLevelDatumWorks()
    {
        TideService service = new TideService(TestApiKey);
        TidesResponse value = await service.GetTides(RedRocksLat, RedRocksLong, tidesDatum: NIWATidesDatum.MeanSeaLevel);

        Assert.NotNull(value);
        Assert.NotNull(value.Metadata);
        Assert.That(value.Metadata.Datum, Is.EqualTo("MSL"));
    }

    [Test]
    public async Task TestThatLowestAstronomicalTideDatumWorks()
    {
        TideService service = new TideService(TestApiKey);
        TidesResponse value = await service.GetTides(RedRocksLat, RedRocksLong, tidesDatum: NIWATidesDatum.LowestAstronomicalTide);

        Assert.NotNull(value);
        Assert.NotNull(value.Metadata);
        Assert.That(value.Metadata.Datum, Is.EqualTo("LAT"));
    }

    [Test]
    public async Task TestThatInvalidCoordinatesThrowException()
    {
        TideService service = new TideService(TestApiKey);
        Assert.ThrowsAsync<Exception>(async () => await service.GetTides(1000, 1000));
    }

    [Test]
    public async Task TestThatTideValuesAreOrderedByTime()
    {
        TideService service = new TideService(TestApiKey);
        TidesResponse value = await service.GetTides(RedRocksLat, RedRocksLong);

        Assert.NotNull(value);
        Assert.NotNull(value.Values);
        Assert.IsNotEmpty(value.Values);

        var orderedValues = value.Values.OrderBy(v => v.Time).ToList();
        CollectionAssert.AreEqual(orderedValues, value.Values);
    }
}
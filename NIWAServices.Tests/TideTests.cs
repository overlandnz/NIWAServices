using NIWAServices.Models;
using NUnit.Framework;

namespace NIWAServices.Tests;

[TestFixture]
public class Tests
{
    private const double RedRocksLat = -41.340963931789474;
    private const double RedRocksLong = 174.74689594599624;

    [Test]
    public void TestThatBlankKeyThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new TideService(string.Empty));
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
        TideService service = new TideService("YOURKEY");
        TidesResponse value = await service.GetTides(RedRocksLat, RedRocksLong);

        Assert.NotNull(value);
        Assert.NotNull(value.Metadata);
        Assert.NotNull(value.Values);
        Assert.IsNotEmpty(value.Values);
    }
}
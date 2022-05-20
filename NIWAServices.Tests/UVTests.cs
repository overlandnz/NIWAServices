using NUnit.Framework;

namespace NIWAServices.Tests;

[TestFixture]
public class UVTests
{
    private const double RedRocksLat = -41.340963931789474;
    private const double RedRocksLong = 174.74689594599624;

    [Test]
    public void TestThatBlankKeyThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new UVService(string.Empty));
    }

    [Test]
    public async Task TestThatInvalidKeyThrowUnauthorizedAccessException()
    {
        UVService service = new UVService("123");
        Assert.ThrowsAsync<UnauthorizedAccessException>(async () => { await service.Get(RedRocksLat, RedRocksLong); });
    }

    [Test]
    public async Task TestThatValidKeyReturnsData()
    {
        UVService service = new UVService("1ecDca2au2UmP8ClbuNcG7TDfcyz6Wt8");
        var value = await service.Get(RedRocksLat, RedRocksLong);

        Assert.NotNull(value);
        Assert.NotNull(value.Products);
        Assert.IsNotEmpty(value.Products);
    }
}
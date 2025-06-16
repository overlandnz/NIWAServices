using NIWAServices.Models;
using NUnit.Framework;

namespace NIWAServices.Tests;

[TestFixture]
public class UVTests
{
    private const double RedRocksLat = -41.340963931789474;
    private const double RedRocksLong = 174.74689594599624;
    private const string TestApiKey = "YOURKEY"; // Replace with your API key for testing

    [Test]
    public void TestThatBlankKeyThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new UVService(string.Empty));
    }

    [Test]
    public void TestThatNullKeyThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new UVService(null));
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
        UVService service = new UVService(TestApiKey);
        var value = await service.Get(RedRocksLat, RedRocksLong);

        Assert.NotNull(value);
        Assert.NotNull(value.Products);
        Assert.IsNotEmpty(value.Products);
    }

    [Test]
    public async Task TestThatProductsContainValidData()
    {
        UVService service = new UVService(TestApiKey);
        var value = await service.Get(RedRocksLat, RedRocksLong);

        Assert.NotNull(value);
        Assert.NotNull(value.Products);
        Assert.IsNotEmpty(value.Products);

        foreach (var product in value.Products)
        {
            Assert.NotNull(product.Name);
            Assert.NotNull(product.Values);
            Assert.IsNotEmpty(product.Values);

            foreach (var uvValue in product.Values)
            {
                Assert.That(uvValue.Time, Is.Not.EqualTo(default(DateTimeOffset)));
                Assert.That(uvValue.Value, Is.GreaterThanOrEqualTo(0));
            }
        }
    }

    [Test]
    public async Task TestThatInvalidCoordinatesThrowException()
    {
        UVService service = new UVService(TestApiKey);
        Assert.ThrowsAsync<Exception>(async () => await service.Get(1000, 1000));
    }

    [Test]
    public async Task TestThatUVValuesAreOrderedByTime()
    {
        UVService service = new UVService(TestApiKey);
        var value = await service.Get(RedRocksLat, RedRocksLong);

        Assert.NotNull(value);
        Assert.NotNull(value.Products);
        Assert.IsNotEmpty(value.Products);

        foreach (var product in value.Products)
        {
            var orderedValues = product.Values.OrderBy(v => v.Time).ToList();
            CollectionAssert.AreEqual(orderedValues, product.Values);
        }
    }
}
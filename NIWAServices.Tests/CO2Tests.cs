using NIWAServices.Models;
using NUnit.Framework;

namespace NIWAServices.Tests;

[TestFixture]
public class CO2Tests
{
    private const string TestApiKey = "YOURKEY"; // Replace with your API key for testing

    [Test]
    public void TestThatBlankKeyThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new CO2Service(string.Empty));
    }

    [Test]
    public void TestThatNullKeyThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new CO2Service(null));
    }

    [Test]
    public async Task TestThatInvalidKeyThrowUnauthorizedAccessException()
    {
        CO2Service service = new CO2Service("123");
        Assert.ThrowsAsync<UnauthorizedAccessException>(async () => { await service.Get(); });
    }

    [Test]
    public async Task TestThatValidKeyReturnsData()
    {
        CO2Service service = new CO2Service(TestApiKey);
        CO2Model value = await service.Get();

        Assert.NotNull(value);
        Assert.NotNull(value.Date);
        Assert.NotNull(value.LatestDailyAverage);
        Assert.NotNull(value.OneDecadeAgo);
        Assert.NotNull(value.OneYearAgo);
    }

    [Test]
    public async Task TestThatCO2ValuesAreValid()
    {
        CO2Service service = new CO2Service(TestApiKey);
        CO2Model value = await service.Get();

        Assert.NotNull(value);
        Assert.That(value.Date, Is.Not.EqualTo(default(DateTime)));
        Assert.That(value.LatestDailyAverage, Is.GreaterThan(0));
        Assert.That(value.OneYearAgo, Is.GreaterThan(0));
        Assert.That(value.OneDecadeAgo, Is.GreaterThan(0));
    }

    [Test]
    public async Task TestThatCO2ValuesAreInExpectedRange()
    {
        CO2Service service = new CO2Service(TestApiKey);
        CO2Model value = await service.Get();

        Assert.NotNull(value);
        // Typical CO2 values are between 300-450 ppm
        Assert.That(value.LatestDailyAverage, Is.InRange(300, 450));
        Assert.That(value.OneYearAgo, Is.InRange(300, 450));
        Assert.That(value.OneDecadeAgo, Is.InRange(300, 450));
    }

    [Test]
    public async Task TestThatDateIsRecent()
    {
        CO2Service service = new CO2Service(TestApiKey);
        CO2Model value = await service.Get();

        Assert.NotNull(value);
        Assert.That(value.Date, Is.GreaterThan(DateTime.UtcNow.AddDays(-7)));
    }
}